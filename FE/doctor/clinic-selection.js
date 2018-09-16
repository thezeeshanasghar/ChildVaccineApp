$(document).ready(function () {
    var id = DoctorId();
    loadData(id);
});

function DoctorId() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0)
        return id;
    else {
        var id = localStorage.getItem('DoctorID');
        if (id)
            return id;
        else 0;
    }
}
//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "doctor/" + id + "/clinics",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) 
                ShowAlert('Error', result.Message, 'danger');
            else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<div class="well well-sm" onclick="SelectedClinic(' + item.ID + ')" style="cursor:pointer;font-size:22px">'+
                    '\n<i class="fa fa-building" aria-hidden="true"></i> ' + item.Name +
                    '<span class="badge pull-right" style="font-size:15px">' + item.childrenCount + '</span></li></div>';
                });
                $('.wells').html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function SelectedClinic(Id) {
    var obj = {
        IsOnline: 'true',
        ID: Id,
        DoctorID: DoctorId()
    }
    $.ajax({
        url: SERVER + "clinic/editClinic/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            localStorage.setItem('OnlineClinic', Id);
            localStorage.setItem('OnlineClinicName', result.ResponseData.Name);
            window.location = 'alert.html';
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
