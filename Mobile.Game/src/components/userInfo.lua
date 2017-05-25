local ImagesComponent = function()
	-- constants

	-- dependencies
	local gameStateService = require("services.GameStateService")

	-- attributes
	local userDisplayGroup, userNameText, coinsText

	-- initialization
	
	-- functions
	local create, show, hide

	-- implementation
	create = function ()
		userDisplayGroup = display.newGroup()
		userNameText = display.newText( "", 100, 200, native.systemFont, 18 )
		userNameText.anchorX = 0 
    	userNameText.anchorY = 0
	    userNameText:setFillColor( 0, 0, 0 )
	    userNameText.x = display.contentCenterX - 10
	    userNameText.y = 0

	    coinsText = display.newText( "", 100, 200, native.systemFont, 18 )
	    coinsText.anchorX = 0 
    	coinsText.anchorY = 0
	    coinsText:setFillColor( 0, 0, 0 )
	    coinsText.x = display.contentCenterX - 10
	    coinsText.y = 20
	    
	    userDisplayGroup:insert(userNameText)
	    userDisplayGroup:insert(coinsText)

		return userDisplayGroup
	end

	show = function ()
		local user = gameStateService.getUser()
		userNameText.text = "USER: " .. user.nickname
		coinsText.text = "COINS: " .. user.coins
	end

	hide = function ()
		
	end

	return {
		create = create,
		show = show,
		hide = hide
	}
end

return ImagesComponent()