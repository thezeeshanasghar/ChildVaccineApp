

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
    //if (localStorage.getItem('UserType') == 'SUPERADMIN') {
    //     $('#menuChild').hide();
    //      // document.getElementById("menuItem").children[3].style.display = "none";
    //}
    if (localStorage.getItem('UserType') == 'DOCTOR') {
        if (pageName == 'vaccine.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-vaccine').hide();
    }
    if (localStorage.getItem('UserType') == 'PARENT') {
        if (pageName != 'child.html' || pageName != 'index.html') {
            window.location.replace('un-authorize.html');
        }
        $('#menu-doctor').hide();
        $('#menu-vaccine').hide();
     }
});

function HideFromAnonmousUsers() {
    $('#menu-logout').hide();
    $('#menu-child').hide();
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
