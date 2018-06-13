//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "message/" + GetUserIDFromLocalStorage() + "/doctor",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                var html = '';
                var smsCountHtml = '';
                smsCountHtml += 'Total SMS: ' + result.ResponseData.length;
                $('#TotalSMS').html(smsCountHtml)
                $.each(result.ResponseData, function (key, item) {
                    html += '<tr>';
                    html += '<td style="width:6%;">' + (key + 1) + '</td>';
                    html += '<td style="width:15%;">' + item.MobileNumber + '</td>';
                    html += '<td>' + item.SMS + '</td>';
                    html += '<td>' + item.ApiResponse + '</td>';
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