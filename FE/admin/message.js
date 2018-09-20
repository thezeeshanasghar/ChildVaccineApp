//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "message/?mobileNumber=" + $.trim($("#MobileNumber").val()) + "&fromDate="
            + $("#FromDate").val() + "&toDate=" + $("#ToDate").val(),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
                $('.tbody').html('');
                $('#TotalSMS').html('')
            }
            else {
                var html = '';
                var smsCountHtml = '';
                smsCountHtml += 'Total SMS: ' + result.ResponseData.length;
                $('#TotalSMS').html(smsCountHtml)
                $.each(result.ResponseData, function (key, item) {
                    html += '<tr>';
                    html += '<td style="width:6%;">' + (key + 1) + '</td>';
                    html += '<td style="width:15%;">' + (item.User != null ? item.User.MobileNumber : 'N/A') + '</td>';
                    html += '<td style="width:15%;">' + item.MobileNumber + '</td>';
                    html += '<td>' + item.SMS + '</td>';
                    html += '<td style="width:15%;">' + item.Created + '</td>';
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

function sendSMS() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var obj = {
        MobileNumber: $("#ReceiverMobileNumber").val(),
        SMS: $("#Message").val()
    };

    $.ajax({
        url: SERVER + "message",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
                ScrollToTop();
            }
            else {
                loadData();
                setTimeout(function () {
                    ShowAlert('Success', "Your SMS has been sent successfully", 'success');
                }, 1000);
                
            }
        },
        error: function (errormessage, e) {
            displayErrors(errormessage, e);
        }
    });
}

function reset() {
    $("#MobileNumber").val("");
    $("#FromDate").val("");
    $("#ToDate").val("");
    loadData();
}
  
//Valdidation using jquery  
function validate() {
    $('#sendSMSForm').validator('validate');
    var validator = $('#sendSMSForm').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}