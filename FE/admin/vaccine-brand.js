﻿//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "vaccine/" + id + "/brands",
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
                    html += '<td>' +
                        '<a href="#" id="btnEditBrand" onclick="return getbyID(' + item.ID + ')"> <span class="glyphicon glyphicon-pencil"></span></a> | ' +
                        '<a href="#" id="btnDeleteBrand" onclick="Delele(' + item.ID + ')"><span class="glyphicon glyphicon-trash"></span></a></td>';
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
        Name: $('#BrandName').val(),
        VaccineID: parseInt(getParameterByName("id")) || 0
    };
    $.ajax({
        url: SERVER + "brand",
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
        url: SERVER + "brand/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#ID").val(result.ResponseData.ID);
                $('#BrandName').val(result.ResponseData.Name);
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
        Name: $('#BrandName').val(),
        VaccineID: parseInt(getParameterByName("id")) || 0
    };
    $.ajax({
        url: SERVER + "brand/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#btnUpdate").prop('disabled', false);
            $("#btnUpdate").button('reset');

            if (!result.IsSuccess)
                ShowAlert('Error', result.Message, 'danger');
            else {
                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);
               
                $('#myModal').modal('hide');
                clearTextBox();
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
            url: SERVER + "brand/" + ID,
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

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('#BrandName').val("");
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