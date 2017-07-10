﻿$(document).ready(function () {
    localStorage.clear();
});
function Login() {
        var obj = {
            MobileNumber: $('#MobileNumber').val(),
            Password: $('#Password').val(),
            UserType: "test",
            DoctorID:"0"
        }
        $.ajax({
            url: 'api/user/login',
            type: 'post',
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.ResponseData.UserType == "SUPERADMIN") {
                    localStorage.setItem('UserType', "SUPERADMIN");
                }
                else if (result.ResponseData.UserType == "DOCTOR") {
                    localStorage.setItem('UserType', "DOCTOR");
                    localStorage.setItem('UserID', result.ResponseData.ID);
                    localStorage.setItem('DoctorID', result.ResponseData.DoctorID);

                }
                else if (result.ResponseData.UserType == "PARENT") {
                    localStorage.setItem('UserType', "PARENT");
                }
                else  {
                    localStorage.setItem('UserType', "TEST");
                }
                window.location.replace('index.html');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
 