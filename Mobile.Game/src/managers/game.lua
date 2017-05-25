local colors = Globals.colors
local composer = require( "composer" )
local widget = require "widget"

local GameManager = function()
	-- constants

	-- dependencies
	local imagesComponent = require("components.images")
	local hiddenWordComponent = require("components.hiddenWord")
	local dictionaryComponent = require("components.dictionary")
	local userInfoComponent = require("components.userInfo")

	local usersService = require("services.usersService")

	-- attributes
	local stage, gameDisplayGroup

	-- initialization
	stage = nil
	
	-- functions
	local create, createButtons, createButton, onRefresh,
		setStage, show,
		onUpdateUserSuccess, onError,
		onLetterClick

	-- implementation
	setStage = function (s)
		stage = s
	end

	onRefresh = function()
		composer.gotoScene( "scenes.selectingStage", "fade", 1000 )
	end

	onUpdateUserSuccess = function()
		native.setActivityIndicator( false )
		print("Updated user information.")
		composer.gotoScene( "scenes.selectingStage", "fade", 1000 )
	end

	onError = function()
		native.setActivityIndicator( false )
		print("Error updating user information.")
		composer.gotoScene( "scenes.selectingStage", "fade", 1000 )
	end

	onLetterClick = function(letter)
		hiddenWordComponent.showLetter(letter)
		
		if (hiddenWordComponent.isResolved()) then 
			local function onComplete( event )
			    if ( event.action == "clicked" ) then
			        local i = event.index
			        if ( i == 1 ) then
			            native.setActivityIndicator(true)
						usersService.updateUserCoins(stage.coins, onUpdateUserSuccess, onError)
			        end
			    end
			end
			  
			-- Show alert with two buttons
			local alert = native.showAlert( "Liquid Content", "You earn " .. stage.coins .. " coins!", { "OK" }, onComplete )
			
		end
	end

	createButton = function(enabled, visible, buttonColors)
		local button = widget.newButton(
		    {
		        label = "Skip >>",
		        onRelease = onRefresh,
		        emboss = false,
		        -- Properties for a rounded rectangle button
		        shape = "roundedRect",
		        width = 80,
		        height = 30,
		        cornerRadius = 2,
		        labelColor = buttonColors.label,
		        fillColor = buttonColors.fill,
		        strokeColor = buttonColors.stroke,
		        strokeWidth = 4
		    }
		)
		 
		-- Center the button
		button.x = display.actualContentWidth - 70
		button.y = display.actualContentHeight - 60
		button:setEnabled(enabled)
		button.isVisible = visible

		return button
	end

	createButtons = function()
		local displayGroup = display.newGroup()
		local button = createButton(true, true, colors.buttons.enabled)
		displayGroup:insert(button)
		return displayGroup
	end

	create = function()
		if (not stage) then
			return
		end
		gameDisplayGroup = display.newGroup()
		
		local buttons = createButtons()
		local images = imagesComponent.create()
		local hiddenWord = hiddenWordComponent.create()
		local dictionary = dictionaryComponent.create(onLetterClick)
		local userInfo = userInfoComponent.create()

		gameDisplayGroup:insert(buttons)
		gameDisplayGroup:insert(images)
		gameDisplayGroup:insert(dictionary)
		gameDisplayGroup:insert(userInfo)

		gameDisplayGroup.isVisible = false
		return gameDisplayGroup
	end

	show = function()
		gameDisplayGroup.isVisible = true
		imagesComponent.show(stage.images)
		hiddenWordComponent.show(stage.hiddenWord)
		dictionaryComponent.show()
		userInfoComponent.show()
	end

	hide = function()
		gameDisplayGroup.isVisible = false
		imagesComponent.hide()
		hiddenWordComponent.hide()
		dictionaryComponent.hide()
		userInfoComponent.hide()
	end

	return {
		create = create,
		setStage = setStage,
		show = show,
		hide = hide
	}
end

return GameManager()