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
                $('#Email').val(result.ResponseData.Email);
                $('#MobileNumber').val(result.ResponseData.MobileNumber);
                $('#PhoneNo').val(result.ResponseData.PhoneNo);
                $('#PMDC').val(result.ResponseData.PMDC);
                $("#ShowPhone").prop("checked", result.ResponseData.ShowPhone);
                $("#ShowMobile").prop("checked", result.ResponseData.ShowMobile);
                $("#ConsultationFee").val(result.ResponseData.ConsultationFee);
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
        IsApproved: $("#IsApproved").is(":checked"),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        MobileNumber: $('#MobileNumber').val(),
        PMDC: $('#PMDC').val(),
        PhoneNo: $("#PhoneNo").val(),
        ShowPhone: $("#ShowPhone").is(":checked"),
        ShowMobile: $("#ShowMobile").is(":checked"),
        Password:$("#Password").val(),
        ConsultationFee: $("#ConsultationFee").val()
    };
    $.ajax({
        url: SERVER + "doctor/" + $('#ID').val(),
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
			
			$("#btnUpdate").prop('disabled', false);
            $("#btnUpdate").button('reset');
			
            //loadData();
            //clearTextBox();
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
//Valdidation using jquery  
function validate() {
    $('#form1').validator('validate');
    var validator = $('#form1').data("bs.validator");
    if (validator.hasErrors())
        return false;
    else
        return true;
}