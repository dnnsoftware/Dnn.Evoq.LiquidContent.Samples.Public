local colors = Globals.colors
local widget = require "widget"

local DictionaryComponent = function()
	-- constants
	local letters = 
		{"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", 
		"N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
	local initialX = 35
	local initialY = display.contentCenterY + 110
	local incrementX = 35
	local incrementY = 35
	local bottonSize = 25
	local maxNumberOfLetters = 8

	-- dependencies

	-- attributes
	local dictionaryDisplayGroup,
		onClick

	-- initialization
	
	-- functions
	local create, show, hide

	createButton = function(letter, x, y)
		local button = widget.newButton(
		    {
		        label = letter,
		        onRelease = function ()
		        	onClick(letter)
	        	end,
		        emboss = false,
		        -- Properties for a rounded rectangle button
		        shape = "roundedRect",
		        width = bottonSize,
		        height = bottonSize,
		        cornerRadius = 2,
		        labelColor = colors.buttons.enabled.label,
		        fillColor = colors.buttons.enabled.fill,
		        strokeColor = colors.buttons.enabled.stroke,
		        strokeWidth = 1
		    }
		)

		button.x = x
		button.y = y

		return button
	end

	-- implementation
	create = function (callback)
		onClick = callback
		dictionaryDisplayGroup = display.newGroup()

		local x = initialX
		local y = initialY
		local numberOfLetters = 0
		for i=1, #letters do
			local button = createButton(letters[i], x, y)
			dictionaryDisplayGroup:insert(button)
			x = x + incrementX
			numberOfLetters = numberOfLetters + 1

			if (numberOfLetters == maxNumberOfLetters) then
				x = initialX
				y = y + incrementY
				numberOfLetters = 0
			end
		end

		return dictionaryDisplayGroup
	end

	show = function ()
		
	end

	hide = function ()
		
	end

	return {
		create = create,
		show = show,
		hide = hide
	}
end

return DictionaryComponent()