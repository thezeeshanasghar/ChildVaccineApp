//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetOnlineClinicIdFromLocalStorage() != 0) {
        DisableOffDays();
    }
    $("#child").show();
    $("#vaccine").hide();

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
                    html += '<div class="form-group">';
                    html += '<label>';
                    html += '<input type="checkbox" name="VaccineName" value="' + item.ID + '"  / >';
                    html += '&nbsp;'+item.Name;
                    html += '</label>';
                    html += '</div>'
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

    var result = [];
    $('input[name="PreferredDayOfWeek"]:checked').each(function () {
        result.push(this.value);
    });

    var preferdayreminder = "0";
    if (document.getElementById("TogglePreferredDayOfReminder").checked) {
        preferdayreminder = $('#PreferredDayOfReminder').val()
    }

    var Vaccines = [];
     $('input[name="VaccineName"]:checked').each(function () {
        Vaccines.push({ ID: this.value });
    });

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

    var validator = $('#form1').data("bs.validator");
    if (!validator.hasErrors()) {
        $("#child").hide();
        GetVaccines();
        $("#vaccine").show();
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