local HiddenWord = function()
	-- constants
	
	-- dependencies

	-- attributes
	local shownText, 
		showLetter, isResolved, reDrawShownText, isAFoundLetter,
		hiddenWord, foundLetters,
		hiddenWordDisplayGroup

	-- initialization
	
	-- functions
	local create, show, hide

	-- implementation
	create = function ()
		hiddenWordDisplayGroup = display.newGroup()
		shownText = display.newText( "", 100, 200, native.systemFont, 30 )
	    shownText:setFillColor( 0, 0, 0 )
	    shownText.x = display.contentCenterX
	    shownText.y = display.contentCenterY + 65
		return hiddenWordDisplayGroup
	end

	show = function (word)
		foundLetters = {}
		hiddenWord = string.upper(word)
		local text = ""
		for i=1, string.len(hiddenWord) do
			text = text .. "_ "
		end
		shownText.text = text
	end

	hide = function ()
		shownText.text = ""
	end

	isResolved = function ()
		for i=1, string.len(hiddenWord) do
			local currentLetter = string.sub(hiddenWord, i, i)
			if (not isAFoundLetter(currentLetter)) then
				return false
			end
		end
		return true
	end

	isAFoundLetter = function(letter)
		local numberOfFoundLetters = #foundLetters
		for i=1, numberOfFoundLetters do
			if (letter == foundLetters[i]) then
				return true
			end
		end

		return false
	end

	reDrawShownText = function()
		local text = ""
		for i=1, string.len(hiddenWord) do
			local currentLetter = string.sub(hiddenWord, i, i)
			if (isAFoundLetter(currentLetter)) then
				text = text .. currentLetter .." "
			else
				text = text .. "_ "
			end
		end
		shownText.text = text
	end

	showLetter = function (l)
		local letter = string.upper(l)
		local containsLetter = string.find(hiddenWord, letter) ~= nil

		if (containsLetter and not isAFoundLetter(l)) then
			table.insert(foundLetters, letter)
			reDrawShownText()
		end
	end
	return {
		create = create,
		show = show,
		hide = hide,
		showLetter = showLetter,
		isResolved = isResolved
	}
end

return HiddenWord()