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
            url: '/admin/Pages/getdata',
            type: 'POST',
            data: function (d) {
                d['deleted'] = deleted;
            },
            complete: function () {
                $('.image-popup').magnificPopup({
                    type: 'image',
                    zoom: {
                        enabled: true,
                        duration: 300,
                        easing: 'ease-in-out',
                        opener: function (openerElement) {
                            return openerElement.is('img') ? openerElement : openerElement.find('img');
                        }
                    }
                });
            },
        },
        columns: [
            { data: 'id' },
            { data: 'title' },
            { data: 'url' },
            { data: 'createdAtFormatted' },
            { data: 'updatedAtFormatted' },
            { data: 'id', responsivePriority: -1 },
        ],
        createdRow: function (row, data, index) {
            if (data['isActive'] == false) {
                $(row).addClass("table-danger");
            }
        },
        columnDefs: [{
            "render": function (data, type, row, meta) {
                return meta.row + 1;
            },
            "targets": 0
        }, {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                html += `<a href="/admin/pages/Update?Id=` + data + `" class="btn btn-sm btn-dark text-white"><i class="fa fa-edit  "></i></a>`;
                if (row['isActive'] == false) {
                    html += `<button onclick="RestoreItem('` + data + `')" type="button" class="btn btn-success btn-sm"><i class="fa fa-check"></i></button></div>`;
                } else {
                    html += `<button onclick="DeleteItem('` + data + `')" type="button" class="btn btn-youtube btn-sm"><i class="fa fa-times"></i></button></div>`;
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
function DeleteItem(Id) {
    CallSwal('/admin/pages/disable?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {
    CallSwal('/admin/pages/activate?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        deleted = true;
    } else {
        deleted = false;
    }
    table.draw('page');
}

