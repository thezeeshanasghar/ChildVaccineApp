

$(document).ready(function () {
    SetMainNav();
    var pageName = document.location.href.match(/[^\/]+$/);
    if (pageName != null)
        pageName = pageName[0];
    if (localStorage.getItem('UserType') == null) {
        // TODO: hide pages from anonymous user
        if (pageName == 'brand-inventory.html') {
            window.location.replace('/un-authorize.html');
        }
    }
    else if (localStorage.getItem('UserType') == 'SUPERADMIN') {
        if (pageName == 'doctor-signup.html') {
            window.location.replace('/un-authorize.html');
        }
        $('#menu-login').hide();
    }
    else if (localStorage.getItem('UserType') == 'DOCTOR') {
        if (pageName == 'vaccine.html'
            || pageName == 'doctor-signup.html'
            || pageName == 'doctor.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-login').hide();
        $('#menu-doctor-signup').hide();
    }
    else if (localStorage.getItem('UserType') == 'PARENT') {
        if (pageName == 'doctor.html' || pageName == 'vaccine.html'
            || pageName == 'clinic.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-login').hide();
    }
});
//capitalize first letter of name throughout the app
$(".CapitalizeFirstLetter").keyup(function (event) {
    $(".CapitalizeFirstLetter").css('textTransform', 'capitalize');
    //var textBox = event.target;
    //var start = textBox.selectionStart;
    //var end = textBox.selectionEnd;
    //textBox.value = textBox.value.charAt(0).toUpperCase() + textBox.value.slice(1);
    //textBox.setSelectionRange(start, end);

});
function SetMainNav() {
    var markup = '';

    var UserType = localStorage.getItem('UserType');
    var OnlineClinic = localStorage.getItem('OnlineClinic');

    if (UserType == 'SUPERADMIN') {
        markup += '<nav class="navbar navbar-default">';
        markup += '<div class="container-fluid">';
        markup += '    <div class="navbar-header">';
        markup += '        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">';
        markup += '            <span class="sr-only">Toggle navigation</span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '        </button>';
        markup += '        <a class="navbar-brand" href="#">Vaccs.io</a>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '    <div id="navbar" class="navbar-collapse collapse">';
        markup += '        <ul class="nav navbar-nav navbar-right">';
        markup += '            <li><a href="/admin/dashboard.html">Home</a></li>';
        markup += '            <li id="menu-vaccine"><a href="/admin/vaccine.html">Vaccine</a></li>';
        markup += '            <li id="menu-doctor"><a href="/admin/doctor.html">Doctor</a></li>';
        markup += '            <li id="menu-login"><a href="/login.html">Login</a></li>';
        markup += '            <li id="menu-logout"><a href="#" onclick="return logout()">Logout</a></li>';
        markup += '        </ul>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '</div><!--/.container-fluid -->';
        markup += '</nav>';
    } else if (UserType == 'DOCTOR') {
        markup += '<div class="btn-group btn-group-justified">';
        markup += '     <a id="openSideNav" href="#" onclick="openNav()" class="btn btn-primary btn-lg" style="width:.35%;padding: 10px 10px;"><span class="glyphicon glyphicon-align-justify"></span></a>';
        markup += '     <a href="/doctor/alert.html" class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-alert"></span></a>';
        markup += '     <a id="addNewChild" href="/doctor/add-new-child.html" class="btn btn-primary  btn-lg"><small><span class="glyphicon glyphicon-plus"></span></small><span class="glyphicon glyphicon-user"></span></a>';
        markup += '     <a href="/doctor/child.html?id=' + OnlineClinic + '" class="btn btn-primary btn-lg"><small><span class="glyphicon glyphicon-th-list"></small><span>&nbsp;<span class="glyphicon glyphicon-user"></span></a>';
        markup += '     <a href="/doctor/doctor-edit.html" class="btn btn-primary btn-lg"><small><span class="glyphicon glyphicon-th-list"></small><span>&nbsp;<span class="glyphicon glyphicon-pencil"></span></a>';
        markup += '</div>';
        markup += '<div id="mySidenav" class="sidenav">';
        markup += '     <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>';
        markup += '     <a id="navBtnClinic" href="/doctor/clinic.html">Clinic</a>';
        markup += '     <a href="/changed-password.html">Change Password</a>';
        markup += '     <a href="/doctor/doctor-schedule.html">Custom Schedule</a>';
        markup += '     <a href="/doctor/brand-inventory.html">Brand Inventory</a>';
        markup += '     <a href="/doctor/brand-amount.html">Brand Amount</a>';
        markup += '     <a href="#" onclick="return logout()">Logout</a>';
        markup += '</div>';
    } else if (UserType == 'PARENT') {
        markup += '<div class="btn-group btn-group-justified">';
        markup += '     <a id="openSideNav" href="#" onclick="openNav()" class="btn btn-primary btn-lg" style="width:.35%;padding: 10px 10px;"><span class="glyphicon glyphicon-align-justify"></span></a>';
        markup += '     <a href="/child/child-specialist.html" class="btn btn-primary btn-lg">Find Child Specialist</a>';
        markup += '     <a href="/child/child-safety.html" class="btn btn-primary btn-lg">Child safety</a>';
        markup += '     <a href="/child/child.html" class="btn btn-primary btn-lg">My Kids</a>';
        markup += '     <a href="#" class="btn btn-primary btn-lg disabled ">Online Help</a>';
        markup += '</div>';
        markup += '<div id="mySidenav" class="sidenav">';
        markup += '     <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>';
        markup += '     <a href="/child/child-specialist.html" >Find Child Specialist</a>';
        markup += '     <a href="/child/child.html">My Kids</a>';
        markup += '     <a href="/child/child-safety.html">Child Safety</a>';
        markup += '     <a href="/child/vaccine-information.html">Vaccine Informations</a>';
        markup += '     <a href="/child/child-food-nutrition.html">Baby food and nutrituion guide</a>';
        markup += '     <a href="#" onclick="return logout()">Logout</a>';
        markup += '</div>';
    } else {
        markup += '<nav class="navbar navbar-default">';
        markup += '<div class="container-fluid">';
        markup += '    <div class="navbar-header">';
        markup += '        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">';
        markup += '            <span class="sr-only">Toggle navigation</span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '        </button>';
        markup += '        <a class="navbar-brand" href="#">Vaccs.io</a>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '    <div id="navbar" class="navbar-collapse collapse">';
        markup += '        <ul class="nav navbar-nav navbar-right">';
        markup += '            <li><a href="/index.html">Home</a></li>';
        markup += '            <li><a id="btnDoctorSignup" href="/doctor/doctor-signup.html">Doctor signup</a></li>';
        markup += '            <li class="dropdown" id="menu-login">';
        markup += '                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="true">Login <span class="caret"></span></a>';
        markup += '                    <ul class="dropdown-menu">';
        markup += '                         <li><a href="/login.html?UserType=SUPERADMIN">Admin Login</a></li>';
        markup += '                         <li><a href="/login.html?UserType=DOCTOR">Doctor Login</a></li>';
        markup += '                         <li><a href="/login.html?UserType=PARENT">Parent Login</a></li>';
        markup += '                    </ul>';
        markup += '            </li>';

        markup += '        </ul>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '</div><!--/.container-fluid -->';
        markup += '</nav>';
    }

    $('#menu').html(markup);
}


