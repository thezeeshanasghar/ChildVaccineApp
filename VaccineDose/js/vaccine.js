//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: "/api/vaccine",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.MinAge + '</td>';
                html += '<td>' + item.MaxAge + '</td>';
                html += '<td>' +
                    '<a href="dose.html?id=' + item.ID + '">Doses</a> | ' +
                    '<a href="dose-rules.html?id=' + item.ID + '">Dose Rules</a> | ' +
                    '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
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
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val()
    };
    $.ajax({
        url: "/api/vaccine",
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
    $('#MinAge').css('border-color', 'lightgrey');
    $('#MaxAge').css('border-color', 'lightgrey');
    
    $.ajax({
        url: "/api/vaccine/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            $('#MinAge').val(result.ResponseData.MinAge);
            $('#MaxAge').val(result.ResponseData.MaxAge);
            
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
        Name: $('#Name').val(),
        MinAge: $('#MinAge').val(),
        MaxAge: $('#MaxAge').val()
    };
    $.ajax({
        url: "/api/vaccine/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Name').val("");
            $('#MinAge').val("");
            $('#MaxAge').val("");
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
            url: "/api/vaccine/" + ID,
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
    if ($('#MaxAge').val().trim() == "") {
        $('#MaxAge').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MaxAge').css('border-color', 'lightgrey');
    }
   
    return isValid;
}