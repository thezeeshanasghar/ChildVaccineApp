$(document).ready(function () {
    HideAlert();
     $("#doctor").show();
    $("#clinic").hide();
  
});
var map, myMarker;
function initMap() {
    var myLatLng = { lat: 33.5614494, lng: 73.069301 };

    map = new google.maps.Map(document.getElementById('Location'), {
        zoom: 14,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    myMarker = new google.maps.Marker({
        position: myLatLng,
        draggable: true
    });

    google.maps.event.addListener(myMarker, 'dragend', function (evt) {
        document.getElementById('current').innerHTML = '<p>Marker dropped: Current Lat: ' + evt.latLng.lat().toFixed(3) + ' Current Lng: ' + evt.latLng.lng().toFixed(3) + '</p>';
    });

    google.maps.event.addListener(myMarker, 'dragstart', function (evt) {
        document.getElementById('current').innerHTML = '<p>Currently dragging marker...</p>';
    });

    map.setCenter(myMarker.position);
    myMarker.setMap(map);
}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var result = [];
    $('input[name="OffDays"]:checked').each(function () {

        result.push(this.value);
        console.log(result);
    });
    var obj = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        Password: PasswordGenerator(),
        MobileNo: $('#MobileNo').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
        ClinicDTO: {
            Name: $('#Name').val(),
            StartTime: $('#StartTime').val(),
            EndTime: $('#EndTime').val(),
            PhoneNumber: $('#PhoneNumber').val(),
            OffDays: result.join(','),
            Lat: myMarker.getPosition().lat(),
            Long: myMarker.getPosition().lng()
        }
    };
    $.ajax({
        url: "/api/doctor",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            ShowAlert('Registration', 'Your are successfully registered,please waite for the admin approval', 'success');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function ShowHide() {
    var res = validate();
    if (res == false) {
        return false;
    }
    else {
        initMap();
        $("#doctor").hide();
         $("#clinic").show();
    }

}
//Valdidation using jquery
function validate() {
    var isValid = true;

    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }

    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }

    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }


    if ($('#MobileNo').val().trim() == "") {
        $('#MobileNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MobileNo').css('border-color', 'lightgrey');
    }

    if ($('#PMDC').val().trim() == "") {
        $('#PMDC').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PMDC').css('border-color', 'lightgrey');
    }

    return isValid;
}