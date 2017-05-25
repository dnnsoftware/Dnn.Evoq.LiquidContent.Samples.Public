---------------------------------------------------------------------------------
--
-- mainTitle.lua
--
---------------------------------------------------------------------------------
local composer = require( "composer" )
local scene = composer.newScene()

---------------------------------------------------------------------------------
-- BEGINNING OF YOUR IMPLEMENTATION
---------------------------------------------------------------------------------

-- -----------------------------------------------------------------------------------------------------------------
-- All code outside of the listener functions will only be executed ONCE unless "composer.removeScene()" is called.
-- -----------------------------------------------------------------------------------------------------------------

-- local forward references should go here

-- -------------------------------------------------------------------------------


-- "scene:create()"
function scene:create( event )
    local sceneGroup = self.view

    -- Initialize the scene here.
    -- Example: add display objects to "sceneGroup", add touch listeners, etc.
    local background = display.newRect( Globals.screen.xCenter, Globals.screen.yCenter, 
                    display.actualContentWidth, display.actualContentHeight )
    background:setFillColor( 0.95, 0.95, 0.95)
    background.anchorX = 0
    background.anchorY = 0
    background.x = 0 + display.screenOriginX 
    background.y = 0 + display.screenOriginY

    local titleLogo = display.newImageRect( "logo.png", 264, 42 )
    titleLogo.x = display.contentCenterX
    titleLogo.y = 100

    local errorText = display.newText( "Error connecting to Liquid Content.", 100, 200, native.systemFont, 16 )
    errorText:setFillColor( 1, 0, 0 )
    errorText.x = display.contentCenterX
    errorText.y = 150
    sceneGroup:insert( background )
    sceneGroup:insert(titleLogo)
    sceneGroup:insert( errorText )
end


-- "scene:show()"
function scene:show( event )

    local sceneGroup = self.view
    local phase = event.phase

    if ( phase == "will" ) then
        -- Called when the scene is still off screen (but is about to come on screen).
        
        --print( "1: enterScene event" )		
    	
    elseif ( phase == "did" ) then
        -- Called when the scene is now on screen.
        -- Insert code here to make the scene come alive.
        -- Example: start timers, begin animation, play audio, etc.
        
    end
end


-- "scene:hide()"
function scene:hide( event )

    local sceneGroup = self.view
    local phase = event.phase

    if ( phase == "will" ) then
        -- Called when the scene is on screen (but is about to go off screen).
        -- Insert code here to "pause" the scene.
        -- Example: stop timers, stop animation, stop audio, etc.
    elseif ( phase == "did" ) then
        -- Called immediately after scene goes off screen.
    end
end


-- "scene:destroy()"
function scene:destroy( event )

    local sceneGroup = self.view

    -- Called prior to the removal of scene's view ("sceneGroup").
    -- Insert code here to clean up the scene.
    -- Example: remove display objects, save state, etc.
end


-- -------------------------------------------------------------------------------

-- Listener setup
scene:addEventListener( "create", scene )
scene:addEventListener( "show", scene )
scene:addEventListener( "hide", scene )
scene:addEventListener( "destroy", scene )

-- -------------------------------------------------------------------------------

return scene