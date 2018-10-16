
$(document).ready(function () {
    HideAlert();
    GenerateScheduleForm();
    initMap();
});

var map, myMarker, myLatLng;
function initMap() {
    //if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function (position) {
            
            myLatLng = { lat: position.coords.latitude, lng: position.coords.longitude };

            map = new google.maps.Map(document.getElementById('Location'), {
                zoom: 14,
                center: myLatLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            myMarker = new google.maps.Marker({
                position: myLatLng,
                draggable: true
            });
            google.maps.event.addListener(myMarker, 'dragend', function (evt) {
                document.getElementById('current').innerHTML = '<p>Marker dropped: Current Lat: ' + evt.latLng.lat().toFixed(3) + ' Current Lng: ' + evt.latLng.lng().toFixed(3) + '</p>';
            });

            google.maps.event.addListener(myMarker, 'dragstart', function (evt) {
                document.getElementById('current').innerHTML = '<p>Currently dragging marker...</p>';
            });

            map.setCenter(myMarker.position);
            myMarker.setMap(map);

        }, function (error)
        {
            console.log(error);
        },
        {
            //maximumAge: 60000,
            timeout: 10000,
            //enableHighAccuracy: true
        });
    //}
  


} 

function Add() {
    var res = validateSchedule();
    if (res == false) {
        return false;
    }
    //$("#btnAddClinic").button('loading');
    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);

    var result = [];
    $('input[name="OffDays"]:checked').each(function () {
        result.push(this.value);
    });

    var obj = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Password: PasswordGenerator(),
        CountryCode: $("#MobileNumber").intlTelInput("getSelectedCountryData").dialCode,
        MobileNumber: $('#MobileNumber').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
        DisplayName: $('#DisplayName').val(),
        DoctorType: $("input[name='DoctorType']:checked").val(),
        Qualification: $('#Qualification').val(),
        AdditionalInfo: $('#AdditionalInfo').val(),
        ClinicDTO: {
            Name: $('#Name').val(),
            PhoneNumber: $('#PhoneNumber').val(),
            ConsultationFee: $('#ConsultationFee').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            OffDays: result.join(','),
            Lat: myMarker.getPosition().lat(),
            Long: myMarker.getPosition().lng(),
            Address: $('#Address').val()
        }
    };
    $.ajax({
        url: SERVER + "doctor",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
                $("#btnAdd").prop('disabled', false);
                $("#btnAdd").button('reset');
                return;
            } else {
                $("#DoctorId").val(result.ResponseData.ID)
                uploadImages();
                $("#btnAdd").prop('disabled', false);
                $("#btnAdd").button('reset');
                AddSchedule();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function uploadImages() {
    var data = new FormData();
    var file = $("#ProfileImage").get(0).files;
    var file1 = $("#SignatureImage").get(0).files;
    var dt = new Date();
    var date = dt.getDate() + "-" + dt.getMonth() + "-" + dt.getFullYear() + "_" + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    // Add the uploaded image content to the form data collection

    if (file.length > 0) {
        file[0].name = "ProfileImage_" + date + file[0].name;
        data.append("ProfileImage", file[0]);
    }
    if (file1.length > 0) {
        file1[0].name = "SignatureImage_" + date + file1[0].name;
        data.append("SignatureImage", file1[0]);
    }
    if (file.length > 0 || file1.length > 0) {
        $.ajax({
            type: "POST",
            url: SERVER + 'doctor/image',    // CALL WEB API TO SAVE THE FILES.
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,         // PREVENT AUTOMATIC DATA PROCESSING.
            cache: false,
            data: data, 		        // DATA OR FILES IN THIS CONTEXT.
            success: function (data, textStatus, xhr) {

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ': ' + errorThrown);
            }
        });
    }

}

//custom schedule start

function GenerateScheduleForm() {
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
                var doses = result.ResponseData;
                $.each(doses, function (key, item) {
                    markup = '';
                    if (key == 0) {
                        markup = GenerateScheduleRow(1, item);
                        $('.btnLine').before(markup);
                        total_forms = 1;
                        $('.add-clinic-form .total-forms').val(total_forms);

                    } else {
                        form_id = $('.form-fields:last').attr('data-form-id');
                        form_id++;
                        markup = GenerateScheduleRow(form_id, item);
                        $('.form-fields:last').after(markup);
                        total_forms = $('.form-fields').length;
                        $('.add-clinic-form .total-forms').val(total_forms);
                    }
                    ///

                });

                for (i = 1; i <= total_forms; i++) {
                    //$("#GapInDays_" + i).prop("disabled", true);

                    //if (doses[i - 1].MinAge ) { // commented because Dose must have MinAge; its required field in dose form
                    $("#GapInDays_" + i + " option").each(function (optionIndex) {
                        // this check is to skip the very first option like -- select min age --
                        if (optionIndex != 0) {
                            if ($(this).val() < doses[i - 1].MinAge)
                                $(this).prop('disabled', true);
                            if ($(this).val() == doses[i - 1].MinAge)
                                $(this).attr('selected', 'selected');

                        }
                    });



                    $("#GapInDays_" + i + " option").each(function (optionIndex) {
                        // this check is to skip the very first option like -- select max age --
                        if (optionIndex != 0) {
                            if ($(this).val() < doses[i - 1].MinAge)
                                $(this).prop('disabled', true);
                        }
                    });
                    //}

                    // first to look for MaxAge is defined or not
                    if (doses[i - 1].MaxAge) {
                        $("#GapInDays_" + i + " option").each(function (optionIndex) {
                            // this check is to skip the very first option like -- select min age --
                            if (optionIndex != 0) {
                                if ($(this).val() > doses[i - 1].MaxAge)
                                    $(this).prop('disabled', true);
                            }
                        });

                        $("#GapInDays_" + i + " option").each(function (optionIndex) {
                            // this check is to skip the very first option like -- select max age --
                            if (optionIndex != 0) {
                                if ($(this).val() > doses[i - 1].MaxAge)
                                    $(this).prop('disabled', true);
                            }
                        });
                    }

                }


                HideAlert();
            }
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }
    });

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

