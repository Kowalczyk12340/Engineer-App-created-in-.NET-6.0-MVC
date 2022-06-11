var dataTable;

$(document).ready(function ()
{
    var url = window.location.search;
    if (url.includes("approved"))
    {
        loadDataTable("GetAllApprovedOrders");
    }
    else
    {
        if (url.includes("pending"))
        {
            loadDataTable("GetAllPendingOrders");
        }
        else
        {
            loadDataTable("GetAllOrders");
        }

    }

    $(function () {
        $("#btnSubmit").click(function () {
            $("input[name='GridHtml']").val($("#tblData").html());
        });
    });
});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "status", "width": "15%" },
            { "data": "timeToOrder", "width": "10%" },
            { "data": "timeToRealisation", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Szczegóły
                                </a>
                            </div>
                            `;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}