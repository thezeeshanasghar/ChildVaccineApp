﻿$(document).ready(function () {
    var id = DoctorId();
    loadData(id);
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
//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "doctor/" + id + "/clinics",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + (key + 1) + '</td>';
                    html += '<td>' + item.Name + '</td>';
                    html += '<td>' + item.ConsultationFee + '</td>';
                    html += '<td>' + item.OffDays + '</td>';
                    html += '<td>' + item.StartTime + ' - ' + item.EndTime + '</td>';
                    html += '<td>' +
                        '<a href="child.html?id=' + item.ID + '">Kids/Patients</a> | ' +
                        '<a id="btnEditClinic" href="#" onclick="return getbyID(' + item.ID + ')"><span class="glyphicon glyphicon-pencil"></span></a> | ' +
                        '<a id="btnDeleteClinic" href="#" onclick="Delele(' + item.ID + ')"><span class="glyphicon glyphicon-trash"></span></a></td>';
                    html += '</tr>';

                });
                $('.tbody').html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {

    var res = validate();
    if (res == false) {
        return false;
    }
    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);

    var result = [];
    $('input[name="OffDays"]:checked').each(function () {
        result.push(this.value);
        console.log(result);
    });
    var clinicTimings = [];
    var clinicTimingArray = [
               {
                   "Day": "Monday",
                   "StartTime": $("#StartTimeMonday1").val(),
                   "EndTime": $("#EndTimeMonday1").val(),
                   "Session": 1,
                   "IsOpen": $("#IsOpenMonday").is(":checked")
               },
              {
                  "Day": "Monday",
                  "StartTime": $("#StartTimeMonday2").val(),
                  "EndTime": $("#EndTimeMonday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenMonday").is(":checked")
              },
                {
                    "Day": "Tuesday",
                    "StartTime": $("#StartTimeTuesday1").val(),
                    "EndTime": $("#EndTimeTuesday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenTuesday").is(":checked")
                },
              {
                  "Day": "Tuesday",
                  "StartTime": $("#StartTimeTuesday2").val(),
                  "EndTime": $("#EndTimeTuesday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenTuesday").is(":checked")
              },
                {
                    "Day": "Wednesday",
                    "StartTime": $("#StartTimeWednesday1").val(),
                    "EndTime": $("#EndTimeWednesday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenWednesday").is(":checked")
                },
              {
                  "Day": "Wednesday",
                  "StartTime": $("#StartTimeWednesday2").val(),
                  "EndTime": $("#StartTimeWednesday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenWednesday").is(":checked")
              },
                {
                    "Day": "Thursday",
                    "StartTime": $("#StartTimeThursday1").val(),
                    "EndTime": $("#EndTimeThursday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenThursday").is(":checked")
                },
              {
                  "Day": "Thursday",
                  "StartTime": $("#StartTimeThursday2").val(),
                  "EndTime": $("#EndTimeThursday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenThursday").is(":checked")
              },
                {
                    "Day": "Friday",
                    "StartTime": $("#StartTimeFriday1").val(),
                    "EndTime": $("#EndTimeFriday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenFriday").is(":checked")
                },
              {
                  "Day": "Friday",
                  "StartTime": $("#StartTimeFriday2").val(),
                  "EndTime": $("#EndTimeFriday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenFriday").is(":checked")
              },
                {
                    "Day": "Saturday",
                    "StartTime": $("#StartTimeSaturday1").val(),
                    "EndTime": $("#EndTimeSaturday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenSaturday").is(":checked")
                },
              {
                  "Day": "Saturday",
                  "StartTime": $("#StartTimeSaturday2").val(),
                  "EndTime": $("#EndTimeSaturday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenSaturday").is(":checked")
              },
                {
                    "Day": "Sunday",
                    "StartTime": $("#StartTimeSunday1").val(),
                    "EndTime": $("#EndTimeSunday1").val(),
                    "Session": 1,
                    "IsOpen": $("#IsOpenSunday").is(":checked")
                },
              {
                  "Day": "Sunday",
                  "StartTime": $("#StartTimeSunday2").val(),
                  "EndTime": $("#EndTimeSunday2").val(),
                  "Session": 2,
                  "IsOpen": $("#IsOpenSunday").is(":checked")
              },
    ];
    for (var i = 0; i <= clinicTimingArray.length - 1; i++) {
        if (clinicTimingArray[i].IsOpen) {
            clinicTimings.push(clinicTimingArray[i]);
        }
    }
    var obj = {
        Name: $('#Name').val(),
        ConsultationFee: $('#ConsultationFee').val(),
        //StartTime: $('#StartTime').val(),
        //EndTime: $('#EndTime').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        OffDays: result.join(','),
        Lat: myMarker.getPosition().lat(),
        Long: myMarker.getPosition().lng(),
        DoctorID: DoctorId(),
        Address: $('#Address').val(),
        ClinicTimings: clinicTimings

    };
    $.ajax({
        url: SERVER + "clinic",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(DoctorId());
            $('#myModal').modal('hide');
            clearTextBox();
            $("#btnAdd").prop('disabled', false);
            $("#btnAdd").button('reset');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon ID  
function getbyID(ID) {

    $.ajax({
        url: SERVER + "clinic/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            $('#ConsultationFee').val(result.ResponseData.ConsultationFee);
            $('#Address').val(result.ResponseData.Address);

            var OffDays = result.ResponseData.OffDays.split(",");
            if ($.inArray("Monday", OffDays) != -1)
                $("#Monday").prop('checked', true);
            if ($.inArray("Tuesday", OffDays) != -1)
                $("#Tuesday").prop('checked', true);
            if ($.inArray("Wednesday", OffDays) != -1)
                $("#Wednesday").prop('checked', true);
            if ($.inArray("Thursday", OffDays) != -1)
                $("#Thursday").prop('checked', true);
            if ($.inArray("Friday", OffDays) != -1)
                $("#Friday").prop('checked', true);
            if ($.inArray("Saturday", OffDays) != -1)
                $("#Saturday").prop('checked', true);
            if ($.inArray("Sunday", OffDays) != -1)
                $("#Sunday").prop('checked', true);

            if (result.ResponseData.OffDays != null && result.ResponseData.OffDays.length > 0)
                $("input[type=checkbox][value=" + result.ResponseData.OffDays.split(",")[0] + "]").prop('checked', true);

            $('#StartTime').val(result.ResponseData.StartTime);
            $('#EndTime').val(result.ResponseData.EndTime);

            $('#PhoneNumber').val(result.ResponseData.PhoneNumber);

            g_lat = result.ResponseData.Lat;
            g_lng = result.ResponseData.Long;
            MapOnEdit();

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    $("#btnUpdate").button('loading');
    $("#btnUpdate").prop('disabled', true);
    var result = [];
    $('input[name="OffDays"]:checked').each(function () {
        result.push(this.value);
    });

    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        ConsultationFee: $('#ConsultationFee').val(),
        OffDays: result.join(','),
        StartTime: $('#StartTime').val(),
        EndTime: $('#EndTime').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        Lat: myMarker.getPosition().lat(),
        Long: myMarker.getPosition().lng(),
        IsOnline: GetOnlineClinicIdFromLocalStorage() == $('#ID').val(),
        DoctorID: DoctorId(),
        Address: $('#Address').val()
    };
    $.ajax({
        url: SERVER + "clinic/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData(DoctorId());
            $("#btnUpdate").button('reset');
            $("#btnUpdate").prop('disabled', false);

            $('#myModal').modal('hide');

            $('#ID').val("");
            $('#Name').val("");
            $('#ConsultationFee').val("");
            $("input:checkbox").prop("checked", false);
            $('#StartTime').val("");
            $('#EndTime').val("");
            $('#PhoneNumber').val("");
            $('#Address').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//function for deleting record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: SERVER + "clinic/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData(DoctorId());
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('#Name').val("");
    $('#ConsultationFee').val("");
    $("input:checkbox").prop("checked", false);
    $('#StartTime').val("");
    $('#EndTime').val("");
    $('#PhoneNumber').val("");
    $('#btnUpdate').hide();
    $('#Address').val("");
    $('#btnAdd').show();
    initMap();
}


//Valdidation using jquery  
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}