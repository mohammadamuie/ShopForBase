
'use strict';
$(function () {
    $("body").addClass("sticky-page-header");

    var tagsInputElementsselect2 = document.getElementsByClassName('select2');
    // بررسی آیا المنت با این کلاس وجود دارد یا نه
    if (tagsInputElementsselect2.length > 0) {
        $('.select2').select2({
            placeholder: 'انتخاب کنید',
        });
    }
    
    // یافتن المنت با کلاس tagsinput
    var tagsInputElements = document.getElementsByClassName('tagsinput');

    // بررسی آیا المنت با این کلاس وجود دارد یا نه
    if (tagsInputElements.length > 0) {
    $(".tagsinput").tagsinput('items');
    }
})
var chooseImageBolb = null;
var chooseCropper;
toastr.options = {
    timeOut: 2345,
    progressBar: true,
    showMethod: "slideDown",
    hideMethod: "slideUp",
    showDuration: 200,
    hideDuration: 200
};
function ShowImage({
    inputFileId = 'ImageFile',
    imageId = 'Image'
}) {
    $('#' + inputFileId).change(function () {
        var input = this;
        var url = $(this).val();
        var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#' + imageId).attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
        else {
            $('#' + inputFileId).attr('src', '/Uploads/tempimage.jpg');
        }
    });
}

async function ajaxPostForm({
    formId = 'ajax-form',
    formData = null,
    form = null,
    url = null,
    showSwal = false,
    showToaster = true,
    showLoading = true,
    triggerForm = true,
    triggerPage = false,
    triggertable = false,
}) {

    return new Promise((resolve, reject) => {

        let formurl;
        if (url == null) {
            formurl = form.action;
        } else {
            formurl = url;
        }
        $.ajax({
            type: 'POST',
            url: formurl,
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                if (showLoading) {
                    loading();
                }

                $("button").prop("disabled", true);
            },
            complete: function () {
                if (showLoading) {
                    swal.close();
                }
                $("button").prop("disabled", false);
            },
            success: function (data) {

                if (data.status == 1) {
                    if (data.message != null) {
                        if (showSwal) {
                            Swal.fire(
                                'پیام',
                                data.message,
                                'question',
                            )
                        } else {
                            if (showToaster) {
                                toastr.success(data.message);
                            }
                        }
                    } else if (data.errors != null) {
                        if (showSwal) {
                            Swal.fire(
                                'پیام',
                                data.errors,
                                'question',
                            )
                        } else {
                            if (showToaster) {
                                toastr.success(data.errors);
                            }
                        }
                    }
                    if (triggerForm) {
                        $('#' + formId).trigger("reset");
                    }
                    if (triggertable) {
                        table.draw();
                    }
                    if (triggerPage) {
                        location.reload();
                    }
                    if (data.returnUrl != null) {
                        location.replace(data.returnUrl);
                    }

                } else {
                    if (data.errors != null) {
                        if (showSwal) {
                            Swal.fire(
                                'پیام',
                                data.errors,
                                'question',
                            )
                        } else {
                            if (showToaster) {
                                toastr.error(data.errors);
                            }
                        }

                    } else if (data.message != null) {

                        if (showSwal) {
                            Swal.fire(
                                'پیام',
                                data.message,
                                'question',
                            )
                        } else {
                            if (showToaster) {
                                toastr.error(data.message);
                            }
                        }
                    }

                }
                resolve(data);
            },
            error: function (data) {

                if (data.responseJSON.errors != null) {
                    if (showSwal) {
                        Swal.fire(
                            'پیام',
                            data.responseJSON.errors,
                            'question',
                        )
                    } else {
                        if (showToaster) {
                            toastr.error(data.responseJSON.errors);
                        }
                    }

                } else if (data.responseJSON.message != null) {
                    if (showSwal) {
                        Swal.fire(
                            'پیام',
                            data.responseJSON.message,
                            'question',
                        )
                    } else {
                        if (showToaster) {
                            toastr.error(data.responseJSON.message);
                        }
                    }
                }
                console.log(data.responseJSON);
                reject(data);
            }
        })




    })
}

$('input.formatted-price').on('input', function () {
    // حذف همه کاراکترهای غیراز عددی
    var numericValue = this.value.replace(/\D/g, '');

    // تبدیل عدد به فرمت قیمت با جدا کردن سه رقم سه رقم
    var formattedValue = numericValue.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');

    // نمایش مقدار فرمت شده در اینپوت
    this.value = formattedValue;
});

function CallSwal(url, title) {
    Swal.fire({
        text: title,
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-facebook',
        cancelButtonClass: 'btn btn-youtube ml-3',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: url,
                beforeSend: function () { loading(); $("button").prop("disabled", true); },
                complete: function () { swal.close(); $("button").prop("disabled", false); },
                success: function (data) {
                    if (data.status == 1) {
                        if (data.message != null) {
                            toastr.success(data.message);
                        }
                    } else {
                        if (data.message != null) {
                            toastr.error(data.message);
                        }
                    }
                    table.draw('page');
                }
            });
        }
    });
    return false;
}


function formatPrice(number) {
    if (number.toString().length > 3) {
        let arr2 = [];
        let arr = number.toString().split("");

        let addedLength = 0;

        if (arr.length % 3 !== 0) {
            arr.unshift("0");

            addedLength += 1;

            if (arr.length % 3 !== 0) {
                arr.unshift("0");
                addedLength += 1;
            }
        }

        arr.forEach((item, index) => {
            if (index !== 0 && index % 3 === 0 && index !== arr.length - 1) {
                arr2.push(",");
                arr2.push(item);
            } else {
                arr2.push(item);
            }
        });

        if (addedLength > 0) arr2.splice(0, addedLength);

        let str = "";
        arr2.forEach((item) => {
            str += item.toString();
        });

        return str;
    } else {
        return number;
    }
}
