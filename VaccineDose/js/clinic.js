//Load Data in Table when documents is ready  
$(document).ready(function () {
    // var id = parseInt(getParameterByName("id")) || 0;
    var id = DoctorId();
    loadData(id);
});

function DoctorId() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0) 
        return id;
    else {
        var id = localStorage.getItem('DoctorID');
        if (id)
            return id;
        else 0;
    }
}
//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: "/api/doctor/" + id + "/clinics",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.OffDays + '</td>';
                html += '<td>' + item.StartTime +' - '+ item.EndTime + '</td>';
                html += '<td>' +
                    '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            HideAlert();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var result = [];
    $('input[type="radio"]:checked').each(function () {
       
        result.push(this.value);
        console.log(result);
    });
    var obj = {
        Name: $('#Name').val(),
         StartTime: $('#StartTime').val(),
        EndTime: $('#EndTime').val(),
      //  OffDays: $("input[name='Monday']:checked").val() + "," + $("input[name='Tuesday']:checked").val() + ","
      //+ $("input[name='Wednesday']:checked").val() + "," + $("input[name='Thursday']:checked").val() + ","
      //    + $("input[name='Friday']:checked").val() + "," + $("input[name='Saturday']:checked").val() + ","
        //  + $("input[name='Sunday']:checked").val(),
        OffDays:result.join(','),
        Lat: myMarker.getPosition().lat(),
        Long: myMarker.getPosition().lng(),
        DoctorID: DoctorId()
    };
    $.ajax({
        url: "/api/clinic",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
             loadData(DoctorId());
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon ID  
function getbyID(ID) {
    $('#Name').css('border-color', 'lightgrey');
     $('#StartTime').css('border-color', 'lightgrey');
    $('#EndTime').css('border-color', 'lightgrey');

    $.ajax({
        url: "/api/clinic/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#ID").val(result.ResponseData.ID);
            $('#Name').val(result.ResponseData.Name);
            
             $('#StartTime').val(result.ResponseData.StartTime);
            $('#EndTime').val(result.ResponseData.EndTime);

            $('#myModal').modal('show');
            myMarker.setPosition(new google.maps.LatLng(result.ResponseData.Lat, result.ResponseData.Long));

                    
            $('#btnUpdate').show();
            $('#btnAdd').hide();
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
    if (res == false) {
        return false;
    }
    var result = [];
    $('input[type="radio"]:checked').each(function () {

        result.push(this.value);
        console.log(result);
    });
    var obj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        OffDays: result.join(','),
         StartTime: $('#StartTime').val(),
        EndTime: $('#EndTime').val(),
        Lat: myMarker.getPosition().lat(),
        Long: myMarker.getPosition().lng(),
        DoctorID: DoctorId()
    };
    $.ajax({
        url: "/api/clinic/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
             loadData(DoctorId());
            $('#myModal').modal('hide');

            $('#ID').val("");
            $('#Name').val("");
            $("input:radio").attr("checked", false);
            $('#StartTime').val("");
            $('#EndTime').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


//function for deleting record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/api/clinic/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                 loadData(DoctorId());
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('#Name').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();

    $('#Name').css('border-color', 'lightgrey');
}


//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }

    return isValid;
}