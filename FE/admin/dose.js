﻿//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "vaccine/" + id + "/dosses",
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
                    html += '<td>' + getUserAge(item.MinGap) + '</td>';
                    html += '<td>' + item.DoseOrder + '</td>';
                    html += '<td>' +
                        '<a href="#" id="btnEditDose" onclick="return getbyID(' + item.ID + ')"> <span class="glyphicon glyphicon-pencil"></span></a> | ' +
                        '<a href="#" id="btnDeleteDose" onclick="Delele(' + item.ID + ')"><span class="glyphicon glyphicon-trash"></span></a></td>';
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

function SuggestDoseName() {
    $.ajax({
        url: SERVER + "vaccine/" + parseInt(getParameterByName("id")),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                var DoseName = result.ResponseData.Name + " # ";
                $("#Name").val(DoseName);

                DisableMinMaxAgeDropdown(result.ResponseData);
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
    var obj = {
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val(),
        MinGap: $('#MinGap').val(),
        DoseOrder: $('#DoseOrder').val(),
        VaccineID: parseInt(getParameterByName("id")) || 0
    };
    $.ajax({
        url: SERVER + "dose",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');

            }
            else {

                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);

                $('#myModal').modal('hide');
            }
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
        url: SERVER + "dose/" + ID,
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
                $('#MinGap').val(result.ResponseData.MinGap);
                $('#DoseOrder').val(result.ResponseData.DoseOrder);

                $('#myModal').modal('show');
                $('#btnUpdate').show();
                $('#btnAdd').hide();

                // get call vaccine to diasable Min Max age dropdown on edit click as well.
                $.ajax({
                    url: SERVER + "vaccine/" + parseInt(getParameterByName("id")),
                    type: "GET",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (!result.IsSuccess) {
                            ShowAlert('Error', result.Message, 'danger');
                        }
                        else {
                            DisableMinMaxAgeDropdown(result.ResponseData);
                        }
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
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
    $("#btnUpdate").button('loading');
    $("#btnUpdate").prop('disabled', true);
    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val(),
        MinGap: $('#MinGap').val(),
        DoseOrder: $('#DoseOrder').val(),
        VaccineID: parseInt(getParameterByName("id")) || 0
    };
    $.ajax({
        url: SERVER + "dose/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);

                $('#myModal').modal('hide');
                $('#ID').val("");
                $('#Name').val("");
                clearTextBox();
            }
            $("#btnUpdate").prop('disabled', false);
            $("#btnUpdate").button('reset');
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
            url: SERVER + "dose/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (!result.IsSuccess) {
                    ShowAlert('Error', result.Message, 'danger');
                }
                else {
                    var id = parseInt(getParameterByName("id")) || 0;
                    loadData(id);
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function DisableMinMaxAgeDropdown(vaccine) {
    // first to look for MinAge is defined or not
    if (vaccine.MinAge) {
        $("#MinAge option").each(function (i) {
            // this check is to skip the very first option like -- select min age --
            if (i != 0) {
                if ($(this).val() < vaccine.MinAge)
                    $(this).prop('disabled', true);
            }
        });



        $("#MaxAge option").each(function (i) {
            // this check is to skip the very first option like -- select max age --
            if (i != 0) {
                if ($(this).val() < vaccine.MinAge)
                    $(this).prop('disabled', true);
            }
        });
    }

    // first to look for MaxAge is defined or not
    if (vaccine.MaxAge) {
        $("#MinAge option").each(function (i) {
            // this check is to skip the very first option like -- select min age --
            if (i != 0) {
                if ($(this).val() > vaccine.MaxAge)
                    $(this).prop('disabled', true);
            }
        });

        $("#MaxAge option").each(function (i) {
            // this check is to skip the very first option like -- select max age --
            if (i != 0) {
                if ($(this).val() > vaccine.MaxAge)
                    $(this).prop('disabled', true);
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
    $('#MinGap').val("");
    $('#DoseOrder').val("");

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