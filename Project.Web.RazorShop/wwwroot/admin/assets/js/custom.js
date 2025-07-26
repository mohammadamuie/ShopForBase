'use strict';
$(function () {
    $('input.alphanum').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
            this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
            toastr.warning(" لطفا حروف انگلیسی و اعداد انگلیسی");
        }
    });
    $('input.numeric').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^0-9]/g)) {
            this.value = this.value.replace(/[^0-9]/g, '');
            toastr.warning("لطفا فقط اعداد انگلیسی");
        }
    });
    $('input.alpha').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^a-zA-Zضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ ]/g)) {
            this.value = this.value.replace(/[^a-zA-Zضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ ]/g, '');
            toastr.warning("لطفا حروف فارسی و حروف انگلیسی");
        }
    });
    $('input.alphacode').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^a-zA-Z0-9ضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ]/g)) {
            this.value = this.value.replace(/[^a-zA-Z0-9ضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ]/g, '_');
            //toastr.warning("لطفا حروف انگلیسی و اعداد انگلیسی و حروف فارسی");
        }
    });
    $('input.english').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^a-zA-Z ]/g)) {
            this.value = this.value.replace(/[^a-zA-Z ]/g, '');
            toastr.warning("لطفا فقط حروف انگلیسی");
        }
    });
    $('input.persian').on('keyup keypress keydown change', function () {
        if (this.value.match(/[^ضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ ]/g)) {
            this.value = this.value.replace(/[^ضصثقفغعهخحجچشسیبلاتنمکگظطزرذدئوآءأإؤژيةۀپ ]/g, '');
            toastr.warning("لطفا فقط حروف فارسی");
        }
    });
    if ($('body').hasClass('horizontal-side-menu') && $(window).width() > 768) {
        if ($('body').hasClass('layout-container')) {
            $('.side-menu .side-menu-body').wrap('<div class="container"></div>');
        } else {
            $('.side-menu .side-menu-body').wrap('<div class="container"></div>');
        }
        setTimeout(function () {
            $('.side-menu .side-menu-body > ul').append('<li><a href="#"><span>Other</span></a><ul></ul></li>');
        }, 100);
        $('.side-menu .side-menu-body > ul > li').each(function () {
            var index = $(this).index(),
                $this = $(this);
            if (index > 7) {
                setTimeout(function () {
                    $('.side-menu .side-menu-body > ul > li:last-child > ul').append($this.clone());
                    $this.addClass('d-none');
                }, 100);
            }
        });
    }


    $('.simple-submit').on('submit', function (e) {
        $('button').attr('disabled', true);
        e.preventDefault();
        var form = $(this);
        var redirect = $(this).attr('data-redirect');
        var reset_form = $(this).attr('data-reset-form') == undefined ? '0' : '1';
        var dismiss_modal = $(this).attr('data-dismiss') == undefined ? '0' : '1';
        var table_draw = $(this).attr('data-table-draw') == undefined ? '0' : '1';
        var reload = $(this).attr('data-reload') == undefined ? '0' : '1';
        console.log(reload);
        var url = form.attr('action');
        var method = form.attr('method');
        toastr.options = {
            timeOut: 2e3,
            progressBar: !0,
            showMethod: "slideDown",
            hideMethod: "slideUp",
            showDuration: 200,
            hideDuration: 200,
            positionClass: "toast-top-center"
        }
        $.ajax({
            type: method,
            url: url,
            data: form.serialize(),
            success: function (data) {
                // data = JSON.parse(data);
                if (data.status == '1') {
                    toastr.success(data.message);
                    if (redirect !== undefined) {
                        setTimeout(() => {
                            window.location.replace(redirect);
                        }, 1000);
                    }
                    if (reload == '1') {
                        setTimeout(() => {
                            window.location.reload();
                        }, 1000);
                    }
                    if (reset_form == '1')
                        form.trigger('reset');
                    if (dismiss_modal == '1')
                        $('.modal').modal('hide');
                    if (table_draw == '1')
                        table.draw('page');
                } else {
                    toastr.error(data.message);
                }
            }
        }).then(() => {
            $('button').attr('disabled', false);
        });
    });

    let url = window.location.href;
    url = url.split('#')[0];
    url = url.split('?')[0];
    if (url.endsWith("/")) {
        url = url.substr(0, url.length - 1);
    }
    var flag = false;
    var element = $('div.navigation-menu-group a').filter(function () {
        if (flag == false) {
            flag = (url == this.href);
        }
        return url == this.href;
    }).addClass('active').parent().parent().parent().addClass('open');
    $("a[data-nav-target='#" + $(element).attr('id') + "']").addClass('active');
    if (flag == false) {
        $('#dashboard-menu').addClass('open');
        $('#li-a-home').addClass('active');
        $("a[data-nav-target='#dashboard-menu']").addClass('active');
    }
});

