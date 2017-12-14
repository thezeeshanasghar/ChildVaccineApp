$(document).ready(function () {
    HideAlert();
    localStorage.clear();
    $("#MobileNumber").intlTelInput({ initialCountry: "PK" });
    $("#ForgotMobileNumber").intlTelInput({ initialCountry: "PK" });
});

function Login() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;

    $("#btnSignIn").button('loading');
    $("#btnSignIn").prop('disabled', true);

    var obj = {
        MobileNumber: $('#MobileNumber').val(),
        Password: $('#Password').val(),
        CountryCode: $("#MobileNumber").intlTelInput("getSelectedCountryData").dialCode
    }

    $.ajax({
        url: SERVER + 'user/login',
        type: 'post',
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $("#btnSignIn").prop('disabled', false);
            $("#btnSignIn").button('reset');

            if (!result.IsSuccess) {
                // TODO Muneeb: add proper bootstrap alert instead of browser alert
                //alert(result.Message);
                ShowAlert('Error', result.Message, 'danger');

                return;
            }
            if (result.ResponseData.UserType == "SUPERADMIN") {
                localStorage.setItem('UserType', "SUPERADMIN");
                window.location.replace('/admin/dashboard.html');
            }
            else if (result.ResponseData.UserType == "DOCTOR") {
                localStorage.setItem('UserType', "DOCTOR");
                localStorage.setItem('UserID', result.ResponseData.ID);
                localStorage.setItem('DoctorID', result.ResponseData.DoctorID);
                window.location.replace('/doctor/clinic-selection.html');
            }
            else if (result.ResponseData.UserType == "PARENT") {
                localStorage.setItem('UserType', "PARENT");
                localStorage.setItem('MobileNumber', result.ResponseData.MobileNumber);
                window.location.replace('/index.html');
            }
            else {
                window.location.replace('/un-authorize.html');
            }

        },
        error: function (jqXHR, exception) {
            $("#btnSignIn").prop('disabled', false);
            $("#btnSignIn").button('reset');
            displayErrors(jqXHR, exception);
        }
    });
}

function SendForgetPasswordRequest() {
    $('#form2').validator('validate');
    var validator = $('#form2').data("bs.validator");
    if (validator.hasErrors())
        return false;

    var obj = {
        MobileNumber: $("#ForgotMobileNumber").val(),
        //Email: $("#ForgotEmail").val(),
        CountryCode: $("#ForgotMobileNumber").intlTelInput("getSelectedCountryData").dialCode
    }
    $.ajax({
        url: SERVER + 'user/forgot-password',
        type: 'post',
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
                return;
            }
            else {
                ShowAlert('Success', result.Message, 'success');
            }

        },
        error: function (jqXHR, exception) {
            displayErrors(jqXHR, exception);
        }
    });
}