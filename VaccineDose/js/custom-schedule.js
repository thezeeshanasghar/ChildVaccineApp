$(document).ready(function () {
    CheckDoctorSchedule();
});
function CheckDoctorSchedule() {
    var html = "";
    $.ajax({
        url: SERVER + "doctorschedule/" + DoctorId(),
        type: "Get",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess){
                LoadCustomSchedule();
                html += '<input type="button" class="btn btn-primary" id="btnAdd" onclick="return Add();" value="Create Schedule" />'
                $("#btn").html(html);
            }   
            else {
                html += '<input type="button" class="btn btn-primary" id="btnEdit" onclick="return Update();" value="Edit Schedule" />' 
                $("#btn").html(html);
            }
            HideAlert();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
 
}
function LoadCustomSchedule() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "dose",
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
                        markup = getClinicForm(1, item);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-clinic-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = getClinicForm(form_id, item);
                        $('.form-fields:last').after(markup);
                        total_forms = $('.form-fields').length;
                        $('.add-clinic-form .total-forms').val(total_forms);

                    }

                });


                HideAlert();

            }
        },
        error: function (errrmessage) {

        }
    });

}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var total_forms = $('.add-clinic-form .total-forms').val();

    var DoctorID = DoctorId();
    var DoctorSchedule = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var doseID = $("#DoseID_" + i).val();
        var Gap = $("#GapInDays_" + i).val();

        DoctorSchedule.push({ DoseID: doseID, GapInDays: Gap, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "doctorschedule",
        data: JSON.stringify(DoctorSchedule),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess)
                ShowAlert('Error', result.Message, 'danger');
            else {
                ShowAlert('Success', 'Custom schedule is saved.', 'success');
                ScrollToTop();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
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
function Update() {

}

function getClinicForm(form_id, dose) {
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<select id="GapInDays_' + form_id + '" name="GapPeriod_' + form_id + '" class="form-control input-box" required>';
    markup += '<option value="">-- select time --</option>';
    markup += '<option value="0">At Birth</option>';
    markup += '<option value="42">6 Weeks</option>';
    markup += '<option value="49">7 Weeks</option>';
    markup += '<option value="56">8 Weeks</option>';
    markup += '<option value="63">9 Weeks</option>';
    markup += '<option value="70">10 Weeks</option>';
    markup += '<option value="77">11 Weeks</option>';
    markup += '<option value="84">12 Weeks</option>';
    markup += '<option value="91">13 Weeks</option>';
    markup += '<option value="98">14 Weeks</option>';
    markup += '<option value="105">15 Weeks</option>';
    markup += '<option value="112">16 Weeks</option>';
    markup += '<option value="119">17 Weeks</option>';
    markup += '<option value="126">18 Weeks</option>';
    markup += '<option value="133">19 Weeks</option>';
    markup += '<option value="140">20 Weeks</option>';
    markup += '<option value="147">21 Weeks</option>';
    markup += '<option value="154">22 Weeks</option>';
    markup += '<option value="161">23 Weeks</option>';
    markup += '<option value="168">24 Weeks</option>';
    markup += '<option value="212">7 Months</option>';
    markup += '<option value="243">8 Months</option>';
    markup += '<option value="273">9 Months</option>';
    markup += '<option value="304">10 Months</option>';
    markup += '<option value="334">11 Months</option>';
    markup += '<option value="365">12 Months</option>';
    markup += '<option value="395">13 Months</option>';
    markup += '<option value="426">14 Months</option>';
    markup += '<option value="456">15 Months</option>';
    markup += '<option value="486">16 Months</option>';
    markup += '<option value="547">1 Year 6 months</option>';
    markup += '<option value="608">1 Year 8 months</option>';
    markup += '<option value="730">2 Years</option>';
    markup += '<option value="760">2 Years 1 months</option>';
    markup += '<option value="1125">3 Years 1 months</option>';
    markup += '</select>';
    markup += '</div>';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="hidden" id="DoseID_' + form_id + '" name="DoseID_' + form_id + '" value="' + dose.ID + '">';
    markup += '<input type="text" class="form-control" disabled  placeholder="Dose Name" id="DoseName_' + form_id + '" name="DoseName_' + form_id + '" value="' + dose.Name + '" />';
    markup += '</div>';
    markup += '</row>';
    markup += '</div>';
    return markup;
}
//Valdidation using jquery  
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