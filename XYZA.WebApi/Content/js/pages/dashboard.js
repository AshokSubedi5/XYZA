var dashboard = function () {

    //variable
    var loadingMessageIntervalId = null;
    var loadJsonFlag = false;


    //events
    $("#loading").on('click', function () {        
        if (!$("#loading").hasClass('start')) {
            StartClicked();
        } else {
            StopClicked();
        }
    });


    //functions
    function StartClicked() {
        $(".message-label").html("Initializing.")
        i = 0;
        loadingMessageIntervalId = setInterval(function () {
            i = ++i % 4;
            $(".message-label").html("Loading" + Array(i + 1).join("."));
        }, 500);
        $("#loading").addClass('loading start');
        // $("#loading").attr("src", "Content/img/stop.png");
        $("#loading").attr("disabled", "disabled");



        //after successfully connection to server and return success
        setTimeout(function () {
            $.ajax({
                type: 'GET',
                dataType: 'json',
                headers: {
                    'Access-Control-Allow-Origin': '*',
                    'content-type': 'application/json'
                },
                url: config.apiUrl + "Email/start",
                complete: function (data) {
                    clearInterval(loadingMessageIntervalId);
                    ChangeToInProgressState();
                }
            });
        }, 4000);


    }
    function StopClicked() {
        clearInterval(loadingMessageIntervalId);
        loadJsonFlag = false;
        $("#loading").addClass('loading');


        $.ajax({
            type: 'GET',
            dataType: 'json',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            url: config.apiUrl + "Email/stop",
            complete: function (data) {
                Initialize();
            }
        });


    }
    function LoadJson(callback) {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            cache: false,
            url: "Log/mail_log.json",
            success: function (data) {
                callback(data);
            }
        });
    }
    function LoadJsonAsync() {

        $.ajax({
            type: 'GET',
            dataType: 'json',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            url: "Log/mail_log.json",
            cache: false,
            success: function (data) {
                $(".message-label").html(data.status);
                $('.logs').html(data.logs.join("<Br />"));
                if ($('.log-container').scrollTop() + $('.log-container').innerHeight() + 40 >= $('.log-container')[0].scrollHeight) {
                    $('.log-container').scrollTop($('.log-container')[0].scrollHeight);
                }
                $('.progress-bar').css({ width: data.percentComplete + "%" });
                $('.progress-bar').html(data.percentComplete + "%");               
                if (data.status == "Completed") {
                    $('.progress-bar').css({ width: "100%" });
                    $('.progress-bar').html("100%");
                    $("#loading").attr("src", "Content/img/stop.png");
                    $("#loading").addClass('start');
                    $("#loading").removeClass('loading');
                    $("#loading").css({ "pointer-events": "auto", "cursor": "pointer" });
                    loadJsonFlag = false;
                } else if (data.status == "Error" || data.status == "Cancelled") {
                    $("#loading").attr("src", "Content/img/stop.png");
                    $("#loading").addClass('start');
                    $("#loading").removeClass('loading');
                    $("#loading").css({ "pointer-events": "auto", "cursor": "pointer" });
                    loadJsonFlag = false;
                } else if (data.status == "Not Started") {
                    loadJsonFlag = false;
                    Initialize();
                }
            },
            complete: function () {
                if (loadJsonFlag)
                    LoadJsonAsync();
               
            }
        });
    }
    function ChangeToInProgressState() {       
        $("#loading").addClass('loading');
        $("#loading").attr("src", "Content/img/start.png");
        $("#loading").css({ "opacity": 0.7, "float": "right", "pointer-events": "none", "cursor": "not-allowed" });
        $("#loading").animate({ width: "10%" }, 100);



        $("#loading").removeClass("sr-only");
        $(".progress-state").removeClass("sr-only");


        loadJsonFlag = true;
        LoadJsonAsync();
    }
    function ChangeToNotStartedState() {
        loadJsonFlag = false;
        $("#loading").removeClass('loading start');
        $(".progress-state").addClass("sr-only");
        $("#loading").attr("src", "Content/img/start.png");
        $("#loading").removeClass("sr-only");
        $(".message-label").html("Click Below Image To Start");
        $("#loading").animate({ width: "20%" }, 100);
        $("#loading").css({ float: "none" });

    }
    function Initialize() {
        //first check json       
        LoadJson(function (json) {
            
            clearInterval(loadingMessageIntervalId);
            if (json == undefined || json.status == "Not Started") {
                //not started
                ChangeToNotStartedState();
            } 
            else{
                //already inprogress state
                ChangeToInProgressState();
            }
        });

    }

    return {
        init: Initialize
    }
}();






