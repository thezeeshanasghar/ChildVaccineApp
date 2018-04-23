//Load Data in Table when documents is ready  
$(document).ready(function () {
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
                if (result.ResponseData[0])
                    $("#childName").text(result.ResponseData[0].Child.Name);

                var html = '';
                var dateVsArrayOfScheuleMap = {};

                $.each(result.ResponseData, function (key, item) {
                    var vaccineSchedule = {
                        doseName: item.Dose.Name,
                        scheduleID: item.ID,
                        isDone: item.IsDone,
                        Due2EPI: item.Due2EPI,
                        GivenDate: item.GivenDate,
                        BrandName: item.Brand ? item.Brand.Name : ''
                    };
                    if (item.Date in dateVsArrayOfScheuleMap) {
                        dateVsArrayOfScheuleMap[item.Date].push(vaccineSchedule);
                    } else {
                        dateVsArrayOfScheuleMap[item.Date] = [];
                        dateVsArrayOfScheuleMap[item.Date].push(vaccineSchedule);
                    }
                });

                for (var date in dateVsArrayOfScheuleMap) {
                    var schedulesOnSameDate = dateVsArrayOfScheuleMap[date];
                    var isAllDone = false;
                    var isDoneSchedulesLength = schedulesOnSameDate.length;
                    var i = 0;
                    for (var index in schedulesOnSameDate) {
                        if (schedulesOnSameDate[index].isDone)
                            i++;
                        if (i == isDoneSchedulesLength)
                            isAllDone = true;
                    }
                    html += '<div class="col-md-12 text-center" style="margin-top: 10px;">';
                    html += '     ' + date;
                    if (isAllDone) {
                        html += '     &nbsp;<a href="#" onclick="return openVaccineDetails(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')"> <img src="../img/injectionFilled.png" style="height: 22px;"></a>';

                    } else {
                        html += '     &nbsp;<a href="#" onclick="return openVaccineDetails(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')"> <img src="../img/injectionEmpty.png" style="height: 22px;"></a>';
                        html += '     <span class="glyphicon glyphicon-calendar scheduleDate_' + date + '" onclick="return openBulkCalender(' + dateVsArrayOfScheuleMap[date][0].scheduleID + ', \'' + date + '\')"></span>';
                    }

                    html += '</div>';

                    html += '<div class="well-sm col-md-12" style="background-color:rgb(240, 240, 240);">';

                    var doseArray = dateVsArrayOfScheuleMap[date];
                    for (var index in doseArray) {

                        html += '   <h5 style="margin-top:5px; margin-bottom:5px">';
                        html += '       <span class="pull-right" style="">';

                        if (!doseArray[index].isDone)
                            html += '       <span class="glyphicon glyphicon-calendar scheduleDate_' + +doseArray[index].scheduleID + '"  onclick=" return openCalender(' + doseArray[index].scheduleID + ', \'' + date + '\' )"></span>'
                        else
                            html += '       <span style="font-size:12px">' + doseArray[index].BrandName + ' &nbsp;&nbsp;' + doseArray[index].GivenDate + '</span>';

                        if (doseArray[index].Due2EPI)
                            html += '<small>EPI</small>';
                        html += '       <a href="#" onclick="return getbyID(' + doseArray[index].scheduleID + ')">';

                        if (doseArray[index].isDone)
                            html += '       <img src="../img/injectionFilled.png" style="height: 18px;" /></a>'
                        else
                            html += '       <img src="../img/injectionEmpty.png" style="height: 18px;" /></a>'

                        html += '       </span> ';
                        html += doseArray[index].doseName;
                        html += '   </h5>'
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
var click = 0;
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
                click++;
                if (result.ResponseData.Weight > 0) {
                    if (click <= 1)
                        $("#Weight").before('<label id="dWeight">Weight</label>');
                    $("#Weight").val(result.ResponseData.Weight);
                } else {
                    $("#Weight").val("");
                    $("#dWeight").remove();
                    click = 0;
                }

                if (result.ResponseData.Height > 0) {
                    if (click <= 1)
                        $("#Height").before('<label id="dHeight">Height</label>');
                    $("#Height").val(result.ResponseData.Height);
                } else {
                    $("#Height").val("");
                    $("#dHeight").remove();
                    click = 0;
                }

                if (result.ResponseData.Circle > 0) {
                    if (click <= 1)
                        $("#Circumference").before('<label id="dCircumference">OFC</label>');
                    $("#Circumference").val(result.ResponseData.Circle);
                } else {
                    $("#Circumference").val("");
                    $("#dCircumference").remove();
                    click = 0;
                }

                if (result.ResponseData.GivenDate && result.ResponseData.GivenDate != "01-01-0001") {
                    $("#GivenDate").val(result.ResponseData.GivenDate);


                    $("#BulkGivenDate").val(result.ResponseData.GivenDate);
                } else {
                    var fullDate = new Date();
                    //$("#GivenDate").val(('0' + fullDate.getDate()).slice(-2) + '-' + ('0' + (fullDate.getMonth() + 1)).slice(-2) + '-' + fullDate.getFullYear());
                    $("#GivenDate").val(result.ResponseData.Date);
                    $("#BulkGivenDate").val(result.ResponseData.Date);
                }

                //if (result.ResponseData.IsDone) {
                //    $("#Weight").prop('readonly', true);
                //    $("#Height").prop('readonly', true);
                //    $("#Circumference").prop('readonly', true);
                //    $("#Brand").attr("disabled", "disabled");
                //    //  $("#GivenDate").prop("disabled", true);
                //    //$('#btnUpdate').hide();
                //}
                //else {
                //    $("#Weight").prop('readonly', false);
                //    $("#Height").prop('readonly', false);
                //    $("#Circumference").prop('readonly', false);
                //    $("#Brand").removeAttr("disabled");
                //    // $("#GivenDate").prop("disabled", false);


                //}
                //show vaccine brands
                var selectedAttribute = ' selected = "selected"';
                html = '<select id="Brand" onchange="checkBrandInventory(this,' + result.ResponseData.Dose.VaccineID + ')" class="form-control" name="Brand" required>';
                html += '<option value="" >-- Select Brand --</option>';
                $.each(result.ResponseData.Brands, function (key, item) {

                    html += '<option value=' + item.ID;
                    if (result.ResponseData.IsDone)
                        html += (result.ResponseData.BrandId == item.ID) ? selectedAttribute : '';
                    else
                        html += (item.ID == localStorage.getItem("Child_" + result.ResponseData.ChildId + "_LastSelectedBrandOfVaccine_" + result.ResponseData.Dose.VaccineID)) ? selectedAttribute : '';
                    html += '>' + item.Name + '</option>';
                });
                html += '</select>';
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

//on brand change
function checkBrandInventory(brand, vaccineId) {
    if (brand.value!="") {
        $.ajax({
            url: SERVER + 'doctor/' + DoctorId(),
            type: 'Get',
            contentType: 'application/json;charset=UTF-8',
            dataType: 'json',
            success: function (result) {
                if (!result.IsSuccess) {
                    ShowAlert('Danger', result.Message, 'danger');
                }
                else {
                    if (result.ResponseData.AllowInventory) {
                        brandId = brand.value;
                        var obj = {
                            BrandID: brandId,
                            DoctorID: DoctorId()
                        }
                        var html = '';
                        $.ajax({
                            url: SERVER + 'schedule/brandinventory-stock',
                            type: 'POST',
                            data: JSON.stringify(obj),
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

                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);

            }
        });
    }


    saveSelectedBrandInLocalStorage(vaccineId);
}

function Update() {

    var res = validate();
    if (res == false)
        return false;

    var obj = {
        ID: $("#ID").val(),
        Weight: $("#Weight").val(),
        Height: $("#Height").val(),
        Circle: $("#Circumference").val(),
        BrandId: $("#Brand").val(),
        DoctorID: DoctorId(),
        IsDone: "true",
        GivenDate: $("#GivenDate").val()
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
                //ScrollToTop();
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
                     //ScrollToTop();
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
                     //ScrollToTop();
                 }
             },
             error: function (errormessage) {
                 alert(errormessage.responseText);
             }
         });
     });
}