function GenerateScheduleRow(form_id, dose) {
    markup = '<div class="form-group form-fields" data-form-id="' + form_id + '">';
    markup += '<div class="row">';
    markup += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
    markup += '<select id="GapInDays_' + form_id + '" name="GapPeriod_' + form_id + '" class="form-control input-box" required onchange="showAlert(' + form_id + ',' + dose.MinGap + ')"  >';
    markup += '<option value="">-- select time --</option>';


    markup += '<option value="0">At Birth</option>';
    markup += '<option value="7">1 Week</option>';
    markup += '<option value="14">2 Weeks</option>';
    markup += '<option value="21">3 Weeks</option>';
    markup += '<option value="28">4 Weeks</option>';
    markup += '<option value="35">5 Weeks</option>';
    markup += '<option value="42">6 Weeks</option>';
    markup += '<option value="49">7 Weeks</option>';
    markup += '<option value="56">8 Weeks</option>';
    markup += '<option value="63">9 Weeks</option>';
    markup += '<option value="70">10 Weeks</option>';
    markup += '<option value="77">11 Weeks</option>';
    markup += '<option value="84">3 Months</option>';
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
    markup += '<option value="168">6 Months</option>';
    markup += '<option value="212">7 Months</option>';
    markup += '<option value="243">8 Months</option>';
    markup += '<option value="274">9 Months</option>';
    markup += '<option value="304">10 Months</option>';
    markup += '<option value="334">11 Months</option>';
    markup += '<option value="365">1 Year</option>';
    markup += '<option value="395">13 Months</option>';
    markup += '<option value="426">14 Months</option>';
    markup += '<option value="456">15 Months</option>';
    markup += '<option value="486">16 Months</option>';
    markup += '<option value="517">17 Months</option>';
    markup += '<option value="547">18 Months</option>';
    markup += '<option value="578">19 Months</option>';
    markup += '<option value="608">20 Months</option>';
    markup += '<option value="639">21 Months</option>';
    markup += '<option value="669">22 Months</option>';
    markup += '<option value="699">23 Months</option>';
    markup += '<option value="730">2 Years</option>';
    markup += '<option value="760">25 Months</option>';
    markup += '<option value="791">26 Months</option>';
    markup += '<option value="821">27 Months</option>';
    markup += '<option value="851">28 Months</option>';
    markup += '<option value="882">29 Months</option>';
    markup += '<option value="912">30 Months</option>';
    markup += '<option value="943">31 Months</option>';
    markup += '<option value="973">32 Months</option>';
    markup += '<option value="1004">33 Months</option>';
    markup += '<option value="1034">34 Months</option>';
    markup += '<option value="1064">35 Months</option>';
    markup += '<option value="1095">3 Years</option>';
    markup += '<option value="1125">37 Months</option>';
    markup += '<option value="1156">38 Months</option>';
    markup += '<option value="1186">39 Months</option>';
    markup += '<option value="1216">40 Months</option>';
    markup += '<option value="1247">41 Months</option>';
    markup += '<option value="1277">42 Months</option>';
    markup += '<option value="1308">43 Months</option>';
    markup += '<option value="1338">44 Months</option>';
    markup += '<option value="1369">45 Months</option>';
    markup += '<option value="1399">46 Months</option>';
    markup += '<option value="1429">47 Months</option>';
    markup += '<option value="1460">4 Years</option>';
    markup += '<option value="1490">49 Months</option>';
    markup += '<option value="1521">50 Months</option>';
    markup += '<option value="1551">51 Months</option>';
    markup += '<option value="1582">52 Months</option>';
    markup += '<option value="1612">53 Months</option>';
    markup += '<option value="1642">54 Months</option>';
    markup += '<option value="1673">55 Months</option>';
    markup += '<option value="1703">56 Months</option>';
    markup += '<option value="1734">57 Months</option>';
    markup += '<option value="1764">58 Months</option>';
    markup += '<option value="1795">59 Months</option>';
    markup += '<option value="1825">5 Years</option>';
    markup += '<option value="2190">6 Years</option>';
    markup += '<option value="2555">7 Years</option>';
    markup += '<option value="2920">8 Years</option>  ';
    markup += '<option value="3285">9 Years</option>';
    markup += '<option value="3315">9 Year 1 Month</option>';
    markup += '<option value="3650">10 Years</option>';
    markup += '<option value="3833">10 Year 6 Months</option>';
    markup += '<option value="4015">11 Years</option>';
    markup += '<option value="4380">12 Years</option>';
    markup += '<option value="4745">13 Years</option>';
    markup += '<option value="5110">14 Years</option>';
    markup += '<option value="5475">15 Years</option>';
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

function AddSchedule() {
    var res = validateSchedule();
    if (res == false) {
        return false;
    }
    var total_forms = $('.add-clinic-form .total-forms').val();

    var DoctorSchedule = [];

    var DoctorID = $("#DoctorId").val();
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
                next();
                ShowAlert('Registration', 'Your are successfully singup for <b>Vaccs.io</b><br/>Your username and password have been send to your email address.<br/>After admin approval you can <a href="/login.html?UserType=DOCTOR">login</a> to <b>https://vaccs.io</b>', 'success');
                ScrollToTop();
            }
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }
    });
}

