
local stagesService = function()
	-- dependencies 
	local gameStateService = require("services.GameStateService")
	local remoteStagesService = require("services.remote.stagesService")

	-- functions
	local selectOneStage, 
			getStages, getStagesOnSuccess,
			mapStage

	-- attributes
	local numberOfStages

	-- initialization

	-- implementation
	mapStage = function (stage)
		return {
			id = stage.id,
			hiddenWord = stage.details.hiddenWord,
			coins = stage.details.coins,
			images = stage.details.images
		}
	end

	getStagesOnSuccess = function (response, query, onSuccess)
		local documents = response.documents
		local stages = {}
		for i=1, #documents do
			local stage = mapStage(documents[i])
			table.insert(stages, stage)
		end

		response.documents = stages
		onSuccess(response)
	end
	getStages = function(query, onSuccess, onError)
		local stageType = gameStateService.getConfiguration().types.stage
		query = query or {}
		query.orderAsc = false
		query.contentTypeId = stageType.id

		remoteStagesService.getStages(query, 
			function(response)
				getStagesOnSuccess(response, query, onSuccess)
			end, onError)
	end

	selectOneStage = function(onSuccess, onError)
		local f = function ()
			local query = {
				maxItems = 1
			}
			local selectedItem = 0
			if (numberOfStages > 1) then
				selectedItem = math.random(numberOfStages)
				selectedItem = selectedItem - 1
				print("numberOfStages " .. numberOfStages)
				print("selected = " .. selectedItem)
				if (selectedItem > 0) then
					query.startIndex = selectedItem
				end
			end

			getStages(query, onSuccess, onError)
		end

		if (numberOfStages == nil) then
			local query = {
				maxItems = 1
			}

			getStages(query, 
				function (response)
					numberOfStages = response.totalResultCount
					f()
				end, 
				onError)
		else
			f()
		end
	end

	return {
		selectOneStage = selectOneStage
	}
end

return stagesService()