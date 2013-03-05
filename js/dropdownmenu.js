if(!sb_dropDownMenu) { var sb_dropDownMenu = new Object(); };

sb_dropDownMenu = {
	init: function() {
		if (!document.getElementById) return;
		var navRoot = document.getElementById('navigation');
		if (!navRoot) return;
		var navItems = navRoot.getElementsByTagName('li');
		for (var i=0; i<navItems.length; i++) {
			var navMenu = navItems[i].getElementsByTagName('ul');
			if (navMenu && navMenu.length > 0 && navMenu[0].id) {
				navItems[i].onmouseover = (function(id) {
					return function() {sb_dropDownMenu.openMenu(id); }
				})(navMenu[0].id);
				navItems[i].onmouseout = function() {
					sb_dropDownMenu.cancelTimer();
					sb_dropDownMenu.timer = window.setTimeout('sb_dropDownMenu.closeMenus()', 500);
				};
				navMenu[0].onmouseover = function() {
					sb_dropDownMenu.cancelTimer();
				};
				navMenu[0].onmouseout = function(e) {
					sb_dropDownMenu.cancelTimer();
					sb_dropDownMenu.timer = window.setTimeout('sb_dropDownMenu.closeMenus()', 500);
				};
			};
		};
		document.onclick = sb_dropDownMenu.closeMenus;
	},

	openMenu: function(id) {
		sb_dropDownMenu.cancelTimer();
		if (sb_dropDownMenu.currentMenu) {
			sb_dropDownMenu.closeMenus();
		};
		sb_dropDownMenu.currentMenu = document.getElementById(id);
		sb_dropDownMenu.currentMenu.style.visibility = 'visible';
	},

	closeMenus: function() {
		if (sb_dropDownMenu.currentMenu) {
			sb_dropDownMenu.currentMenu.style.visibility = 'hidden';
		};
	},

	cancelTimer: function() {
		if (sb_dropDownMenu.timer) {
			window.clearTimeout(sb_dropDownMenu.timer);
			sb_dropDownMenu.timer = null;
		};
	},

	currentMenu: null,
	timer: null
};

sb_javaScriptLoader.callBack('sb_dropDownMenu');