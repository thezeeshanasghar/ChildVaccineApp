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
        CountryCode: $('#CountryCode').val(),
        MobileNumber: $('#MobileNumber').val(),
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
        url: SERVER + "doctor",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            ShowAlert('Registration', 'Your are successfully singup for <b>MyVacc</b><br/>Now admin will approve your singup then you can <a href="/login.html">login</a> to <b>MyVacc</b><br/>Your username and password have been send to your email address', 'success');
            ScrollToTop();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function validate() {
    var validator = $('#form2').data("bs.validator");
    $('#form2').validator('validate');
    if (validator.hasErrors())
        return false;
    else
        return true;
}
function ShowHide(event) {
    $('#form1').validator('validate');

    var validator = $('#form1').data("bs.validator");
    if (!validator.hasErrors()) {
        initMap();
        $("#doctor").hide();
        $("#clinic").show();
    }
}
