//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: SERVER + "child/" + id + "/schedule",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            var map = {};
            var obj = {
                dose: '',
                ID: 0
            };
            $.each(result.ResponseData, function (key, item) {
                var obj = {
                    dose: '',
                    ID: 0
                };
                obj.ID = item.ID
                $.ajax({
                    url: SERVER + "dose/" + item.DoseId,
                    typr: "GET",
                    contentType: "application/json;charset=UTF-8",
                    async: false,
                    dataType: "json",
                    success: function (r) {
                        obj.dose = r.ResponseData.Name
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
                if (item.Date in map) {
                    map[item.Date].push(obj);
                } else {
                    map[item.Date] = [];
                    map[item.Date].push(obj);
                }
            });

            for (var key in map) {
                console.log(key);
                html += "<h3 style='text-align:center'>" + key + "</h3>";
                var arr = map[key];
                for (var index in arr) {
                    html += '<a href="#" onclick="return getbyID(' + arr[index].ID + ')">';
                    html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';
                    html += '<div class="col-md-1">' +
                                            '</div>';
                    html += '<div class="col-md-6" style="padding:10px;">';
                    html += '<p><h3>' + arr[index].dose + '</h3>';
                    html += '</div></div> </a>';
                    console.log('\t' + arr[index].dose);
                }

            }
            $('#schedule').html(html);
            HideAlert();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
function getbyID(ID) {
    $("#ID").val(ID);
    $.ajax({
        url: SERVER + "schedule/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#Weight").val(result.ResponseData.Weight),
             $("#Height").val(result.ResponseData.Height),
            $("#Circumference").val(result.ResponseData.Circle),
           $("#Brand").val(result.ResponseData.Brand)
            $('#myModal').modal('show');
            $('#btnUpdate').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var obj = {
        ID:$("#ID").val(),
        Weight:$("#Weight").val(),
        Height:$("#Height").val(),
        Circle: $("#Circumference").val(),
        Brand:$("#Brand").val(),
        IsDone:"true",
    }
    $.ajax({
        url: SERVER + "schedule/child-schedule/",
        data: JSON.stringify(obj),
        type: "PUT",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
          $("#Weight").val(""),
          $("#Height").val(""),
          $("#Circumference").val(""),
         $("#Brand").val("")
             $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

