//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');

    // Get approved doctors
    $.ajax({
        url: SERVER + "doctor",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + item.FirstName + ' ' + item.LastName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.MobileNo + '</td>';
                html += '<td>' + item.PMDC + '</td>';
                
                html += '<td>' +
                    '<a href="clinic.html?id=' + item.ID + '">Clinics</a> | ' +
                    '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    // Get unapproved doctors
    $.ajax({
        url: SERVER + "doctor/unapproved",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';

                html += '<td>' + item.FirstName + ' ' + item.LastName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.MobileNo + '</td>';
                html += '<td>' + item.PMDC + '</td>';
                
                html += '<td>' +
                    '<a href="#" onclick="return Approve(' + item.ID + ')">Approve</a> | ' +
                    '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbodyUnApproved').html(html);
            HideAlert();
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
    var obj = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Password: PasswordGenerator(),
        MobileNo: $('#MobileNo').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
    };
    $.ajax({
        url: SERVER + "doctor",
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
    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
     $('#MobileNo').css('border-color', 'lightgrey');
    $('#PMDC').css('border-color', 'lightgrey');
    
    $.ajax({
        url: SERVER + "doctor/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#FirstName').val(result.ResponseData.FirstName);
            $('#LastName').val(result.ResponseData.LastName);
            $('#Email').val(result.ResponseData.Email);
             $('#MobileNo').val(result.ResponseData.MobileNo);
            $('#PhoneNo').val(result.ResponseData.PhoneNo);
            $('#PMDC').val(result.ResponseData.PMDC);
            $("#ShowPhone").prop("checked", result.ResponseData.ShowPhone);
            $("#ShowMobile").prop("checked", result.ResponseData.ShowMobile);
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
    var obj = {
        ID: $('#ID').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        MobileNo: $('#MobileNo').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
    };
    $.ajax({
        url: SERVER + "doctor/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');

            clearTextBox();
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
            url: SERVER + "doctor/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

// function for approve doctor
function Approve(ID) {
    var ans = confirm("Are you sure you want to approve ?");
    if (ans) {
        $.ajax({
            url: SERVER + "doctor/approve/" + ID,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                ShowAlert('Approved', 'Doctor\'s approval request succeded', 'info');
                //code before the pause
                setTimeout(function () {
                    //do what you need here
                    loadData();
                }, 2000);
                
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

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#MobileNo').css('border-color', 'lightgrey');
    $('#PMDC').css('border-color', 'lightgrey');
}


//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }

    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }

    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
 
    if ($('#MobileNo').val().trim() == "") {
        $('#MobileNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MobileNo').css('border-color', 'lightgrey');
    }

    if ($('#PMDC').val().trim() == "") {
        $('#PMDC').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PMDC').css('border-color', 'lightgrey');
    }

    return isValid;
}