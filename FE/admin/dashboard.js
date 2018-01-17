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
                    html += '   <td>'
                    html += '<label>Allow Invoice</label>&nbsp;<input  type="checkbox"  id="ShowInvoice_' + item.ID + '" ';
                    if(item.ShowInvoice)
                        html +='checked="checked"';
                    html+='  />';
                    html += '<label>Allow Follow up</label>&nbsp;<input   type="checkbox"  id="ShowFollowUp_' + item.ID + '" '
                    if (item.ShowFollowUp)
                        html += 'checked="checked"';
                    html += '  />';
                    html += '<label>Allow chart</label>&nbsp;<input  type="checkbox" id="ShowChart_' + item.ID + '" '
                    if (item.ShowChart)
                        html += 'checked="checked"';
                    html += '  />';
                    html += '<label>Allow inventory</label>&nbsp;<input  type="checkbox" id="CheckInventory_' + item.ID + '" '
                    if (item.CheckInventory)
                        html += 'checked="checked"';
                    html += '  />';
                    html += '<button class="btn btn-primary" onclick="UpdateDoctor(' + item.ID + ')"  >Done</button>'
                 
                    '</td>';
                  
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

function UpdateDoctor(DoctorId) {
    var obj = {
        ShowInvoice: $("#ShowInvoice_" + DoctorId).is(":checked"),
        ShowFollowUp: $("#ShowFollowUp_" + DoctorId).is(":checked"),
        ShowChart: $("#ShowChart_" + DoctorId).is(":checked"),
        CheckInventory: $("#CheckInventory_" + DoctorId).is(":checked"),
        ID: DoctorId
    }

    $.ajax({
        url: SERVER + "doctor/" + DoctorId + "/update-permission",
        type: "PUT",
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                loadDoctors();
                ScrollToTop();
                ShowAlert('Success', result.Message, 'success');

            }
        },
        error: function (error) {
            ShowAlert('Error', errormessage.responseText, 'danger');
        }

    });
}