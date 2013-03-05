if(!sb_xmlHttp) { var sb_xmlHttp = new Object(); };

sb_xmlHttp = {
  http: null,

  sendRequest: function(url,data,method,callback) {
    sb_xmlHttp.createRequestObject();
    switch(method) {
      case 'GET' :
        try {
          sb_xmlHttp.http.open('GET', url, true);
          sb_xmlHttp.http.onreadystatechange = (function(c) { return function() { sb_xmlHttp.handleResponse(c); }; })(callback);
          sb_xmlHttp.http.send(data);
        } catch(e) {
          alert('Request send failed. '+e);
        };
        break;
      case 'POST' :
        try {
          sb_xmlHttp.http.open('POST', url, true);
          sb_xmlHttp.http.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
          sb_xmlHttp.http.onreadystatechange = (function(c) { return function() { sb_xmlHttp.handleResponse(c); }; })(callback);
          sb_xmlHttp.http.send(data);
        } catch(e) {
          alert('Request send failed. '+e);
        };
        break;
    };
  },

  createRequestObject: function() {
    try {
      // internet explorere 6+
      sb_xmlHttp.http = new ActiveXObject('Msxml2.XMLHTTP');
    } catch(e) {
      try {
        // internet explorer 5.5+
        sb_xmlHttp.http = new ActiveXObject('Microsoft.XMLHTTP');
      } catch(e) {
        sb_xmlHttp.http = null;
      };
    };
    if(!sb_xmlHttp.http && typeof XMLHttpRequest!='undefined') {
      // frefox, opera 8.0+ and safari
      sb_xmlHttp.http = new XMLHttpRequest();
    };
  },

  handleResponse: function(callback) {
    if(callback) eval(callback);
  }
};

sb_javaScriptLoader.callBack('sb_xmlHttp');