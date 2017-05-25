local json = require( "json" )
local api = require("services.remote.api")

local liquidContentConfig = Globals.liquidContent

local stagesService = function()
	local serviceEndPoint = liquidContentConfig.apiServiceUrl .. "api/ContentItems"
	-- functions
	local getStages

	-- implementation
	getStages = function (query, onSuccess, onError)
		local function networkListener( event )
		    if (event.isError or event.status ~= 200) then
		        if (onError) then
			        onError(event.response)
			    end
		    else
		        if (onSuccess) then
		        	onSuccess(event.response)
		        end
		    end
		end

		local params = {}
		params.body = json.encode(user)
		
		local orderAsc = "false"
		if (query.orderAsc) then
			orderAsc = "true"
		end
		local url = serviceEndPoint .. "?maxItems=" .. query.maxItems .."&orderAsc=" .. orderAsc .. "&contentTypeId=".. query.contentTypeId
		if (query.startIndex) then
			url = url .. "&startIndex=" .. query.startIndex
		end
		print(url)
		api.request(url, 
				"GET", networkListener, params)
	end

	return {
		getStages = getStages
	}
end

return stagesService()