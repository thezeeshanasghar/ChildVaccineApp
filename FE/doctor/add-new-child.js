//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetOnlineClinicIdFromLocalStorage() != 0) {
        DisableOffDays();
    }
    $("#child").show();
    $("#vaccine").hide();
    var searchedName = getParameterByName("searchKeyword");
    if (searchedName != "") {
        $("#Name").val(searchedName);
    }
    var selectedCity = localStorage.getItem("DoctorId_" + DoctorId() + "_City");
    if (selectedCity) {
        $("#City").val(selectedCity);
    }
});

function DisableOffDays() {
    $.ajax({
        url: SERVER + 'clinic/' + GetOnlineClinicIdFromLocalStorage(),
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
//all vaccines to for child
function GetVaccines() {
    $.ajax({
        url: SERVER + "vaccine",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Loading data', 'Checking for existing brands', 'info');
            }
            else {
                $.each(result.ResponseData, function (key, item) {
                    if ($("input[name='gender']:checked").val() == "Boy" && item.Name.indexOf("HPV") >= 0) {
                    } else {
                        html += '<div class="form-group">';
                        html += '<label>';
                        html += '<input type="checkbox" checked name="VaccineName" value="' + item.ID + '"  / >';
                        html += '&nbsp;' + item.Name;
                        html += '</label>';
                        html += '</div>'
                    }
                });
            }
            $("#childVaccine").html(html);
            HideAlert();
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

    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);

    var Vaccines = [];
    $('input[name="VaccineName"]:checked').each(function () {
        Vaccines.push({ ID: this.value });
    });

    var obj = {
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        DOB: $('#DOB').val(),
        CountryCode: $("#MobileNumber").intlTelInput("getSelectedCountryData").dialCode,
        MobileNumber: $('#MobileNumber').val(),
        PreferredDayOfWeek: $('#PreferredDayOfWeek').find(":selected").val(),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').find(":selected").val(),
        PreferredSchedule: $('#PreferredVacccineSchedule').find(":selected").val(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),
        Password: PasswordGenerator(),
        ClinicID: GetOnlineClinicIdFromLocalStorage(),
        ChildVaccines: Vaccines,
    };
    $.ajax({
        url: SERVER + "child",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $("#btnAdd").prop('disabled', false);
            $("#btnAdd").button('reset');

            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
                ScrollToTop();
            }
            else {
                window.location = 'child.html?id=' + GetOnlineClinicIdFromLocalStorage();
            }
        },
        error: function (errormessage, e) {
            $("#btnAdd").prop('disabled', false);
            $("#btnAdd").button('reset');
            displayErrors(errormessage, e);
        }
    });
}


function ShowHide(event) {
    $('#form1').validator('validate');
    localStorage.setItem("DoctorId_" +DoctorId() + "_City", $('#City').find(":selected").text());
    var validator = $('#form1').data("bs.validator");
    if (!validator.hasErrors()) {
        var obj = {
            Name: $('#Name').val(),
            CountryCode: $("#MobileNumber").intlTelInput("getSelectedCountryData").dialCode,
            MobileNumber: $('#MobileNumber').val(),
        };
        $.ajax({
            url: SERVER + "child/validate-nameAndNumber",
            data: JSON.stringify(obj),
            type: "Post",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result==null)  {
                    $("#child").hide();
                    GetVaccines();
                    $("#vaccine").show();
                }
            },
            error: function (errormessage, e) {
                ShowAlert('Error', errormessage.statusText, 'danger');
                ScrollToTop();
            }
        });
       
    }
}
//Valdidation using jquery  
function validate() {
    $('#form2').validator('validate');
    var validator = $('#form2').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}