$(document).ready(function () {
    ShowAddForm();
});
function DoctorId() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0)
        return id;
    else {
        var id = localStorage.getItem('DoctorID');
        if (id)
            return id;
        else 0;
    }
}
function ShowAddForm() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "vaccine",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    markup = '';
                    if (key == 0) {
                        markup = getInventoryForm(1, item);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-inventory-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = getInventoryForm(form_id, item);
                        $('.form-fields:last').after(markup);
                        total_forms = $('.form-fields').length;
                        $('.add-inventory-form .total-forms').val(total_forms);
                    }
                });
                HideAlert();
            }
        },
        error: function (errrmessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }
    });

}
 
function getInventoryForm(form_id, vaccine) {
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="text" disabled id="Name_' + form_id + '" name="name_' + form_id + '" value="' + vaccine.Name + '" class="form-control" />';
    markup += '</div>';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="hidden" id="VaccineID_' + form_id + '" name="VaccineID_' + form_id + '" value="' + vaccine.ID + '">';
    markup += '<input type="text" placeholder="Total vaccines" id="vaccineCount_' + form_id + '" name="vaccineCount_' + form_id + '" class="form-control input-box" required />';
    markup += '</div>';
    markup += '</row>';
    markup += '</div>';
    return markup;
}

function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var total_forms = $('.add-inventory-form .total-forms').val();

    var DoctorID = DoctorId();
    var VaccineInventory = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var vaccineId = $("#VaccineID_" + i).val();
        var count = $("#vaccineCount_" + i).val();

        VaccineInventory.push({ VaccineID: vaccineId, Count: count, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "vaccineinventory",
        data: JSON.stringify(VaccineInventory),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess)
                ShowAlert('Error', result.Message, 'danger');
            else {
                ShowAlert('Success', 'Vaccine inventory is saved.', 'success');
                ScrollToTop();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function validate() {
    var total_errors = 0;
    $('.form-fields .input-box').each(function () {
        if ($(this).val() == '') {
            $(this).addClass('input-error').removeClass('input-success');
            total_errors += 1;
        } else {
            $(this).addClass('input-success').removeClass('input-error');
        }
    });
    if (total_errors == 0)
        return true;
    else
        return false;

}