/*
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip(); 
});
*/

if (hasElement('SelectedItemIds')) {
	favoriteLoaded();
}
if (hasElement('Zeros')) {
	document.getElementById('Zeros').addEventListener('click', zeroWeights);
}
if (hasElement('GetFavorite')) {
	document.getElementById('GetFavorite').addEventListener('input', favoriteChanged);
}
if (hasElement('textFilter')) {
	document.getElementById('textFilter').addEventListener('keyup', textFilterChanged);
}

function hasElement(elementName) {
	var element = document.getElementById(elementName);
	return (typeof (element) != 'undefined' && element != null);
}

function zeroWeights() {
	var inputs = document.getElementsByTagName('input');
	for (i = 0; i < inputs.length; i++) {
		if (inputs[i].name.endsWith('Weight')) {
			inputs[i].value = '0';
		}
	}
};

function favoriteLoaded() {
	favoriteChanged();
}

function favoriteChanged() {
	var favorite = parseInt(document.getElementById('GetFavorite').value);
	if (favorite >= 0) {
		var inputs = document.getElementsByTagName('option');
		var arr = [];
		for (let i = favorite + 1; i < 15; i++) {
			arr.push('(' + i + ')');
		}
		for (i = 0; i < inputs.length; i++) {
			inputs[i].hidden = false;
			for (let j = 0; j < arr.length; j++) {
				if (inputs[i].innerHTML.endsWith(arr[j])) {
					inputs[i].hidden = true;
					break;
				}
			}
		}
	}
}

function textFilterChanged() {
	var filter = document.getElementById('textFilter').value.toLowerCase();
	var inputs = document.getElementsByTagName('option');
	for (i = 0; i < inputs.length; i++) {
		if (filter.length > 0) {
			inputs[i].hidden = !inputs[i].innerHTML.toLowerCase().includes(filter);
		} else {
			inputs[i].hidden = false;
		}
	}
}
