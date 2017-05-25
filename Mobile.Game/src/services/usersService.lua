
local usersService = function()
	-- dependencies 
	local gameStateService = require("services.GameStateService")
	local remoteUsersService = require("services.remote.usersService")

	-- functions
	local createUser, createUserOnSuccess, 
			updateUserCoins, 
			mapUserType

	-- attributes

	-- initialization

	-- implementation
	mapUserType = function (user)
		return {
			id = user.id,
			nickname = user.details.nickname,
			coins = user.details.coins
		}
	end
	createUserOnSuccess = function (user, response, onSuccess)
		user.id = response.id
		gameStateService.saveUser(mapUserType(user))

		if (onSuccess) then
			onSuccess()
		end
	end

	createUser = function (nickname, onSuccess, onError)
		local userType = gameStateService.getConfiguration().types.user
		
		local user = {
			details = {
				coins = 0, 
				nickname = nickname
			}, 
			name = nickname, 
			description = "",
			tags = {},
			contentTypeId = userType.id
		}
		remoteUsersService.createUser(user, 
			function (response)
				createUserOnSuccess(user, response, onSuccess)
			end, onError)
	end

	updateUserCoins = function (coinsToAdd, onSuccess, onError)
		local userType = gameStateService.getConfiguration().types.user
		local user = gameStateService.getUser()

		user.coins = user.coins + coinsToAdd
		gameStateService.saveUser(user)
		local user = {
			id = user.id,
			details = {
				coins = user.coins, 
				nickname = user.nickname
			}, 
			name = user.nickname, 
			description = "",
			tags = {},
			contentTypeId = userType.id
		}
		remoteUsersService.update(user, 
			onSuccess, onError)
	end
	
	return {
		createUser = createUser,
		updateUserCoins = updateUserCoins
	}
end

return usersService()