if (!sb_imageModal) { var sb_imageModal = new Object(); };

sb_imageModal.objects = {
  images: [],
  overlay: null,
  modalDiv: null,
  modalImage: null,
  loadingIcon: null,
  caption: null,
  resText: null,
  currentImage: null
};

sb_imageModal.system = {
  init: function() {
    if(!document.getElementById) return;
    var anchors = document.getElementsByTagName('a');
    var imageIndex = 0;
    for(var i=0; i<anchors.length; i++) {
      if(anchors[i].getAttribute('rel') === 'imagemodal') {
        anchors[i].onclick = (function(t) { return function() { try { sb_imageModal.system.showImageModal(t); } catch(e) {}; return false; }; })(imageIndex);
        sb_imageModal.objects.images.push(anchors[i]);
        imageIndex++;
      };
    };
    if(imageIndex) sb_imageModal.system.createLayout();
  },

  createLayout: function() {
    // overlay div
    sb_imageModal.objects.overlay = document.createElement('div');
    sb_imageModal.objects.overlay.id = 'tOverlay';
    sb_imageModal.objects.overlay.style.display = 'none';
    sb_imageModal.objects.overlay.onclick = function() { sb_imageModal.system.hideImageModal(); };
    // modal div
    sb_imageModal.objects.modalDiv = document.createElement('div');
    sb_imageModal.objects.modalDiv.id = 'tModal';
    // image wraped in achor
    sb_imageModal.objects.modalImage = document.createElement('img');
    sb_imageModal.objects.modalImage.id = 'tModalImage';
    var anchorDiv = document.createElement('div');
    anchorDiv.style.textAlign = 'center';
    var anchor = document.createElement('a');
	if (sb_imageModal.objects.images.length > 1) {
      anchor.href = 'javascript:sb_imageModal.system.nextImage();';
    } else {
      anchor.href = 'javascript:sb_imageModal.system.hideImageModal();';
    };
    anchor.appendChild(sb_imageModal.objects.modalImage);
    anchorDiv.appendChild(anchor);
    sb_imageModal.objects.modalDiv.appendChild(anchorDiv);
    // content div
    var contentDiv = document.createElement('div');
    contentDiv.id = 'tModalContent';
    sb_imageModal.objects.caption = document.createElement('p');
    sb_imageModal.objects.caption.id = 'tModalCaption';
    sb_imageModal.objects.resText = document.createElement('p');
    sb_imageModal.objects.resText.id = 'tModalResText';
    var controls = document.createElement('p');
    controls.id = 'tModalControls';
	if (sb_imageModal.objects.images.length > 1) {
      var prevLink = document.createElement('a');
      var nextLink = document.createElement('a');
      prevLink.href = 'javascript:sb_imageModal.system.previousImage();';
      nextLink.href = 'javascript:sb_imageModal.system.nextImage();';
      prevLink.appendChild(document.createTextNode('<< Previous'));
      nextLink.appendChild(document.createTextNode('Next >>'));
      controls.appendChild(prevLink);
      controls.appendChild(document.createTextNode(' | '));
      controls.appendChild(nextLink);
	};
    contentDiv.appendChild(sb_imageModal.objects.caption);
    contentDiv.appendChild(sb_imageModal.objects.resText);
    contentDiv.appendChild(controls);
    sb_imageModal.objects.modalDiv.appendChild(contentDiv);
    // close button
    var closeButton = document.createElement('img');
    closeButton.id = 'tModalClose';
    closeButton.src = '/images/close.gif';
    var closeAnchor = document.createElement('a');
    closeAnchor.href = 'javascript:sb_imageModal.system.hideImageModal();';
    closeAnchor.appendChild(closeButton);
    sb_imageModal.objects.modalDiv.appendChild(closeAnchor);
    // insert the new elements into the page
    var body = document.getElementsByTagName('body')[0];
    body.insertBefore(sb_imageModal.objects.overlay, body.firstChild);
    body.insertBefore(sb_imageModal.objects.modalDiv, sb_imageModal.objects.overlay.nextSibling);
  },

  showImageModal: function(imageId) {
    var arrayPageSize = sb_imageModal.tools.getPageSize();
    var arrayPageScroll = sb_imageModal.tools.getPageScroll();
    sb_imageModal.objects.overlay.style.height = (arrayPageSize[1] + 'px');
    sb_imageModal.objects.overlay.style.display = 'block';
    var preloadImage = new Image();
    preloadImage.onload = function() {
      var imageWidth = preloadImage.width;
      var imageHeight = preloadImage.height;
      while((imageWidth + 40) > arrayPageSize[0] || (imageHeight + 60) > arrayPageSize[3]) {
        if((imageWidth + 40) > arrayPageSize[0]) {
          imageWidth = arrayPageSize[0] - 40;
          imageHeight = imageWidth *(preloadImage.width / preloadImage.height);
        } else if((imageHeight + 60) > arrayPageSize[3]) {
          imageHeight = arrayPageSize[3] - 60;
          imageWidth = imageHeight * (preloadImage.width / preloadImage.height);
        };
      };
      var modalTop;
      var modalLeft;
      if(preloadImage.width < 300) {
        modalLeft = ((arrayPageSize[0] - 20 - 300) / 2);
        sb_imageModal.objects.modalDiv.style.width = '320px';
      } else {
        modalLeft = ((arrayPageSize[0] - 20 - preloadImage.width) / 2);
        sb_imageModal.objects.modalDiv.style.width = preloadImage.width + 'px';
      };
      modalTop = (arrayPageScroll[1] + ((arrayPageSize[3] - 35 - preloadImage.height) / 2) - 40);
      sb_imageModal.objects.modalImage.src = sb_imageModal.objects.images[imageId].href;
      sb_imageModal.objects.modalDiv.style.top = (modalTop < 0) ? "0px" : modalTop + "px";
      sb_imageModal.objects.modalDiv.style.left = (modalLeft < 0) ? "0px" : modalLeft + "px";
      if(sb_imageModal.objects.images[imageId].getAttribute('title')) {
        sb_imageModal.objects.caption.innerHTML = sb_imageModal.objects.images[imageId].getAttribute('title');
        sb_imageModal.objects.caption.style.display = 'block';
      } else {
        sb_imageModal.objects.caption.style.display = 'none';
      };
      if(sb_imageModal.objects.images.length > 0) {
        sb_imageModal.objects.resText.innerHTML = 'Image ' + (imageId + 1) + ' of ' + sb_imageModal.objects.images.length;
      };
      sb_imageModal.objects.modalDiv.style.display = 'block';
    };
    preloadImage.src = sb_imageModal.objects.images[imageId].href;
    sb_imageModal.objects.currentImage = imageId;
  },

  hideImageModal: function() {
    sb_imageModal.objects.overlay.style.display = 'none';
    sb_imageModal.objects.modalDiv.style.display = 'none';
    sb_imageModal.objects.currentImage = null;
  },

  nextImage: function() {
    if(sb_imageModal.objects.currentImage == null) return;
    if(sb_imageModal.objects.currentImage >= (sb_imageModal.objects.images.length - 1)) {
      this.showImageModal(0);
    } else {
      this.showImageModal(sb_imageModal.objects.currentImage + 1);
    };
  },

  previousImage: function() {
    if(sb_imageModal.objects.currentImage == null) return;
    if(sb_imageModal.objects.currentImage == 0) {
      this.showImageModal(sb_imageModal.objects.images.length - 1);
    } else {
      this.showImageModal(sb_imageModal.objects.currentImage - 1);
    };
  }
};

