-----------------------------------------------------------------------------------------
--
-- menu.lua
--
-----------------------------------------------------------------------------------------

local composer = require( "composer" )
local scene = composer.newScene()
local gameStateService = require("services.GameStateService")
local usersService = require("services.usersService")
local gameManager = require("managers.game")

--------------------------------------------

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
	titleLogo.x = 80
	titleLogo.y = 15
	
	local game = gameManager.create()
	
	-- all display objects must be inserted into group
	sceneGroup:insert( background )
	sceneGroup:insert(titleLogo)
	sceneGroup:insert(game)
end

function scene:show( event )
	local sceneGroup = self.view
	local phase = event.phase
	
	if phase == "will" then
		-- Called when the scene is still off screen and is about to move on screen
		gameManager.show()
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
		gameManager.hide()
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
	
	
end

---------------------------------------------------------------------------------

-- Listener setup
scene:addEventListener( "create", scene )
scene:addEventListener( "show", scene )
scene:addEventListener( "hide", scene )
scene:addEventListener( "destroy", scene )

-----------------------------------------------------------------------------------------

return scene