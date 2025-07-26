



toastr.options = {
    timeOut: 4345,
    progressBar: true,
    showMethod: "slideDown",
    hideMethod: "slideUp",
    showDuration: 200,
    hideDuration: 200
};

var table;
var isActive = true;
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
            url: '/admin/vendors/dataTable/fa.json'
        },
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        ordering: true,
        ajax: {
            url: '/admin/category/getdata',
            type: 'POST',
            data: function (d) {
                d['isActive'] = isActive;
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
            { data: 'parent' },
            { data: 'rote' },
            { data: 'childsCount' },
            { data: 'special' },
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
                return `<span class="badge bg-instagram mr-2 d-sm-inline d-none">` + data + `</span>`;
            },
            "targets": 2
        }, {
            "render": function (data, type, row, meta) {
                return `<div class="custom-control custom-switch custom-checkbox-warning mx-3">
                            <input type="checkbox" class="custom-control-input" id="specialcheck` + row.id + `" name="specialcheck` + row.id + `" onchange="Switchspecialcheck(` + row.id + `)"  ` + (data == true ? "checked" : "") + `>
                            <label class="custom-control-label" for="specialcheck` + row.id + `">نمایش ویژه</label>
                        </div>`;
            },
            "targets": 5
        }, {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                if (row['isActive'] == false) {
                    html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-success btn-sm"><i class="fa fa-retweet"></i></button>`;
                } else {
                    html += `<button onclick="DeleteItem(` + data + `)" class="btn btn-youtube btn-sm"><i class="fa fa-trash"></i></button>`;
                }
                html += `<a href="javascript:void(0)" onclick=(modalEditCreateCategory(` + meta.row + `)) class="btn text-white btn-dark btn-sm"><i class="fa fa-edit "></i></a>`;


                return html;
            },
            "targets": -1
        }]
    });

});

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        isActive = false;
    } else {
        isActive = true;
    }
    table.draw('page');
}


function DeleteItem(Id) {
    CallSwal('/admin/category/disable?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {
    CallSwal('/admin/category/activate?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}


function modalCreatCreateCategory() {
    document.getElementById('formCreate').reset();
    $('#MODALCreateCategory').modal();
}
function modalEditCreateCategory(rowId) {
    document.getElementById('formEdit').reset();
    document.getElementById('Name').value = table.row(rowId).data().name;
    document.getElementById('Name').value = table.row(rowId).data().name;
    if (table.row(rowId).data().image != null) {
        $('#showimageEdit').attr('src', table.row(rowId).data().image);
    } else {
        $('#showimageEdit').attr('src', '/admin/assets/media/image/portfolio-one.jpg');
    }

    $('#ParentId').val(table.row(rowId).data().parentId).trigger('change');
    typeId = table.row(rowId).data().id;
    $('#MODALEditCategory').modal();
}

$('#submit-formCreat').on('click', function () {
    let form = document.getElementById('formCreate');
    let formdata = new FormData(form);
    ajaxPostForm({
        formId: 'formCreate',
        formData: formdata,
        form: form,
        triggertable: true,
        url: '/admin/category/upsert',
        triggerForm: true
    });
});
$('#submit-formEdit').on('click', function () {
    let form = document.getElementById('formEdit');
    let formdata = new FormData(form);
    formdata.append('Id', typeId);
    ajaxPostForm({
        formId: 'formEdit',
        formData: formdata,
        form: form,
        triggertable: true,
        url: '/admin/category/upsert',
        triggerForm: false
    });

});


function Switchspecialcheck(Id) {
    CallSwal('/admin/category/Special?Id=' + Id, "آیا مطمئن به تغییر وضعیت نمایش ویژه هستید؟");
    table.draw('page');
}

$('#ImageAdd').on('click', function () {
    ShowImage({
        inputFileId: 'ImageAdd',
        imageId: 'showimageAdd'
    });
});

$('#ImageEdit').on('click', function () {
    ShowImage({
        inputFileId: 'ImageEdit',
        imageId: 'showimageEdit'
    });
});