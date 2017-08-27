// TODO: Muneeb change calendar to selected date if ValidUpto date exists in db
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');

    // Get approved doctors
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
                    html += '   <td>' + item.MobileNo + '</td>';
                    html += '   <td>' + item.PMDC + '</td>';
                    //TODO:testing for this check
                    if (item.ValidUpto != null) {
                        html += '   <td>' + item.ValidUpto;
                    }
                    else {
                        html += '   <td>';
                    }
                    html += '      &nbsp;&nbsp;<span class="glyphicon glyphicon-calendar validUpto_' + key + '"  onclick=" return openCalender(' + item.ID + ',' + key + ',\''+item.ValidUpto+'\')"></span>';
                    html += '   </td>';
                    html += '</tr>';
                });
                $('.tbody').html(html);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    // Get unapproved doctors
    $.ajax({
        url: SERVER + "doctor/unapproved",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '   <td>' + (key + 1) + '</td>';
                html += '   <td>' + item.FirstName + ' ' + item.LastName + '</td>';
                html += '   <td>' + item.Email + '</td>';
                html += '   <td>' + item.MobileNo + '</td>';
                html += '   <td>' + item.PMDC + '</td>';

                html += '   <td>' +
                    '           <a href="#" onclick="return Approve(' + item.ID + ')">Approve</a> | ' +
                    '           <a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '           <a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbodyUnApproved').html(html);
            HideAlert();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function openCalender(doctorId,index,validUpTO) {

    $(".validUpto_" + index).datepicker({
            format: 'dd-mm-yyyy',
            todayBtn: true,
            autoclose: true,
            todayHighlight: true,
    });
    if (validUpTO!=null)
    $('.validUpto_' + index).datepicker('update', validUpTO);
    $(".validUpto_" + index).datepicker('show');

        var obj = {};
        obj.ID = doctorId;

    $(".validUpto_" + index).datepicker()
         .on('changeDate', function (e) {
             obj.ValidUpto = e.date;
             $.ajax({
                 url: SERVER + "doctor/" + obj.ID + "/validUpto",
                 data: JSON.stringify(obj),
                 type: "PUT",
                 contentType: "application/json;charset=UTF-8",
                 dataType: "json",
                 success: function (result) {
                     if (!result.IsSuccess) {
                         ShowAlert('Error', result.Message, 'danger');
                     }
                     else {
                          loadData();
                         ShowAlert('Success', result.Message, 'success');
                         ScrollToTop();
                     }
                 },
                 error: function (errormessage) {
                     alert(errormessage.responseText);
                 }
             });
         });
    }


// function for approve doctor
function Approve(ID) {
    var ans = confirm("Are you sure you want to approve ?");
    if (ans) {
        $.ajax({
            url: SERVER + "doctor/approve/" + ID,
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                ShowAlert('Approved', 'Doctor\'s approval request succeded', 'info');
                //code before the pause
                setTimeout(function () {
                    //do what you need here
                    loadData();
                }, 2000);

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