function openVaccineDetails(ID, date) {

    var obj = {
        ChildId: parseInt(getParameterByName("id")),
        Date: date
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
                var selectedAttribute = ' selected = "selected"';
                var bulkScheduleLength = result.ResponseData.length;
                var i = 0;
                var isAllDone = false;
                $.each(result.ResponseData, function (key, schedule) {

                    html += '<input type="hidden" value="' + schedule.ID + '" id="ScheduleId_' + (key + 1) + '"  />'
                    //show vaccine brands
                    html += '<select id="BrandId_' + (key + 1) + '" onchange="checkBrandInventory(this,' + schedule.Dose.VaccineID + ')";" class="form-control" name="Brand" >';
                    html += '<option value="">-- Select ' + schedule.Dose.Name + ' Brand --</option>';
                    $.each(schedule.Brands, function (key, brand) {
                        html += '<option value=' + brand.ID;
                        html += (brand.ID == localStorage.getItem("Child_" + schedule.ChildId + "_LastSelectedBrandOfVaccine_" + schedule.Dose.VaccineID)) ? selectedAttribute : '';
                        html += '>' + brand.Name + '</option>';

                    });
                    html += '</select>';
                    html += "<br>";
                    if (schedule.IsDone)
                        i++;
                    if (i == bulkScheduleLength)
                        isAllDone = true;
                });

                if (isAllDone) {
                    $("#BulkHeight").prop("readonly", true);
                    $("#BulkWeight").prop("readonly", true);
                    $("#BulkCircumference").prop("readonly", true);
                } else {
                    $("#BulkHeight").prop("readonly", false);
                    $("#BulkWeight").prop("readonly", false);
                    $("#BulkCircumference").prop("readonly", false);
                }
                $("#ddBrand_bulk").html(html);
                $("#BulkGivenDate").val(result.ResponseData[0].Date);
                $("#btnbulkInjection").show();
                $('#bulkModel').modal('show');
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
        Weight: $("#BulkWeight").val(),
        Height: $("#BulkHeight").val(),
        Circle: $("#BulkCircumference").val(),
        IsDone: "true",
        ScheduleBrands: scheduleBrands,
        DoctorID: DoctorId(),
        GivenDate: $("#BulkGivenDate").val()

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
                $('#bulkModel').modal('hide');
                $("#ID").val("");
                $('#date').val("");
                $("#BulkWeight").val("");
                $("#BulkHeight").val("");
                $("#BulkCircumference").val("");

                var id = parseInt(getParameterByName("id")) || 0;
                loadData(id);

                ShowAlert('Success', result.Message, 'success');
                //ScrollToTop();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function saveSelectedBrandInLocalStorage(vaccineId) {
    var childId = parseInt(getParameterByName("id")) || 0;
    localStorage.setItem('Child_' + childId + '_LastSelectedBrandOfVaccine_' + vaccineId, $("#Brand").val());
    for (i = 1; i <= 10; i++) {
        if ($("#ScheduleId_" + i).val() && $("#BrandId_" + i).val()) {
            localStorage.setItem('Child_' + childId + '_LastSelectedBrandOfVaccine_' + vaccineId, $("#BrandId_" + i).val());
        }
    }

}

function validate() {
    $('#UpdateSecheduleForm').validator('validate');
    var validator = $('#UpdateSecheduleForm').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}
