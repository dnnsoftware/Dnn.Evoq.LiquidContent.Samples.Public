local liquidContentConfig = Globals.liquidContent
local api = require("services.remote.api")

local contentTypesService = function()
	local serviceEndPoint = liquidContentConfig.apiServiceUrl .. "api/ContentTypes"

	-- functions
	local getContentTypes, getUsersType

	-- attributes
	local contentTypes

	-- initialization
	
	-- implementation
	getContentTypes = function (successCallback, errorCallback)
		if (contentTypes) then
			return contentTypes
		end
		local function networkListener( event )
		    if ( event.isError or event.status ~= 200) then
		        if (errorCallback) then
			        errorCallback(event.response)
			    end
		    else
		        if (successCallback) then
		        	successCallback(event.response)
		        end
		    end
		end
		
		api.request(serviceEndPoint, "GET", networkListener)
	end
	
	return {
		getContentTypes = getContentTypes
	}
end

return contentTypesService()