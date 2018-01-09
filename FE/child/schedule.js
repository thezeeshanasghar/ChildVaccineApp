//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);

});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "child/" + id + "/schedule",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                var html = '';
                var dateVsArrayOfScheuleMap = {};

                $.each(result.ResponseData, function (key, item) {
                    var vaccineSchedule = {
                        doseName: item.Dose.Name,
                        scheduleID: item.ID,
                        isDone: item.IsDone
                    };
                    if (item.Date in dateVsArrayOfScheuleMap) {
                        dateVsArrayOfScheuleMap[item.Date].push(vaccineSchedule);
                    } else {
                        dateVsArrayOfScheuleMap[item.Date] = [];
                        dateVsArrayOfScheuleMap[item.Date].push(vaccineSchedule);
                    }
                });

                for (var date in dateVsArrayOfScheuleMap) {
                    html += '   <h3 style="text-align:center">' + date + ' <span class="glyphicon glyphicon-calendar scheduleDate_' + date + '" onclick="return openBulkCalender(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')" style="font-size:smaller"></span></h3>';
                    html += '<div class="well" style="background-color:rgb(240, 240, 240); padding-top:9px;padding-bottom:9px">';

                    var doseArray = dateVsArrayOfScheuleMap[date];
                    for (var index in doseArray) {

                        html += '   <h4>';
                        html += '       <span class="pull-right" style="font-size:20px">';
                        html += '       <span class="glyphicon glyphicon-calendar scheduleDate_' + +doseArray[index].scheduleID + '"  onclick=" return openCalender(' + doseArray[index].scheduleID + ', \'' + date + '\' )"></span>'
                        html += '       </span> ';
                        html += doseArray[index].doseName;
                        html += '   </h4>'
                    }
                    html += '   </div>'
                }
                $('#schedule').html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function openCalender(scheduleId, date) {
    $(".scheduleDate_" + scheduleId).datepicker({
        format: 'dd-mm-yyyy',
        todayBtn: true,
        autoclose: true,
        todayHighlight: true,
    });
    $('.scheduleDate_' + scheduleId).datepicker('update', date);
    $(".scheduleDate_" + scheduleId).datepicker('show');

    var obj = {};
    obj.ID = scheduleId;

    $(".scheduleDate_" + scheduleId).datepicker()
     .on('changeDate', function (e) {
         obj.Date = ('0' + e.date.getDate()).slice(-2) + '-' + ('0' + (e.date.getMonth() + 1)).slice(-2) + '-' + e.date.getFullYear();
         $.ajax({
             url: SERVER + "schedule/update-schedule/",
             data: JSON.stringify(obj),
             type: "PUT",
             contentType: "application/json;charset=UTF-8",
             dataType: "json",
             success: function (result) {
                 if (!result.IsSuccess) {
                     ShowAlert('Error', result.Message, 'danger');
                 }
                 else {
                     var id = parseInt(getParameterByName("id")) || 0;
                     loadData(id);

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
function openBulkCalender(scheduleId, date) {
    $(".scheduleDate_" + date).datepicker({
        format: 'dd-mm-yyyy',
        todayBtn: true,
        autoclose: true,
        todayHighlight: true,
    });
    $('.scheduleDate_' + date).datepicker('update', date);
    $(".scheduleDate_" + date).datepicker('show');

    var obj = {};
    obj.ID = scheduleId;

    $(".scheduleDate_" + date).datepicker()
     .on('changeDate', function (e) {
         obj.Date = ('0' + e.date.getDate()).slice(-2) + '-' + ('0' + (e.date.getMonth() + 1)).slice(-2) + '-' + e.date.getFullYear();
         $.ajax({
             url: SERVER + "schedule/update-bulk-schedule/",
             data: JSON.stringify(obj),
             type: "PUT",
             contentType: "application/json;charset=UTF-8",
             dataType: "json",
             success: function (result) {
                 if (!result.IsSuccess) {
                     ShowAlert('Error', result.Message, 'danger');
                 }
                 else {
                     var id = parseInt(getParameterByName("id")) || 0;
                     loadData(id);

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
