require ("utils.ice")
local liquidContentConfig = Globals.liquidContent
local GameStateService = function()
	-- constants
	
	-- dependencies
	local gameRepository = require("repository.GameRepository")
	local remoteService = require("services.remote.remoteService")
	
	-- functions
	local isAlreadyConfigured, configureGame, getConfiguration,
		mapContenType,
		getUser, saveUser

	-- attributes
	
	-- initialization

	isAlreadyConfigured = function ()
		local game = gameRepository.readGame()
		return game ~= nil
	end

	mapContenType = function(contentType)
		return {
			id = contentType.id,
			name = contentType.name
		}
	end

	saveConfiguration = function(contentTypesResponse, successCallback)
		local documents = contentTypesResponse.documents;
		local game = gameRepository.readGame() or {}
		game.types = {}

		for i=1, #documents do
			local contentType = documents[i]

			if (contentType.name == liquidContentConfig.userContentTypeName) then
				game.types.user = mapContenType(contentType)
			end

			if (contentType.name == liquidContentConfig.stageContentTypeName) then
				game.types.stage = mapContenType(contentType)
			end
		end

		gameRepository.saveGame(game)
		successCallback()
	end

	configureGame = function (successCallback, errorCallback)
		local game = gameRepository.readGame()
		
		if (game == nil or game.contentTypes == nil) then
			remoteService.getConfigurationData(
				function (response)
					saveConfiguration(response, successCallback)
				end, errorCallback)
		else
			successCallback()
		end
	end

	getConfiguration = function ()
		return gameRepository.readGame()
	end

	saveUser = function (user)
		local game = gameRepository.readGame()
		game.user = user
		gameRepository.saveGame(game)
	end

	getUser = function ()
		local game = gameRepository.readGame()
		if (game == nil) then
			return nil
		end
		
		return game.user
	end

	return {
		isAlreadyConfigured = isAlreadyConfigured,
		configureGame = configureGame,
		getConfiguration = getConfiguration,
		getUser = getUser,
		saveUser = saveUser
	}
end

return GameStateService()