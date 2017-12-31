$(document).ready(function () {
    loadData();
});

function loadData() {
    var childId = parseInt(getParameterByName("id")) || 0;
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "doctor/" + childId + "/doctor-clinics",
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
                    if (item.ProfileImage) {
                        html += '<img src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/UserImages/" + item.ProfileImage + '" />';
                    } else {
                        html += '<img src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/img/avatar.png"+'"/>';
                    }
                    html += '</div>';
                    html += '<div class="col-md-6 ">';
                    html += '<p style="color: #48afe9;font-size:17px;">' + item.FirstName + " " + item.LastName + '</p>';
                    $.each(item.Clinics, function (key, clinic) {
                        html += '<span>' + clinic.Name + '</span>';
                        if (item.ChildDTO.ClinicID!=clinic.ID)
                        html += '<a href="#" style="margin-left:21px;"  onclick="openModel(' + childId + "," + clinic.ID + ')">Select</a>';
                        html += '</br>';
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

function openModel(childId, clinicId) {
    $("#ChildId").val(childId);
    $("#ClinicId").val(clinicId);
    $("#changeDoctorModel").modal('show');
}
function ChangeDoctor() {
    var obj = {
        ID: $("#ChildId").val(),
        ClinicID: $("#ClinicId").val()
    }
    var changeDoctor = $("input[name='ChangeDoctor']:checked").val();
    if (changeDoctor == "Yes") {
        $.ajax({
            url: SERVER + "child/change-doctor/",
            type: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (!result.IsSuccess) {
                    ShowAlert('Error', result.Message, 'danger');
                } else {
                    ShowAlert('Success', "Doctor is successfully changed", 'success');
                    $("#Yes").attr("checked",false);
                }
                $("#changeDoctorModel").modal('hide');
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }
    else {
        $("#No").attr("checked",false);
        $("#changeDoctorModel").modal('hide');
    }
}