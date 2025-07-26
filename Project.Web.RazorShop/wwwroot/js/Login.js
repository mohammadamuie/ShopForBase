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
                    var phone = document.getElementById("PhoneNumber").value;
                    toastr.success(data.message);
                    setTimeout(
                        function () {
                            location.href = "/account/verify?phonenumber=" + phone;
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