//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: "/api/vaccine/" + id + "/dose-rules",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.DoseFrom + '</td>';
                html += '<td>' + item.DoseTo + '</td>';
                html += '<td>' + item.Days + '</td>';
                html += '<td>' +
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
        DoseFrom: $('#DoseFrom').val(),
        DoseTo: $('#DoseTo').val(),
        Days: $('#Days').val(),
        VaccineID: parseInt(getParameterByName("id")) || 0
    };
    $.ajax({
        url: "/api/doserule",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var id = parseInt(getParameterByName("id")) || 0;
            loadData(id);
            $('#myModal').modal('hide');
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
            url: "/api/doserule/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);
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
    $('#DoseFrom').val("");
    $('#DoseTo').val("");
    $('#Days').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#DoseFrom').css('border-color', 'lightgrey');
    $('#DoseTo').css('border-color', 'lightgrey');
    $('#Days').css('border-color', 'lightgrey');
}


//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#DoseFrom').val().trim() == "") {
        $('#DoseFrom').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DoseFrom').css('border-color', 'lightgrey');
    }

    if ($('#DoseTo').val().trim() == "") {
        $('#DoseTo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DoseTo').css('border-color', 'lightgrey');
    }
    if ($('#Days').val().trim() == "") {
        $('#Days').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Days').css('border-color', 'lightgrey');
    }
    return isValid;
}