var UserCart = function () {
    this.AddCart = function (param) {
        commonUtil.ValidateUserSession(param.ReturnUrl);

        togo = commonUtil.AppRootDir + "/Cart/AddCart";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'POST',
            //contentType: "application/json; charset=utf8",
            data: param,
            success: function (response) {
                if (commonUtil.HandleResponseError(response) === false) {
                    bootbox.confirm("Successfully added to card. Do you want to view the cart.", function (result) {
                        if (result === true) {
                            window.location = commonUtil.AppRootDir + '/Cart/Index';
                        }
                    });
                }

                uiBlocker.hideUIBlocker();

            },
            error: function () {
                uiBlocker.hideUIBlocker();
                bootbox.alert('error');
            }
        });
    }

    this.Initialize = function () {
        $('.btnCheckOut').on('click', function () {
            var id = $(this).data("cartid");
            CheckOut(id);
        });
    }

    var CheckOut = function (id) {
        commonUtil.ValidateUserSession(window.location.href);

        togo = commonUtil.AppRootDir + "/Cart/CheckOut";
        uiBlocker.showUIBlocker("Payment Processing...");

        var param = {
            "CartId": id
        };

        $.ajax({
            url: togo,
            type: 'POST',
            //contentType: "application/json; charset=utf8",
            data: param,
            success: function (response) {
                if (commonUtil.HandleResponseError(response) === false) {
                    var redirectUrl = commonUtil.AppRootDir + '/Cart/Index';
                    bootbox.alert({
                        message: "Payment completed.Your order is submitted successfully",
                        callback: function () {
                            window.location = redirectUrl;
                        }
                    });
                }

                uiBlocker.hideUIBlocker();
            },
            error: function () {
                uiBlocker.hideUIBlocker();
                bootbox.alert('error');
            }
        });
    }

    var showMessageAndRedirect = function(message, redirectUrl, callback) {
        // Show the message
        bootbox.alert(message);

        // Execute the callback after the message is acknowledged
        if (callback) {
            callback(redirectUrl);
        }
    }

    var redirect = function(url) {
        window.location.href = url;
    }
}

var userCart = new UserCart();