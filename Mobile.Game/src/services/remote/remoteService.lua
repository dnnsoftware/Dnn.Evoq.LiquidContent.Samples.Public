local liquidContentConfig = Globals.liquidContent

local remoteService = function()
	-- dependencies
	local contentTypesService = require("services.remote.contentTypesService")

	-- functions
	local getConfigurationData

	-- attributes
	
	-- initialization
	
	getConfigurationData = function (successCallback, errorCallback)
		contentTypesService.getContentTypes(successCallback, errorCallback)
	end
	
	return {
		getConfigurationData = getConfigurationData
	}
end

return remoteService()