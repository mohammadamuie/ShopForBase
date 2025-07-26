var interval;

$('form').submit(function (e) {

    e.preventDefault();
    var form = $(this);
    var url = form.attr('action');
    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(),
        beforeSend: function () {
            $("#btnSubmit").html(`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span><span>درحال بررسی</span>`);
            $("button").prop("disabled", true);
        },
        complete: function () {
            $("#btnSubmit").html(`ورود`);
            $("button").prop("disabled", false);
        },
        success: function (data) {

            if (data.status == 1) {
                if (data.message != null) {
                    toastr.success(data.message);
                    setTimeout(
                        function () {
                            location.href = "/customer";
                        }, 1000);
                } else if (data.errors != null) {

                    toastr.success(data.errors);
                }
            } else {
                if (data.errors != null) {
                    toastr.error(data.errors);
                } else if (data.message != null) {
                    toastr.error(data.message);
                }
            }
        },
        error: function (data) {
            if (data.responseJSON.errors != null) {
                toastr.error(data.responseJSON.errors);
            } else if (data.responseJSON.message != null) {
                toastr.error(data.responseJSON.message);
            }
            console.log(data.responseJSON);
        }
    });
});

$(document).ready(function () {
    countdown();
});
function countdown() {
    clearInterval(interval);
    interval = setInterval(function () {
        var minutes = Math.floor(time / 60);
        var seconds = time - minutes * 60;
        if (minutes < 0) return;
        else if (seconds < 0 && minutes != 0) {
            minutes -= 1;
            seconds = 59;
        }
        else if (seconds < 10 && length.seconds != 2) seconds = '0' + seconds;

        $('.js-timeout').html(minutes + ':' + seconds);
        time = time - 1;
        if (minutes == 0 && seconds == 0) {
            clearInterval(interval);
            let div = document.getElementById('js-timeout-div');
            div.innerHTML = "";
            let newlink = document.createElement('a');
            newlink.setAttribute('href', 'javascript:void(0)');
            newlink.setAttribute('class', 'text-danger');
            newlink.setAttribute('onclick', 'SendCode()');
            newlink.setAttribute('id', 'submit-SendCode');
            div.appendChild(newlink);
            document.getElementById("submit-SendCode").innerHTML = 'دریافت مجدد کد <i class="icon-long-arrow-left"></i>';

        }
    }, 1000);
}
function SendCode() {
    let formdata = new FormData();
    let phone = document.getElementById("PhoneNumber").value;
    formdata.append("PhoneNumber", phone);
    $.ajax({
        type: "POST",
        url: '/account/signIn',
        data: formdata,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $("button").prop("disabled", true);
        },
        complete: function () {
            $("button").prop("disabled", false);
        },
        success: function (data) {

            if (data.status == 1) {
                if (data.message != null) {
                    toastr.success("پیامک ارسال شد");
                    setTimeout(
                        function () {
                            location.reload();
                        }, 1000);
                } else if (data.errors != null) {

                    toastr.success(data.errors);
                }
            } else {
                if (data.errors != null) {
                    toastr.error(data.errors);
                } else if (data.message != null) {
                    toastr.error(data.message);
                }
            }
        },
        error: function (data) {
            if (data.responseJSON.errors != null) {
                toastr.error(data.responseJSON.errors);
            } else if (data.responseJSON.message != null) {
                toastr.error(data.responseJSON.message);
            }
            console.log(data.responseJSON);
        }
    });
};
