//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);
    $.ajax({
        url: SERVER + "doctor/" + GetFromLocalStorage("DoctorID") + "/clinics",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#btnAdd").prop('disabled', false);
            $("#btnAdd").button('reset');
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<div class="form-group">';
                    html += '<label>';
                    html += '<input type="checkbox" checked name="Clinic" value="' + item.ID + '"  / >';
                    html += '&nbsp;' + item.Name;
                    html += '</label>';
                    html += '</div>'

                });
                $('#clinics').html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

/*
Add vacations
*/


function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }

    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);

    var Clinics = [];
    $('input[name="Clinic"]:checked').each(function () {
        Clinics.push({ ID: this.value });
    });

    var obj = {
        FromDate: $('#FromDate').val(),
        ToDate: $('#ToDate').val(),
        Clinics: Clinics,
    };
    if (obj.Clinics.length > 0) {
        $.ajax({
            url: SERVER + "schedule/add-vacation",
            data: JSON.stringify(obj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (!result.IsSuccess) {
                    ShowAlert('Error', result.Message, 'danger');
                    $("#btnAdd").prop('disabled', false);
                    $("#btnAdd").button('reset');
                    ScrollToTop();
                }
                else {
                    ScrollToTop();
                    ShowAlert('Success', result.Message, 'success');
                    $("#btnAdd").prop('disabled', false);
                    $("#btnAdd").button('reset');
                }
            },
            error: function (errormessage, e) {
                $("#btnAdd").prop('disabled', false);
                $("#btnAdd").button('reset');
                displayErrors(errormessage, e);
            }
        });
    } else {
        ShowAlert('Error', 'Please select at least one clinic', 'danger');
        ScrollToTop();
        $("#btnAdd").prop('disabled', false);
        $("#btnAdd").button('reset');
    }

}



//
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}