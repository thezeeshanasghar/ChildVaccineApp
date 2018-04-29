//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetOnlineClinicIdFromLocalStorage() != 0) {
        loadData();
        // DisableOffDays();
    }
});
function goToByScroll(id) {
    $('html,body').animate({ scrollTop: $(id).offset().top }, 'slow');
}
//Load Data function  
function loadData() {


    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "doctor/" + GetUserIDFromLocalStorage() + "/childs?searchKeyword=" + $.trim($("#SearchItem").val()),
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            } else {
                var boys = girls = 0;
                $.each(result.ResponseData, function (key, item) {
                    html += '<div class="child well" style="border-width:2px;background-color:white;padding-top:9px; padding-bottom:9px;margin-bottom:9px;border-color:';
                    if (item.Gender == 'Boy') {
                        html += 'blue">';
                        boys++;
                    }
                    else {
                        html += '#FF1493">';
                        girls++;
                    }
                    html += '       <img id="ImgMaleFemale" src="/img/';
                    if (item.Gender == 'Boy')
                        html += 'male.png" class="img-responsive pull-left" alt="male" style="max-width:90px;max-height:90px" />';
                    else
                        html += 'female.png" class="img-responsive pull-left" alt="female" style="max-width:90px;max-height:90px" />';
                    html += '   <h4>';
                    html += '       <span class="pull-right" style="font-size:16px">';
                    html += '           <a href="#" onclick="return getbyID(' + item.ID + ')"><span class="glyphicon glyphicon-pencil"></span></a>';
                    html += '           &nbsp;<a href="#" onclick="Delele(' + item.ID + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                    html += '       </span>';
                    html += '       &nbsp;';
                    html += '       <a href="schedule.html?id=' + item.ID + '" id="' + item.ID + '">' + item.Name + '</a><br/>';
                    html += '   </h4>';
                    html += '   <div style="font-size:12px;padding-left:100px">';
                    html += '       <i class="glyphicon glyphicon-user"></i> ' + item.FatherName + '<br/>';
                    html += '       <i class="glyphicon glyphicon-calendar"></i> ' + item.DOB + ' <br />';
                    html += '       <i class="glyphicon glyphicon-earphone"></i> ' + item.MobileNumber;
                    html += '   </div>';
                    html += '   <div style="padding-left:100px">';
                    html += '       <a style="margin: 2px" class="btn btn-success btn-sm" href="schedule.html?id=' + item.ID + '">Vaccines</a>';
                    if (item.Clinic.Doctor.AllowFollowUp)
                        html += '       <a style="margin: 2px" class="btn btn-success btn-sm" onclick="GetFollowUpById(' + item.ID + ')"  >Follow Up</a>';
                    if (item.Clinic.Doctor.AllowChart)
                        html += '       <a style="margin: 2px" class="btn btn-success btn-sm"  onclick="GrowthChart(' + item.ID + ')">Growth Chart</a>';
                    if (item.Clinic.Doctor.AllowInvoice)
                        html += '       <a style="margin: 2px" class="btn btn-success btn-sm" onClick="OpenGenerateInvoiceModel(' + item.ID + ')" >Invoice</a>';
                    html += '   </div>';
                    html += '</div>';
                });
                $("#childrecords").html(html);
                debugger;
                $("#Boys").html('Boys: ' + boys);
                $("#Girls").html('Girls: ' + girls);
                $("#TotalChilds").html('Total: ' + (boys + girls));
                HideAlert();
                if ($("#SearchItem").val() != "" && result.ResponseData.length <= 0) {
                    var newHtml = '';
                    newHtml += 'No data found against search result.<br />You can add this child in application: &nbsp;<a href="/doctor/add-new-child.html?searchKeyword=' + $("#SearchItem").val() + '"';
                    newHtml += ' class="btn btn-primary" >Add New Child</a>';
                    $("#MessageFromSearch").html(newHtml);
                    $("#MessageFromSearch").show();
                } else {
                    $("#MessageFromSearch").hide();
                }


                if (window.location.hash) {
                    goToByScroll(window.location.hash);
                    $('a' + window.location.hash).parent().parent().effect("highlight", {}, 5000);
                }
            }
        },
        error: function (errormessage, e) {
            displayErrors(errormessage, e);
        }
    });
}

