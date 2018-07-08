//initialize global variables

var config = function () {
    var apiUrl = "http://localhost:51865/api/";

    var progressState = ["Not Started", "In Progress", "Cancelled", "Error", "Completed"];

    return {
        apiUrl: apiUrl
    }
}();
