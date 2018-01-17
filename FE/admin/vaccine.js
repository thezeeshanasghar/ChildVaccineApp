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
                          '<a id="btnBrand" href="vaccine-brand.html?id=' + item.ID + '">Brands</a> | ' +
                        '<a id="btnDose" href="dose.html?id=' + item.ID + '">Doses</a> | ' +
                        '<a href="#" id="btnEdit" onclick="return getbyID(' + item.ID + ')">  <span class="glyphicon glyphicon-pencil"></span></a> | ' +
                        '<a href="#" id="btnDelete"  onclick="Delele(' + item.ID + ')"> <span class="glyphicon glyphicon-trash"></span></a></td>';
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
    if (res === false) {
        return false;
    }
    $("#btnAdd").button('loading');
    $("#btnAdd").prop('disabled', true);
   
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
                $("#btnAdd").prop('disabled', false);
                $("#btnAdd").button('reset');
                
            }
            else {
                loadData();
                $('#myModal').modal('hide');
            }
        },
        error: function (errormessage) {
            $("#btnAdd").prop('disabled', false);
            $("#btnAdd").button('reset');
          
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
    if (res === false) {
        return false;
    }
    $("#btnUpdate").button('loading');
    $("#btnUpdate").prop('disabled', true);
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
                $("#btnUpdate").prop('disabled', false);
                $("#btnUpdate").button('reset');
            }
            else {
                loadData();
                $("#btnUpdate").prop('disabled', false);
                $("#btnUpdate").button('reset');
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



function updateDoctorSchedule() {
    $.ajax({
        url: SERVER + "doctorschedule/update-schedule",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                ShowAlert('Success', 'New Doses added into all doctor schedules', 'success'); 
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}