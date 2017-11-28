//Load Data in Table when documents is ready  
$(document).ready(function () {
    $("#btnbulkInjection").hide();
    $('#btnUpdate').hide();
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
    $('#print').attr('href', SERVER + 'child/' + id + '/Download-Schedule-PDF');

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
                    html += '<div class="col-md-7">'
                    html += '<h3 style="text-align:right">' + date + ' <span class="glyphicon glyphicon-calendar scheduleDate_' + date + '" onclick="return openBulkCalender(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')" style="font-size:smaller"></span></h3>';
                    html += '</div>'
                    html += '<div class="col-md-5">'
                    html += '<span><a href="#" onclick="return openVaccineDetails(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')">';
                    html += ' <img src="../img/injectionEmpty.png" style="height: 30px;margin-top: 15px;">'
                    html += '</a></span>';
                    html += '</div>';

                    html += '<div class="well col-md-12" style="background-color:rgb(240, 240, 240); padding-top:9px;padding-bottom:9px">';

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
                    $("#Brand").attr("disabled", true);
                }
                else {
                    $("#Weight").prop('readonly', false);
                    $("#Height").prop('readonly', false);
                    $("#Circumference").prop('readonly', false);
                    $("#Brand").attr("disabled", false);
                }
                //show vaccine brands
                var selectedAttribute = ' selected = "selected"';
                html = '<select id="Brand" onchange="checkBrandInventory(this);" class="form-control" name="Brand" >';
                html += '<option value="">-- Select Brand --</option>';
                $.each(result.ResponseData.Brands, function (key, item) {

                    html += '<option value=' + item.ID;
                    html += (result.ResponseData.BrandId == item.ID) ? selectedAttribute : '';
                    html += '>' + item.Name + '</option>';
                });
                html += '</select>';
                $("#ddBrand").html(html);
                $('#myModal').modal('show');
                $('#btnUpdate').show();
                $("#btnbulkInjection").hide();

            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//on brand change
function checkBrandInventory(brand) {
    brandId = brand.value;
    var html = '';
    $.ajax({
        url: SERVER + 'schedule/brandinventory-stock/' + brandId,
        type: 'GET',
        contentType: 'application/json;charset=UTF-8',
        dataType: 'json',
        success: function (result) {
            if (!result.IsSuccess) {
                html = '<span><b style="color:red">' + result.Message + '</b></span>';
                $("#ddBrand").append(html);
                $('#btnUpdate').hide();
            }
            else {
                $('#btnUpdate').show();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);

        }
    });
}

function Update() {
    var obj = {
        ID: $("#ID").val(),
        Weight: $("#Weight").val(),
        Height: $("#Height").val(),
        Circle: $("#Circumference").val(),
        BrandId: $("#Brand").val(),
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
        format: 'dd-mm-yyyy hh:ii',
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

function openVaccineDetails(ID, date) {
  
    $("#Weight").prop('readonly', false);
    $("#Height").prop('readonly', false);
    $("#Circumference").prop('readonly', false);
    $("#Brand").attr("disabled", false);
    $("#Weight").val("");
    $("#Height").val("");
    $("#Circumference").val("");
    $("#Brand").hide();
    var obj = {
        ChildId: parseInt(getParameterByName("id")),
        Date:date
    }
    $.ajax({
        url: SERVER + "schedule/bulk-brand/",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#ID").val(ID);
                $('#date').val(date);
                var html = '';
                $.each(result.ResponseData, function (key, schedule) {
                    html += '<input type="hidden" value="' + schedule.ID + '" id="ScheduleId_' + (key + 1) + '"  />'
                    //show vaccine brands
                    html += '<select id="BrandId_' + (key + 1) + '" onchange="checkBrandInventory(this);" class="form-control" name="Brand" >';
                    html += '<option value="">-- Select '+schedule.Dose.Name+ ' Brand --</option>';
                    $.each(schedule.Brands, function (key, brand) {
                        html += '<option value=' + brand.ID;
                        html += '>' + brand.Name + '</option>';

                    });
                    html += '</select>';
                    html += "<br>";
                });
                $("#ddBrand").html(html);
                $('#myModal').modal('show');
                $("#btnbulkInjection").show();
                $('#btnUpdate').hide();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}
function UpdateBulkInjection() {
       
    var scheduleBrands = [];
    //for time being I'm using loop upto 10 dropdown values
    for (i = 1; i <= 10; i++) {
        if ($("#ScheduleId_" + i).val() && $("#BrandId_" + i).val()) {
            scheduleBrands.push({ ScheduleId: $("#ScheduleId_" + i).val(), BrandId: $("#BrandId_" + i).val() });
        }
    }
       
    var obj = {
        ID: $("#ID").val(),
        Date: $('#date').val(),
        Weight: $("#Weight").val(),
        Height: $("#Height").val(),
        Circle: $("#Circumference").val(),
        IsDone: "true",
        ScheduleBrands: scheduleBrands
    }

    $.ajax({
        url: SERVER + "schedule/update-bulk-injection/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $('#myModal').modal('hide');
                $("#ID").val("");
                $('#date').val("");
                $("#Weight").val("");
                $("#Height").val("");
                $("#Circumference").val("");

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
}

