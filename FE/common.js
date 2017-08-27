

$(document).ready(function () {
    SetMainNav();
    var pageName = document.location.href.match(/[^\/]+$/);
    if (pageName != null)
        pageName = pageName[0];
    if (localStorage.getItem('UserType') == null) {
        // TODO: hide pages from anonymous user
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
        markup += '        <a class="navbar-brand" href="#">MyVaccs</a>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '    <div id="navbar" class="navbar-collapse collapse">';
        markup += '        <ul class="nav navbar-nav navbar-right">';
        markup += '            <li><a href="index.html">Home</a></li>';
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
        markup += '     <a href="#" onclick="openNav()" class="btn btn-primary btn-lg" style="width:.35%;padding: 10px 10px;"><span class="glyphicon glyphicon-align-justify"></span></a>';
        markup += '     <a href="alert.html" class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-alert"></span></a>';
        markup += '     <a href="add-new-child.html" class="btn btn-primary  btn-lg"><small><span class="glyphicon glyphicon-plus"></span></small><span class="glyphicon glyphicon-user"></span></a>';
        markup += '     <a href="child.html?id=' + OnlineClinic + '" class="btn btn-primary btn-lg"><small><span class="glyphicon glyphicon-th-list"></small><span>&nbsp;<span class="glyphicon glyphicon-user"></span></a>';
        markup += '     <a href="doctor-edit.html" class="btn btn-primary btn-lg"><small><span class="glyphicon glyphicon-th-list"></small><span>&nbsp;<span class="glyphicon glyphicon-pencil"></span></a>';
        markup += '</div>';
        markup += '<div id="mySidenav" class="sidenav">';
        markup += '     <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>';
        markup += '     <a href="/doctor/clinic.html">Clinic</a>';
        markup += '     <a href="/changed-password.html">Change Password</a>';
        markup += '     <a href="/doctor/doctor-schedule.html">Custom Schedule</a>';
        markup += '     <a href="/doctor/vaccine-inventory.html">Vaccine Inventory</a>';
        markup += '     <a href="#" onclick="return logout()">Logout</a>';
        markup += '</div>';
    } else if (UserType == 'PARENT') {
        markup += '<div class="btn-group btn-group-justified">';
        markup += '     <a href="child-specialist.html" class="btn btn-primary btn-lg">Find Child Specialist</a>';
        markup += '     <a href="#" class="btn btn-primary btn-lg">Child safety</a>';
        markup += '     <a href="child.html" class="btn btn-primary btn-lg">My Children</a>';
        markup += '     <a href="#" class="btn btn-primary btn-lg" onclick="return logout()" id="menu-logout">Logout</a>';
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
        markup += '        <a class="navbar-brand" href="#">MyVaccs</a>';
        markup += '    </div><!--/.nav-collapse -->';
        markup += '    <div id="navbar" class="navbar-collapse collapse">';
        markup += '        <ul class="nav navbar-nav navbar-right">';
        markup += '            <li><a href="/index.html">Home</a></li>';
        markup += '            <li><a href="/doctor/doctor-signup.html">Doctor signup</a></li>';
        markup += '            <li id="menu-login"><a href="/login.html">Login</a></li>';
        
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



function GetUserIDFromLocalStorage() {
    var id = localStorage.getItem('UserID');
    if (id)
        return id;
    else return 0;
}

function GetMobileNumberFromLocalStorage() {
    var id = localStorage.getItem('MobileNumber');
    if (id)
        return id;
    else return 0;
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
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}