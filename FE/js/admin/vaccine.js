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
        case 516:
            day = '17 Months';
            break;
        case 546:
            day = '18 Months';
            break;
        case 576:
            day = '19 Months';
            break;
        case 606:
            day = '20 Months';
            break;
        case 636:
            day = '21 Months';
            break;
        case 666:
            day = '22 Months';
            break;
        case 696:
            day = '23 Months';
            break;
        case 726:
            day = '24 Months';
            break;
        case 756:
            day = '25 Months';
            break;
        case 786:
            day = '26 Months';
            break;
        case 816:
            day = '27 Months';
            break;
        case 846:
            day = '28 Months';
            break;
        case 876:
            day = '29 Months';
            break;
        case 906:
            day = '30 Months';
            break;
        case 936:
            day = '31 Months';
            break;
        case 966:
            day = '32 Months';
            break;
        case 996:
            day = '33 Months';
            break;
        case 1026:
            day = '34 Months';
            break;
        case 1056:
            day = '35 Months';
            break;
        case 1086:
            day = '36 Months';
            break;
        case 1116:
            day = '37 Months';
            break;
        case 1146:
            day = '38 Months';
            break;
        case 1176:
            day = '39 Months';
            break;
        case 1206:
            day = '40 Months';
            break;
        case 1236:
            day = '41 Months';
            break;
        case 1266:
            day = '42 Months';
            break;
        case 1296:
            day = '43 Months';
            break;
        case 1326:
            day = '44 Months';
            break;
        case 1356:
            day = '45 Months';
            break;
        case 1386:
            day = '46 Months';
            break;
        case 1416:
            day = '47 Months';
            break;
        case 1446:
            day = '48 Months';
            break;
        case 1476:
            day = '49 Months';
            break;
        case 1506:
            day = '50 Months';
            break;
        case 1536:
            day = '51 Months';
            break;
        case 1566:
            day = '52 Months';
            break;
        case 1596:
            day = '53 Months';
            break;
        case 1626:
            day = '54 Months';
            break;
        case 1656:
            day = '55 Months';
            break;
        case 1686:
            day = '56 Months';
            break;
        case 1716:
            day = '57 Months';
            break;
        case 1746:
            day = '58 Months';
            break;
        case 1776:
            day = '59 Months';
            break;
        case 1806:
            day = '60 Months';
            break;

    }
    return day;
}