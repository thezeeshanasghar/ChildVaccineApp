var SERVER_IP = 'http://localhost';
var SERVER_PORT = '4309';
var SERVER_BASE_PATH = 'api';

var SERVER = SERVER_IP + ':' + SERVER_PORT + '/' + SERVER_BASE_PATH + '/';

//window.addEventListener("load", function () {
//    var content = document.getElementById("menuImport").import;
//    var el = content.querySelector('.navbar');
//    document.getElementById("menu").appendChild(el.cloneNode(true));

//    content = document.getElementById("footerImport").import;
//    el = content.querySelector('.footer');
//    document.getElementById("footer").appendChild(el.cloneNode(true));
//}, false);
$(document).ready(function () {
    var pageName = document.location.href.match(/[^\/]+$/)[0]
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
    }
    else if (localStorage.getItem('UserType') == 'PARENT') {
        if (pageName == 'doctor.html' || pageName == 'vaccine.html'
            || pageName=='clinic.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-doctor').hide();
        $('#menu-vaccine').hide();
        $('#menu-clinic').hide();
        $('#menu-doctor-signup').hide();
        $('#menu-login').hide();

     }
});
 
function HideFromAnonmousUsers() {
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
    $(AlertMsg).addClass('alert-' + msg_type);
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