function StartupToString(input) {
    switch (input) {
        case 0:
        case 'Other':
            return 'دیگر';
        case 1:
        case 'Handicrafts':
            return 'صنایع دستی';
        case 2:
        case 'HomeJobs':
            return 'مشاغل خانگی';
        case 3:
        case 'Insurance':
            return 'خدمات بیمه';
        case 4:
        case 'Legal':
            return 'خدمات حقوقی';
        case 5:
        case 'DigitalMarketing':
            return 'دیجیتال مارکتینگ';
        case 6:
        case 'Agriculture':
            return 'خدمات کشاورزی';
        case 7:
        case 'Computer':
            return 'خدمات کامپیوتر';
        case 8:
        case 'Cinema':
            return 'سینما وتئاتر';
        case 9:
        case 'Engineering':
            return 'خدمات مهندسی';
        case 10:
        case 'LaborRelations':
            return 'روابط کار';
        case 11:
        case 'NutritionAffairs':
            return 'امور تغذیه';
        case 12:
        case 'Finance':
            return 'امور مالی و بازرگانی';
        case 13:
        case 'Educational':
            return 'خدمات آموزشی';
        case 14:
        case 'Industry':
            return 'خدمات صنعت';
        case 15:
        case 'JobSearch':
            return 'امور کاریابی';
        case 16:
        case 'Market':
            return 'فروشگاه';
        case 17:
        case 'Services':
            return 'خدمات';
    }
}

function EmploymentStatusToString(input) {
    switch (input) {
        case 0:
        case 'NotWorking':
            return 'مشغول به کار نیست';
        case 1:
        case 'BusinessOwner':
            return 'مالک کسب و کار شخصی';
        case 2:
        case 'FullTimeJob':
            return 'مشغول به کار تمام وقت';
        case 3:
        case 'HalfTimeJob':
            return 'مشغول به کار پاره وقت';
        case 4:
        case 'Student':
            return 'دانشجو';
        case 5:
        case 'FreeLancer':
            return 'فریلنسر';
    }
}

function NumberFromat(input) {
    return parseInt(input).toLocaleString() + " <small>تومان</small>";
}

Object.defineProperty(String.prototype, "ToPersianDateString", {
    value: function ToPersianDateString() {
        return moment(this).locale('fa').format('LLL');
    },
    writable: true,
    configurable: true
});

function setCookie(cname, cvalue, exminutes) {
    var d = new Date();
    d.setTime(d.getTime() + (exminutes * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function removeCookie(cname) {
    var d = new Date();
    d.setTime(d.getTime() - (24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + '' + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

if (getCookie('success') != '') {
    toastr.success(decodeURIComponent(getCookie('success')).replaceAll('+', ' '));
    removeCookie('success');
}
if (getCookie('error') != '') {
    toastr.error(decodeURIComponent(getCookie('error')).replaceAll('+', ' '));
    removeCookie('error');
}

function date_to_string(date) {
    var options = { dateStyle: 'medium', timeStyle: 'short' };
    return new Date(date).toLocaleString("fa-IR", options);
}