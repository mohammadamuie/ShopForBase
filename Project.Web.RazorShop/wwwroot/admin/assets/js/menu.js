var table;
var deleted = false;
$(function () {
    $("body").addClass("sticky-page-header");
    $('.select2').select2({ placeholder: 'انتخاب کنید' });
    $('input[name="id"]').val(new URLSearchParams(window.location.search).get('id'));
    table = $('.ajax-data-table').DataTable({
        language: {
            url: '/admin/vendors/dataTable/fa.json'
        },
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        ordering: false,
        ajax: {
            url: '/admin/menuitem/getdata',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
            complete: function () {
            },
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'url' },
            { data: 'parent' },
            { data: 'childsCount' },
            { data: 'createdAtFormatted' },
            { data: 'id', responsivePriority: -1 },
        ],
        createdRow: function (row, data, index) {
            if (data['isActive'] == false) {
                $(row).addClass("table-danger");
                $(row).addClass("text-center");
            }
            $(row).addClass("text-center");
        },
        columnDefs: [{
            "render": function (data, type, row, meta) {
                return meta.row + 1;
            },
            "targets": 0
        }, {
            "render": function (data, type, row, meta) {
                return `<div class="text-center" dir="ltr"><a href="` + data + `" target="_blank" class="btn btn-outline-dark btn-sm">` + data + `</a></div>`;
            },
            "targets": 2
        }, {
            "render": function (data, type, row, meta) {
                if (data == null) {
                    return `<span class="badge badge-dark">ندارد</span>`;
                }
                return data;
            },
            "targets": 3
        },

        {
            "render": function (data, type, row, meta) {
                return `<span class="badge badge-dark">` + data + `</span>`;
            },
            "targets": -2
        }, {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                html += `<button onclick="EditItemModal(` + meta.row + `)" class="btn btn-sm btn-dark"><i class="fa fa-edit  "></i></button>`;
                if (row['isActive'] == true) {
                    html += `<button onclick="DeleteItem(` + data + `)" class="btn btn-youtube btn-sm"><i class="fa fa-trash mx-1"></i></button></div>`;
                } else {
                    html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-success btn-sm"><i class="fa fa-plus mx-1"></i></button></div>`;
                }
                return html;
            },
            "targets": -1
        }]
    });

    $('#dataTableSearchForm').on('submit', function (e) {
        e.preventDefault();
        table.draw('page');
    });

    $('.data-table-reset').on('click', function (e) {
        $('.select2').val(0).trigger('change');
        $("#dataTableSearchForm").trigger("reset");
        table.draw('page');
    });
});

function EditItemModal(id) {
    document.getElementById('editForm').reset();
    $('#editform-id').val(table.row(id).data().id);
    $('#editform-name').val(table.row(id).data().name);
    if (table.row(id).data().manualUrl == true) {
        $('#editform-manualUrl').val(table.row(id).data().url);
        
    } else {
        $('#editform-PagesUrl').val(table.row(id).data().url).trigger('change');
    }
    console.log(table.row(id).data().url);
    $('#editform-parentid').val(table.row(id).data().parentId).trigger('change');
    $('#EditItemModal').modal('show');
}

function DeleteItem(Id) {
    CallSwal('/admin/menuItem/disable?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {

    CallSwal('/admin/menuItem/activate?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        deleted = true;
    } else {
        deleted = false;
    }
    table.draw('page');
}