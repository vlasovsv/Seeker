$(document).ready(function () {
    $.map($("ul.pagination > li > a"), function (elem) {
        var url = $(elem).attr("href");
        $(elem).attr("href", encodeURI(url));
    })
});