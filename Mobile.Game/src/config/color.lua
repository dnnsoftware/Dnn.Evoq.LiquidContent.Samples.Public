local config = {
	buttons = {
		enabled = {
			label = { default={1, 1, 1, 1}, over={1, 1, 1, 1} },
			fill = { default={30/255, 136/255, 195/255, 1}, over={33/255, 163/255, 218/255, 1} },
			stroke = { default={30/255, 136/255, 195/255, 1}, over={33/255, 163/255, 218/255, 1} }
		},
		disabled = {
			label = { default={149/255, 150/255, 149/255, 1}, over={149/255, 150/255, 149/255, 1} },
			fill = { default={229/255, 231/255, 230/255, 1}, over={229/255, 231/255, 230/255, 1} },
			stroke = { default={229/255, 231/255, 230/255, 1}, over={229/255, 231/255, 230/255, 1} }
		}
	}
}
Globals.colors = config