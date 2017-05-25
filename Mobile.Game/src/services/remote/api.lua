local json = require( "json" )
local apiKey = Globals.liquidContent.apiKey

local api = function()
	-- functions
	local request
	
	-- implementation
	request = function (url, method, listener, params)
		if (contentTypes) then
			return contentTypes
		end
		local function networkListener( event )
			event.response = json.decode( event.response )
		    listener(event)
		end
		params = params or {}
		local headers = params.headers or {}
  		headers["Authorization"] = "Bearer " .. apiKey
  		headers["Content-Type"] = "application/json; charset=utf-8"
  		
		params.headers = headers
		network.request( url, method, networkListener , params)
	end
	
	return {
		request = request
	}
end

return api()
