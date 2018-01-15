$('#TimePeriod a').click(function () {
    $(this).addClass('active').siblings().removeClass('active');
    loadData();

});
$(document).ready(function () {
    loadData();


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



});
function loadData() {
    var Id = $('#TimePeriod a.active').attr('data-value');
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
                var markup,html = '';
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
                        markup += '<div class="child well top-buffer">';
                        markup += '   <h2 class="text-center">' + key + '</h2>';
                        markup += '   <div style="font-size:20px">';
                        markup += '       &nbsp;';
                        markup += '       <span class="pull-right">';
                        markup += '          <i class="glyphicon glyphicon-earphone"></i>';
                        markup += '          &nbsp;';
                        markup += '          <i class="glyphicon glyphicon-envelope"></i>';
                        markup += '       </span>';
                        markup += '   </div>';
                        markup += '   <ul class="list-group">';
                        var arry = map[key];
                        for (var index in arry) {
                            markup += '       <li class="list-group-item">' + arry[index].DoseName + '<span class="badge pull-right">' + arry[index].Date + '</span</li>';
                        }
                        markup += '   </ul>';
                        markup += '</div>';
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
                    markup = '<h3 style="color:red">No alert for today.</h3>';
                }
                $(".data").html(markup);
                $("#childRecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            ShowAlert('Error', errormessage.responseText, 'danger');
        }
    });
}