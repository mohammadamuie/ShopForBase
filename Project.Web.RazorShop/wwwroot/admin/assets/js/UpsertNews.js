
$('#Image').change(function () {
    var input = this;
    var url = $(this).val();
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#thumbimage').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        $('#Image').attr('src', '/Images/default.png');
    }
});



$('#submit-form').on('click', function () {
    let form = document.getElementById('ajax-form');
    let formdata = new FormData(form);
    formdata.set('Content', CKEDITOR.instances.Content.getData());

    ajaxPostForm({
        formId: 'ajax-form',
        formData: formdata,
        form: form,
        triggertable: false,
        url: '/Admin/News/upsert',
        triggerForm: false
    });
});