///////////////////////////////////////////
///     ALERT SHOW HIDE UTILITY METHODS
function ShowAlert(msg_title, msg_body, msg_type) {
    var AlertMsg = $('#alert_message');
    $(AlertMsg).find('strong').html(msg_title);
    $(AlertMsg).find('p').html(msg_body);

    if (msg_type == 'success') {
        $(AlertMsg).removeClass('alert-info');
        $(AlertMsg).removeClass('alert-warning');
        $(AlertMsg).removeClass('alert-danger');
        $(AlertMsg).addClass('alert-' + msg_type);
    } else if (msg_type == 'info') {
        $(AlertMsg).removeClass('alert-success');
        $(AlertMsg).removeClass('alert-warning');
        $(AlertMsg).removeClass('alert-danger');
        $(AlertMsg).addClass('alert-' + msg_type);
    } else if (msg_type == 'warning') {
        $(AlertMsg).removeClass('alert-success');
        $(AlertMsg).removeClass('alert-info');
        $(AlertMsg).removeClass('alert-danger');
        $(AlertMsg).addClass('alert-' + msg_type);
    } else if (msg_type == 'danger') {
        $(AlertMsg).removeClass('alert-success');
        $(AlertMsg).removeClass('alert-warning');
        $(AlertMsg).removeClass('alert-info');
        $(AlertMsg).addClass('alert-' + msg_type);
    }
    $(AlertMsg).show();
}

