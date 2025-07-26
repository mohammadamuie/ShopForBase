$('input.colorpicker-example').colorpicker({
    format: 'hex'
});
$('#BannerUrl').on('click', function () {
    ShowImage({
        inputFileId: 'BannerUrl',
        imageId: 'showimage'
    });
});

CKEDITOR.replace('Description', {
    language: 'fa'
});
$('.repeater-default').repeater({
    show: function () {
        $(this).slideDown();
    },
    hide: function (deleteElement) {
        Swal.fire({
            title: '',
            text: "آیا مطمئن به حذف هستید؟",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'بله',
            cancelButtonText: 'خیر',
            confirmButtonClass: 'btn btn-primary',
            cancelButtonClass: 'btn btn-danger ml-1',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                $(this).slideUp(deleteElement);
            }
        });
    }
});
$('#submit-form').on('click', function () {
    var form = document.getElementById('ajax-form');
    var formdata = new FormData(form);
    if (chooseImageBolb != null) {
        formdata.append('Image', chooseImageBolb, 'image.jpg');
    }
    let producttypeSwitch = document.getElementById('producttypeSwitch').checked;
    let ispriceSwitch = document.getElementById('ispriceSwitch').checked;
    formdata.set('TypeIsColor', producttypeSwitch);
    formdata.set('IsPrice', ispriceSwitch);
    formdata.set('Id', new URLSearchParams(window.location.search).get('Id'));
    formdata.set('Description', CKEDITOR.instances.Description.getData());
    ajaxPostForm({
        formData: formdata,
        form: form,
        triggerForm: false
    });
});

$('#Add_GaleyImage').on('click', function () {
    var formdata = new FormData();
    let Image = document.getElementById('GaleryUrl');
    if (Image.files.length > 0) {
        formdata.set('Image', Image.files[0]);
    }
    formdata.set('ProductId', new URLSearchParams(window.location.search).get('Id'));
    ajaxPostForm({
        formData: formdata,
        url: "/admin/product/addimagegalery",
        triggerForm: true
    }).then(function (resp) {
        // ایجاد المان تصویر
        var imgElement = document.createElement("img");
        imgElement.src = resp.data.url;
        imgElement.classList.add("img-fluid", "rounded");

        // ایجاد دیو محاط با تصویر
        var cardDiv = document.createElement("div");
        cardDiv.classList.add("card", "col-lg-4", "mb-3");

        // افزودن تصویر به دیو
        cardDiv.appendChild(imgElement);

        // ایجاد المان دیگر (لینک برای حذف تصویر)
        var deleteLink = document.createElement("a");
        deleteLink.href = "javascript:void(0)";
        deleteLink.onclick = function () {
            DeleteImageGalery(resp.data.id); // تابع حذف تصویر
        };
        deleteLink.classList.add("close");

        // ایجاد آیکون حذف
        var deleteIcon = document.createElement("i");
        deleteIcon.classList.add("fa", "fa-times-circle", "fa-4x");

        // افزودن آیکون حذف به لینک
        deleteLink.appendChild(deleteIcon);

        // ایجاد دیو دیگر (برای نمایش آیکون حذف)
        var overlayDiv = document.createElement("div");
        overlayDiv.classList.add("card-img-overlay", "d-flex", "justify-content-center", "mt-5");

        // افزودن لینک حذف به دیو
        overlayDiv.appendChild(deleteLink);

        // افزودن دیو نمایش آیکون حذف به دیو اصلی
        cardDiv.appendChild(overlayDiv);

        // افزودن دیو نهایی به صفحه
        document.getElementById("imageContainer").appendChild(cardDiv);

    });
    
});
function DeleteImageGalery(Id) {
    Swal.fire({
        text: "آیا مطمئن به حذف هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
        confirmButtonClass: 'btn btn-facebook',
        cancelButtonClass: 'btn btn-youtube ml-3',
        buttonsStyling: false,
    }).then(function (result) {
        if (result.value) {

            var formdata = new FormData();
            formdata.set('Id', Id);
            ajaxPostForm({
                formData: formdata,
                url: "/admin/product/deleteimagegalery",
                triggerForm: true
            });
            setTimeout(function () { location.reload() }, 1000);

        }
    });
}
$('#ispriceSwitch').on('change', function () {
    let checkswitch = document.getElementById('ispriceSwitch').checked;
    if (checkswitch) {
        document.getElementById('pills-producttype-tab').classList.add("disabled");
        document.getElementById('pills-producttype-tab').classList.add("bg-danger-light");
        document.getElementById('pills-producttype-tab').classList.add("text-white");
    } else {
        document.getElementById('pills-producttype-tab').classList.remove("disabled");
        document.getElementById('pills-producttype-tab').classList.remove("bg-danger-light");
        document.getElementById('pills-producttype-tab').classList.remove("text-white");
    }
});
$('#producttypeSwitch').on('change', function () {
    let checkswitch = document.getElementById('producttypeSwitch').checked;
    if (checkswitch) {
        document.getElementById('ispriceSwitch').checked = false;
        $(".producttypetext").html("بر اساس رنگ");
        $("input.color-palet").addClass("colorpicker-example");
        $("input.color-palet").attr("placeholder", "رنگ را وارد کنید");
        $('input.colorpicker-example').colorpicker({
            format: 'hex'
        });
    } else {
        document.getElementById('ispriceSwitch').checked = false;
        $(".producttypetext").html("بر اساس سایز");
        $("input.color-palet").removeClass("colorpicker-example colorpicker-element");
        $("input.color-palet").attr("placeholder", "سایز را وارد کنید");
        $('input.colorpicker-example').colorpicker({
            format: 'hex'
        });
    }
});