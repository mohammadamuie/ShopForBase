$("#rangeSlider-create").ionRangeSlider({
    min: 0,
    max: 100,
    from: 0,
    skin: "round"
});
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
            url: '/admin/discountcode/getdata',
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
            { data: 'code' },
            { data: 'discountCodeType' },
            { data: 'discount' },
            { data: 'description' },
            { data: 'expireTime' },
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
        }, {
            "render": function (data, type, row, meta) {
                return `<span class="badge bg-instagram mr-2 d-sm-inline d-none">` + data + `</span>`;
            },
            "targets": 2
        }, {
            "render": function (data, type, row, meta) {
                return `%` + data;
            },
            "targets": 3
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
    CallSwal('/admin/discountcode /disable?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {
    CallSwal('/admin/discountcode/activate?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}


$('#openModelUpsertDiscountCode').on('click', function () {
    document.getElementById('formUpsert').reset();
    $('#MODALUpsertDisCountCode').modal();
});

function modalEditCreateCategory(rowId) {
    document.getElementById('formUpsert').reset();
    document.getElementById('Code').value = table.row(rowId).data().code;
    document.getElementById('Id').value = table.row(rowId).data().id;
    document.getElementById('ExpireTime').value = table.row(rowId).data().expireTime;
    document.getElementById('Description').value = table.row(rowId).data().description;

    var instance = $("#rangeSlider-create").data("ionRangeSlider");

    instance.update({
        from: table.row(rowId).data().discount 
    });

    if (table.row(rowId).data().discountCodeTypeEnum == 1) {
        $("input[name=DiscountCodeType][value=ForUser]").attr('checked', 'checked');
        $("#ProductIdDiv").addClass("d-none");
        $("#UserIdDiv").removeClass("d-none");
        $("#UserId").val(table.row(rowId).data().userId).change();
        $("#ProductId").val("").change();
    } else if (table.row(rowId).data().discountCodeTypeEnum == 0) {
        $("input[name=DiscountCodeType][value=ForProduct]").attr('checked', 'checked');
        $("#ProductIdDiv").removeClass("d-none");
        $("#UserIdDiv").addClass("d-none");
        $("#ProductId").val(table.row(rowId).data().productId).change();
        $("#UserId").val("").change();
    } else if (table.row(rowId).data().discountCodeTypeEnum == 2) {
        $("input[name=DiscountCodeType][value=ForPeoductAndUser]").attr('checked', 'checked');
        $("#ProductIdDiv").removeClass("d-none");
        $("#UserIdDiv").removeClass("d-none");
        $("#UserId").val(table.row(rowId).data().userId).change();
        $("#ProductId").val(table.row(rowId).data().productId).change();
    } else {
        $("input[name=DiscountCodeType][value=DiscountForAll]").attr('checked', 'checked');
        $("#ProductIdDiv").addClass("d-none");
        $("#UserIdDiv").addClass("d-none");

        $("#UserId").val("").change();
        $("#ProductId").val("").change();
    }

    $('#ParentId').val(table.row(rowId).data().parentId).trigger('change');
    typeId = table.row(rowId).data().id;
    $('#MODALUpsertDisCountCode').modal();
}

$('#submit-formUpsert').on('click', function () {
    let form = document.getElementById('formUpsert');
    let formdata = new FormData(form);
    ajaxPostForm({
        formId: 'formUpsert',
        formData: formdata,
        form: form,
        triggertable: true,
        url: '/admin/discountcode/upsert',
        triggerForm: true
    });
});

$('#ForUser').on('click', function () {
    $("#ProductIdDiv").addClass("d-none");
    $("#UserIdDiv").removeClass("d-none");
    $("#UserId").val("").change();
    $("#ProductId").val("").change();
});

$('#ForProduct').on('click', function () {
    $("#ProductIdDiv").removeClass("d-none");
    $("#UserIdDiv").addClass("d-none");
    $("#UserId").val("").change();
    $("#ProductId").val("").change();
});

$('#ForPeoductAndUser').on('click', function () {
    $("#ProductIdDiv").removeClass("d-none");
    $("#UserIdDiv").removeClass("d-none");
    $("#UserId").val("").change();
    $("#ProductId").val("").change();
});

$('#DiscountForAll').on('click', function () {
    $("#ProductIdDiv").addClass("d-none");
    $("#UserIdDiv").addClass("d-none");
    $("#UserId").val("").change();
    $("#ProductId").val("").change();
});