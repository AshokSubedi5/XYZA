

var customer = function () {


    //Events
    //for update get data from table and published to modal
    $('#grid').on('draw.dt', function () {
        $(".edit-customer").unbind('click').bind('click', function () {
            var table = $('#grid').DataTable();
            var row = $(this).parent().parent();
            var getData = table.rows(row).data()[0];
            var serverData = getData.backupData;

            //clear input fields first
            ClearCustomer();

            //Now fill data
            $("#id").val(serverData.Id);

            $("#fname").val(serverData.FirstName);
            $("#mname").val(serverData.MiddleName);
            $("#lname").val(serverData.LastName);

            $("#country").val(serverData.Country);
            $("#state").val(serverData.State);
            $("#city").val(serverData.City);

            $("#phone").val(serverData.Phone);
            $("#address1").val(serverData.Address1);
            $("#address2").val(serverData.Address2);

            $("#email").val(serverData.Email);
            $("#facebook").val(serverData.Facebook);
            $("#instagram").val(serverData.Instagram);

            $("#remarks").val(serverData.Remarks);
            $("#isactive").prop("checked", serverData.SendMail);
            $(".modal-title").html("Update Customer Information");
            $(".btn-save").text("Update Changes");


            //Now open modal
            $('#exampleModal').modal('show');
        });
        $(".remove-customer").unbind('click').bind('click', function () {
            var table = $('#grid').DataTable();
            var row = $(this).parent().parent();
            var getData = table.rows(row).data()[0];
            var serverData = getData.backupData;
            bootbox.confirm("Are you sure?", function (result) {
                if (result) {
                    //server api call
                    $.ajax({
                        type: 'Delete',
                        dataType: 'json',
                        headers: {
                            'Access-Control-Allow-Origin': '*',
                            'content-type': 'application/json'
                        },
                        url: config.apiUrl + "customer/" + serverData.Id,
                        complete: function (data) {
                            if (data.status == 200) {
                                table.rows(row).remove().draw();

                                //success message
                                ShowMessage("success", "Customer Removed Successfully!");
                            }
                            else {
                                //error message
                                ShowMessage("error", "Some Error Occur, Try Again Later!");
                            }
                        }
                    });
                }
            });


        });
    });


    //modal Save/Update button click event
    $(".btn-save").on('click', function () {

        if (!IsValid())
            return;

        var method = $(".btn-save").html() == "Save Changes" ? "POST" : "PUT";
        var requestedData = GetDataFromHtmlInput();

        $.ajax({
            type: method,
            contentType: 'application/json',
            data: JSON.stringify(requestedData),
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            url: config.apiUrl + "customer",
            complete: function (data) {
                $('#exampleModal').modal('hide');
                if (data.status == 200) {
                    var table = $('#grid').DataTable();
                    if (method == "POST") {
                        //temporary add row to table so that no need to refresh from server
                        newRow = GenerateTempRow(requestedData);
                        table.row.add(newRow).draw(true);
                        ShowMessage("success", "Customer Added Successfully!");
                    } else {
                        //temporary update row to table so that no need to refresh from server
                        var selectedRow = table.rows(function (idx, data, node) {
                            return data.backupData.Id === requestedData.Id ? true : false;
                        });
                        var newRow = GenerateTempRow(requestedData);
                        table.row(selectedRow).data(newRow).draw(true);
                        ShowMessage("success", "Customer Updated Successfully!");
                    }
                } else {
                    //error message
                    ShowMessage("error", "Some Error Occur, Try Again Later!");
                }
            }
        });

    });


    //when modal open
    $("#open-modal-link").on('click', function () {
        ClearCustomer();
    });



    //methods
    function LoadCustomer() {
        $.ajax({
            type: 'GET',
            dataType: 'json',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'content-type': 'application/json'
            },
            url: config.apiUrl + "customer",
            success: function (data) {
                var finalData = data.map(function (cust, i) {
                    return {
                        SN: i + 1,
                        FullName: cust.FirstName + ' ' + cust.MiddleName + ' ' + cust.LastName,
                        Address: cust.Address1 + ' ' + cust.City + ', ' + cust.Country,
                        Phone: cust.Phone,
                        Email: cust.Email,
                        Social: cust.Facebook + ' ' + cust.Instagram,
                        Active: cust.SendMail,
                        Action: "#" + cust.Id,
                        backupData: cust
                    }
                });
                $('#grid').DataTable({
                    responsive: true,
                    "aaData": finalData,
                    "aoColumns": [
                        {
                            "mDataProp": "SN"
                        },
                        {
                            "mDataProp": "FullName"
                        },
                        {
                            "mDataProp": "Address"
                        },
                        {
                            "mDataProp": "Phone"
                        },
                        {
                            "mDataProp": "Email"
                        },
                        {
                            "mDataProp": "Social"
                        },
                        {
                            "mDataProp": "Active"
                        },
                        {
                            "mDataProp": "Action"
                        }
                    ],
                    "aoColumnDefs": [
                        {
                            "aTargets": [5],
                            "mData": "Socal_link",
                            "mRender": function (data, type, full) {
                                return '<a class="social-facebook" target="_blank" href="' + data + '">Facebook</a>&nbsp;&nbsp;<a class="social-instagram" target="_blank" href="' + data + '">Instagram</a>';
                            }
                        },
                        {
                            "aTargets": [7],
                            "mData": "Action",
                            "mRender": function (data, type, full) {
                                return '<a class="edit-customer" href="#">Edit</a>&nbsp;&nbsp;<a class="remove-customer" href="#">Remove</a>';
                            }
                        }]
                });
            }
        });
    }
    function ClearCustomer() {

        $("#id").val('');

        $("#fname").val('');
        $("#mname").val('');
        $("#lname").val('');

        $("#country").val('');
        $("#state").val('');
        $("#city").val('');

        $("#phone").val('');
        $("#address1").val('');
        $("#address2").val('');

        $("#email").val('');
        $("#facebook").val('');
        $("#instagram").val('');

        $("#remarks").val('');
        $("#isactive").prop("checked", true);
        $(".modal-title").html("Add Customer Information");
        $(".btn-save").text("Save Changes");
    }
    function GetDataFromHtmlInput() {
        var data = {};

        data.Id = $("#id").val();
        data.FirstName = $("#fname").val();
        data.MiddleName = $("#mname").val();
        data.LastName = $("#lname").val();

        data.Country = $("#country").val();
        data.State = $("#state").val();
        data.City = $("#city").val();

        data.Phone = $("#phone").val();
        data.Address1 = $("#address1").val();
        data.Address2 = $("#address2").val();

        data.Email = $("#email").val();
        data.Facebook = $("#facebook").val();
        data.Instagram = $("#instagram").val();

        data.Remarks = $("#remarks").val();
        data.SendMail = $("#isactive").prop("checked");

        return data;
    }
    function ShowMessage(type, message) {
        if (type == "success") {
            //success message
            $("#success-alert").find("span").text(message);
            $("#success-alert").removeClass("sr-only");
            $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
                $("#success-alert").slideUp(500);
            });
        }
        else {
            $("#danger-alert").find("span").text(message);
            $("#danger-alert").removeClass("sr-only");
            $("#danger-alert").fadeTo(2000, 500).slideUp(500, function () {
                $("#danger-alert").slideUp(500);
            });
        }
    }
    function IsValid() {       
        var flag = true;
        $('.modal-body').find('input').each(function () {
            if ($(this).prop('required') && $(this).val() == '') {
                flag = false;
                return;
            }
        });
        return flag;
    }
    function GenerateTempRow(data) {
        return {
            SN: '00_New',
            FullName: data.FirstName + ' ' + data.MiddleName + ' ' + data.LastName,
            Address: data.Address1 + ' ' + data.City + ', ' + data.Country,
            Phone: data.Phone,
            Email: data.Email,
            Social: data.Facebook + ' ' + data.Instagram,
            Active: data.SendMail,
            Action: "#" + data.Id,
            backupData: data
        };
    }
    return {
        init: LoadCustomer
    }
}();

