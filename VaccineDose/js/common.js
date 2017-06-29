

window.addEventListener("load", function () {
    var content = document.getElementById("menuImport").import;
    var el = content.querySelector('.navbar');
    document.getElementById("menu").appendChild(el.cloneNode(true));

    content = document.getElementById("footerImport").import;
    el = content.querySelector('.footer');
    document.getElementById("footer").appendChild(el.cloneNode(true));
}, false);


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