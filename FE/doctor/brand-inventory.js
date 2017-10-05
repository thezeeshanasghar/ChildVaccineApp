$(document).ready(function () {
    CheckDoctorInventory();
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
function CheckDoctorInventory() {
    ShowAlert('Loading data', 'Checking for existing schedule', 'info');
    var html = "";
    $.ajax({
        url: SERVER + "brandinventory/" + DoctorId(),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAddForm();
            }
            else {
                ShowUpdateForm(result.ResponseData);
                $("#btnAdd").hide();
                $("#btnUpdate").show();
            }
            HideAlert();
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }
    });

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
    markup += '     <div class="row">';
    markup += '         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">';
    markup += '             <input type="text" disabled id="Name_' + form_id + '" name="name_' + form_id + '" value="' + vaccine.Name + '" class="form-control" />';
    markup += '         </div>';

    markup += '         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">';
    //markup +=           getBrands(form_id, vaccine.Brands);
    markup += '            <select id="BrandID_' + form_id + '" name="Name_' + form_id + '" class="form-control input-box" required>';
    markup += '                <option value="">-- select brand --</option>';
    for (var index in vaccine.Brands) {
        markup += '             <option value="' + vaccine.Brands[index].ID + '">' + vaccine.Brands[index].Name + '</option>';
    }
    markup += '            </select>';
    markup += '         </div>';

    markup += '         <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '             <input type="hidden" id="VaccineID_' + form_id + '" name="VaccineID_' + form_id + '" value="' + vaccine.ID + '">';
    markup += '             <input type="text" placeholder="Total vaccines" id="vaccineCount_' + form_id + '" name="vaccineCount_' + form_id + '" class="form-control input-box" required />';
    markup += '         </div>';

    markup += '     </div>';
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
    var BrandInventory = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var brandId = $("#BrandID_" + i).val();
        var count = $("#vaccineCount_" + i).val();

        BrandInventory.push({ BrandID: brandId, Count: count, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "brandinventory",
        data: JSON.stringify(BrandInventory),
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
function ShowUpdateForm(brandinventories) {
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

                        markup = getVaccineInventoryFormForEditView(1, item, brandinventories);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-inventory-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = getVaccineInventoryFormForEditView(form_id, item, brandinventories);
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
function getVaccineInventoryFormForEditView(form_id, vaccine, brandinventories) {
    var selectedAttribute = ' selected="selected" ';
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">';
    markup += '<input type="text" disabled id="Name_' + form_id + '" name="name_' + form_id + '" value="' + vaccine.Name + '" class="form-control" />';

    markup += '</div>';

    markup += '         <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">';
    markup += '            <select id="BrandID_' + form_id + '" name="Name_' + form_id + '" class="form-control input-box" required>';
    markup += '                <option value="">-- select brand --</option>';
    var inventoryCount = -1;
    for (var index in vaccine.Brands) {
        var brand = vaccine.Brands[index];
        markup += '             <option value="' + brand.ID + '"';
        for (var brandInventoryIndex in brandinventories) {
            if (brandinventories[brandInventoryIndex].BrandID == brand.ID) {
                markup += selectedAttribute;
                inventoryCount = brandinventories[brandInventoryIndex].Count;
                markup += '                                               >' + brand.Name + '</option>';
                markup += '            </select>';
                markup += '         </div>';
                break;
            }
        }
    }
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="text" class="form-control"   placeholder="Vaccine Count" id="Count_' + form_id + '" name="Count_' + form_id + '" value="' + inventoryCount + '"  />';
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
        var BrandID_ = $("#BrandID_" + i).val();
        var count = $("#Count_" + i).val();

        BrandInventory.push({ ID: BrandID_, Count: count, DoctorID: DoctorID })
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
                ShowAlert('Success', 'Vaccine inventory is updated.', 'success');
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