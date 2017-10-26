//Load Data in Table when documents is ready  
$(document).ready(function () {

    if (GetChildMobileNumberFromLocalStorage() != 0) {

        loadChildDataAgainstMobileNumber();
    }

});
function loadChildDataAgainstMobileNumber() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "child/" + GetChildMobileNumberFromLocalStorage() + "/GetChildAgainstMobile",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<div class="child well" style="border-width:2px;background-color:white;padding-top:9px; padding-bottom:9px;margin-bottom:9px;border-color:';
                    if (item.Gender == 'Boy')
                        html += 'blue">';
                    else
                        html += '#FF1493">';
                    html += '       <img id="ImgMaleFemale" src="/img/';
                    if (item.Gender == 'Boy')
                        html += 'male.png" class="img-responsive pull-left" alt="male" style="max-width:90px;max-height:90px" />';
                    else
                        html += 'female.png" class="img-responsive pull-left" alt="female" style="max-width:90px;max-height:90px" />';
                    html += '   <h4>';
                    html += '       <a href="schedule.html?id=' + item.ID + '">' + item.Name + ' ' + item.FatherName + '</a>';
                    html += '   </h4>';
                    html += '   <div style="font-size:20px;padding-left:50px">';
                    html += '       <i class="glyphicon glyphicon-calendar"></i> ' + item.DOB + ' <br />';
                    html += '   </div>';
                    html += '   <div style="padding-left:100px">';
                    html += '       <a class="btn btn-success btn-sm"  onclick="GrowthChart(' + item.ID + ')">Growth Chart</a>';
                    html += '       <a class="btn btn-success btn-sm" onclick="GetFollowUpById(' + item.ID + ')"  >Follow Up</a>';
                    html += ' </div>';  
                    html += '</div>';

                });

                $("#childrecords").html(html);
                HideAlert();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function GetChildMobileNumberFromLocalStorage() {
    var mobileNumber = localStorage.getItem('MobileNumber');
    return mobileNumber;
}
//followup  
function GetFollowUpById(childId) {
    $("#followUpID").val(childId);
    var obj = {
        ChildID: childId
    }
    $.ajax({
        url: SERVER + 'child/followup',
        type: 'post',
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Loading data', 'Checking for existing followups', 'info');
            }
            else {
                $.each(result.ResponseData, function (key, item) {
                    html += '<tr>'
                    html += '   <td>' + (key + 1) + '</td>';
                    html += '   <td>' + item.Date + '</td>';
                    html += '   <td>' + item.Disease + '</td>';
                    html += '</tr>'

                });
            }
            $(".tbody").html(html);
            $("#followUpModal").modal("show");
            HideAlert();
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            ShowAlert('Error', ob.Message, 'danger');
        }


    });

}

//function for chart modal
function GrowthChart(id) {

    var dateArray = [];
    var weightArray = [];
    var heightArray = [];
    var cercumference = [];

    $.ajax({
        url: SERVER + "child/" + id + "/schedule",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.IsSuccess) {

                for (var i = 0; i < result.ResponseData.length; i++) {
                    if (result.ResponseData[i].Weight == 0 || result.ResponseData[i].Height == 0 || result.ResponseData[i].Circle == 0) {
                        continue;
                    }
                    else {
                        dateArray.push(result.ResponseData[i].Date);
                        weightArray.push(result.ResponseData[i].Weight);
                        heightArray.push(result.ResponseData[i].Height);
                        cercumference.push(result.ResponseData[i].Circle);
                    }
                }

            }
            else {
                ShowAlert('Error', result.Message, 'danger');
            }

            $("#chartModal").modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);

        }

    });
    var childWeightChart = $("childWeightChart");
    var childLengthChart = $("childLengthChart");
    var childCercumferenceChart = $("childCercumferenceChart");
    // var chart = $("myChart");
    var ctxWeight = document.getElementById('childWeightChart').getContext('2d');
    var ctxLength = document.getElementById('childLengthChart').getContext('2d');
    var ctxCercm = document.getElementById('childCercumferenceChart').getContext('2d');
    var childWeightChart = new Chart(ctxWeight, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: dateArray,
            datasets: [{
                label: "wieght",

                fill: false,
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: weightArray
                //  data: [0, 15, 25, 27, 28, 30, 33],

            }

            ]
        },

        // Configuration options go here
        options: {}
    });
    var childLengthChart = new Chart(ctxLength, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: dateArray,
            datasets: [{

                label: "height",
                fill: false,
                backgroundColor: 'rgb(100, 255, 100)',
                borderColor: 'rgb(100, 255, 100)',
                data: heightArray
                // data: [0, 10, 15, 20, 24, 30, 55],
            }

            ]
        },

        // Configuration options go here
        options: {}
    });
    var childCercumferenceChart = new Chart(ctxCercm, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: dateArray,
            datasets: [
            {
                label: "cercumfrance",
                fill: false,
                backgroundColor: 'rgb(100, 100,255)',
                borderColor: 'rgb(100, 100, 255)',
                data: cercumference
                //data: [0, 15, 25, 29, 30, 45],
            }
            ]
        },

        // Configuration options go here
        options: {}
    });
    //$("#modal-body").html(html);

    //$("#chartModal").modal('show');

}

