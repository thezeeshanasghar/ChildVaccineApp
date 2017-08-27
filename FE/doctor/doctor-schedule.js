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

    markup += '<option value="486" ';
    markup += doctorSchdule.GapInDays == 486 ? selectedAttribute : '';
    markup += '>16 Months</option>';

    markup += '<option value="516" ';
    markup += doctorSchdule.GapInDays == 516 ? selectedAttribute : '';
    markup += '>17 Months</option>';

    markup += '<option value="546" ';
    markup += doctorSchdule.GapInDays == 546 ? selectedAttribute : '';
    markup += '>18 Months</option>';

    markup += '<option value="576" ';
    markup += doctorSchdule.GapInDays == 576 ? selectedAttribute : '';
    markup += '>19 Months</option>';

    markup += '<option value="606" ';
    markup += doctorSchdule.GapInDays == 606 ? selectedAttribute : '';
    markup += '>20 Months</option>';

    markup += '<option value="636" ';
    markup += doctorSchdule.GapInDays == 636 ? selectedAttribute : '';
    markup += '>21 Months</option>';

    markup += '<option value="666" ';
    markup += doctorSchdule.GapInDays == 666 ? selectedAttribute : '';
    markup += '>22 Months</option>';

    markup += '<option value="696" ';
    markup += doctorSchdule.GapInDays == 696 ? selectedAttribute : '';
    markup += '>23 Months</option>';

    markup += '<option value="726" ';
    markup += doctorSchdule.GapInDays == 726 ? selectedAttribute : '';
    markup += '>24 Months</option>';

    markup += '<option value="756" ';
    markup += doctorSchdule.GapInDays == 756 ? selectedAttribute : '';
    markup += '>25 Months</option>';

    markup += '<option value="786" ';
    markup += doctorSchdule.GapInDays == 786 ? selectedAttribute : '';
    markup += '>26 Months</option>';

    markup += '<option value="816" ';
    markup += doctorSchdule.GapInDays == 816 ? selectedAttribute : '';
    markup += '>27 Months</option>';

    markup += '<option value="846" ';
    markup += doctorSchdule.GapInDays == 846 ? selectedAttribute : '';
    markup += '>28 Months</option>';

    markup += '<option value="876" ';
    markup += doctorSchdule.GapInDays == 876 ? selectedAttribute : '';
    markup += '>29 Months</option>';

    markup += '<option value="906" ';
    markup += doctorSchdule.GapInDays == 906 ? selectedAttribute : '';
    markup += '>30 Months</option>';

    markup += '<option value="936" ';
    markup += doctorSchdule.GapInDays == 936 ? selectedAttribute : '';
    markup += '>31 Months</option>';

    markup += '<option value="966" ';
    markup += doctorSchdule.GapInDays == 966 ? selectedAttribute : '';
    markup += '>32 Months</option>';

    markup += '<option value="996" ';
    markup += doctorSchdule.GapInDays == 996 ? selectedAttribute : '';
    markup += '>33 Months</option>';

    markup += '<option value="1026" ';
    markup += doctorSchdule.GapInDays == 1026 ? selectedAttribute : '';
    markup += '>34 Months</option>';

    markup += '<option value="1056" ';
    markup += doctorSchdule.GapInDays == 1056 ? selectedAttribute : '';
    markup += '>35 Months</option>';

    markup += '<option value="1086" ';
    markup += doctorSchdule.GapInDays == 1086 ? selectedAttribute : '';
    markup += '>36 Months</option>';

    markup += '<option value="1116" ';
    markup += doctorSchdule.GapInDays == 1116 ? selectedAttribute : '';
    markup += '>37 Months</option>';

    markup += '<option value="1146" ';
    markup += doctorSchdule.GapInDays == 1146 ? selectedAttribute : '';
    markup += '>38 Months</option>';

    markup += '<option value="1176" ';
    markup += doctorSchdule.GapInDays == 1176 ? selectedAttribute : '';
    markup += '>39 Months</option>';

    markup += '<option value="1206" ';
    markup += doctorSchdule.GapInDays == 1206 ? selectedAttribute : '';
    markup += '>40 Months</option>';

    markup += '<option value="1236" ';
    markup += doctorSchdule.GapInDays == 1236 ? selectedAttribute : '';
    markup += '>41 Months</option>';

    markup += '<option value="1266" ';
    markup += doctorSchdule.GapInDays == 1266 ? selectedAttribute : '';
    markup += '>42 Months</option>';

    markup += '<option value="1296" ';
    markup += doctorSchdule.GapInDays == 1296 ? selectedAttribute : '';
    markup += '>43 Months</option>';

    markup += '<option value="1326" ';
    markup += doctorSchdule.GapInDays == 1326 ? selectedAttribute : '';
    markup += '>44 Months</option>';

    markup += '<option value="1356" ';
    markup += doctorSchdule.GapInDays == 1356 ? selectedAttribute : '';
    markup += '>45 Months</option>';

    markup += '<option value="1386" ';
    markup += doctorSchdule.GapInDays == 1386 ? selectedAttribute : '';
    markup += '>46 Months</option>';

    markup += '<option value="1416" ';
    markup += doctorSchdule.GapInDays == 1416 ? selectedAttribute : '';
    markup += '>47 Months</option>';

    markup += '<option value="1446" ';
    markup += doctorSchdule.GapInDays == 1446 ? selectedAttribute : '';
    markup += '>48 Months</option>';

    markup += '<option value="1476" ';
    markup += doctorSchdule.GapInDays == 1476 ? selectedAttribute : '';
    markup += '>49 Months</option>';

    markup += '<option value="1506" ';
    markup += doctorSchdule.GapInDays == 1506 ? selectedAttribute : '';
    markup += '>50 Months</option>';

    markup += '<option value="1536" ';
    markup += doctorSchdule.GapInDays == 1536 ? selectedAttribute : '';
    markup += '>51 Months</option>';

    markup += '<option value="1566" ';
    markup += doctorSchdule.GapInDays == 1566 ? selectedAttribute : '';
    markup += '>52 Months</option>';

    markup += '<option value="1596" ';
    markup += doctorSchdule.GapInDays == 1596 ? selectedAttribute : '';
    markup += '>53 Months</option>';

    markup += '<option value="1626" ';
    markup += doctorSchdule.GapInDays == 1626 ? selectedAttribute : '';
    markup += '>54 Months</option>';

    markup += '<option value="1656" ';
    markup += doctorSchdule.GapInDays == 1656 ? selectedAttribute : '';
    markup += '>55 Months</option>';

    markup += '<option value="1686" ';
    markup += doctorSchdule.GapInDays == 1686 ? selectedAttribute : '';
    markup += '>56 Months</option>';

    markup += '<option value="1716" ';
    markup += doctorSchdule.GapInDays == 1716 ? selectedAttribute : '';
    markup += '>57 Months</option>';

    markup += '<option value="1746" ';
    markup += doctorSchdule.GapInDays == 1746 ? selectedAttribute : '';
    markup += '>58 Months</option>';

    markup += '<option value="1776" ';
    markup += doctorSchdule.GapInDays == 1776 ? selectedAttribute : '';
    markup += '>59 Months</option>';

    markup += '<option value="1806" ';
    markup += doctorSchdule.GapInDays == 1806 ? selectedAttribute : '';
    markup += '>60 Months</option>';

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
    markup += '<option value="516">17 Months</option>';
    markup += '<option value="546">18 Months</option>';
    markup += '<option value="576">19 Months</option>';
    markup += '<option value="606">20 Months</option>';
    markup += '<option value="636">21 Months</option>';
    markup += '<option value="666">22 Months</option>';
    markup += '<option value="696">23 Months</option>';
    markup += '<option value="726">24 Months</option>';
    markup += '<option value="756">25 Months</option>';
    markup += '<option value="786">26 Months</option>';
    markup += '<option value="816">27 Months</option>';
    markup += '<option value="846">28 Months</option>';
    markup += '<option value="876">29 Months</option>';
    markup += '<option value="906">30 Months</option>';
    markup += '<option value="936">31 Months</option>';
    markup += '<option value="966">32 Months</option>';
    markup += '<option value="996">33 Months</option>';
    markup += '<option value="1026">34 Months</option>';
    markup += '<option value="1056">35 Months</option>';
    markup += '<option value="1086">36 Months</option>';
    markup += '<option value="1116">37 Months</option>';
    markup += '<option value="1146">38 Months</option>';
    markup += '<option value="1176">39 Months</option>';
    markup += '<option value="1206">40 Months</option>';
    markup += '<option value="1236">41 Months</option>';
    markup += '<option value="1266">42 Months</option>';
    markup += '<option value="1296">43 Months</option>';
    markup += '<option value="1326">44 Months</option>';
    markup += '<option value="1356">45 Months</option>';
    markup += '<option value="1386">46 Months</option>';
    markup += '<option value="1416">47 Months</option>';
    markup += '<option value="1446">48 Months</option>';
    markup += '<option value="1476">49 Months</option>';
    markup += '<option value="1506">50 Months</option>';
    markup += '<option value="1536">51 Months</option>';
    markup += '<option value="1566">52 Months</option>';
    markup += '<option value="1596">53 Months</option>';
    markup += '<option value="1626">54 Months</option>';
    markup += '<option value="1656">55 Months</option>';
    markup += '<option value="1686">56 Months</option>';
    markup += '<option value="1716">57 Months</option>';
    markup += '<option value="1746">58 Months</option>';
    markup += '<option value="1776">59 Months</option>';
    markup += '<option value="1806">60 Months</option>';
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