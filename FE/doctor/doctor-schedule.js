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
    markup += '<select onchange="showAlert(' + form_id + ',' + doctorSchdule.Dose.MinGap + ')" id="GapInDays_' + form_id + '" name="GapPeriod_' + form_id + '" class="form-control input-box" required>';
    markup += '<option value="">-- select time --</option>';

    markup += '<option value="0" ';
    markup += (doctorSchdule.GapInDays == 0) ? selectedAttribute : '';
    markup += '>At Birth</option>';

    markup += '<option value="7"';
    markup += doctorSchdule.GapInDays == 7 ? selectedAttribute : '';
    markup += '>1 Week</option>';

    markup += '<option value="14"';
    markup += doctorSchdule.GapInDays == 14 ? selectedAttribute : '';
    markup += '>2 Weeks</option>';

    markup += '<option value="21"';
    markup += doctorSchdule.GapInDays == 21 ? selectedAttribute : '';
    markup += '>3 Weeks</option>';

    markup += '<option value="28"';
    markup += doctorSchdule.GapInDays == 28 ? selectedAttribute : '';
    markup += '>4 Weeks</option>';

    markup += '<option value="35"';
    markup += doctorSchdule.GapInDays == 35 ? selectedAttribute : '';
    markup += '>5 Weeks</option>';

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
    markup += '>3 Months</option>';

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
    markup += '>6 Months</option>';

    markup += '<option value="212"';
    markup += doctorSchdule.GapInDays == 212 ? selectedAttribute : '';
    markup += '>7 Months</option>';

    markup += '<option value="243"';
    markup += doctorSchdule.GapInDays == 243 ? selectedAttribute : '';
    markup += '>8 Months</option>';

    markup += '<option value="274"';
    markup += doctorSchdule.GapInDays == 274 ? selectedAttribute : ''
    markup += '>9 Months</option>';

    markup += '<option value="304"';
    markup += doctorSchdule.GapInDays == 304 ? selectedAttribute : '';
    markup += '>10 Months</option>';

    markup += '<option value="334"';
    markup += doctorSchdule.GapInDays == 334 ? selectedAttribute : ''
    markup += '>11 Months</option>';

    markup += '<option value="365"';
    markup += doctorSchdule.GapInDays == 365 ? selectedAttribute : '';
    markup += '>1 Year</option>';

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

    markup += '<option value="517" ';
    markup += doctorSchdule.GapInDays == 517 ? selectedAttribute : '';
    markup += '>17 Months</option>';

    markup += '<option value="547" ';
    markup += doctorSchdule.GapInDays == 547 ? selectedAttribute : '';
    markup += '>18 Months</option>';

    markup += '<option value="578" ';
    markup += doctorSchdule.GapInDays == 578 ? selectedAttribute : '';
    markup += '>19 Months</option>';

    markup += '<option value="608" ';
    markup += doctorSchdule.GapInDays == 608 ? selectedAttribute : '';
    markup += '>20 Months</option>';

    markup += '<option value="639" ';
    markup += doctorSchdule.GapInDays == 639 ? selectedAttribute : '';
    markup += '>21 Months</option>';

    markup += '<option value="669" ';
    markup += doctorSchdule.GapInDays == 669 ? selectedAttribute : '';
    markup += '>22 Months</option>';

    markup += '<option value="699" ';
    markup += doctorSchdule.GapInDays == 699 ? selectedAttribute : '';
    markup += '>23 Months</option>';

    markup += '<option value="730" ';
    markup += doctorSchdule.GapInDays == 730 ? selectedAttribute : '';
    markup += '>2 Years</option>';

    markup += '<option value="760" ';
    markup += doctorSchdule.GapInDays == 760 ? selectedAttribute : '';
    markup += '>25 Months</option>';

    markup += '<option value="791" ';
    markup += doctorSchdule.GapInDays == 791 ? selectedAttribute : '';
    markup += '>26 Months</option>';

    markup += '<option value="821" ';
    markup += doctorSchdule.GapInDays == 821 ? selectedAttribute : '';
    markup += '>27 Months</option>';

    markup += '<option value="851" ';
    markup += doctorSchdule.GapInDays == 851 ? selectedAttribute : '';
    markup += '>28 Months</option>';

    markup += '<option value="882" ';
    markup += doctorSchdule.GapInDays == 882 ? selectedAttribute : '';
    markup += '>29 Months</option>';

    markup += '<option value="912" ';
    markup += doctorSchdule.GapInDays == 912 ? selectedAttribute : '';
    markup += '>30 Months</option>';

    markup += '<option value="943" ';
    markup += doctorSchdule.GapInDays == 943 ? selectedAttribute : '';
    markup += '>31 Months</option>';

    markup += '<option value="973" ';
    markup += doctorSchdule.GapInDays == 973 ? selectedAttribute : '';
    markup += '>32 Months</option>';

    markup += '<option value="1004" ';
    markup += doctorSchdule.GapInDays == 1004 ? selectedAttribute : '';
    markup += '>33 Months</option>';

    markup += '<option value="1034" ';
    markup += doctorSchdule.GapInDays == 1034 ? selectedAttribute : '';
    markup += '>34 Months</option>';

    markup += '<option value="1064" ';
    markup += doctorSchdule.GapInDays == 1064 ? selectedAttribute : '';
    markup += '>35 Months</option>';

    markup += '<option value="1095" ';
    markup += doctorSchdule.GapInDays == 1095 ? selectedAttribute : '';
    markup += '>3 Years</option>';

    markup += '<option value="1125" ';
    markup += doctorSchdule.GapInDays == 1125 ? selectedAttribute : '';
    markup += '>37 Months</option>';

    markup += '<option value="1156" ';
    markup += doctorSchdule.GapInDays == 1156 ? selectedAttribute : '';
    markup += '>38 Months</option>';

    markup += '<option value="1186" ';
    markup += doctorSchdule.GapInDays == 1186 ? selectedAttribute : '';
    markup += '>39 Months</option>';

    markup += '<option value="1216" ';
    markup += doctorSchdule.GapInDays == 1216 ? selectedAttribute : '';
    markup += '>40 Months</option>';

    markup += '<option value="1247" ';
    markup += doctorSchdule.GapInDays == 1247 ? selectedAttribute : '';
    markup += '>41 Months</option>';

    markup += '<option value="1277" ';
    markup += doctorSchdule.GapInDays == 1277 ? selectedAttribute : '';
    markup += '>42 Months</option>';

    markup += '<option value="1308" ';
    markup += doctorSchdule.GapInDays == 1308 ? selectedAttribute : '';
    markup += '>43 Months</option>';

    markup += '<option value="1338" ';
    markup += doctorSchdule.GapInDays == 1338 ? selectedAttribute : '';
    markup += '>44 Months</option>';

    markup += '<option value="1369" ';
    markup += doctorSchdule.GapInDays == 1369 ? selectedAttribute : '';
    markup += '>45 Months</option>';

    markup += '<option value="1399" ';
    markup += doctorSchdule.GapInDays == 1399 ? selectedAttribute : '';
    markup += '>46 Months</option>';

    markup += '<option value="1429" ';
    markup += doctorSchdule.GapInDays == 1429 ? selectedAttribute : '';
    markup += '>47 Months</option>';

    markup += '<option value="1460" ';
    markup += doctorSchdule.GapInDays == 1460 ? selectedAttribute : '';
    markup += '>4 Years</option>';

    markup += '<option value="1490" ';
    markup += doctorSchdule.GapInDays == 1490 ? selectedAttribute : '';
    markup += '>49 Months</option>';

    markup += '<option value="1521" ';
    markup += doctorSchdule.GapInDays == 1521 ? selectedAttribute : '';
    markup += '>50 Months</option>';

    markup += '<option value="1551" ';
    markup += doctorSchdule.GapInDays == 1551 ? selectedAttribute : '';
    markup += '>51 Months</option>';

    markup += '<option value="1582" ';
    markup += doctorSchdule.GapInDays == 1582 ? selectedAttribute : '';
    markup += '>52 Months</option>';

    markup += '<option value="1612" ';
    markup += doctorSchdule.GapInDays == 1612 ? selectedAttribute : '';
    markup += '>53 Months</option>';

    markup += '<option value="1642" ';
    markup += doctorSchdule.GapInDays == 1642 ? selectedAttribute : '';
    markup += '>54 Months</option>';

    markup += '<option value="1673" ';
    markup += doctorSchdule.GapInDays == 1673 ? selectedAttribute : '';
    markup += '>55 Months</option>';

    markup += '<option value="1703" ';
    markup += doctorSchdule.GapInDays == 1703 ? selectedAttribute : '';
    markup += '>56 Months</option>';

    markup += '<option value="1734" ';
    markup += doctorSchdule.GapInDays == 1734 ? selectedAttribute : '';
    markup += '>57 Months</option>';

    markup += '<option value="1764" ';
    markup += doctorSchdule.GapInDays == 1764 ? selectedAttribute : '';
    markup += '>58 Months</option>';

    markup += '<option value="1795" ';
    markup += doctorSchdule.GapInDays == 1795 ? selectedAttribute : '';
    markup += '>59 Months</option>';

    markup += '<option value="1825" ';
    markup += doctorSchdule.GapInDays == 1825 ? selectedAttribute : '';
    markup += '>5 Years</option>';

    markup += '<option value="2190" ';
    markup += doctorSchdule.GapInDays == 2190 ? selectedAttribute : '';
    markup += '>6 Years</option>';

    markup += '<option value="2555" ';
    markup += doctorSchdule.GapInDays == 2555 ? selectedAttribute : '';
    markup += '>7 Years</option>';

    markup += '<option value="2920" ';
    markup += doctorSchdule.GapInDays == 2920 ? selectedAttribute : '';
    markup += '>8 Years</option>';

    markup += '<option value="3285" ';
    markup += doctorSchdule.GapInDays == 3285 ? selectedAttribute : '';
    markup += '>9 Years</option>';

    markup += '<option value="3315" ';
    markup += doctorSchdule.GapInDays == 3315 ? selectedAttribute : '';
    markup += '>9 Year 1 Month</option>';

    markup += '<option value="3650" ';
    markup += doctorSchdule.GapInDays == 3650 ? selectedAttribute : '';
    markup += '>10 Years</option>';

    markup += '<option value="3833" ';
    markup += doctorSchdule.GapInDays == 3833 ? selectedAttribute : '';
    markup += '>10 Year 6 Months</option>';

    markup += '<option value="4015" ';
    markup += doctorSchdule.GapInDays == 4015 ? selectedAttribute : '';
    markup += '>11 Years</option>';

    markup += '<option value="4380" ';
    markup += doctorSchdule.GapInDays == 4380 ? selectedAttribute : '';
    markup += '>12 Years</option>';

    markup += '<option value="4745" ';
    markup += doctorSchdule.GapInDays == 4745 ? selectedAttribute : '';
    markup += '>13 Years</option>';

    markup += '<option value="5110" ';
    markup += doctorSchdule.GapInDays == 5110 ? selectedAttribute : '';
    markup += '>14 Years</option>';

    markup += '<option value="5475" ';
    markup += doctorSchdule.GapInDays == 5475 ? selectedAttribute : '';
    markup += '>15 Years</option>';

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

function showAlert(id, MinGap) {
    if (MinGap && id > 1) {

        currentDropdownVal = $("#GapInDays_" + id).val();
        currentDoseName = $("#DoseName_" + id).val();
        previousDropdownVal = $("#GapInDays_" + (id - 1)).val();
        previousDoseName = $("#DoseName_" + (id - 1)).val();
        difference = MinGap - (currentDropdownVal - previousDropdownVal);
        if (currentDoseName.substr(0, currentDoseName.indexOf('#') + 1) === previousDoseName.substr(0, previousDoseName.indexOf('#') + 1)) {
            //console.log(previousDoseName.substr(0, previousDoseName.indexOf('#')+1));

            if (parseInt(previousDropdownVal)) {
                if (difference > 2) {
                    alert('Cannot set this value because minimum gap from previous dose is ' + (MinGap / 7) + ' weeks');
                    $("#GapInDays_" + id).val('');
                }
            }
        }
        console.log('selected value: ' + currentDropdownVal + ' Prev: ' + previousDropdownVal + ' Min: ' + MinGap);
    }
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