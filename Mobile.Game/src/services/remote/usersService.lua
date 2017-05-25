local json = require( "json" )
local api = require("services.remote.api")

local liquidContentConfig = Globals.liquidContent

local usersService = function()
	local serviceEndPoint = liquidContentConfig.apiServiceUrl .. "api/ContentItems"
	-- functions
	local createUser, update

	-- implementation
	createUser = function (user, onSuccess, onError)
		local function networkListener( event )
		    if ( event.isError or event.status ~= 201) then
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
		
		api.request(serviceEndPoint .. "?publish=true", "POST", networkListener, params)
	end

	update = function (user, onSuccess, onError)
		local function networkListener( event )
		    if ( event.isError or event.status ~= 200) then
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
		
		api.request(serviceEndPoint .. "/" .. user.id .. "?publish=true", "PUT", networkListener, params)
	end
	
	return {
		createUser = createUser,
		update = update
	}
end

return usersService()