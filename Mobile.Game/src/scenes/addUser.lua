-----------------------------------------------------------------------------------------
--
-- menu.lua
--
-----------------------------------------------------------------------------------------

local composer = require( "composer" )
local scene = composer.newScene()
local gameStateService = require("services.GameStateService")
local usersService = require("services.usersService")

-- include Corona's "widget" library
local widget = require "widget"
local colors = Globals.colors

-- variables
local nicknameField, 
		addUserButtonEnabled, addUserButtonDisabled

--------------------------------------------
local function onAddNewUserSuccess ()
	composer.gotoScene("scenes.selectingStage", "fade", 500)
end

local function onAddNewUserError (response)
	print("error onAddNewUserError")
end

local function onAddNewUser()
	usersService.createUser(nicknameField.text, onAddNewUserSuccess, onAddNewUserError)
end

local function createUserButtonSetEnabled(enabled)
	addUserButtonEnabled.isVisible = enabled
	addUserButtonDisabled.isVisible = not enabled
end

local function createUserButton(enabled, visible, buttonColors)
	local button = widget.newButton(
	    {
	        label = "Create",
	        onRelease = onAddNewUser,
	        emboss = false,
	        -- Properties for a rounded rectangle button
	        shape = "roundedRect",
	        width = 150,
	        height = 40,
	        cornerRadius = 2,
	        labelColor = buttonColors.label,
	        fillColor = buttonColors.fill,
	        strokeColor = buttonColors.stroke,
	        strokeWidth = 4
	    }
	)
	 
	-- Center the button
	button.x = display.contentCenterX
	button.y = display.contentCenterY + 50
	button:setEnabled(enabled)
	button.isVisible = visible

	return button
end

local function createUserButtons()
	local displayGroup = display.newGroup()
	addUserButtonEnabled = createUserButton(true, false, colors.buttons.enabled)
	addUserButtonDisabled = createUserButton(false, true, colors.buttons.disabled)
	displayGroup:insert(addUserButtonEnabled)
	displayGroup:insert(addUserButtonDisabled)
	return displayGroup
end

local function textListener( event )
 
    if ( event.phase == "began" ) then
        -- User begins editing "nicknameField"
 
    elseif ( event.phase == "ended" or event.phase == "submitted" ) then
        -- Output resulting text from "nicknameField"
        print( event.target.text )
 
    elseif ( event.phase == "editing" ) then
    	createUserButtonSetEnabled(string.len(event.text) >= 4)
    end

    if ( "submitted" == event.phase ) then
		native.setKeyboardFocus( nil )
	end
end
local function createNickNameInput(sceneGroup)
	local displayGroup = display.newGroup()
	nicknameField = native.newTextField( display.contentCenterX, display.contentCenterY, 180, 30 )
	nicknameField:addEventListener( "userInput", textListener )
	local nicknameLabel = display.newText( "Please, insert your nickname:", 
		display.contentCenterX, display.contentCenterY - 50, native.systemFont, 20 )
	nicknameLabel:setFillColor( 0, 0, 0 )
	
	displayGroup:insert(nicknameLabel)
	displayGroup:insert(nicknameField)
	
	return displayGroup
end

function scene:create( event )
	local sceneGroup = self.view
	--local sceneGroup = screenUtils.getCenteredGroup()
    --scene:insert(sceneGroup)

	-- Called when the scene's view does not exist.
	-- 
	-- INSERT code here to initialize the scene
	-- e.g. add display objects to 'sceneGroup', add touch listeners, etc.

	-- display a background image
	local background = display.newRect( Globals.screen.xCenter, Globals.screen.yCenter, 
                    display.actualContentWidth, display.actualContentHeight )
    background:setFillColor( 0.95, 0.95, 0.95)
    background.anchorX = 0
    background.anchorY = 0
    background.x = 0 + display.screenOriginX 
    background.y = 0 + display.screenOriginY
	
	-- create/position logo/title image on upper-half of the screen
	local titleLogo = display.newImageRect( "logo.png", 264, 42 )
	titleLogo.x = display.contentCenterX
	titleLogo.y = 50
	
	local nicknameInputDisplayGroup = createNickNameInput(sceneGroup)
	local buttons = createUserButtons()
	
	-- all display objects must be inserted into group
	sceneGroup:insert( background )
	sceneGroup:insert(titleLogo)
	sceneGroup:insert(nicknameInputDisplayGroup)
	sceneGroup:insert(buttons)
end

function scene:show( event )
	local sceneGroup = self.view
	local phase = event.phase
	
	if phase == "will" then
		-- Called when the scene is still off screen and is about to move on screen
	elseif phase == "did" then
		-- Called when the scene is now on screen
		-- 
		-- INSERT code here to make the scene come alive
		-- e.g. start timers, begin animation, play audio, etc.
	end	
end

function scene:hide( event )
	local sceneGroup = self.view
	local phase = event.phase
	
	if event.phase == "will" then
		-- Called when the scene is on screen and is about to move off screen
		--
		-- INSERT code here to pause the scene
		-- e.g. stop timers, stop animation, unload sounds, etc.)
		nicknameField:removeSelf()
	elseif phase == "did" then
		-- Called when the scene is now off screen
	end	
end

function scene:destroy( event )
	local sceneGroup = self.view
	
	-- Called prior to the removal of scene's "view" (sceneGroup)
	-- 
	-- INSERT code here to cleanup the scene
	-- e.g. remove display objects, remove touch listeners, save state, etc.
	
	if playBtn then
		playBtn:removeSelf()	-- widgets must be manually removed
		playBtn = nil
	end
end

---------------------------------------------------------------------------------

-- Listener setup
scene:addEventListener( "create", scene )
scene:addEventListener( "show", scene )
scene:addEventListener( "hide", scene )
scene:addEventListener( "destroy", scene )

-----------------------------------------------------------------------------------------

return scene