$(function () {
    $('.search-container input').on('keypress', function (e) {
        if (e.which === 13)
            doSearch();
    });

    $('.search-container .js-search-button').on('click', doSearch);
});

function doSearch() {
    var tbody = $('.js-log-table-container > table > tbody');
    $('.results-container .js-log-table-container').addClass('hidden');
    tbody.html('');
    $('.results-container .loader').removeClass('hidden');
    $.get('api/v1/search', { 'q': $('.search-container input').val() }, function (data) {
        $('.results-container .loader').addClass('hidden');
        $('.results-container .js-log-table-container').removeClass('hidden');
        let template = $.templates('#log-row-template');
        data.forEach(function (e) {
            tbody.append($(template.render(e)));
        });
    }, 'json');
}

$.views.helpers({
    dateFormat: function (val, format) {
        var date = moment(val);
        return date.format(format);
    }
});