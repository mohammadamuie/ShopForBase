
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
let statusFilterValue;
let rowIdCustomer;
$(function () {
    $("body").addClass("sticky-page-header");
    $('input[name="isToday"]').val(new URLSearchParams(window.location.search).get('isToday'));
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
            url: '/Admin/PurchaseRequest/GetData',
            type: 'POST',
            data: function (d) {
                d['IsActive'] = deleted;
                if (new URLSearchParams(window.location.search).get('isToday') != null) {
                    d['IsToday'] = new URLSearchParams(window.location.search).get('isToday');
                }
                d['Status'] = statusFilterValue;
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
            { data: 'user.firstName' },
            { data: 'finalPrice' },
            { data: 'sendPrice' },
            { data: 'createdAtFormatted' },
            { data: 'code' },
            { data: 'purchaseRequestStatus' },
            { data: 'isPaid' },
            { data: 'id' },
            { data: 'id', responsivePriority: -1 },
        ],
        createdRow: function (row, data, index) {
            if (data.purchaseRequestStatus == 0) {
                $(row).addClass("bg-light-info-cutome");
            } else if (data.purchaseRequestStatus == 1) {
                $(row).addClass("bg-light-warning-cutome");
            } else if (data.purchaseRequestStatus == 2) {
                //$(row).addClass("bg-light-primary-cutome");
                $(row).addClass("bg-light-secondary-cutome");
            } else if (data.purchaseRequestStatus == 3) {
                $(row).addClass("bg-light-success-cutome");
            } else if (data.purchaseRequestStatus == 4) {
                $(row).addClass("bg-light-danger-cutome");
            } else if (data.purchaseRequestStatus == 5) {
                $(row).addClass("bg-light-danger-cutome");
            } else if (data.purchaseRequestStatus == 6) {
                $(row).addClass("bg-light-danger-cutome");
            } else if (data.purchaseRequestStatus == 7) {
                $(row).addClass("bg-light-primary-cutome");
            }
        },
        columnDefs: [{
            "render": function (data, type, row, meta) {
                return meta.row + 1;
            },
            "targets": 0
        }, {
            "render": function (data, type, row, meta) {
                var html = `<span>` + row.user.firstName + " " + row.user.lastName + `</span>`;
                html += `<p>` + row.user.phoneNumber + `</p>`;
                return html;
            },
            "targets": 1
        }, {
            "render": function (data, type, row, meta) {
                if (data == -1) {
                    return `<span class="badge badge-dark">ندارد</span>`;
                } else {
                    let html = `<span class="">` + data.toLocaleString() + ` <small>تومان</small></span>`;
                    return html;
                }
            },
            "targets": 2
        }, {
            "render": function (data, type, row, meta) {
                if (data == -1) {

                    return `<span class="badge badge-dark">ندارد</span>`;
                } else if (data == 0) {

                    return `<del class="text-danger">` + row.distancePrice.toLocaleString() + `</del><br/><span>0 تومان</span>`;
                } else {

                    return `<span class="">` + data.toLocaleString() + ` <small>تومان</small></span>`;
                }
            },
            "targets": 3
        }, {
            "render": function (data, type, row, meta) {
                return `<span class="badge badge-dark">` + data + `</span>`;

            },
            "targets": 4
        }, {
            "render": function (data, type, row, meta) {
                var html = "";
                if (data == 0) {
                    html += `<span class="badge badge-info">در انتظار تایید</span>`;
                } else if (data == 1) {
                    html += `<span class="badge badge-warning">در حال آماده سازی</span>`;
                } else if (data == 2) {
                    html += `<span class="badge badge-primary">در حال ارسال</span>`;

                } else if (data == 3) {
                    html += `<span class="badge badge-secondary">تحویل شده</span>`;
                } else if (data == 4) {
                    html += `<span class="badge badge-success">لغو سفارش</span>`;
                } else if (data == 5) {
                    html += `<span class="badge badge-danger">لغو سفارش توسط ادمین</span>`;
                } else if (data == 6) {
                    html += `<span class="badge badge-danger">ناموفق</span>`;
                } else if (data == 7) {
                    html += `<span class="badge badge-danger">آرشیو</span>`;
                }
                return html;
            },
            "targets": 6
        }, {
            "render": function (data, type, row, meta) {
                if (data) {

                    return `<span class="badge badge-success my-2">پرداخت شده</span>`;
                } else {

                    return `<span class="badge badge-danger my-2">پرداخت نشده</span>`;
                }
            },
            "targets": 7
        }, {
            "render": function (data, type, row, meta) {
                let html = `
<div class="shop-list" style="padding:0 !important">
<table class="shop-list-table">
                    <thead >
                        <tr style="border-bottom: 2px solid black !important; ">
                            <th style="border-left: 2px solid black; width:45%">
                                <button>
                                    نام کالا
                                </button>
                            </th>
                            <th >
                                <button >
                                    کد های کالا
                                </button>
                            </th>

                        </tr>
                    </thead>
                    <tbody>`;

                if (row.cartItems != null) {

                    if (row.cartItems.length > 0) {
                        row.cartItems.forEach(cartItem => {
                            html += `<tr style="border-bottom: 1px solid black !important; " >
                                                <td>
                                                <figure class="avatar">
                                                    <img src="${cartItem.product.bannerImage}" class="rounded" alt="image">
                                                </figure>
                                                    <a href="/product/details?Url=${cartItem.product.url}" target="_blanck">عنوان: ${cartItem.product.title}
                                                     </a>
                                                </td>
                                                <td>
                                                    <button>

                                      ${cartItem.count} عدد 
                                    | ${cartItem.price.toLocaleString()} تومان
                                    ${cartItem.product.isPrice == false ? (cartItem.product.typeIsColor == true ? ` | رنگ: <span class="fa fa-circle  mr-1" style="color: ${cartItem.type} !important;"></span>` : ` | سایز: ` + cartItem.type) : ""}
                                                    </button>

                                                </td>
                                            </tr>`;
                        })
                    }
                }
                html += `
                    </tbody>
                </table></div>
<!-- section 6  -->
            <div class="shop-payment">
                <div class="shop-payment-item pt-2 pb-2">
                    <button class="secondDiv">   ${row.postKey == null ? "ثبت نشده " : row.postKey }  </button>
                    <button >کدپیگیری مرسوله پستی: </button>
                </div>
                <div class="shop-payment-item pt-2 pb-2">
                    <button class="secondDiv">   ${row.sendPrice.toLocaleString()} تومان </button>
                    <button >هزینه ارسال: </button>
                </div>
                  <div class="shop-payment-item pt-2 pb-2">
                      <button class="secondDiv">  ${row.user.firstName} ${row.user.lastName} </button>
                    <button >نام و نام خانوادگی : </button>
                 </div>
                 <div class="shop-payment-item pt-2 pb-2">
                      <button class="secondDiv">  ${row.addressAdmin != null ? row.addressAdmin : (row.adress.toLocaleString() == null ? "نامشخص" : row.adress.toLocaleString())} </button>
                    <button >آدرس : </button>
                 </div> 

                <div class="shop-payment-item pt-2 pb-2">
                    <button class="secondDiv">  ${row.finalPrice.toLocaleString()}  تومان</button>
                    <button >جمع کل فاکتور: </button>
                </div>
            </div>
`;

                return html;
            },
            "targets": 8
        }, {
            "render": function (data, type, row, meta) {
                let html = `<div class="btn-group" role="group">`;
                html += `<button onclick="ChangeToCakeStatus(${data},'CancelledByAdmin')" title="لغو سفارش توسط ادمین" data-toggle="tooltip" class="btn btn-youtube "><i class="fa fa-ban"></i></button>`;

                html += `<a style="background:#ccd8a2" href="/admin/purchaserequest/detail?Id=` + data + `" class="btn  text-white " title="جزئیات سفارش" data-toggle="tooltip"><i class="ti-info "></i></a>`;

                if (row.purchaseRequestStatus == 0) {
                    html += `<button style="background:#99d499" class="btn  text-white"  onclick="ChangeToCakeStatus(${data},'AcceptedByAdmin_CakeIsBeingBaked')" data-kt-docs-table-action="edit_row" data-bs-toggle="tooltip" title="تایید سفارش و تغییر به درحال آماده سازی" data-index="${meta.row}"><i class="ti-check"></i></button>`;
                }
                if (row.purchaseRequestStatus == 1) {

                    html += `<a style="background:orange" href="javascript:void(0)" onclick="changeStatuspreparing(${data})" class="btn  text-white" data-bs-toggle="tooltip" title="آماده سازی و تغییر به در حال ارسال" data-index="${meta.row}"><i class="ti-truck"></i></a>`;
                }
                if (row.purchaseRequestStatus == 2) {
                    html += `<a style="background:#dcacdc" href="javascript:void(0)" onclick="ChangeToCakeStatus(${data},'Delivered')" class="btn  text-white" data-bs-toggle="tooltip" title="سفارش ارسال شده و تغییر به تکمیل شده" data-index="${meta.row}"><i class="ti-flag"></i></a>`;
                }

                html += `<a style="background:#8585bb" href="javascript:void(0)"  onclick="changeStatusDynamic(${data})" class="btn  text-white "><svg viewBox="0 0 24 24" width="24" height="24" stroke="currentColor" stroke-width="2" fill="none" stroke-linecap="round" stroke-linejoin="round" class="css-i6dzq1"><polyline points="23 4 23 10 17 10"></polyline><polyline points="1 20 1 14 7 14"></polyline><path d="M3.51 9a9 9 0 0 1 14.85-3.36L23 10M1 14l4.64 4.36A9 9 0 0 0 20.49 15"></path></svg></a>`;


                return html;
            },
            "targets": -1
        }]
    });

});

