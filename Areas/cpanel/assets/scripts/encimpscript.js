function SubmitsEncry() {
    var txtpassword = $('#Password').val();
    var skey = $('#hdns').val();

    if (txtpassword == "") {
        alert('Please enter Password');
        return false;
    }
    else {
        var key = CryptoJS.enc.Utf8.parse(skey);
        var iv = CryptoJS.enc.Utf8.parse(skey);
        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key,

            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

        $('#Password').val(encryptedpassword);
        //alert(encryptedpassword);
    }
}

function Validatepage() {
    debugger;
    var txtpassword = document.getElementById('NewPassword').value;
    var cKey = document.getElementById('hdns').value;
    if (txtpassword == "") {
        alert('Please enter Password');
        return false;
    }
    else {
        var key = CryptoJS.enc.Utf8.parse(cKey);
        var iv = CryptoJS.enc.Utf8.parse(cKey);
        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword + cKey), key,
            { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        document.getElementById('NewPassword').value = encryptedpassword;
        //alert('Kay :' + cKey);
        //alert('encrypted password :' + encryptedpassword);
    }
}