function HideAlert() {
    var AlertMsg = $('#alert_message');
    $(AlertMsg).hide();
}
///    END


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function logout() {
    localStorage.clear();
    window.location = '/index.html';
}

function PasswordGenerator() {
    var length = 4,
        charset = "0123456789",
        retVal = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
        retVal += charset.charAt(Math.floor(Math.random() * n));
    }
    return retVal;
}

function ScrollToTop() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
}

///////////////////////////////////////////
///     ALERT SHOW HIDE UTILITY METHODS
///////////////////////////////////////////
function GetUserTypeFromLocalStorage() {
    return GetFromLocalStorage('UserType');
}

function GetUserIDFromLocalStorage() {
    return GetFromLocalStorage('UserID');
}

function GetMobileNumberFromLocalStorage() {
    return GetFromLocalStorage('MobileNumber');
}
function GetOnlineClinicIdFromLocalStorage() {
    return GetFromLocalStorage('OnlineClinic');
}
function GetFromLocalStorage(key) {
    return localStorage.getItem(key);
}
function DoctorId() {
    //var id = parseInt(getParameterByName("id")) || 0;
    //if (id != 0)
    //    return id;
    //else {
    var id = localStorage.getItem('DoctorID');
    if (id)
        return id;
    else 0;
    //}
}
//-----------------------------------------


// return error messages
function displayErrors(jqXHR, exception) {
    var msg = '';
    if (jqXHR.status === 0) {
        msg = 'You are Not Connected to the Internet. Please Verify Your Internet Connection.';
    } else if (jqXHR.status == 404) {
        msg = 'Requested page not found.';
    } else if (jqXHR.status == 500) {
        msg = 'Internal Server Error.';
    } else if (exception === 'parsererror') {
        msg = 'Unable to parse JSON.';
    } else if (exception === 'timeout') {
        msg = 'Could Not Complete. Request Time out, Please Try Again.';
    } else if (exception === 'abort') {
        msg = 'Request aborted.';
    } else {
        msg = 'Uncaught Error.\n' + jqXHR.responseText;
    }
    alert(msg);
}
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

function GetCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    return dd + '-' + mm + '-' + yyyy;
}




