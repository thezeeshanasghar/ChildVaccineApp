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
                    html += "<h3 style='text-align:center'>" + date + "</h3>";
                    var doseArray = dateVsArrayOfScheuleMap[date];
                    for (var index in doseArray) {

                        html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';
                        html += '<div class="col-md-1">' +
                                                '</div>';
                        html += '<div class="col-md-6" style="padding:10px;">';
                        html += '<h3>' + doseArray[index].doseName + '</h3></div>';
                        html += '<div class="col-md-4" style="padding:10px;">';
                        html += '<span class="glyphicon glyphicon-calendar scheduleDate_' + +doseArray[index].scheduleID + '"  onclick=" return openCalender(' + doseArray[index].scheduleID + ', \'' + date + '\' )"></span>'
                                  
                        html += '<a href="#" onclick="return getbyID(' + doseArray[index].scheduleID + ')">';
                        if (doseArray[index].isDone)
                            html += '<img src="../img/injectionFilled.png" style="height: 40px;" /></a>'
                        else
                            html += '<img src="../img/injectionEmpty.png" style="height: 40px;" /></a>'

                        html += '</div></div> ';
                        console.log('\t' + doseArray[index].doseName);
                    }

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
function getbyID(ID) {
    $("#ID").val(ID);
    $.ajax({
        url: SERVER + "schedule/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#Weight").val(result.ResponseData.Weight),
                $("#Height").val(result.ResponseData.Height),
                $("#Circumference").val(result.ResponseData.Circle),
                $("#Brand").val(result.ResponseData.Brand)

                $('#myModal').modal('show');
                $('#btnUpdate').show();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var obj = {
        ID: $("#ID").val(),
        Weight: $("#Weight").val(),
        Height: $("#Height").val(),
        Circle: $("#Circumference").val(),
        Brand: $("#Brand").val(),
        IsDone: "true",
    }
    $.ajax({
        url: SERVER + "schedule/child-schedule/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#Weight").val(""),
                $("#Height").val(""),
                $("#Circumference").val(""),
                $("#Brand").val("")
                $('#myModal').modal('hide');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
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
         obj.Date = e.date;
         $.ajax({
             url: SERVER + "schedule/update-schedule/",
             data: JSON.stringify(obj),
             type: "Put",
             contentType: "application/json;charset=UTF-8",
             dataType: "json",
             success: function (result) {
                 if (!result.IsSuccess) {
                     ShowAlert('Error', result.Message, 'danger');
                 }
                 else {
                     ShowAlert('Success', result.Message, 'success');
                     ScrollToTop();
                     
                     
                     var id = parseInt(getParameterByName("id")) || 0;
                     loadData(id);
                 }
             },
             error: function (errormessage) {
                 alert(errormessage.responseText);
             }
         });
        
     });
}