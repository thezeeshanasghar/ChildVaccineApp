$(document).ready(function () {
    CheckDoctorSchedule();
});
function CheckDoctorSchedule() {
    ShowAlert('Loading data', 'Checking for existing schedule', 'info');
    var html = "";
    $.ajax({
        url: SERVER + "doctorschedule/" + DoctorId(),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAddForm();
            }
            else {
                ShowUpdateForm(result);
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
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
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

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var total_forms = $('.add-clinic-form .total-forms').val();

    var DoctorID = DoctorId();
    var DoctorSchedule = [];

    var DoctorID = DoctorId();
    for (var i = 1; i <= total_forms; i++) {
        var DoctorScheduleID = $("#DoctorSchduleID_" + i).val();
        var doseID = $("#DoseID_" + i).val();
        var Gap = $("#GapInDays_" + i).val();

        DoctorSchedule.push({ ID: DoctorScheduleID, DoseID: doseID, GapInDays: Gap, DoctorID: DoctorID })
    }

    $.ajax({
        url: SERVER + "doctorschedule",
        data: JSON.stringify(DoctorSchedule),
        type: "PUT",
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

function ShowUpdateForm(result) {
    $.each(result.ResponseData, function (key, item) {
        markup = '';
        if (key == 0) {
            markup = getClinicFormForEditView(1, item);
            $('.btnLine').before(markup);
            total_forms = 1;
            $('.add-clinic-form .total-forms').val(total_forms);

        } else {
            form_id = $('.form-fields:last').attr('data-form-id');
            form_id++;
            markup = getClinicFormForEditView(form_id, item);
            $('.form-fields:last').after(markup);
            total_forms = $('.form-fields').length;
            $('.add-clinic-form .total-forms').val(total_forms);
        }
    });
}

function getClinicFormForEditView(form_id, doctorSchdule) {
    var selectedAttribute = ' selected="selected" ';
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<input type="hidden" id="DoctorSchduleID_' + form_id + '" name="DoctorSchduleID_' + form_id + '" value="' + doctorSchdule.ID + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<select id="GapInDays_' + form_id + '" name="GapPeriod_' + form_id + '" class="form-control input-box" required>';
    markup += '<option value="">-- select time --</option>';

    markup += '<option value="0" ';
    markup += (doctorSchdule.GapInDays == 0) ? selectedAttribute : '';
    markup += '>At Birth</option>';

    markup += '<option value="42"';
    markup += doctorSchdule.GapInDays == 42 ? selectedAttribute : '';
    markup += '>6 Weeks</option>';

    markup += '<option value="49"';
    markup += doctorSchdule.GapInDays == 49 ? selectedAttribute : '';
    markup += '>7 Weeks</option>';

    markup += '<option value="56"';
    markup += doctorSchdule.GapInDays == 56 ? selectedAttribute : '';
    markup += '>8 Weeks</option>';

    markup += '<option value="63"';
    markup += doctorSchdule.GapInDays == 63 ? selectedAttribute : '';
    markup += '>9 Weeks</option>';

    markup += '<option value="70"';
    markup += doctorSchdule.GapInDays == 70 ? selectedAttribute : '';
    markup += '>10 Weeks</option>';

    markup += '<option value="77"';
    markup += doctorSchdule.GapInDays == 77 ? selectedAttribute : ''
    markup += '>11 Weeks</option>';

    markup += '<option value="84"';
    markup += doctorSchdule.GapInDays == 84 ? selectedAttribute : '';
    markup += '>12 Weeks</option>';

    markup += '<option value="91"';
    markup += doctorSchdule.GapInDays == 91 ? selectedAttribute : '';
    markup += '>13 Weeks</option>';

    markup += '<option value="98"';
    markup += doctorSchdule.GapInDays == 98 ? selectedAttribute : '';
    markup += '>14 Weeks</option>';

    markup += '<option value="105"';
    markup += doctorSchdule.GapInDays == 105 ? selectedAttribute : '';
    markup += '>15 Weeks</option>';

    markup += '<option value="112"';
    markup += doctorSchdule.GapInDays == 112 ? selectedAttribute : '';
    markup += '>16 Weeks</option>';

    markup += '<option value="119"';
    markup += doctorSchdule.GapInDays == 119 ? selectedAttribute : '';
    markup += '>17 Weeks</option>';

    markup += '<option value="126"';
    markup += doctorSchdule.GapInDays == 126 ? selectedAttribute : '';
    markup += '>18 Weeks</option>';

    markup += '<option value="133"';
    markup += doctorSchdule.GapInDays == 133 ? selectedAttribute : '';
    markup += '>19 Weeks</option>';

    markup += '<option value="140"';
    markup += doctorSchdule.GapInDays == 140 ? selectedAttribute : '';
    markup += '>20 Weeks</option>';

    markup += '<option value="147"';
    markup += doctorSchdule.GapInDays == 147 ? selectedAttribute : '';
    markup += '>21 Weeks</option>';

    markup += '<option value="154"';
    markup += doctorSchdule.GapInDays == 154 ? selectedAttribute : '';
    markup += '>22 Weeks</option>';

    markup += '<option value="161"';
    markup += doctorSchdule.GapInDays == 161 ? selectedAttribute : '';
    markup += '>23 Weeks</option>';

    markup += '<option value="168"';
    markup += doctorSchdule.GapInDays == 168 ? selectedAttribute : '';
    markup += '>24 Weeks</option>';

    markup += '<option value="212"';
    markup += doctorSchdule.GapInDays == 212 ? selectedAttribute : '';
    markup += '>7 Months</option>';

    markup += '<option value="243"';
    markup += doctorSchdule.GapInDays == 243 ? selectedAttribute : '';
    markup += '>8 Months</option>';

    markup += '<option value="273"';
    markup += doctorSchdule.GapInDays == 273 ? selectedAttribute : ''
    markup += '>9 Months</option>';

    markup += '<option value="304"';
    markup += doctorSchdule.GapInDays == 304 ? selectedAttribute : '';
    markup += '>10 Months</option>';

    markup += '<option value="334"';
    markup += doctorSchdule.GapInDays == 334 ? selectedAttribute : ''
    markup += '>11 Months</option>';

    markup += '<option value="365"';
    markup += doctorSchdule.GapInDays == 365 ? selectedAttribute : '';
    markup += '>12 Months</option>';

    markup += '<option value="395"';
    markup += doctorSchdule.GapInDays == 395 ? selectedAttribute : '';
    markup += '>13 Months</option>';

    markup += '<option value="426"';
    markup += doctorSchdule.GapInDays == 426 ? selectedAttribute : '';
    markup += '>14 Months</option>';

    markup += '<option value="456"';
    markup += doctorSchdule.GapInDays == 456 ? selectedAttribute : '';
    markup += '>15 Months</option>';

    markup += '<option value="486"';
    markup += doctorSchdule.GapInDays == 486 ? selectedAttribute : '';
    '>16 Months</option>';

    markup += '<option value="547"';
    markup += doctorSchdule.GapInDays == 547 ? selectedAttribute : '';
    markup += '>1 Year 6 months</option>';

    markup += '<option value="608"';
    markup += doctorSchdule.GapInDays == 608 ? selectedAttribute : '';
    markup += '>1 Year 8 months</option>';

    markup += '<option value="730"';
    markup += doctorSchdule.GapInDays == 730 ? selectedAttribute : '';
    markup += '>2 Years</option>';

    markup += '<option value="760"';
    markup += doctorSchdule.GapInDays == 760 ? selectedAttribute : '';
    markup += '>2 Years 1 months</option>';

    markup += '<option value="1125';
    markup += doctorSchdule.GapInDays == 1125 ? selectedAttribute : '';
    markup += '">3 Years 1 months</option>';

    markup += '</select>';
    markup += '</div>';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<input type="hidden" id="DoseID_' + form_id + '" name="DoseID_' + form_id + '" value="' + doctorSchdule.Dose.ID + '">';
    markup += '<input type="text" class="form-control" disabled  placeholder="Dose Name" id="DoseName_' + form_id + '" name="DoseName_' + form_id + '" value="' + doctorSchdule.Dose.Name + '" />';
    markup += '</div>';
    markup += '</row>';
    markup += '</div>';
    return markup;
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