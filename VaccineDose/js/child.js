﻿//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetClinicIdFromUrlOrLocalStorage() != 0) {
        loadData();
        DisableOffDays();
    }
    else {
        loadChildData();
    }
    
});

function GetClinicIdFromUrlOrLocalStorage() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0)
        return id;
    else {
        var id = localStorage.getItem('ClinicID');
        if (id)
            return id;
        else return 0;
    }
}
function GetChildMobileNumberFromLocalStorage() {
    var mobileNumber = localStorage.getItem('MobileNumber');
    return mobileNumber;
}
//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "clinic/" + GetClinicIdFromUrlOrLocalStorage() + "/childs",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<a href="schedule.html?id=' + item.ID + '">';
                    html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';

                    html += '<div class="col-md-1">' +
                        '<img  src="img/child.jpg"  style="width: 80px; height:80px;padding: 10px;" />' +
                        '</div>';
                    html += '<div class="col-md-6" style="padding:10px;">'

                    html += '<p><h3>' + item.Name + ' ' + item.FatherName + '</h3>';
                    html += '<div style="margin:10px;">';
                    html += '<p class="glyphicon glyphicon-calendar">' +
                        '<span style="margin-left: 10px;">' + item.DOB + '</span></p>' +
                        '</br> <p class="glyphicon glyphicon-earphone">' +
                        '<span style="margin-left: 10px;">' + item.MobileNumber + '</span></p>';
                    html += '</div>';
                    html += '<div class="col-md-4">' +
                      '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                      '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></div>';
                    html += '</div></div></a>';

                });
                $("#childrecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function loadChildData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "child/" + GetChildMobileNumberFromLocalStorage(),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<a href="schedule.html?id=' + item.ID + '">';
                    html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';

                    html += '<div class="col-md-1">' +
                        '<img  src="img/child.jpg"  style="width: 80px; height:80px;padding: 10px;" />' +
                        '</div>';
                    html += '<div class="col-md-6" style="padding:10px;">'

                    html += '<p><h3>' + item.Name + ' ' + item.FatherName + '</h3>';
                    html += '<div style="margin:10px;">';
                    html += '<p class="glyphicon glyphicon-calendar">' +
                        '<span style="margin-left: 10px;">' + item.DOB + '</span></p>' +
                        '</br> <p class="glyphicon glyphicon-earphone">' +
                        '<span style="margin-left: 10px;">' + item.MobileNumber + '</span></p>';
                    html += '</div>';
                    html += '<div class="col-md-4">' +
                      '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                      '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></div>';
                    html += '</div></div></a>';

                });
                $("#childrecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function DisableOffDays() {
    $.ajax({
        url: SERVER + 'clinic/'+ GetClinicIdFromUrlOrLocalStorage(),
        type: 'Get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.IsSuccess) {
                var OffDays = result.ResponseData.OffDays;
                if (OffDays.length > 1) {
                    OffDays = OffDays.split(",");
                    if ($.inArray("Monday", OffDays) != -1)
                        $("#Monday").prop('disabled', true);
                    if ($.inArray("Tuesday", OffDays) != -1)
                        $("#Tuesday").prop('disabled', true);
                    if ($.inArray("Wednesday", OffDays) != -1)
                        $("#Wednesday").prop('disabled', true);
                    if ($.inArray("Thursday", OffDays) != -1)
                        $("#Thursday").prop('disabled', true);
                    if ($.inArray("Friday", OffDays) != -1)
                        $("#Friday").prop('disabled', true);
                    if ($.inArray("Saturday", OffDays) != -1)
                        $("#Saturday").prop('disabled', true);
                    if ($.inArray("Sunday", OffDays) != -1)
                        $("#Sunday").prop('disabled', true);
                }
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

    var result = [];
    $('input[name="PreferredDayOfWeek"]:checked').each(function () {
        result.push(this.value);
    });

    var obj = {
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        DOB: $('#DOB').val(),
        MobileNumber: $('#MobileNumber').val(),
        PreferredDayOfWeek: result.join(','),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').find(":selected").val(),
        PreferredSchedule: $('#PreferredSchedule').find(":selected").text(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),

        Password: PasswordGenerator(),
        ClinicID: GetClinicIdFromUrlOrLocalStorage()
    };
    $.ajax({
        url: SERVER + "child",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                ShowAlert('Custom Schedule', 'Custom Schedule is saved successfully.', 'success');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon ID  
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#FatherName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#DOB').css('border-color', 'lightgrey');
    $('#MobileNumber').css('border-color', 'lightgrey');
    $('#Gender').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $.ajax({
        url: SERVER + "child/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            $('#FatherName').val(result.ResponseData.FatherName);
            $('#Email').val(result.ResponseData.Email);
            $('#DOB').val(result.ResponseData.DOB);
            $('#MobileNumber').val(result.ResponseData.MobileNumber);
            $("input[name=gender][value=" + result.ResponseData.Gender + "]").prop('checked', true);
            $('#City').val(result.ResponseData.City);
            $('#PreferredDayOfReminder').val(result.ResponseData.PreferredDayOfReminder);
            $('#PreferredSchedule').val(result.ResponseData.PreferredSchedule);

            var PreferredDayOfWeek = result.ResponseData.PreferredDayOfWeek.split(",");
            if ($.inArray("Monday", PreferredDayOfWeek) != -1)
                $("#Monday").prop('checked', true);
            if ($.inArray("Tuesday", PreferredDayOfWeek) != -1)
                $("#Tuesday").prop('checked', true);
            if ($.inArray("Wednesday", PreferredDayOfWeek) != -1)
                $("#Wednesday").prop('checked', true);
            if ($.inArray("Thursday", PreferredDayOfWeek) != -1)
                $("#Thursday").prop('checked', true);
            if ($.inArray("Friday", PreferredDayOfWeek) != -1)
                $("#Friday").prop('checked', true);
            if ($.inArray("Saturday", PreferredDayOfWeek) != -1)
                $("#Saturday").prop('checked', true);
            if ($.inArray("Sunday", PreferredDayOfWeek) != -1)
                $("#Sunday").prop('checked', true);

            $("#IsEPIDone").prop("checked", result.ResponseData.IsEPIDone);
            $("#IsVerified").prop("checked", result.ResponseData.IsVerified);

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

    var result = [];
    $('input[name="PreferredDayOfWeek"]:checked').each(function () {
        result.push(this.value);
        console.log(result);
    });

    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        DOB: $('#DOB').val(),
        MobileNumber: $('#MobileNumber').val(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),
        PreferredDayOfWeek: result.join(','),
        PreferredSchedule: $('#PreferredSchedule').find(":selected").text(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').find(":selected").val(),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
        ClinicID: GetClinicIdFromUrlOrLocalStorage()
    };
    $.ajax({
        url: SERVER + "child/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                loadData();

                $('#myModal').modal('hide');
                $('#ID').val("");
                $("#Name").val("");
                $("#FatherName").val("");
                $("#Email").val("");
                $("#DOB").val("");
                $("#MobileNumber").val("");

                $("input:checkbox").prop("checked", false);
                $("#City").val("");
            }
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
            url: SERVER + "child/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.IsSuccess)
                    loadData();
                else
                    ShowAlert('Rquest Failed', result.Message, 'error');
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
    $("#Name").val("");
    $("#FatherName").val("");
    $("#Email").val("");
    $("#DOB").val("");
    $("#MobileNumber").val("");
    $("#City").val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#Name').css('border-color', 'lightgrey');
    $('#FatherName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#DOB').css('border-color', 'lightgrey');
    $('#MobileNumber').css('border-color', 'lightgrey');
    $('#Gender').css('border-color', 'lightgrey');
    $('#City').css('border-color', 'lightgrey');
    $("input:checkbox").prop("checked", false);

}

//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($("input[type='radio'][name='gender']:checked").val() == false) {
        $('#Gender').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Gender').css('border-color', 'lightgrey');
    }

    return isValid;
}