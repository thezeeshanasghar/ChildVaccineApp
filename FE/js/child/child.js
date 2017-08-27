//Load Data in Table when documents is ready  
$(document).ready(function () {
    if (GetClinicIdFromUrlOrLocalStorage() != 0) {
        checkCustomScheduleAgainstClinic();
        loadData();
        DisableOffDays();
    }
    else {
        if (GetChildMobileNumberFromLocalStorage() != 0) {
            $("btnAddNew").hide();
            loadChildDataAgainstMobileNumber();
        }
    }

});
function checkCustomScheduleAgainstClinic() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "child/" + GetClinicIdFromUrlOrLocalStorage() + "/GetCustomScheduleAgainsClinic",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            if (!result.IsSuccess) {
                $("btnAddNew").hide();
                ShowAlert('Error', result.Message, 'danger');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
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
                    html += '<div class="child well top-buffer" style="background-color:';
                    if (item.Gender == 'Boy')
                        html += 'lightblue">';
                    else
                        html += '#FFE1E6">';
                    html += '   <h2>';
                    html += '       <img id="ImgMaleFemale" src="/img/';
                    if (item.Gender == 'Boy')
                        html += 'male.png" class="img-responsive pull-left" alt="male" style="max-width:30px;max-height:30px" />';
                    else
                        html += 'female.png" class="img-responsive pull-left" alt="male" style="max-width:30px;max-height:30px" />';
                    html += '       &nbsp;';
                    html += '       <a href="schedule.html?id=' + item.ID + '">' + item.Name + ' ' + item.FatherName + '</a>';
                    html += '   </h2>';
                    html += '   <div style="font-size:20px;padding-left:50px">';
                    html += '       <i class="glyphicon glyphicon-calendar"></i> ' + item.DOB + ' <br />';
                    html += '       <i class="glyphicon glyphicon-earphone"></i> ' + item.MobileNumber + ' <br />';
                    html += '   </div>';
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
function GetClinicIdFromUrlOrLocalStorage() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0)
        return id;
    else {
        var id = localStorage.getItem('ClinicID');
        if (id)
            return id;
        else return 0;
    }
}
function GetChildMobileNumberFromLocalStorage() {
    var mobileNumber = localStorage.getItem('MobileNumber');
    return mobileNumber;
}
//Load Data function  
function loadData() {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "clinic/" + GetClinicIdFromUrlOrLocalStorage() + "/childs",
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
                    html += '       <img id="ImgMaleFemale" src="../img/';
                    if (item.Gender == 'Boy')
                        html += 'male.png" class="img-responsive pull-left" alt="male" style="max-width:90px;max-height:90px" />';
                    else
                        html += 'female.png" class="img-responsive pull-left" alt="female" style="max-width:90px;max-height:90px" />';
                    html += '   <h4>';
                    html += '       <span class="pull-right" style="font-size:12px">';
                    html += '           <a href="#" onclick="return getbyID(' + item.ID + ')"><span class="glyphicon glyphicon-pencil"></span></a> |';
                    html += '           <a href="#" onclick="Delele(' + item.ID + ')"><span class="glyphicon glyphicon-trash"></span></a>';
                    html += '       </span>';
                    
                    html += '       &nbsp;';
                    html += '       <a href="schedule.html?id=' + item.ID + '">' + item.Name + '</a><br/>';
                    html += '   </h4>';
                    html += '   <div style="font-size:12px;padding-left:100px">';
                    html += '       <i class="glyphicon glyphicon-user"></i> ' + item.FatherName + '<br/>';
                    html += '       <i class="glyphicon glyphicon-calendar"></i> ' + item.DOB + ' <br />';
                    html += '       <i class="glyphicon glyphicon-earphone"></i> ' + item.MobileNumber;
                    html += '   </div>';
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

function DisableOffDays() {
    $.ajax({
        url: SERVER + 'clinic/' + GetClinicIdFromUrlOrLocalStorage(),
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
            $("#Email").prop("disabled", true);

            $('#DOB').val(result.ResponseData.DOB);
            $('#DOB').attr('disabled', true);
            $('#CountryCode').val(result.ResponseData.CountryCode);
            debugger;
            $('#MobileNumber').val(result.ResponseData.MobileNumber);
            $("input[name=gender][value=" + result.ResponseData.Gender + "]").prop('checked', true);
            $('#City').val(result.ResponseData.City);
            $('#PreferredDayOfReminder').val(result.ResponseData.PreferredDayOfReminder);
            $('#PreferredSchedule').val(result.ResponseData.PreferredSchedule);

            var PreferredDayOfWeek = [];
            var PDW = result.ResponseData.PreferredDayOfWeek;
            if (PDW != null) {
                if (PDW.indexOf(",") >= 0) {
                    PreferredDayOfWeek = PDW.split(",");
                } else {
                    PreferredDayOfWeek.push(PDW);
                }
                if ($.inArray("Monday", PreferredDayOfWeek) != -1)
                    $("#Monday").prop('checked', true);
                if ($.inArray("Tuesday", PreferredDayOfWeek) != -1)
                    $("#Tuesday").prop('checked', true);
                if ($.inArray("Wednesday", PreferredDayOfWeek) != -1)
                    $("#Wednesday").prop('checked', true);
                if ($.inArray("Thursday", PreferredDayOfWeek) != -1)
                    $("#Thursday").prop('checked', true);
                if ($.inArray("Friday", PreferredDayOfWeek) != -1)
                    $("#Friday").prop('checked', true);
                if ($.inArray("Saturday", PreferredDayOfWeek) != -1)
                    $("#Saturday").prop('checked', true);
                if ($.inArray("Sunday", PreferredDayOfWeek) != -1)
                    $("#Sunday").prop('checked', true);
            }
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

//function for updating record  
function Update() {
    var res = validate();
    if (res == false)
        return false;

    var result = [];
    $('input[name="PreferredDayOfWeek"]:checked').each(function () {
        result.push(this.value);
    });

    var preferdayreminder = "0";
    if (document.getElementById("TogglePreferredDayOfReminder").checked) {
        preferdayreminder = $('#PreferredDayOfReminder').val()
    }

    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        FatherName: $('#FatherName').val(),
        Email: $('#Email').val(),
        DOB: $('#DOB').val(),
        CountryCode:$('#CountryCode').val(),
        MobileNumber: $('#MobileNumber').val(),
        IsEPIDone: $("#IsEPIDone").is(':checked'),
        IsVerified: $("#IsVerified").is(':checked'),
        PreferredDayOfWeek: result.join(','),
        PreferredSchedule: $('#PreferredSchedule').find(":selected").val(),
        PreferredDayOfReminder: preferdayreminder,
        Gender: $("input[name='gender']:checked").val(),
        City: $('#City').find(":selected").val(),
        ClinicID: GetClinicIdFromUrlOrLocalStorage()
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
                if (result.IsSuccess)
                    loadData();
                else
                    ShowAlert('Rquest Failed', result.Message, 'error');
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
    $('#CountryCode').val("");
    $("#MobileNumber").val("");
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