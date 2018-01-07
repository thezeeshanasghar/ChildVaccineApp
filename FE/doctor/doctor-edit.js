$(document).ready(function () {
    if (GetDoctorIdFromUrlOrLocalStorage() != null)
        getbyID(GetDoctorIdFromUrlOrLocalStorage());
});
function GetDoctorIdFromUrlOrLocalStorage() {
    var id = parseInt(getParameterByName("id")) || 0;
    if (id != 0)
        return id;
    else {
        var id = localStorage.getItem('DoctorID');
        if (id)
            return id;
        else return 0;
    }
}
//Function for getting the Data Based upon ID  
function getbyID(ID) {
    $('#FirstName').css('border-color', 'lightgrey');
    $('#LastName').css('border-color', 'lightgrey');
    $('#DisplayName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#MobileNumber').css('border-color', 'lightgrey');
    $('#PMDC').css('border-color', 'lightgrey');

    $.ajax({
        url: SERVER + "doctor/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            if (!result.IsSuccess) {
                ShowAlert('Error', result.Message, 'danger');
            }
            else {
                $("#ID").val(result.ResponseData.ID);
                $("#Password").val(result.ResponseData.Password);
                $("#IsApproved").prop("checked", result.ResponseData.IsApproved);
                $('#FirstName').val(result.ResponseData.FirstName);
                $('#LastName').val(result.ResponseData.LastName);
                $('#DisplayName').val(result.ResponseData.DisplayName);
                $('#Email').val(result.ResponseData.Email);
                $('#MobileNumber').val(result.ResponseData.MobileNumber);
                $('#PhoneNo').val(result.ResponseData.PhoneNo);
                $('#PMDC').val(result.ResponseData.PMDC);
                $("#ShowPhone").prop("checked", result.ResponseData.ShowPhone);
                $("#ShowMobile").prop("checked", result.ResponseData.ShowMobile);
                var profileImageHtml = '';
                if (result.ResponseData.ProfileImage) {
                    profileImageHtml += '<img style="height: 44px;  width: 88px;border-radius: 5px;" src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/UserImages/" + result.ResponseData.ProfileImage + '"   />'
                } else {
                    profileImageHtml += '<img style="height: 44px;  width: 88px;border-radius: 5px;" src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/img/avatar.png" + '" />'
                }
                var signatureImageHtml = '';
                if (result.ResponseData.SignatureImage) {
                    signatureImageHtml += '<img  style="height: 44px;  width: 88px;border-radius: 5px;" src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/UserImages/" + result.ResponseData.SignatureImage + '" />'
                } else {
                    signatureImageHtml += '<img style="height: 44px;  width: 88px;border-radius: 5px;" src="' + SERVER_IP + ":" + SERVER_PORT + "/Content/img/avatar.png" + '" />'
                }
                $("#oldProfileImage").html(profileImageHtml);
                $("#oldSignatureImage").html(signatureImageHtml);

                HideAlert();
            }
          

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for updating record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
	
	$("#btnUpdate").button('loading');
    $("#btnUpdate").prop('disabled', true);
	
    var obj = {
        ID: $('#ID').val(),
        FirstName: $('#FirstName').val(),
        DisplayName: $("#DisplayName").val(),
        IsApproved: $("#IsApproved").is(":checked"),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
    };
    $.ajax({
        url: SERVER + "doctor/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var file = $("#ProfileImage").get(0).files;
            var file1 = $("#SignatureImage").get(0).files;
            if (file.length > 0 || file1.length > 0)
            {
                updateImages(result.ResponseData.ID);
            }
			$("#btnUpdate").prop('disabled', false);
            $("#btnUpdate").button('reset');
			
            ScrollToTop();
            ShowAlert('Success',"Updated sucessfully" , 'success');
        },
        error: function (errormessage) {
			
			$("#btnUpdate").prop('disabled', false);
            $("#btnUpdate").button('reset');
			
            alert(errormessage.responseText);
			
        }
    });
}

function updateImages(doctorId) {
    var data = new FormData();
    var file = $("#ProfileImage").get(0).files;
    var file1 = $("#SignatureImage").get(0).files;
    var dt = new Date();
    var date = dt.getDate() + "-" + dt.getMonth() + "-" + dt.getFullYear() + "_" + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    // Add the uploaded image content to the form data collection

    if (file.length > 0) {
        file[0].name = "ProfileImage_" + date + file[0].name;
        data.append("ProfileImage", file[0]);
    }
    if (file1.length > 0) {
        file1[0].name = "SignatureImage_" + date + file1[0].name;
        data.append("SignatureImage", file1[0]);
    }
    if (file.length > 0 || file1.length > 0) {
        $.ajax({
            type: "POST",
            url: SERVER + 'doctor/' + doctorId + '/update-images',    // CALL WEB API TO SAVE THE FILES.
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,         // PREVENT AUTOMATIC DATA PROCESSING.
            cache: false,
            data: data, 		        // DATA OR FILES IN THIS CONTEXT.
            success: function (data, textStatus, xhr) {

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ': ' + errorThrown);
            }
        });
    }

}
//Valdidation using jquery  
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}