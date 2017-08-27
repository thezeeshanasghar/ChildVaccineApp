$(document).ready(function () {
    localStorage.clear();
});
function Login() {
        $('#form1').validator('validate');
        var validator = $('#form1').data("bs.validator");
        if (validator.hasErrors())
            return false;
      

        var obj = {
            MobileNumber: $('#MobileNumber').val(),
            Password: $('#Password').val(),
            CountryCode: $('#CountryCode').val()
        }
        $.ajax({
            url: SERVER + 'user/login',
            type: 'post',
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (!result.IsSuccess) {
                    // TODO Muneeb: add proper bootstrap alert instead of browser alert
                    alert(result.Message);
                    return;
                }
                if (result.ResponseData.UserType == "SUPERADMIN") {
                    localStorage.setItem('UserType', "SUPERADMIN");
                    window.location.replace('/index.html');
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
                else  {
                    window.location.replace('/un-authorize.html');
                }
              
            },
            error: function (jqXHR, exception) {
                displayErrors(jqXHR, exception);
            }
        });
    }
 