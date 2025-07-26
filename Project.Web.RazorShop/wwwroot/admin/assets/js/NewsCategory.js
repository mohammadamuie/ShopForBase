
toastr.options = {
    timeOut: 4345,
    progressBar: true,
    showMethod: "slideDown",
    hideMethod: "slideUp",
    showDuration: 200,
    hideDuration: 200
};

var table;
var deleted = true;
let formUrl;
let typeId;
let userId;
let userName;
let rowIdCustomer;
$(function () {
    $("body").addClass("sticky-page-header");
    $('.select2').select2({ placeholder: 'انتخاب کنید' });
    $('input[name="id"]').val(new URLSearchParams(window.location.search).get('id'));
    table = $('.ajax-data-table').DataTable({
        language: {
            url: '/Admin/vendors/dataTable/fa.json'
        },
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        ordering: false,
        ajax: {
            url: '/Admin/NewsCategory/GetData',
            type: 'POST',
            data: function (d) {
                d['IsActive'] = deleted;
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
            { data: 'name' },
            { data: 'newsCount' },
            { data: 'createdAt' },
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
        },
        {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                if (row['isActive'] == false) {
                    html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-success btn-sm"><i class="fa fa-retweet"></i></button>`;
                } else {
                    html += `<button onclick="DeleteItem(` + data + `)" class="btn btn-youtube btn-sm"><i class="fa fa-trash"></i></button>`;
                }
                html += `<a href="javascript:;" onclick=(ModalUpsertEdit(` + meta.row + `)) class="btn text-white btn-dark btn-sm"><i class="fa fa-edit "></i></a>`;


                return html;
            },
            "targets": -1
        }]
    });

});

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        deleted = false;
    } else {
        deleted = true;
    }
    table.draw('page');
}



function ModalUpsertCreate() {
    document.getElementById('ajax-form').reset();
    $('#Upsert').modal();
}
function ModalUpsertEdit(rowId) {
    document.getElementById('ajax-form').reset();
    document.getElementById('Id').value = table.row(rowId).data().id;
    $('#Language').val(table.row(rowId).data().language).trigger('change');
    document.getElementById('Name').value = table.row(rowId).data().name;
    $('#Upsert').modal();
}

$('#submit-form').on('click', function () {
    let form = document.getElementById('ajax-form');
    let formdata = new FormData(form);
    ajaxPostForm({
        formId: 'ajax-form',
        formData: formdata,
        form: form,
        triggertable: true,
        url: '/Admin/NewsCategory/upsert',
        triggerForm: true
    });
});
function DeleteItem(Id) {
    CallSwal('/Admin/news/disableNews?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {
    CallSwal('/Admin/news/activateNews?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}
