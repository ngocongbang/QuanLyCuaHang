//'' khi so ban ghi thay doi
function changeListPage(res) {
    debugger
    var a = res;
    $('#pagesizelist').on('click', function (event) {
        var form = $(event.target).parents('form');
        var bang = $('#pagesizelist').val();
        form.submit();
        $('#pageCurrent').val(1);
    });
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

