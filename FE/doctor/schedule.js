//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
    $('#print').attr('href', SERVER + 'child/' + id + '/download-pdf');

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

                        if (!doseArray[index].isDone)
                            html += '       <span class="glyphicon glyphicon-calendar scheduleDate_' + +doseArray[index].scheduleID + '"  onclick=" return openCalender(' + doseArray[index].scheduleID + ', \'' + date + '\' )"></span>'
                        if (localStorage.getItem("UserType") == "DOCTOR") {
                            html += '       <a href="#" onclick="return getbyID(' + doseArray[index].scheduleID + ')">';
                            if (doseArray[index].isDone)
                                html += '       <img src="../img/injectionFilled.png" style="height: 30px;" /></a>'
                            else
                                html += '       <img src="../img/injectionEmpty.png" style="height: 30px;" /></a>'
                        }
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
function getbyID(ID) {
    $("#ID").val(ID);
    var html = '';
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
                
                $("#Weight").val(result.ResponseData.Weight);
                $("#Height").val(result.ResponseData.Height);
                $("#Circumference").val(result.ResponseData.Circle);

                if (result.ResponseData.IsDone) {
                    $("#Weight").prop('readonly', true);
                    $("#Height").prop('readonly', true);
                    $("#Circumference").prop('readonly', true);
                }
                else {
                    $("#Weight").prop('readonly', false);
                    $("#Height").prop('readonly', false);
                    $("#Circumference").prop('readonly', false);
                    }
                //show vaccine brands
                var selectedAttribute = ' selected = "selected"';
                html = '<select id="Brand" class="form-control" name="Brand" >';
                html += '<option value="">-- Select Brand --</option>';
                $.each(result.ResponseData.Brands, function (key, item) {
                   
                    html += '<option value=' + item.Name;
                    html += (result.ResponseData.Brand) ? selectedAttribute : '';
                    html += '>'+item.Name + '</option>';
                });
                html+='</select>';
                $("#ddBrand").html(html);
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
                ScrollToTop();
                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);
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
         obj.Date = e.date.getDate() + '-' + ('0' + (e.date.getMonth() + 1)).slice(-2) + '-' + e.date.getFullYear();
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
        format: 'dd-mm-yyyy hh:ii',
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
         obj.Date = e.date.getDate() + '-' + ('0' + (e.date.getMonth() + 1)).slice(-2) + '-' + e.date.getFullYear();

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

