$(document).ready(function () {

    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');

    loadDoctors();
    loadChilds();

    HideAlert();

});


function loadDoctors() {
    $.ajax({
        url: SERVER + "doctor",
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
                    html += '   <td>' + (key + 1) + '</td>';
                    html += '   <td>' + item.FirstName + ' ' + item.LastName + '</td>';
                    html += '   <td>' + item.Email + '</td>';
                    html += '   <td>' + item.MobileNumber + '</td>';
                    html += '   <td>' + item.PMDC + '</td>';
                    html += '   <td>' + item.ValidUpto + '</td>';
                    html += '</tr>';
                });
                $('.tbody-doctor').html(html);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadChilds() {
    $.ajax({
        url: SERVER + "child",
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
                    html += '   <td>' + (key + 1) + '</td>';
                    html += '   <td>' + item.Name + '</td>';
                    html += '   <td>' + item.FatherName + '</td>';
                    html += '   <td>' + item.MobileNumber + '</td>';
                    html += '   <td>' + item.City + '</td>';
                    html += '</tr>';
                });
                $('.tbody-child').html(html);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}