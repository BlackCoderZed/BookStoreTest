var BookDetail = function () {
    this.Initialize = function () {
        $('#bookDetailAdd #BT_AddToCart').on('click', function () {
            AddToCart();
        });
    }

    var AddToCart = function () {
        var param = {
            BookId: $('#BookDetail #Id').val(),
            Qty: $('#BookDetail #bookDetailAdd #req_qty').val(),
            ReturnUrl: commonUtil.AppRootDir + "/Book/Detail/" + $('#BookDetail #Id').val() 
        }

        userCart.AddCart(param);
    }
}

var bookDetail = new BookDetail();