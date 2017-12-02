$(document).ready(function () {
    loadData();
});

function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "doctor/approved",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                     html += '<div class="col-md-12 well" style="background-color:rgb(255, 255, 255); padding-top:9px;padding-bottom:9px;border-width:2px;" >';
                    html += '<div class="col-md-4 map-infowindow" style="width:113px !important">';
                    html += '<img src="' + SERVER_IP + ":" + SERVER_PORT + "/" + item.ProfileImage + '" />';
                    html += '</div>';
                    html += '<div class="col-md-4 ">';
                    html += '<p style="color: #48afe9;font-size:17px;">' + item.FirstName + " " + item.LastName + '</p>';
                    $.each(item.Clinics, function (key, clinic) {
                        html += '<p>' + clinic.Name+'</p>';
                    });
                    html += '</div>';
                    html += '</div>';

                });
                 
                $("#doctorRecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}