sb_imageModal.tools = {
  getPageScroll: function() {
    var yScroll;
    if(self.pageYOffset) {
      yScroll = self.pageYOffset;
    } else if(document.documentElement && document.documentElement.scrollTop) { // Explorer 6 Strict
      yScroll = document.documentElement.scrollTop;
    } else if(document.body) { // all other Explorers
      yScroll = document.body.scrollTop;
    };
    var arrayPageScroll = new Array('', yScroll);
    return arrayPageScroll;
  },

  getPageSize: function() {
    var xScroll, yScroll;
    if(window.innerHeight && window.scrollMaxY) {
      xScroll = document.body.scrollWidth;
      yScroll = window.innerHeight + window.scrollMaxY;
    } else if(document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
      xScroll = document.body.scrollWidth;
      yScroll = document.body.scrollHeight;
    } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
      xScroll = document.body.offsetWidth;
      yScroll = document.body.offsetHeight;
    };
    var windowWidth, windowHeight;
    if(self.innerHeight) { // all except Explorer
      windowWidth = self.innerWidth;
      windowHeight = self.innerHeight;
    } else if(document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
      windowWidth = document.documentElement.clientWidth;
      windowHeight = document.documentElement.clientHeight;
    } else if(document.body) { // other Explorers
      windowWidth = document.body.clientWidth;
      windowHeight = document.body.clientHeight;
    };
    // for small pages with total height less then height of the viewport
    if(yScroll < windowHeight) {
      pageHeight = windowHeight;
    } else {
      pageHeight = yScroll;
    };
    // for small pages with total width less then width of the viewport
    if(xScroll < windowWidth) {
      pageWidth = windowWidth;
    } else {
      pageWidth = xScroll;
    };
    var arrayPageSize = new Array(pageWidth, pageHeight, windowWidth, windowHeight);
    return arrayPageSize;
  },

  addEvent: function(obj, evType, fn, useCapture) {
    if(obj.addEventListener) {
      obj.addEventListener(evType, fn, useCapture);
      return true;
    } else if(obj.attachEvent) {
      var r = obj.attachEvent('on' + evType, fn);
      return r;
    } else {
      obj['on' + evType] = fn;
    };
    return false;
  }
};

sb_javaScriptLoader.callBack('sb_imageModal');