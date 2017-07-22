//Load Data in Table when documents is ready  
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
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.GapInDays + '</td>';
                html += '<td>' + item.DoseOrder + '</td>';
                html += '<td>' +
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
function DoseName() {
    $.ajax({
        url: SERVER + "vaccine/" + parseInt(getParameterByName("id")),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var DoseName = '';
            $.each(result.ResponseData, function (key, item) {
                DoseName = result.ResponseData.Name + " - Dose ";
            });
            $("#Name").val(function(){
                return this.value = DoseName;
            });
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
        GapInDays: $('#GapInDays').val(),
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
            var id = parseInt(getParameterByName("id")) || 0;
            loadData(id);
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
    $('#GapInDays').css('border-color', 'lightgrey');
    $('#DoseOrder').css('border-color', 'lightgrey');

    $.ajax({
        url: SERVER + "dose/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            $('#GapInDays').val(result.ResponseData.GapInDays);
            $('#DoseOrder').val(result.ResponseData.DoseOrder);

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
        GapInDays: $('#GapInDays').val(),
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
            var id = parseInt(getParameterByName("id")) || 0;
            loadData(id);
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Name').val("");
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
    $('#Name').val("");
    $('#GapInDays').val("");
    $('#DoseOrder').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#Name').css('border-color', 'lightgrey');
    $('#GapInDays').css('border-color', 'lightgrey');
    $('#DoseOrder').css('border-color', 'lightgrey');
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

    if ($('#GapInDays').val().trim() == "") {
        $('#GapInDays').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#GapInDays').css('border-color', 'lightgrey');
    }

    if ($('#DoseOrder').val().trim() == "") {
        $('#DoseOrder').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DoseOrder').css('border-color', 'lightgrey');
    }

    return isValid;
}