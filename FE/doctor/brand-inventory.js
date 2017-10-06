$(document).ready(function () {
    getDoctorBrandInventory();
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
function getDoctorBrandInventory() {
    ShowAlert('Loading data', 'Checking for existing brands', 'info');
    var html = "";
    $.ajax({
        url: SERVER + "brandinventory/" + DoctorId(),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Loading data', 'Checking for existing brands', 'info');
            }
            else {
                $.each(result.ResponseData, function (key, item) {
                    markup = '';
                    if (key == 0) {

                        markup = getBrandInventoryFormForEditView(1, item);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-inventory-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = getBrandInventoryFormForEditView(form_id, item);
                        $('.form-fields:last').after(markup);
                        total_forms = $('.form-fields').length;
                        $('.add-inventory-form .total-forms').val(total_forms);
                    }
                    $("#btnUpdate").show();
                });
            }
            HideAlert();
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }
    });

}


function getBrandInventoryFormForEditView(form_id, brandInventory) {
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">';
    markup += '<input type="hidden"  id="BrandCountID_' + form_id + '" name="BrandCountID_' + form_id + '" value="' + brandInventory.ID + '"   />';
    markup += '<input type="text" disabled id="VaccineName_' + form_id + '" name="VaccineName_' + form_id + '" value="' + brandInventory.VaccineName + '" class="form-control" />';
    markup += '</div>';
    markup += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">';
    markup += '<input type="text" disabled id="BrandName_' + form_id + '" name="BrandName_' + form_id + '" value="' + brandInventory.Brand.Name + '" class="form-control" />';
    markup += '</div>';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="text" class="form-control"   placeholder="Brand Count" id="Count_' + form_id + '" name="Count_' + form_id + '" value="' + brandInventory.Count + '"  />';
    markup += '</div>';
    markup += '</row>';
    markup += '</div>';
    return markup;
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var total_forms = $('.add-inventory-form .total-forms').val();

    var DoctorID = DoctorId();
    var BrandInventory = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var BrandCountID_ = $("#BrandCountID_" + i).val();
        var count = $("#Count_" + i).val();

        BrandInventory.push({ ID: BrandCountID_, Count: count, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "brandinventory",
        data: JSON.stringify(BrandInventory),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess)
                ShowAlert('Error', result.Message, 'danger');
            else {
                ShowAlert('Success', 'Brand inventory is updated.', 'success');
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