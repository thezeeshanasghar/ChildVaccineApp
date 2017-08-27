//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetClinicIdFromUrlOrLocalStorage() != 0) {
        DisableOffDays();
    }
});

function GetClinicIdFromUrlOrLocalStorage() {
    var id = localStorage.getItem('OnlineClinic');
    if (id)
        return id;
    else return 0;

}

function DisableOffDays() {
    $.ajax({
        url: SERVER + 'clinic/' + GetClinicIdFromUrlOrLocalStorage(),
        type: 'Get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.IsSuccess) {
                var OffDays = result.ResponseData.OffDays;
                if (OffDays.length > 1) {
                    OffDays = OffDays.split(",");
                    if ($.inArray("Monday", OffDays) != -1)
                        $("#Monday").prop('disabled', true);
                    if ($.inArray("Tuesday", OffDays) != -1)
                        $("#Tuesday").prop('disabled', true);
                    if ($.inArray("Wednesday", OffDays) != -1)
                        $("#Wednesday").prop('disabled', true);
                    if ($.inArray("Thursday", OffDays) != -1)
                        $("#Thursday").prop('disabled', true);
                    if ($.inArray("Friday", OffDays) != -1)
                        $("#Friday").prop('disabled', true);
                    if ($.inArray("Saturday", OffDays) != -1)
                        $("#Saturday").prop('disabled', true);
                    if ($.inArray("Sunday", OffDays) != -1)
                        $("#Sunday").prop('disabled', true);
                }
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var result = [];
    $('input[name="PreferredDayOfWeek"]:checked').each(function () {
        result.push(this.value);
    });

    var preferdayreminder = "0";
    if (document.getElementById("TogglePreferredDayOfReminder").checked) {
        preferdayreminder = $('#PreferredDayOfReminder').val()
    }

    var obj = {
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        DOB: $('#DOB').val(),
        CountryCode:$('#CountryCode').val(),
        MobileNumber: $('#MobileNumber').val(),
        PreferredDayOfWeek: result.join(','),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').find(":selected").val(),
        PreferredSchedule: preferdayreminder,
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),

        Password: PasswordGenerator(),
        ClinicID: GetClinicIdFromUrlOrLocalStorage()
    };
    $.ajax({
        url: SERVER + "child",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                window.location = 'child.html?id=' + GetClinicIdFromUrlOrLocalStorage();
            }
        },
        error: function (errormessage, e) {
            displayErrors(errormessage, e);
        }
    });
}

//Valdidation using jquery  
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}