//function for updating record end
function DisableOffDays() {
    $.ajax({
        url: SERVER + 'clinic/' + GetOnlineClinicIdFromLocalStorage(),
        type: 'Get',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.IsSuccess) {
                var OffDays = result.ResponseData.OffDays;
                if (OffDays.length > 1) {
                    OffDays = OffDays.split(",");
                    if ($.inArray("Monday", OffDays) != -1)
                        $("#Monday").prop('disabled', true);
                    if ($.inArray("Tuesday", OffDays) != -1)
                        $("#Tuesday").prop('disabled', true);
                    if ($.inArray("Wednesday", OffDays) != -1)
                        $("#Wednesday").prop('disabled', true);
                    if ($.inArray("Thursday", OffDays) != -1)
                        $("#Thursday").prop('disabled', true);
                    if ($.inArray("Friday", OffDays) != -1)
                        $("#Friday").prop('disabled', true);
                    if ($.inArray("Saturday", OffDays) != -1)
                        $("#Saturday").prop('disabled', true);
                    if ($.inArray("Sunday", OffDays) != -1)
                        $("#Sunday").prop('disabled', true);
                }
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon ID  
function getbyID(ID) {
    $.ajax({
        url: SERVER + "child/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            $('#FatherName').val(result.ResponseData.FatherName);

            $('#Email').val(result.ResponseData.Email);
            //$("#Email").prop("disabled", true);

            $('#DOB').val(result.ResponseData.DOB);
            $('#DOB').attr('disabled', true);

            $('#MobileNumber').val(result.ResponseData.MobileNumber);
            //$('#MobileNumber').attr('disabled', true);

            $("input[name=gender][value=" + result.ResponseData.Gender + "]").prop('checked', true);
            $('#City').val(result.ResponseData.City);
            $('#PreferredDayOfReminder').val(result.ResponseData.PreferredDayOfReminder);
            $('#PreferredSchedule').val(result.ResponseData.PreferredSchedule);
            $('#PreferredDayOfWeek').val(result.ResponseData.PreferredDayOfWeek);
            $("#IsEPIDone").prop("checked", result.ResponseData.IsEPIDone);
            $("#IsVerified").prop("checked", result.ResponseData.IsVerified);

            $('#myModal').modal('show');
            $('#btnAdd').hide();
            $('#btnUpdate').show();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Update() {
    var res = validate();
    if (res == false)
        return false;


    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        DOB: $('#DOB').val(),
        Email: $('#Email').val(),
        MobileNumber: $('#MobileNumber').val(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),
        PreferredDayOfWeek: $('#PreferredDayOfWeek').val(),
        PreferredSchedule: $('#PreferredSchedule').find(":selected").val(),
        PreferredDayOfReminder: $('#PreferredDayOfReminder').val(),
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").val(),
        ClinicID: GetOnlineClinicIdFromLocalStorage()
    };
    $.ajax({
        url: SERVER + "child/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                loadData();

                $('#myModal').modal('hide');
                $('#ID').val("");
                $("#Name").val("");
                $("#FatherName").val("");
                $("#Email").val("");
                $("#DOB").val("");
                $("#MobileNumber").val("");
                $('#CountryCode').val("");
                $("input:checkbox").prop("checked", false);
                $("#City").val("");
            }
        },
        error: function (errormessage) {
            var ob = JSON.parse(errormessage.responseText);
            alert(ob.Message);;
        }
    });
}

//function for deleting record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: SERVER + "child/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.IsSuccess) {
                    ShowAlert('Success', result.Message, 'success');
                    loadData();
                }
                else {
                    ShowAlert('Rquest Failed', result.Message, 'error');
                }
            },
            error: function (errormessage) {
                var ob = JSON.parse(errormessage.responseText);
                ShowAlert('Error', ob.Message, 'danger');
            }
        });
    }
}



//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $("#Name").val("");
    $("#FatherName").val("");

    $("#Email").val("");
    $("#Email").prop("disabled", false);

    $("#DOB").val("");
    $("#DOB").prop("disabled", false);

    $('#CountryCode').val("");
    $("#CountryCode").prop("disabled", false);

    $("#MobileNumber").val("");
    $("#MobileNumber").prop("disabled", false);

    $("#City").val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $("input:checkbox").prop("checked", false);

}

//Valdidation using jquery  
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}









//Generate invoice
function OpenGenerateInvoiceModel(childId) {
    $("#ChildId").val(childId);
    $("#generateInvoiceModal").modal("show");
}

function GenerateInvoice() {
    var obj = {
        ID: $("#ChildId").val(),
        IsBrand: $("#IsBrand").is(':checked'),
        IsConsultationFee: $("#IsConsultationFee").is(':checked'),
        InvoiceDate: $("#InvoiceDate").val(),
        DoctorID: DoctorId()
    }
    $.download(SERVER + 'child/Download-Invoice-PDF', obj, "POST");
    $("#generateInvoiceModal").modal("hide");
}

function PrintFollowUp() {
    var obj = {
        ChildID: $("#followUpID").val(),
        DoctorID: DoctorId()
    }
    console.log(obj);
    $.download(SERVER + 'child/Download-FollowUp-PDF', obj, "POST");
}

jQuery.download = function (url, data, method) {
    //url and data options required
    if (url && data) {
        //data can be string of parameters or array/object
        data = typeof data == 'string' ? data : jQuery.param(data);
        //split params into form inputs
        var inputs = '';
        jQuery.each(data.split('&'), function () {
            var pair = this.split('=');
            inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
        });
        //send request
        jQuery('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>')
		.appendTo('body').submit().remove();
    };
};

//followup  
function GetFollowUpById(childId) {
    $("#followUpID").val(childId);
    var obj = {
        ChildID: childId,
        DoctorID: DoctorId()
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
                    html += '   <td>' + item.CurrentVisitDate + '</td>';
                    html += '   <td>' + item.Disease + '</td>';
                    html += '   <td>' + item.Weight + '</td>';
                    html += '   <td>' + item.Height + '</td>';
                    html += '   <td>' + item.OFC + '</td>';
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
//followup static
function AddFollowUp() {
    var obj = {
        Disease: $("#Disease").val(),
        CurrentVisitDate: GetCurrentDate(),
        NextVisitDate: $("#Date").val(),
        ChildID: $("#followUpID").val(),
        DoctorID: DoctorId(),
        Weight: $("#Weight").val(),
        Height: $("#Height").val(),
        OFC: $("#OFC").val()
    }
    $.ajax({
        url: SERVER + 'followup',
        type: 'post',
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                ShowAlert('Added', 'Follow up is successfully added', 'success');
                $("#Disease").val("");
                $("#Date").val("");
                $("#followUpID").val("");
                $("#Weight").val("");
                $("#Height").val("");
                $("#OFC").val("");
                $("#followUpModal").modal("hide");
            }
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
                label: "OFC",
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
