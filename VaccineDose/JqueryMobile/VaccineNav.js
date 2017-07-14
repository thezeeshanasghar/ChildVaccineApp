//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({

         url: "http://vac.afz-sol.com/api/vaccine",
        type: "GET",
        contentType: "application/json;charset=utf-8",
       dataType: "json",
       success: function (result) {
             var html = '';
            $.each(result.ResponseData, function (key, item) {

                html += '<tr>';
                    html += '<td>' + (key + 1) + '</td>';
                    html += '<td>' + item.Name + '</td>';
                    html += '<td>' + item.MinAge + '</td>';
                    html += '<td>' + item.MaxAge + '</td>';
                html += '</tr>';
            });
            //$("#table-column-toggle-popup-popup").remove();
            $('#tbody').html(html).enhanceWithin();
            $("#t tbody").closest("table#t").table('refresh').trigger('create');
           
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
 