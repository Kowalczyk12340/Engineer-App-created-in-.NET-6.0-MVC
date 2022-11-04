var dataTable;

$(document).ready(function () {
    loadDataTable();

    $(function () {
        $("#btnSubmit").click(function () {
            $("input[name='GridHtml']").val($("#tblData").html());
        });
    });
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/Commodity/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "longDesc", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center">
                            <a href="/Admin/Commodity/AddOrUpdate/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Edytuj
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;' onclick=Delete('/admin/Commodity/Delete/'+${data})>
                               <i class='far fa-trash-alt'></i> Usuń
                            </a></div>
                        `;
                }, "width": "30%"
            }


        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Czy na pewno chcesz usunąć ten towar?",
        text: "Po usunięciu nie możesz przywrócić tej zawartości!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DE3M39",
        confirmButtonText: "Tak, usuń to!",
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}