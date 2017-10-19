//'' khi so ban ghi thay doi
function changeListPage(res) {
    //var a = res;
    //$('#pagesizelist').on('click', function (event) {
    //    var form = $(event.target).parents('form');
    //    var bang = $('#pagesizelist').val();
    //    form.submit();
    //    $('#pageCurrent').val(1);
    //});


    $('#pageCurrent').val(1);
    $('#formVendor').submit();
}
// next
function Next() {
    var b = parseInt($('#pageCurrent').val().toString()) + 1;
    var itotalpage = parseInt($('#totalPage').text());
    $('#pageCurrent').val(b);
    if (itotalpage <= b) {
        $('#pageCurrent').val(itotalpage);
    }
}
function Back() {
    var b = parseInt($('#pageCurrent').val().toString()) - 1;
    if (b == 0) {
        b = 1;
    }
    $('#pageCurrent').val(b);
}

//sắp xếp các trường
function orderHeader(order) {
    debugger
    $('#orderId').val(order);
    $('#formVendor').submit();
}

// thanh sắp xếp lên xuống theo
function selectedCls(column) {
    if ($('#orderId').val(order).split(" ")[0] == column) {
        if ($scope.asc) {
            return "fa fa-sort-up"
        }
        else {
            return "fa fa-sort-down"
        }
    }
    else {
        return "";
    }
}