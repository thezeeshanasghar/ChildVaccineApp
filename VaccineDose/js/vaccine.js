//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "vaccine",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                var html = '';
                $.each(result.ResponseData, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + (key + 1) + '</td>';
                    html += '<td>' + item.Name + '</td>';
                    html += '<td>' + getUserAge(item.MinAge) + '</td>';
                    html += '<td>' + getUserAge(item.MaxAge) + '</td>';
                    html += '<td>' +
                        '<a href="dose.html?id=' + item.ID + '">Doses</a> | ' +
                        '<a href="#" onclick="return getbyID(' + item.ID + ')">  <span class="glyphicon glyphicon-pencil"></span></a> | ' +
                        '<a href="#" onclick="Delele(' + item.ID + ')"> <span class="glyphicon glyphicon-trash"></span></a></td>';
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
    var obj = {
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val()
    };
    $.ajax({
        url: SERVER + "vaccine",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                loadData();
                $('#myModal').modal('hide');
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
    $('#MinAge').css('border-color', 'lightgrey');
    $('#MaxAge').css('border-color', 'lightgrey');

    $.ajax({
        url: SERVER + "vaccine/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#ID").val(result.ResponseData.ID);
                $('#Name').val(result.ResponseData.Name);
                $('#MinAge').val(result.ResponseData.MinAge);
                $('#MaxAge').val(result.ResponseData.MaxAge);

                $('#myModal').modal('show');
                $('#btnUpdate').show();
                $('#btnAdd').hide();
            }
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
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val()
    };
    $.ajax({
        url: SERVER + "vaccine/" + $('#ID').val(),
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
                $('#Name').val("");
                $('#MinAge').val("");
                $('#MaxAge').val("");
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
            url: SERVER + "vaccine/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (!result.IsSuccess) {
                    ShowAlert('Error', result.Message, 'danger');
                } else {
                    loadData();
                }
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
    $('#MinAge').val("");
    $('#MaxAge').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#MinAge').css('border-color', 'lightgrey');
    $('#MaxAge').css('border-color', 'lightgrey');
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
    if ($('#MinAge').val().trim() == "") {
        $('#MinAge').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MinAge').css('border-color', 'lightgrey');
    }
    //if ($('#MaxAge').val().trim() == "") {
    //    $('#MaxAge').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#MaxAge').css('border-color', 'lightgrey');
    //}

    return isValid;
}

function getUserAge(ageNum) {
    var day = 'At Birth'
    switch (ageNum) {
        case null:
            day = '';
            break;
        case 0:
            day = 'At Birth';
            break;
        case 42:
            day = '6 Weeks';
            break;
        case 49:
            day = '7 Weeks';
            break;
        case 56:
            day = '8 Weeks';
            break;
        case 63:
            day = '9 Weeks';
            break;
        case 70:
            day = '10 Weeks';
            break;
        case 77:
            day = '11 Weeks';
            break;
        case 84:
            day = '12 Weeks';
            break;
        case 91:
            day = '13 Weeks';
            break;
        case 98:
            day = '14 Weeks';
            break;
        case 105:
            day = '15 Weeks';
            break;
        case 112:
            day = '16 Weeks';
            break;
        case 119:
            day = '17 Weeks';
            break;
        case 126:
            day = '18 Weeks';
            break;
        case 133:
            day = '19 Weeks';
            break;
        case 140:
            day = '20 Weeks';
            break;
        case 147:
            day = '21 Weeks';
            break;
        case 154:
            day = '22 Weeks';
            break;
        case 161:
            day = '23 Weeks';
            break;
        case 168:
            day = '24 Weeks';
            break;
        case 212:
            day = '7 Months';
            break;
        case 243:
            day = '8 Months';
            break;
        case 273:
            day = '9 Months';
            break;
        case 304:
            day = '10 Months';
            break;
        case 334:
            day = '11 Months';
            break;
        case 365:
            day = '12 Months';
            break;
        case 395:
            day = '13 Months';
            break;
        case 426:
            day = '14 Months';
            break;
        case 456:
            day = '15 Months';
            break;
        case 486:
            day = '16 Months';
            break;
        case 547:
            day = '1 Year 6 Months';
            break;
        case 608:
            day = '1 Year 8 Months';
            break;
        case 730:
            day = '2 Years';
            break;
        case 760:
            day = '2 Years 1 Month';
            break;
        case 1125:
            day = '3 Year 1 Month';
            break;
    }
    return day;
}