$(document).ready(function () {
    getDoctorBrandAmounts();
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
function getDoctorBrandAmounts() {
    ShowAlert('Loading data', 'Checking for existing brands', 'info');
    var html = "";
    $.ajax({
        url: SERVER + "brandamount/" + DoctorId(),
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

                        markup = getBrandAmountFormForEditView(1, item);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-inventory-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = getBrandAmountFormForEditView(form_id, item);
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

 
function getBrandAmountFormForEditView(form_id,brandamount) {
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">';
    markup += '<input type="hidden"  id="BrandAmountID_' + form_id + '" name="BrandAmountID_' + form_id + '" value="'+brandamount.ID+'"   />';
    markup += '<input type="text" disabled id="VaccineName_' + form_id + '" name="VaccineName_' + form_id + '" value="' + brandamount.VaccineName + '" class="form-control" />';
    markup += '</div>';
    markup += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">';
    markup += '<input type="text" disabled id="BrandName_' + form_id + '" name="BrandName_' + form_id + '" value="' + brandamount.Brand.Name + '" class="form-control" />';
    markup += '</div>';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="text" class="form-control"   placeholder="Vaccine Amount" id="Amount_' + form_id + '" name="Amount_' + form_id + '" value="' + brandamount.Amount + '"  />';
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
    var BrandAmount = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var BrandAmountID_ = $("#BrandAmountID_" + i).val();
        var amound = $("#Amount_" + i).val();

        BrandAmount.push({ ID: BrandAmountID_, Amount: amound, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "brandamount",
        data: JSON.stringify(BrandAmount),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess)
                ShowAlert('Error', result.Message, 'danger');
            else {
                ShowAlert('Success', 'Brand Amount is updated.', 'success');
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