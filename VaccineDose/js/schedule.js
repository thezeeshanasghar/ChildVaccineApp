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
                var dateVsArrayOfVaccineScheuleMap = {};

                $.each(result.ResponseData, function (key, item) {
                    var vaccineSchedule = {
                        doseName: item.Dose.Name,
                        scheduleID: item.ID,
                        isDone: item.IsDone
                    };
                    if (item.Date in dateVsArrayOfVaccineScheuleMap) {
                        dateVsArrayOfVaccineScheuleMap[item.Date].push(vaccineSchedule);
                    } else {
                        dateVsArrayOfVaccineScheuleMap[item.Date] = [];
                        dateVsArrayOfVaccineScheuleMap[item.Date].push(vaccineSchedule);
                    }
                });

                for (var key in dateVsArrayOfVaccineScheuleMap) {
                    html += "<h3 style='text-align:center'>" + key + "</h3>";
                    var arr = dateVsArrayOfVaccineScheuleMap[key];
                    for (var index in arr) {

                        html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';
                        html += '<div class="col-md-1">' +
                                                '</div>';
                        html += '<div class="col-md-6" style="padding:10px;">';
                        html += '<h3>' + arr[index].doseName + '</h3></div>';
                        html += '<div class="col-md-4" style="padding:10px;">';
                        //html += '<div class="glyphicon glyphicon-calendar" style="height: 40px;"></div>'
                        html += '<a href="#" onclick="return getbyID(' + arr[index].scheduleID + ')">';
                        if (arr[index].isDone)
                            html += '<img src="../img/injectionFilled.png" style="height: 40px;" /></a>'
                        else
                            html += '<img src="../img/injectionEmpty.png" style="height: 40px;" /></a>'

                        html += '</div></div> ';
                        console.log('\t' + arr[index].doseName);
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

