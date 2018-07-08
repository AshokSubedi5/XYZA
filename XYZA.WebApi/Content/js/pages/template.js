
CKEDITOR.replace('message');


$(document).ready(function () {
    //initial load
    $.ajax({
        type: 'GET',
        dataType: 'json',
        headers: {
            'Access-Control-Allow-Origin': '*',
            'content-type': 'application/json'
        },
        url: config.apiUrl + "emailtemplate",
        success: function (data) {
            $("#subject").val(data.Subject);
            $("#message").val(data.Message);
        }
    });


    //save
    $("#save").on('click', function () {
        var data = {
            Subject: $("#subject").val(),
            Message: CKEDITOR.instances.message.getData()
        }
        $.ajax({
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            url: config.apiUrl + "emailtemplate",
            complete: function (data) {
                if (data.status == 200) {
                    //success message 
                    $("#success-alert").find("span").text("Changed Saved Successfully!");
                    $("#success-alert").removeClass("sr-only");
                    $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
                        $("#success-alert").slideUp(500);
                    });
                } else {
                    //error message
                    $("#danger-alert").find("span").text("Some Error Occur, Try Again Later!");
                    $("#danger-alert").removeClass("sr-only");
                    $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                        $("#danger-alert").slideUp(500);
                    });
                }
            }
        });
    });



    //reset
    $("#reset").on('click', function () {
        $("#subject").val('');
        $("#message").val('');
        CKEDITOR.instances.message.setData('');
    });
});