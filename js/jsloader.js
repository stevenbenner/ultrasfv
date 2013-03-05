if(!sb_javaScriptLoader) { var sb_javaScriptLoader = new Object(); };

sb_javaScriptLoader.init = function() {
	if(!document.getElementById) return;
	sb_javaScriptLoader.loadScript('/js/dropdownmenu.js');
	if(document.getElementById('contactform')) {
		sb_javaScriptLoader.loadScript('/js/formcheck.js');
	};
	var anchors = document.getElementsByTagName('a');
	var loadImageModal = false;
	for (var i=0; i<anchors.length; i++) {
		if (anchors[i].getAttribute('rel') == 'imagemodal') {
			loadImageModal = true;
		};
		if (anchors[i].href.indexOf('/acknowledgements.html') > -1) {
			anchors[i].onclick = function() {
				window.open('/acknowledgements.html', 'Acknowledgements', 'menubar=no,scrollbars=no,status=no,toolbar=no,resizable=no,width=400,height=300,toolbar=no');
				return false;
			};
		};
	};
	if (loadImageModal) {
		sb_javaScriptLoader.loadScript('/js/sb_imageModal.js');
	};
};

sb_javaScriptLoader.loadScript = function(src) {
	var s = document.createElement('script');
	s.type = 'text/javascript';
	s.src = src;
	document.getElementsByTagName('head')[0].appendChild(s);
};

sb_javaScriptLoader.callBack = function(id) {
	switch(id) {
		case 'sb_dropDownMenu' :
			sb_dropDownMenu.init();
			break;
		case 'sb_formCheck' :
			sb_formCheck.errorImgSrc = '/images/alert.gif';
			sb_formCheck.errorMsgText = 'Some information was missing or invalid. Please check the info in the highlighted fields.';
			var preload_image1 = new Image(15,15);
			preload_image1.src = sb_formCheck.errorImgSrc;
			if(document.getElementById('contactform')) {
				sb_formCheck.targetForm = 'contactform';
				sb_javaScriptLoader.loadScript('/js/xmlhttp.js');
			};
			break;
		case 'sb_xmlHttp' :
			sb_javaScriptLoader.loadScript('/js/contactform.js');
			break;
		case 'sb_contactForm' :
			var preload_image2 = new Image(220,19);
			preload_image2.src = '/images/loading.gif';
			window.onunload = function() { sb_contactForm.purgeEvents(document.getElementById('contactform')); };
			sb_contactForm.init();
			break;
		case 'sb_imageModal' :
			sb_imageModal.system.init();
			break;
	};
};

window.onload = sb_javaScriptLoader.init;