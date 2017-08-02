//var SERVER_IP = 'http://www.vac.afz-sol.com';
//var SERVER_PORT = '80';
var SERVER_IP = 'http://localhost';
var SERVER_PORT = '4309';
var SERVER_BASE_PATH = 'api';

var SERVER = SERVER_IP + ':' + SERVER_PORT + '/' + SERVER_BASE_PATH + '/';

$(document).ready(function () {
    SetMainNav();
    var pageName = document.location.href.match(/[^\/]+$/);
    if (pageName!=null)
        pageName = pageName[0];
    if (localStorage.getItem('UserType') == null) {
        HideFromAnonmousUsers();
    }
    else if (localStorage.getItem('UserType') == 'SUPERADMIN') {
        if (pageName == 'doctor-signup.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-doctor-signup').hide();
        $('#menu-login').hide();
        $('#menu-clinic').hide();
        $('#menu-child').hide();
        $('#menu-custom-schedule').hide();
    }
    else if (localStorage.getItem('UserType') == 'DOCTOR') {
        if (pageName == 'vaccine.html'
            || pageName == 'doctor-signup.html'
            || pageName == 'doctor.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-doctor').hide();
        $('#menu-vaccine').hide();
        $('#menu-login').hide();
        $('#menu-doctor-signup').hide();
        $('#menu-child').hide();
    }
    else if (localStorage.getItem('UserType') == 'PARENT') {
        if (pageName == 'doctor.html' || pageName == 'vaccine.html'
            || pageName == 'clinic.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-doctor').hide();
        $('#menu-vaccine').hide();
        $('#menu-clinic').hide();
        $('#menu-doctor-signup').hide();
        $('#menu-login').hide();
        $('#menu-custom-schedule').hide();
    }
});

function SetMainNav() {
    var markup = '';

    var UserType = localStorage.getItem('UserType');
    var OnlineClinic = localStorage.getItem('OnlineClinic');

    if (UserType != 'DOCTOR') {
        markup += '<nav class="navbar navbar-default">';
        markup += '<div class="container-fluid">';
        markup += '    <div class="navbar-header">';
        markup += '        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">';
        markup += '            <span class="sr-only">Toggle navigation</span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '            <span class="icon-bar"></span>';
        markup += '        </button>';
        markup += '        <a class="navbar-brand" href="#">MyVaccs</a>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '    <div id="navbar" class="navbar-collapse collapse">';
        markup += '        <ul class="nav navbar-nav navbar-right">';
        markup += '            <li><a href="index.html">Home</a></li>';
        markup += '            <li id="menu-doctor-signup"><a href="doctor-signup.html">Doctor Signup</a></li>';
        markup += '            <li id="menu-vaccine"><a href="vaccine.html">Vaccine</a></li>';
        markup += '            <li id="menu-doctor"><a href="doctor.html">Doctor</a></li>';
        markup += '            <li id="menu-clinic"><a href="clinic.html">Clinic</a></li>';
        markup += '            <li id="menu-custom-schedule" class="dropdown">';
        markup += '                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Schedules <span class="caret"></span></a>';
        markup += '                <ul class="dropdown-menu">';
        markup += '                    <li><a href="custom-schedule.html">Custom Schedule</a></li>';
        markup += '                    <li class="disabled"><a href="#">WHO Schedule</a></li>';
        markup += '                </ul>';
        markup += '            </li>';
        markup += '            <li id="menu-child"><a href="child.html">Child</a></li>';
        markup += '            <li id="menu-login"><a href="login.html">Login</a></li>';
        markup += '            <li id="menu-logout"><a href="#" onclick="return logout()">Logout</a></li>';
        markup += '        </ul>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '</div><!--/.container-fluid -->';
        markup += '</nav>';
    } else {
        markup += '<div class="btn-group btn-group-justified">';
        markup += '     <a href="alert.html" class="btn btn-primary btn-lg">Alert</a>';
        markup += '     <a href="#" class="btn btn-primary">SMS</a>';
        markup += '     <a href="child.html?id=' + OnlineClinic + '" class="btn btn-primary">Childs</a>';
        markup += '     <div class="btn-group">';
        markup += '         <button type="button" class="btn btn-primary btn-lg dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
        markup += '             Profile &nbsp;<span class="caret"></span><span class="sr-only">Toggle Dropdown</span></button>';
        markup += '             <ul class="dropdown-menu" role="menu">';
        markup += '                 <li><a href="clinic.html">Clinic</a></li>';
        markup += '                 <li><a href="#">Change Password</a></li>';
        markup += '                 <li><a href="doctor-schedule.html">Custom Schedule</a></li>';
        markup += '                 <li id="menu-logout"><a href="#" onclick="return logout()">Logout</a></li>';
        markup += '             </ul>';
        markup += '     </div>';
        markup += '</div>';
    }

    $('#menu').html(markup);
}
function HideFromAnonmousUsers() {
    $('#menu-custom-schedule').hide();
    $('#menu-logout').hide();
    $('#menu-child').hide();
    $('#menu-clinic').hide();
    $('#menu-doctor').hide();
    $('#menu-vaccine').hide();
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
    window.location = 'index.html';
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