// simple doctor signup
function simpleDoctorSignup() {

    var result = [];
    $('input[name="OffDays"]:checked').each(function () {
        result.push(this.value);
    });
    $("#btnCinicNext").button('loading');
    $("#btnCinicNext").prop('disabled', true);
    var obj = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Password: PasswordGenerator(),
        CountryCode: $("#MobileNumber").intlTelInput("getSelectedCountryData").dialCode,
        MobileNumber: $('#MobileNumber').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
        DisplayName: $('#DisplayName').val(),
        DoctorType: $("input[name='DoctorType']:checked").val(),
        Qualification: $('#Qualification').val(),
        AdditionalInfo: $('#AdditionalInfo').val(),
        ClinicDTO: {
            Name: $('#Name').val(),
            PhoneNumber: $('#PhoneNumber').val(),
            ConsultationFee: $('#ConsultationFee').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            OffDays: result.join(','),
            Lat: myMarker.getPosition().lat(),
            Long: myMarker.getPosition().lng(),
            Address: $('#Address').val()
        }
    };
    $.ajax({
        url: SERVER + "doctor",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger'); 
                return;
            } else {
                next();
                next();
                ShowAlert('Registration', 'Your are successfully singup for <b>Vaccs.io</b><br/>Your username and password have been send to your email address.<br/>After admin approval you can <a href="/login.html?UserType=DOCTOR">login</a> to <b>https://vaccs.io</b>', 'success');
                ScrollToTop();
                $("#btnCinicNext").prop('disabled', false);
                $("#btnCinicNext").button('reset');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function doctorNext() {
    var res = validateDoctor();
    if (res == false) {
        return false;
    }
    initMap();
    next();
}
function clinicNext() {
    var res = validateClinic();
    if (res == false) {
        return false;
    }
    var doctorType = $("input[name='DoctorType']:checked").val();
    if (doctorType == "D") {
        $("#step3").hide(); 
        simpleDoctorSignup();
    } else {
        next();
    }
   
}
function next() {
    var $active = $('.wizard .nav-tabs li.active');
    $active.next().removeClass('disabled');
    nextTab($active);
    ScrollToTop();
}
//custom schedule end
function validateDoctor() {
    $('#personalInfoForm').validator('validate');
    var validator = $('#personalInfoForm').data("bs.validator");
    if (!validator.hasErrors()) {
        return true;
    } else {
        return false;
    }
}
function validateClinic() {
    $('#clinicForm').validator('validate');
    var validator = $('#clinicForm').data("bs.validator");
    if (validator.hasErrors()) {
        return false;
    }
    else {
        return true;
    }

}
function validateSchedule() {
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

