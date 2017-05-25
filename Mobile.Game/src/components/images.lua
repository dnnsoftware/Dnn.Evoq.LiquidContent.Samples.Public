local ImagesComponent = function()
	-- constants
	local imagesSize = 100
	local xOffset = 60
	local topYOffset = 130
	local bottomYOffset = -10

	-- dependencies

	-- attributes
	local imagesDisplayGroup, 
		imageTL, imageTR, imageBL, image4

	-- initialization
	
	-- functions
	local create, show, hide

	-- implementation
	create = function ()
		imagesDisplayGroup = display.newGroup()
		return imagesDisplayGroup
	end

	local function networkListener( event )
	    if ( event.isError ) then
	        print ( "Network error - download failed" )
	    else
	        event.target.width = imagesSize
			event.target.height = imagesSize
			imagesDisplayGroup:insert(event.target)
	    end
	end

	show = function (images)
		display.loadRemoteImage(
					images[1].url, "GET", networkListener, 
					images[1].fileName, 
					system.TemporaryDirectory,
					display.contentCenterX - xOffset, display.contentCenterY - topYOffset)
		
		
		display.loadRemoteImage(
					images[2].url, "GET", networkListener, 
					images[2].fileName, 
					system.TemporaryDirectory,
					display.contentCenterX + xOffset, display.contentCenterY - topYOffset)
		
		display.loadRemoteImage(
					images[3].url, "GET", networkListener, 
					images[3].fileName, 
					system.TemporaryDirectory,
					display.contentCenterX - xOffset, display.contentCenterY + bottomYOffset)
		
		display.loadRemoteImage(
					images[4].url, "GET", networkListener, 
					images[4].fileName, 
					system.TemporaryDirectory,
					display.contentCenterX + xOffset, display.contentCenterY + bottomYOffset)
	end

	hide = function ()
		for i=1, imagesDisplayGroup.numChildren do
			imagesDisplayGroup[1]:removeSelf()
		end
	end

	return {
		create = create,
		show = show,
		hide = hide
	}
end

return ImagesComponent()