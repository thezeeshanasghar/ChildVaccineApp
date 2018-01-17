﻿$(document).ready(function () {
    loadData(0);
    HideAlert();
    enableAccordion();
});

function loadData(Id) {
    var OnlineClinic = GetOnlineClinicIdFromLocalStorage();
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + 'schedule/alert/' + Id + '/' + OnlineClinic,
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                var html = '';
                if (result.ResponseData.length != 0) {
                    var map = {};
                    $.each(result.ResponseData, function (key, item) {
                        var obj = {
                            Date: item.Date,
                            DoseName: item.Dose.Name,
                            Gender:item.Child.Gender
                        }
                        if (item.Child.Name in map) {
                            map[item.Child.Name].push(obj);
                        } else {
                            map[item.Child.Name] = [];
                            map[item.Child.Name].push(obj);
                        }
                    });

                    for (var key in map) {
                        //
                        var arry = map[key];
                        html += '<div class="row">';
                        html += '<div class="col-md-2">';
                        html += '<img src="/img/'
                        if (arry[0].Gender == "Boy")
                            html += 'male.png"   class="img-responsive pull-right" alt="male" style="max-width:30px;max-height:30px" />';
                        else
                            html += 'female.png"   class="img-responsive pull-right" alt="female" style="max-width:30px;max-height:30px" />';

                        html += ' </div>';
                        html += ' <div class="col-md-8">';
                        html += '<h5 style="border-bottom:solid 1px pink">' + key + '</h5>';
                        html += '  <div class="pull-right">';
                        html += '<i class="fa fa-phone" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;<i class="fa fa-envelope" aria-hidden="true"></i>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                          
                    }
                } else {
                    html = '<h4 style="color:red">No alert for today.</h4>';
                }
                $("#childRecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            ShowAlert('Error', errormessage.responseText, 'danger');
        }
    });

    $.ajax({
        url: SERVER + 'followup/alert/' + Id + '/' + OnlineClinic,
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                var html = '';
                if (result.ResponseData.length != 0) {
                    var map = {};
                    $.each(result.ResponseData, function (key, item) {
                        var obj = {
                            Date: item.NextVisitDate,
                            Gender: item.Child.Gender
                        }
                        if (item.Child.Name in map) {
                            map[item.Child.Name].push(obj);
                        } else {
                            map[item.Child.Name] = [];
                            map[item.Child.Name].push(obj);
                        }


                    });
                    for (var key in map) {
                        var arry = map[key];
                        //
                        html += '<div class="row">';
                        html += '<div class="col-md-2">';
                        html += '<img src="/img/'
                        if (arry[0].Gender == "Boy")
                            html += 'male.png"   class="img-responsive pull-right" alt="female" style="max-width:30px;max-height:30px" />';
                        else
                            html += 'female.png"   class="img-responsive pull-right" alt="female" style="max-width:30px;max-height:30px" />';

                        html += ' </div>';
                        html += ' <div class="col-md-8">';
                        html += '<h5 style="border-bottom:solid 1px pink">' + key + '</h5>';
                        html += '  <div class="pull-right">';
                        html += '<i class="fa fa-phone" aria-hidden="true"></i>&nbsp;&nbsp;&nbsp;<i class="fa fa-envelope" aria-hidden="true"></i>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';

                    }
                } else {
                    html = '<h4 style="color:red">No alert for today.</h4>';
                }
                $("#followupRecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            ShowAlert('Error', errormessage.responseText, 'danger');
        }
    });
}


function enableAccordion() {
    $(".toggle-accordion").on("click", function () {
        var accordionId = $(this).attr("accordion-id"),
          numPanelOpen = $(accordionId + ' .collapse.in').length;

        $(this).toggleClass("active");

        if (numPanelOpen == 0) {
            openAllPanels(accordionId);
        } else {
            closeAllPanels(accordionId);
        }
    })

    openAllPanels = function (aId) {
        console.log("setAllPanelOpen");
        $(aId + ' .panel-collapse:not(".in")').collapse('show');
    }
    closeAllPanels = function (aId) {
        console.log("setAllPanelclose");
        $(aId + ' .panel-collapse.in').collapse('hide');
    }
}