$('#statusFilter').change(function () {
    statusFilterValue = $(this).val();
    table.draw('page');
});

function SwitchDeleted() {
    if ($('#showdelted:checked').length > 0) {
        isActive = false;
    } else {
        isActive = true;
    }
    table.draw('page');
}



function ChangeToCakeStatus(Id, status) {

    Swal.fire({
        text: "آیا مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-facebook',
        cancelButtonClass: 'btn btn-youtube ml-3',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {

            let formdata = new FormData();
            formdata.set("Id", Id);
            formdata.set("Status", status);
            ajaxPostForm({
                formData: formdata,
                url: '/Admin/PurchaseRequest/ChangeStatus',
                triggertable: true
            });
        }
    });
}


function changeStatuspreparing(Id) {

    Swal.fire({
        text: "",
        icon: "question",
        showCancelButton: true,
        html: ` 
<div class="form-group">
                                    <label>کدپیگیری مرسوله پستی</label><label class="text-danger mr-1">*</label>
                                    <input id="statusReq" class="form-control" required />
                                </div>`,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-facebook btn-lg',
        cancelButtonClass: 'btn btn-youtube btn-lg ml-3',
        buttonsStyling: false,
        preConfirm: () => {
            const statusReq = Swal.getPopup().querySelector('#statusReq').value
            return { statusReq: statusReq }
        }
    }).then(function (result) {
        if (result.value) {
            let formdata = new FormData();
            console.log(Id);
            formdata.set("Id", Id);
            formdata.set("Status", 'preparing');
            formdata.set("Value", result.value.statusReq);

            ajaxPostForm({
                formData: formdata,
                url: '/Admin/PurchaseRequest/ChangeStatus',
                triggertable: true
            });
        }
    });
}

function changeStatusDynamic(Id) {

    Swal.fire({
        text: "آیا مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        html: ` 
<span>وضعیت مورد نظر را انتخاب کنید</span>
<select class="form-control select2" id="statusReq" >
<option value="PaymentCompeleted_WaitForAdminConfirmation">در انتظار تایید</option>
                            <option value="AcceptedByAdmin_CakeIsBeingBaked">در حال آماده سازی</option>
                            <option value="preparing">در حال ارسال</option>
                            <option value="Delivered">تکمیل سفارش</option>
                            <option value="CancelledByAdmin">لغو سفارش توسط ادمین</option>
                            </select>`,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-facebook btn-lg',
        cancelButtonClass: 'btn btn-youtube btn-lg ml-3',
        buttonsStyling: false,
        preConfirm: () => {
            const statusReq = Swal.getPopup().querySelector('#statusReq').value
            return { statusReq: statusReq }
        }
    }).then(function (result) {
        if (result.value) {
            let formdata = new FormData();
            console.log(Id);
            formdata.set("Id", Id);
            formdata.set("Status", result.value.statusReq);

            ajaxPostForm({
                formData: formdata,
                url: '/Admin/PurchaseRequest/ChangeStatus',
                triggertable: true
            });
        }
    });
}

