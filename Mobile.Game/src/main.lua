-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------
require("config.config")

local gameStateService = require("services.GameStateService")

-- hide the status bar
display.setStatusBar( display.HiddenStatusBar )

-- include the Corona "composer" module
local composer = require "composer"

-- load menu screen
local alreadyConfigured = gameStateService.isAlreadyConfigured()

if (not alreadyConfigured) then
	gameStateService.configureGame(
		function ()
			composer.gotoScene( "scenes.addUser", "fade", 500  )
		end,
		function ()
			composer.gotoScene("scenes.noService", "fade", 500 )
		end
	)
else
	local user = gameStateService.getUser()
	if (user == nil) then
		composer.gotoScene( "scenes.addUser", "fade", 500  )
	else 
		composer.gotoScene("scenes.selectingStage", "fade", 500 )
	end
end