
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


$('#submit_updateform').on('click', function () {
    let form = document.getElementById('ajax-form');
    let formdata = new FormData(form);
    formdata.set('UserName', new URLSearchParams(window.location.search).get('UserName'));
    ajaxPostForm({
        formData: formdata,
        form: form,
        triggerForm: false
    });
});