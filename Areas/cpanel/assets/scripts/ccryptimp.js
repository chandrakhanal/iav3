function Validatepage() {
    debugger;
    var ctrl_psw = 'Password';
    var ctrl_clpwd = 'hdns';
    //var ctrl_fip = 'hdks';
    var pwd = document.getElementById(ctrl_psw).value;
    if (pwd.length == 0) { document.getElementById(ctrl_psw).value = ''; return false; }
    var hashpwd = hex_sha512(pwd);
    var seed = document.getElementById(ctrl_clpwd).value;
    var hsh1 = hex_sha512(pwd);
    var hashpwd = hex_sha512(hex_sha512(hsh1)+ seed);
    document.getElementById(ctrl_psw).value = hashpwd;

    return true;
}

function ValidatePIN() {
    debugger;
    var ctrl_psw = 'mpin1';
    var ctrl_psw1 = 'mpin2';
    var ctrl_salt = 'TxtSalt';
    var pwd = document.getElementById(ctrl_psw).value;
    var pwd1 = document.getElementById(ctrl_psw1).value;
    var salt = document.getElementById(ctrl_salt).value;
    var hashpwd = hex_sha512(pwd);
   // var hashpwd = hex_sha512(hashpwd.toUpperCase() + salt);

    var hashpwd1 = hex_sha512(pwd1);
 //   var hashpwd1 = hex_sha512(hashpwd1.toUpperCase() + salt);

    //if (pwd.length == 0) {alert("Please enter your second level password."); return false; }
    document.getElementById(ctrl_psw).value = hashpwd;
    document.getElementById(ctrl_psw1).value = hashpwd1;
    document.getElementById(ctrl_salt).value = '';
    return true;
}
function ChangePas() {    
    document.getElementById('txtCurFLPwd').value = hex_sha512(document.getElementById('txtCurFLPwd').value);
    document.getElementById('txtFirstLPwd').value = hex_sha512(document.getElementById('txtFirstLPwd').value);
    document.getElementById('txtConfirmFirstLPwd').value = hex_sha512(document.getElementById('txtConfirmFirstLPwd').value);
}

function ValidateChangePIN() {

    debugger;

    var ctrl_mpin = 'currentmpin';
    var ctrl_psw = 'mpin1';
    var ctrl_psw1 = 'mpin2';
    var ctrl_salt = 'TxtSalt';
    var mpin = document.getElementById(ctrl_mpin).value;

    var pwd = document.getElementById(ctrl_psw).value;
    var pwd1 = document.getElementById(ctrl_psw1).value;
    var salt = document.getElementById(ctrl_salt).value;

    var hashpwd = hex_sha512(pwd);
    

    var hashpwd1 = hex_sha512(pwd1);
    
    var hasgmpin = hex_sha512(mpin);
    
    //if (pwd.length == 0) {alert("Please enter your second level password."); return false; }
    document.getElementById(ctrl_mpin).value = hasgmpin;

    document.getElementById(ctrl_psw).value = hashpwd;
    document.getElementById(ctrl_psw1).value = hashpwd1;
    document.getElementById(ctrl_salt).value = '';

    return true;
}
function ChangePasnNew() {
   
    var OldPwd = document.getElementById('OldPassword').value;   
    if (OldPwd.length == 0) { document.getElementById('OldPassword').value = ''; return false; }
    document.getElementById('OldPassword').value = hex_sha512(hex_sha512(OldPwd));
   
    var NewPwd = document.getElementById('NewPassword').value;
    if (NewPwd.length == 0) { document.getElementById('NewPassword').value = ''; return false; }
    document.getElementById('NewPassword').value = hex_sha512(NewPwd);
   
    var ConfirmPwd = document.getElementById('ConfirmPassword').value;
    if (ConfirmPwd.length == 0) { document.getElementById('ConfirmPassword').value = ''; return false; }
    document.getElementById('ConfirmPassword').value = hex_sha512(ConfirmPwd);
    //alert(document.getElementById('ConfirmPassword').value);
    //document.getElementById('iResrt').value = document.getElementById('ConfirmPassword').value;
    return true;
}
// Check Password Stregnth
function validatePassword(ctrl) {
    var p = document.getElementById(ctrl).value,  errors = [];
    if (p.length < 8) {
        errors.push("Your password must be at least 8 characters");
    }
    if (p.search(/[a-z]/i) < 0) {
        errors.push("Your password must contain at least one Lower Case letter.");
    }
    if (p.search(/[0-9]/) < 0) {
        errors.push("Your password must contain at least one digit.");
    }
    if (p.search(/[A-Z]/i) < 0) {
        errors.push("Your password must contain at least one upper case letter.");
    }
    if (p.search(/[!@#$%&?"]/) < 0) {
        errors.push("Your password must contain at least one special character.");
    }
    if (errors.length > 0) {
        alert(errors.join("\n"));
        return false;
    }
    ChangePasnNew();
    //ValidateMe(arGs);
    return true;
}
function MakePasw() {

    var NewPwd = document.getElementById('Password').value;
    if (NewPwd.length == 0) { document.getElementById('Password').value = ''; return false; }
    document.getElementById('Password').value = hex_sha512(NewPwd);

    var ConfirmPwd = document.getElementById('ConfirmPassword').value;
    if (ConfirmPwd.length == 0) { document.getElementById('ConfirmPassword').value = ''; return false; }
    document.getElementById('ConfirmPassword').value = hex_sha512(ConfirmPwd);
    //alert(document.getElementById('ConfirmPassword').value);
    //document.getElementById('iResrt').value = document.getElementById('ConfirmPassword').value;
    return true;
}
function ValidateMe(arGs) {
    debugger;
    var txtoldpassword = document.getElementById('OldPassword').value;
    var txtpassword = document.getElementById('NewPassword').value;
    var txtconfirmpassword = document.getElementById('ConfirmPassword').value;
   
    var cKey = arGs;
    if (txtpassword == "") {
        alert('Please enter Password');
        return false;
    }
    else {
       
        var key = CryptoJS.enc.Utf8.parse(cKey);
        var iv = CryptoJS.enc.Utf8.parse(cKey);
      
        var encryptedoldpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtoldpassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        document.getElementById('OldPassword').value = encryptedoldpassword;

        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        document.getElementById('NewPassword').value = encryptedpassword;

        var encryptedConfpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtconfirmpassword), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        document.getElementById('ConfirmPassword').value = encryptedConfpassword;

        alert('Kay :' + cKey);
        alert('encrypted password :' + encryptedpassword);
    }
}