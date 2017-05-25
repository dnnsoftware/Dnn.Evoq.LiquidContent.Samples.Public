require ("utils.ice")

local GameRepository = function()
	-- constants
	local USER_KEY = "USER"
	local TYPES_KEY = "TYPES"

	-- dependencies
	local gameBox = ice:loadBox( "lq" )


	-- initialization
	gameBox:enableAutomaticSaving()

	-- functions
	local readGame, saveGame, resetGame

	readGame = function ()
		local user = nil
		local types = nil

		local previousValue = false
		if (gameBox:hasValue(USER_KEY)) then
			previousValue = true
			user = gameBox:retrieve(USER_KEY)
		end
		if (gameBox:hasValue(TYPES_KEY)) then
			previousValue = true
			types = gameBox:retrieve(TYPES_KEY)
		end

		if (not previousValue) then
			return nil
		end

		return {
			user = user,
			types = types
		}
	end

	saveGame = function (game)
		gameBox:store(USER_KEY, game.user)
		gameBox:store(TYPES_KEY, game.types)
	end

	resetGame = function ()
		gameBox:remove(USER_KEY)
		gameBox:remove(TYPES_KEY)
	end

	return {
		readGame = readGame,
		saveGame = saveGame,
		resetGame = resetGame
	}
end

return GameRepository()