var BookList = function () {
    var table = null;
    this.Initialize = function () {
        InitializeGridview();

    }

    var InitializeGridview = function () {
        table = $('#tblBookList').DataTable({
            responsive: true,
            //"scrollY": '50vh',
            "processing": true,
            "serverSide": true,
            //"filter": false, // remove search box
            "orderMulti": false, // disable multiple columns at once
            "ajax": {
                "url": '/Book/GetBookList',
                "type": 'POST',
                "datatype": 'json',
                "dataSrc": function (response) {
                    if (commonUtil.HandleGridviewResponseError(response) === false) {
                        return response.data;
                    }
                },
                "complete": function (response) {
                    //commonUtil.HandleGridviewResponseError(response);
                },
                "error": function (jqXHR, textStatus, errorThrown) {
                    // Handle the error event (called when there is an error)
                    alert('Failed to load data: ' + textStatus + ' - ' + errorThrown);
                }
            },
            "columns": [
                { "data": null, "autoWidth": false, "orderable": false, "render": function () { return null; } },
                {
                    "data": "ImageUrl", "autoWidth": true, "orderable": false,
                    "render": function (data, type, row) {
                        var url = commonUtil.AppRootDir + '/Book/Detail/' + row.Id;
                        return `<a href="${url}"><img src="${data}" class="datatable-image" alt="${row.name}"></a>`;
                    }
                },
                {
                    "data": "Title", "autoWidth": true,
                    "render": function (data, type, row) {
                        var url = commonUtil.AppRootDir + '/Book/Detail/' + row.Id;
                        return `<a href="${url}">${data}</a>`
                    }
                },
                { "data": "Category", "autoWidth": true },
                { "data": "Author", "autoWidth": true },
                //{ "data": "Price", "autoWidth": true },
                //{
                //    "data": "ReleaseDate", "autoWidth": true,
                //    "render": function (data, row, type) {
                //        var dateVal = new Date(data);
                //        return dateVal.toLocaleString('en-GB');
                //    }
                //},
                //{
                //    "data": null, "autoWidth": false, "orderable": false,
                //    "render": function (data, type, row) {
                //        var addButton = '<button class="btn btn-sm btn-outline-warning book-cart" data-employeeid="' + row.Id + '"><i class="fa fa-edit"></i>Add to card </button>';

                //        return addButton
                //    }
                //}
            ],
            "order": [[2, "desc"]]
        });
    }


    var Refresh = function () {
        table.ajax.reload();
    }
}

var booklist = new BookList();