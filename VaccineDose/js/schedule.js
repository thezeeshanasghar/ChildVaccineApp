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
            $.each(result.ResponseData, function (key, item) {

                var dose = '';

                $.ajax({
                    url: SERVER + "dose/" + item.DoseId,
                    typr: "GET",
                    contentType: "application/json;charset=UTF-8",
                    async: false,
                    dataType: "json",
                    success: function (r) {
                        dose = r.ResponseData.Name
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
                if (item.Date in map) {
                    map[item.Date].push(dose);
                } else {
                    map[item.Date] = [];
                    map[item.Date].push(dose);
                }
            });

            for (var key in map) {
                console.log(key);
                html += "<h3 style='text-align:center'>" + key + "</h3>";
                var arr = map[key];
                for (var index in arr) {
                    html += '<div class="col-lg-12" style="background-color:rgb(240, 240, 240);border-radius:4px;margin-bottom: 8px;border:1px solid black;">';
                    html += '<div class="col-md-1">' +
                                            '</div>';
                    html += '<div class="col-md-6" style="padding:10px;">';
                    html += '<p><h3>' + arr[index] + '</h3>';
                    html += '</div></div> ';
                    console.log('\t' + arr[index]);
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
