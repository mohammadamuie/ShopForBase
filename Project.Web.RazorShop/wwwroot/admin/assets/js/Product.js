function truncateString(str, maxLength) {
    if (str.length > maxLength) {
        return str.substring(0, maxLength) + '...';
    } else {
        return str;
    }
}
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
            url: '/admin/product/getdata',
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
            { data: 'bannerUrl' },
            { data: 'title' },
            { data: 'categoryName' },
            { data: 'status' },
            { data: 'price' },
            { data: 'discount' },
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
        }, {
            "render": function (data, type, row, meta) {
                return `<a class="avatar-group image-popup" href="` + data + `">
                            <figure class="avatar ">
                                <img src="`+ data + `" class="rounded-circle">
                            </figure>
                        </a>`;
            },
            "targets": 1
        }, {
            "render": function (data, type, row, meta) {
                return truncateString(data, 28);;
            },
            "targets": 2
        }, {
            "render": function (data, type, row, meta) {
                return `<span class="badge bg-instagram mr-2 d-sm-inline d-none">` + data + `</span>`;
            },
            "targets": 3
        }, {
            "render": function (data, type, row, meta) {
                if (data == "رنگ") {
                    return `<span class="badge bg-dribbble mr-2 d-sm-inline ">براساس رنگ</span>`;
                } else if (data == "سایز") {
                    return `<span class="badge bg-linkedin mr-2 d-sm-inline ">براساس سایز</span>`;
                } else {
                    return `<span class="badge bg-twitter mr-2 d-sm-inline ">تک قیمت</span>`;
                }

            },
            "targets": 4
        }, {
            "render": function (data, type, row, meta) {


                if (data == null) {
                    return `<span class="badge badge-dark mr-2 d-sm-inline ">ندارد</span>`;
                } else {
                    return `<span class="badge bg-google mr-2 d-sm-inline ">` + data + `%</span>`;
                }


            },
            "targets": 6
        }, {
            "render": function (data, type, row, meta) {
                return `<div class="custom-control custom-switch custom-checkbox-warning mx-3">
                            <input type="checkbox" class="custom-control-input" id="specialcheck` + row.id + `" name="specialcheck` + row.id + `" onchange="Switchspecialcheck(` + row.id + `)"  ` + (data == true ? "checked" : "") + `>
                            <label class="custom-control-label" for="specialcheck` + row.id + `">نمایش ویژه</label>
                        </div>`;
            },
            "targets": 7
        }, {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                if (row['isActive'] == false) {
                    html += `<button onclick="RestoreItem(` + data + `)" class="btn btn-success btn-sm"><i class="fa fa-retweet"></i></button>`;
                } else {
                    html += `<button onclick="DeleteItem(` + data + `)" class="btn btn-youtube btn-sm"><i class="fa fa-trash"></i></button>`;
                }
                html += `<a href="/admin/product/edit?Id=` + data + `"  class="btn text-white btn-dark btn-sm"><i class="fa fa-edit "></i></a>`;


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

function Switchspecialcheck(Id) {
    CallSwal('/admin/product/Special?Id=' + Id, "آیا مطمئن به تغییر وضعیت پیشنهاد ویژه هستید؟");
    table.draw('page');
}


function DeleteItem(Id) {
    CallSwal('/admin/product/disable?Id=' + Id, "آیا مطمئن به غیرفعال کردن هستید؟");
}

function RestoreItem(Id) {
    CallSwal('/admin/product/activate?Id=' + Id, "آیا مطمئن به بازگردانی کردن هستید؟");
}