function getUserAge(ageNum) {
    var day = 'At Birth'
    switch (ageNum) {
        
                                    //<option value="7">1 Week</option>
                                    //<option value="14">2 Weeks</option>
                                    //<option value="21">3 Weeks</option>
                                    //<option value="28">4 Weeks</option>
                                    //<option value="35">5 Weeks</option>
        case null:
            day = '';
            break;
        case 0:
            day = 'At Birth';
            break;
        case 7:
            day = '1 Week';
            break;
        case 14:
            day = '2 Weeks';
            break;
        case 21:
            day = '3 Weeks';
            break;
        case 28:
            day = '4 Weeks';
            break;
        case 35:
            day = '5 Weeks';
            break;
        case 42:
            day = '6 Weeks';
            break;
        case 49:
            day = '7 Weeks';
            break;
        case 56:
            day = '8 Weeks';
            break;
        case 63:
            day = '9 Weeks';
            break;
        case 70:
            day = '10 Weeks';
            break;
        case 77:
            day = '11 Weeks';
            break;
        case 84:
            day = '12 Weeks';
            break;
        case 91:
            day = '13 Weeks';
            break;
        case 98:
            day = '14 Weeks';
            break;
        case 105:
            day = '15 Weeks';
            break;
        case 112:
            day = '16 Weeks';
            break;
        case 119:
            day = '17 Weeks';
            break;
        case 126:
            day = '18 Weeks';
            break;
        case 133:
            day = '19 Weeks';
            break;
        case 140:
            day = '20 Weeks';
            break;
        case 147:
            day = '21 Weeks';
            break;
        case 154:
            day = '22 Weeks';
            break;
        case 161:
            day = '23 Weeks';
            break;
        case 168:
            day = '24 Weeks';
            break;
        case 212:
            day = '7 Months';
            break;
        case 243:
            day = '8 Months';
            break;
        case 273:
            day = '9 Months';
            break;
        case 304:
            day = '10 Months';
            break;
        case 334:
            day = '11 Months';
            break;
        case 365:
            day = '12 Months';
            break;
        case 395:
            day = '13 Months';
            break;
        case 426:
            day = '14 Months';
            break;
        case 456:
            day = '15 Months';
            break;
        case 486:
            day = '16 Months';
            break;
        case 516:
            day = '17 Months';
            break;
        case 546:
            day = '18 Months';
            break;
        case 576:
            day = '19 Months';
            break;
        case 606:
            day = '20 Months';
            break;
        case 636:
            day = '21 Months';
            break;
        case 666:
            day = '22 Months';
            break;
        case 696:
            day = '23 Months';
            break;
        case 726:
            day = '24 Months';
            break;
        case 756:
            day = '25 Months';
            break;
        case 786:
            day = '26 Months';
            break;
        case 816:
            day = '27 Months';
            break;
        case 846:
            day = '28 Months';
            break;
        case 876:
            day = '29 Months';
            break;
        case 906:
            day = '30 Months';
            break;
        case 936:
            day = '31 Months';
            break;
        case 966:
            day = '32 Months';
            break;
        case 996:
            day = '33 Months';
            break;
        case 1026:
            day = '34 Months';
            break;
        case 1056:
            day = '35 Months';
            break;
        case 1086:
            day = '36 Months';
            break;
        case 1116:
            day = '37 Months';
            break;
        case 1146:
            day = '38 Months';
            break;
        case 1176:
            day = '39 Months';
            break;
        case 1206:
            day = '40 Months';
            break;
        case 1236:
            day = '41 Months';
            break;
        case 1266:
            day = '42 Months';
            break;
        case 1296:
            day = '43 Months';
            break;
        case 1326:
            day = '44 Months';
            break;
        case 1356:
            day = '45 Months';
            break;
        case 1386:
            day = '46 Months';
            break;
        case 1416:
            day = '47 Months';
            break;
        case 1446:
            day = '48 Months';
            break;
        case 1476:
            day = '49 Months';
            break;
        case 1506:
            day = '50 Months';
            break;
        case 1536:
            day = '51 Months';
            break;
        case 1566:
            day = '52 Months';
            break;
        case 1596:
            day = '53 Months';
            break;
        case 1626:
            day = '54 Months';
            break;
        case 1656:
            day = '55 Months';
            break;
        case 1686:
            day = '56 Months';
            break;
        case 1716:
            day = '57 Months';
            break;
        case 1746:
            day = '58 Months';
            break;
        case 1776:
            day = '59 Months';
            break;
        case 1806:
            day = '60 Months';
            break;
        case 2190:
            day = '6 Years';
            break;
        case 2555:
            day = '7 Years';
            break;
        case 2920:
            day = '8 Years';
            break;
        case 3285:
            day = '9 Years';
            break;
        case 3315:
            day = '9 Year 1 Month';
            break;
        case 3650:
            day = '10 Years';
            break;
        case 3833:
            day = '10 Year 6 Months';
            break;

        case 4015:
            day = '11 Years';
            break;
        case 4380:
            day = '12 Years';
            break;
        case 4745:
            day = '13 Years';
            break;
        case 5110:
            day = '14 Years';
            break;
        case 5475:
            day = '15 Years';
            break;

    }
    return day;
}