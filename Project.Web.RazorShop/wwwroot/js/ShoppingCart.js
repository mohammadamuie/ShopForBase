// تابع برای ذخیره آیدی و موجودی محصول در کوکی
function saveToCookie() {
    // ساخت یک رشته JSON با اطلاعات محصول
    var productInfo = JSON.stringify([{ id: 1, quantity: 3 }, { id: 2, quantity: 2 }]);

    // ذخیره اطلاعات در کوکی با نام "cart"
    document.cookie = "cart=" + encodeURIComponent(productInfo) + "; path=/";
}

// تابع برای خواندن اطلاعات از کوکی
function readFromCookie() {
    var name = "cart=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(';');

    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i];
        while (cookie.charAt(0) == ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) == 0) {
            // بازیابی اطلاعات محصول از کوکی و تبدیل آن به شیء JSON
            var cartInfo = JSON.parse(cookie.substring(name.length, cookie.length));
            return cartInfo;
        }
    }
    return null; // اگر کوکی یافت نشد
}

function addToCart(id, isPrice, type) {
    // خواندن تعداد از المان ورودی مرتبط
    var quantity = Number(document.getElementById("qty").value);
    var el = document.getElementById('size');
    var size;
    if (el != null) {

        size = el.options[el.selectedIndex].innerHTML;
    }

    var color = $('input[name=radio]:checked').val();
    var keyValue;

    if (isPrice == 'False') {
        if (type == 'True') {
            if (color == undefined) {
                toastr.warning('لطفا یک رنگ را انتخاب کنین!');
                return;
            } else {
                keyValue = color;
            }
        } else {
            if (size == 'سایز را انتخاب کنید') {
                toastr.warning('لطفا یک سایز را انتخاب کنین!');
                return;
            } else {
                keyValue = size;
            }
        }
    }



    // اطلاعات محصول را به شیء جدید اضافه کنید
    var newItem = {
        id: id,
        quantity: quantity,
        key: keyValue
    };

    // خواندن اطلاعات سبد خرید از کوکی (اگر وجود داشت)
    var cookieValue = getCookie('manoShoppingCart');
    console.log(newItem);
    console.log(cookieValue);
    var existingCart = [];
    try {
        existingCart = JSON.parse(cookieValue) || [];
    } catch (error) {
        console.error('Error parsing JSON from cookie:', error);
    }
    // چک کردن آیا محصول در سبد خرید وجود دارد یا نه
    var existingItem = existingCart.find(item => item.id === newItem.id && item.key === keyValue);

    if (existingItem) {
        // اگر محصول وجود داشت، نمایش هشدار وجو داشتن
        toastr.warning('محصول مورد نظر با این مشخصات در سبد خرید موجود میباشد!');
        return;
    } else {
        // اگر محصول وجود نداشت، به سبد خرید اضافه کنید
        existingCart.push(newItem);
    }


    var productInfo = JSON.stringify(existingCart);
    document.cookie = "manoShoppingCart=" + encodeURIComponent(productInfo) + "; path=/";

    toastr.success('محصول به سبد خرید اضافه شد!');

}


function removeFromCart(productId) {

    // خواندن اطلاعات سبد خرید از کوکی
    var cookieValue = getCookie('manoShoppingCart');
    var existingCart = [];
    try {
        existingCart = JSON.parse(cookieValue) || [];
    } catch (error) {
        console.error('Error parsing JSON from cookie:', error);
    }
    console.log(productId);
    console.log(existingCart);
    // یافتن ایندکس محصول در سبد خرید
    var itemIndex = existingCart.findIndex(item => item.id == productId);

    if (itemIndex !== -1) {
        // حذف محصول از سبد خرید با توجه به ایندکس یافته
        existingCart.splice(itemIndex, 1);

        // ذخیره مجدد اطلاعات سبد خرید در کوکی
        var productInfo = JSON.stringify(existingCart);
        document.cookie = "manoShoppingCart=" + encodeURIComponent(productInfo) + "; path=/";
        document.getElementById("product-item-" + productId).remove();
        toastr.success('محصول از سبد خرید حذف شد!');
    } else {
        toastr.warning('محصول در سبد خرید یافت نشد!');
    }
}

function updateQuantityInCookie(productId, changeAmount) {
    // خواندن اطلاعات سبد خرید از کوکی
    var cookieValue = getCookie('manoShoppingCart');
    var existingCart = [];

    try {
        existingCart = JSON.parse(cookieValue) || [];
    } catch (error) {
        console.error('Error parsing JSON from cookie:', error);
    }

    // یافتن ایندکس محصول در سبد خرید
    var itemIndex = existingCart.findIndex(item => item.id == productId);

    if (itemIndex !== -1) {
        // افزایش یا کاهش تعداد محصول با توجه به مقدار changeAmount
        existingCart[itemIndex].quantity = changeAmount;

        // بررسی حداقل و حداکثر تعداد مجاز
        var minQuantity = 1; // حداقل تعداد
        var maxQuantity = 10; // حداکثر تعداد

        existingCart[itemIndex].quantity = Math.min(maxQuantity, Math.max(minQuantity, existingCart[itemIndex].quantity));

        // ذخیره مجدد اطلاعات سبد خرید در کوکی
        var productInfo = JSON.stringify(existingCart);
        document.cookie = "manoShoppingCart=" + encodeURIComponent(productInfo) + "; path=/";
        // نمایش پیام
        toastr.success('تعداد محصول با موفقیت به‌روزرسانی شد!');
        location.reload();
    } else {
        toastr.warning('محصول در سبد خرید یافت نشد!');
    }
}

