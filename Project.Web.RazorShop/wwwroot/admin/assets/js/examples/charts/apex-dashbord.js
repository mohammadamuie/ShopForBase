'use strict';

function getParameterByName(params) {
    return "";
}


async function postData(url = '', data = {}) {
    // Default options are marked with *
    const response = await fetch(url, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects
}


async function getData(url = '', data = {}) {
    // Default options are marked with *
    const response = await fetch(url, {
        method: 'GET', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        //body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return response.json(); // parses JSON response into native JavaScript objects
}


var myData = [];

function apex_chart_two() {

    //fetch

    var dates = [];
    var spikes = [5, -5, 3, -3, 8, -8]


    for (var i = 0; i < myData.length; i++) {
        let d = new Date(myData[i].date);
        var innerArr = [d.getTime(), myData[i].value];
        dates.push(innerArr)
    }

    var options = {
        chart: {

            type: 'area',
            stacked: false,
            height: 350,
            zoom: {
                type: 'x',
                enabled: true
            },
            toolbar: {
                autoSelected: 'zoom'
            }
        },
        dataLabels: {
            enabled: false
        },
        series: [{
            name: 'تعداد بازدید روز',
            data: dates
        }],
        markers: {
            size: 0,
        },
        title: {
            text: 'بازدید ها',
            align: 'right'
        },
        fill: {
            type: 'gradient',
            gradient: {
                shadeIntensity: 1,
                inverseColors: false,
                opacityFrom: 0.5,
                opacityTo: 0,
                stops: [0, 90, 100]
            },
        },
        yaxis: {
            min: 1,
            max: 999,
            labels: {
                formatter: function (val) {
                    return (val).toFixed(0);
                },
            },
            title: {
                text: 'بازدید',
                align: 'right'
            },
        },
        xaxis: {
            type: 'datetime',
        },

        tooltip: {
            shared: false,
            y: {
                formatter: function (val) {
                    return (val).toFixed(0)
                }
            }
        }
    }

    var chart = new ApexCharts(
        document.querySelector("#apex_chart_two"),
        options
    );

    chart.render();
}


$(document).ready(function () {
    Apex.chart = {
        fontFamily: 'inherit',
        //locales: [{
        //    "name": "fa",
        //    "options": {
        //        "months": ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهرویور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
        //        "shortMonths": ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهرویور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
        //        "days": ["یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"],
        //        "shortDays": ["ی", "د", "س", "چ", "پ", "ج", "ش"],

        //    }
        //}],
        //defaultLocale: "fa"
    }

    getData('/admin/home/DashboardData').then(data => {
        myData = data;
        setTimeout(() => {
            apex_chart_two();
        }, 456)
    });




});