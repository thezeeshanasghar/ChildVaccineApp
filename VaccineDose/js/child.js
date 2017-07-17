//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: "/api/child",
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
                    html += '<td>' + item.FatherName + '</td>';
                    html += '<td>' + item.Email + '</td>';
                    html += '<td>' + item.DOB + '</td>';
                    html += '<td>' + item.MobileNumber + '</td>';
                    html += '<td>' + item.Gender + '</td>';
                    html += '<td>' + item.City + '</td>';
                    html += '<td>' +
                      '<a href="schedule.html?id=' + item.ID + '">Schedule</a> | ' +
                      '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                      '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
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

    var result = [];
    $('.PreferredDayOfWeek').each(function () {
        result.push(this.value);
        console.log(result);
    });

     var obj = {
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),
        DOB: $('#DOB').val(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').find(":selected").val(),
        PreferredSchedule: $('#PreferredSchedule').find(":selected").text(),
        MobileNumber: $('#MobileNumber').val(),
        PreferredDayOfWeek: result.join(','),
        Password: $('#Password').val(),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
    };
    $.ajax({
        url: "/api/child",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
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
    $('#Password').css('border-color', 'lightgrey');
    $.ajax({
        url: "/api/child/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.Id);
            $('#Name').val(result.ResponseData.Name);
            $('#FatherName').val(result.ResponseData.FatherName);
            $('#Email').val(result.ResponseData.Email);
            $('#Password').val(result.ResponseData.Password),
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
 
            $("#IsEPIDone").attr("checked", result.ResponseData.IsEPIDone);
            $("#IsVerified").attr("checked", result.ResponseData.IsVerified);

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
    $('.PreferredDayOfWeek').each(function () {
        result.push(this.value);
        console.log(result);
    });

    var obj = {
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
        Password: $('#Password').val(),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").text(),
    };
    $.ajax({
        url: "/api/child/", 
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $("#Name").val("");
            $("#FatherName").val("");
            $("#Email").val("");
            $("#DOB").val("");
            $("#MobileNumber").val("");
            $("input:radio").attr("checked", false);
            $("input:checkbox").attr("checked", false);
            $("#City").val("");   
            $('#Password').val("")

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
            url: "/api/child/" + ID,
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
    $('#Password').val(""),
    //$("input:radio").attr("checked", false);
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
    $("input:checkbox").attr("checked", false);

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
    if ($("input[type='radio'][name='gender']:checked").val()==false){
        $('#Gender').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Gender').css('border-color', 'lightgrey');
    }

    return isValid;
}