$(document).ready(function () {
    // var id = parseInt(getParameterByName("id")) || 0;
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
        url: "/api/doctor/" + id + "/clinics",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.OffDays + '</td>';
                html += '<td>' + item.StartTime + ' - ' + item.EndTime + '</td>';
                html += '<td>' +
                    '<a href="#" onclick="SelectedClinic(' + item.ID + ')">Select</a>';
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
function SelectedClinic(Id) {
    var obj = {
        IsOnline: 'true',
        ID: Id,
        DoctorID:DoctorId()
     }
    $.ajax({
        url: "/api/clinic/" + Id,
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
       
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
