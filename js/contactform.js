if(!sb_contactForm) { var sb_contactForm = new Object(); };

sb_contactForm = {
  init: function() {
    if(!document.getElementById) return;
    // attach the onsubmit event to the form
    var tgtForm = document.getElementById('contactform');
    if(!tgtForm) return;
    tgtForm.onsubmit = function() { if(sb_formCheck.checkForm()) sb_contactForm.sendEmail(); return false; };
    // make sure all form controls are enabled
    sb_contactForm.disableForm(false);
  },

  sendEmail: function() {
    sb_contactForm.showContactTimer();
    // convert illegal url encoded characters(&, +, =) to something we can pass
    var name = document.getElementById('name').value;
    name = name.replace(/&/g,"__am__");
    name = name.replace(/=/g,"__eq__");
    name = name.replace(/\+/g,"__pl__");
    var org = document.getElementById('org').value;
    org = org.replace(/&/g,"__am__");
    org = org.replace(/=/g,"__eq__");
    org = org.replace(/\+/g,"__pl__");
    var email = document.getElementById('email').value;
    email = email.replace(/&/g,"__am__");
    email = email.replace(/=/g,"__eq__");
    email = email.replace(/\+/g,"__pl__");
    var message = document.getElementById('message').value;
    message = message.replace(/&/g,"__am__");
    message = message.replace(/=/g,"__eq__");
    message = message.replace(/\+/g,"__pl__");
    // create the post string then send the ajax request
    var data = 'name='+name+'&org='+org+'&email='+email+'&message='+message;
    sb_xmlHttp.sendRequest('contactxml.php',data,'POST','sb_contactForm.update();');
  },

  showContactTimer: function() {
    var status = document.createElement('div');
    status.id = 'contactstatus';
    // create the new elements for the text and loading image
    var p = document.createElement('p');
    var strong = document.createElement('strong');
    var image = document.createElement('img');
    strong.appendChild(document.createTextNode('Sending your email message. Please wait...'));
    image.src = '/images/loading.gif';
    image.alt = 'Loading...';
    p.appendChild(strong);
    p.appendChild(document.createElement('br'));
    p.appendChild(image);
    // append the new elements to the status
    status.appendChild(p);
    document.getElementById('contactform').appendChild(status);
    // disable all of the form controls
    sb_contactForm.disableForm(true);
  },

  update: function() {
    // if the ajax readystate is "loaded"
    if (sb_xmlHttp.http.readyState==4) {
      // create the status elements
      var status = document.getElementById('contactstatus');
      var strong = document.createElement('strong');
      // if the serverstatus is "OK"
      if (sb_xmlHttp.http.status==200) {
        // grab the email status from the xml return
        // we're going to use the try statement on this because we never know when the server will break and deliver a malformed return
        // if that were to happen it would break the script and leave the user stuck on the loading message with no clue what happened
        var xmlReturn;
        try {
          xmlReturn = sb_xmlHttp.http.responseXML.documentElement.getElementsByTagName('status')[0].childNodes[0].nodeValue;
        } catch(e) {
          xmlReturn = 'FAILED: '+e;
        };
        // handle all of the possible return states
        switch(xmlReturn) {
          case 'OK' :
            strong.appendChild(document.createTextNode(sb_xmlHttp.http.responseXML.documentElement.getElementsByTagName('confirmation')[0].childNodes[0].nodeValue));
            strong.className = 'ok';
            break;
          case 'NOTOK' :
            strong.appendChild(document.createTextNode(sb_xmlHttp.http.responseXML.documentElement.getElementsByTagName('confirmation')[0].childNodes[0].nodeValue));
            strong.className = 'error';
            break;
          case 'ERROR' :
            strong.appendChild(document.createTextNode(sb_xmlHttp.http.responseXML.documentElement.getElementsByTagName('confirmation')[0].childNodes[0].nodeValue));
            strong.className = 'error';
            // cycle through the errors and add them to the message
            var errors = sb_xmlHttp.http.responseXML.documentElement.getElementsByTagName('errors')[0].getElementsByTagName('error');
            for(var i=0;i<errors.length;i++) {
              strong.appendChild(document.createElement('br'));
              strong.appendChild(document.createTextNode(errors[i].childNodes[0].nodeValue));
            };
            break;
          default :
            strong.appendChild(document.createTextNode('There was a problem with our email system. Invalid response from XML ('+xmlReturn+').'));
            strong.className = 'error';
        };
      } else {
        strong.appendChild(document.createTextNode('There was a problem with our email system. Invalid server status ('+sb_xmlHttp.http.status+').'));
        strong.className = 'error';
      };
      // create the close button and attach its onclick event
      var closeLink = document.createElement('a');
      closeLink.appendChild(document.createTextNode('close'));
      closeLink.href = '#';
      closeLink.onclick = function() { var s = document.getElementById('contactstatus'); s.parentNode.removeChild(s); sb_contactForm.disableForm(false); return false; };
      // create the two p elements and append the status text and close button
      var p1 = document.createElement('p');
      p1.appendChild(strong);
      var p2 = document.createElement('p');
      p2.appendChild(closeLink);
      // cycle through the status div and kill anything that is there
      while(status.hasChildNodes()) {
        status.removeChild(status.lastChild);
      };
      // append our new status elements
      status.appendChild(p1);
      status.appendChild(p2);
    }
  },

  disableForm: function(arg) {
    var i, form = document.getElementById('contactform');
    // cycle through the form input controls and set their disabled status
    var inputs = form.getElementsByTagName('input');
    for(i=inputs.length-1;i>=0;i--) {
      inputs[i].disabled = arg;
    };
    // cycle through all of the textarea controls and set their disabled status
    var textareas = form.getElementsByTagName('textarea');
    for(i=textareas.length-1;i>=0;i--) {
      textareas[i].disabled = arg;
    };
    // cycle through the selects controls and set their disabled status
    var selects = form.getElementsByTagName('select');
    for(i=selects.length-1;i>=0;i--) {
      selects[i].disabled = arg;
    };
  },

  purgeEvents: function(elem) {
    if(!elem) elem = document.getElementsByTagName('body')[0];
    if(!elem) return;
    // cycle through the attributes for this element
    var i, attr = elem.attributes;
    if(attr) {
      for(i=attr.length-1;i>=0;i--) {
        var n = attr[i].name;
        // if this attribute is a function then remove it
        if(typeof elem[n]==='function')  elem[n] = null;
      };
    };
    // and purgeevents on all child nodes
    var children = elem.childNodes;
    if(children) {
      for(i=children.length-1;i>=0;i--) {
        this.purgeEvents(elem.childNodes[i]);
      };
    };
  }
};

sb_javaScriptLoader.callBack('sb_contactForm');