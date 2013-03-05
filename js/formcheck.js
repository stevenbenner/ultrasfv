if(!sb_formCheck) { var sb_formCheck = new Object(); };

sb_formCheck = {
  targetForm: 'sb_form', // form to target for field checking
  errorClass: 'error', // class to give any form field which has an error
  errorMsgDiv: 'errormsg', // id to give the error message div element to when it's created
  errorMsgText: 'Please enter or change the fields marked with a ', // text to fill the error message box
  errorImgSrc: 'alert.gif', // error icon image that will be displayed next to any field which has an error
  errorImgAlt: 'Error', // alt text of the error image
  errorImgTitle: 'This field has an error!', // title given to each error image
  errorImgClass: 'errorimage', // class for each error image

  init: function() {
    if(!document.getElementById) return;
    var tgtForm = document.getElementById(sb_formCheck.targetForm);
    if(!tgtForm) return;
    // attach the onsubmit event to the form
    tgtForm.onsubmit = function() { return sb_formCheck.checkForm(); };
  },

  checkForm: function() {
    var i, field, reqFields = document.getElementById('required').value.split(',');
    if(!reqFields) return true;
    // if there is an old error box remove it, then remove all error classes and images
    var errorBox = document.getElementById(this.errorMsgDiv);
    if(errorBox) {
      errorBox.parentNode.removeChild(errorBox);
      // remove old images and classes from the required fields
      for(i=0;i<reqFields.length;i++) {
        field = document.getElementById(reqFields[i]);
        if(!field) continue;
        var fieldSib = field.nextSibling;
        if(fieldSib && fieldSib.className===this.errorImgClass) {
          field.parentNode.removeChild(fieldSib);
        };
        field.className = '';
      };
    };
    // now check each of the required fields based on its .type
    for(i=reqFields.length-1;i>=0;i--) {
      field = document.getElementById(reqFields[i]);
      if(!field) continue;
      switch(field.type) {
        case 'text' :
          if(!field.value && field.id!=='email') {
            this.addError(field);
          } else if(field.id==='email' && !this.isEmailAddr(field.value)) {
            this.addError(field);
          };
          break;
        case 'textarea' :
          if(!field.value) this.addError(field);
          break;
        case 'checkbox' :
          if(!field.checked) this.addError(field);
          break;
        case 'select-one' :
          if(!field.selectedIndex && field.selectedIndex===0) this.addError(field);
          break;
        case 'password' :
          if(!field.value) this.addError(field);
          break;
        case 'file' :
          if(!field.value) this.addError(field);
          break;
      };
    };
    // was a new error box created? If not then this form must have validated
    if(!document.getElementById(this.errorMsgDiv)) return true;
    // but if we didnt return in the last line that means there was an error
    return false;
  },

  addError: function(field) {
    // create new error image and set its attributes
    var errorImage = document.createElement('img');
    errorImage.src = this.errorImgSrc;
    errorImage.alt = this.errorImgAlt;
    errorImage.title = this.errorImgTitle;
    errorImage.className = this.errorImgClass;
    // then set this fields class to reflect then error and display the error image next to it
    field.className = this.errorClass;
    field.parentNode.insertBefore(errorImage,field.nextSibling);
    // if there isn't allready an error message then we need to add one
    if(!document.getElementById(this.errorMsgDiv)) {
      // create the paragraph element then insert the error message text and error image
      var p = document.createElement('p');
      p.appendChild(errorImage.cloneNode(true));
      p.appendChild(document.createTextNode(' '+this.errorMsgText))
      // create the error message div, set its id then add the error paragraph
      var errorBox = document.createElement('div');
      errorBox.id = this.errorMsgDiv;
      errorBox.appendChild(p);
      var sb, inputs = document.getElementById(this.targetForm).getElementsByTagName('input');
      // cycle through the form inputs to find the submit button
      for(var i=inputs.length-1;i>=0;i--) {
        if(inputs[i].type==='submit') {
          sb = inputs[i];
          break;
        };
      };
      // now insert the error message after the submit button (assuming we detected one)
      if(sb) sb.parentNode.insertBefore(errorBox,sb.nextSibling);
    };
  },

  isEmailAddr: function(str) {
    // verify that the string matches the email address regex
    return str.match(/^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/);
  }
};

sb_javaScriptLoader.callBack('sb_formCheck');