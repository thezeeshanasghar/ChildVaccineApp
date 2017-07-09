//Load Data in Table when documents is ready  
$(document).ready(function () {
    var id = parseInt(getParameterByName("id")) || 0;
    loadData(id);
});

//Load Data function  
function loadData(id) {
    ShowAlert('Loading data', 'Please wait, fetching data from server', 'info');
    $.ajax({
        url: "/api/child/" + id + "/schedule",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.ResponseData, function (key, item) {
                var child = '';
                var dose = '';
                $.ajax({
                    url: "/api/child/" + item.ChildId,
                    typr: "GET",
                    contentType: "application/json;charset=UTF-8",
                    async: false,
                    dataType: "json",
                    success: function (r) {
                        child = r.ResponseData.Name
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
                $.ajax({
                    url: "/api/dose/" + item.DoseId,
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
                html += '<tr>';
                html += '<td>' + (key + 1) + '</td>';
                html += '<td>' + child + '</td>';
                html += '<td>' + dose + '</td>';
                html += '<td>' + item.Date + '</td>';
                html += '<td>' + item.Weight + '</td>';
                html += '<td>' + item.Height + '</td>';
                html += '<td>' + item.Circle + '</td>';
                html += '<td>' + item.Brand + '</td>';
                html += '<td>' +
                    '<a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | ' +
                    '<a href="#" onclick="Delele(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            HideAlert();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
