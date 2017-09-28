//Load Data in Table when documents is ready  
$(document).ready(function () {

    if (GetChildMobileNumberFromLocalStorage() != 0) {

        loadChildDataAgainstMobileNumber();
    }

});
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
function GetChildMobileNumberFromLocalStorage() {
    var mobileNumber = localStorage.getItem('MobileNumber');
    return mobileNumber;
}

