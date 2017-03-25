$(function () {
    $('.search-container input').on('keypress', function (e) {
        if (e.which === 13)
            doSearch();
    });

    $('.search-container .js-search-button').on('click', doSearch);
});

function doSearch() {
    var tbody = $('.results-container .js-log-table tbody');
    $('.results-container .js-log-table-container').addClass('hidden');
    tbody.html('');
    $('.results-container .loader').removeClass('hidden');
    $.get('/search', { 'q': $('.search-container input').val() }, function (data) {
        $('.results-container .loader').addClass('hidden');
        $('.results-container .js-log-table-container').removeClass('hidden');
        data.forEach(function (e) {
            let template = $('.js-table-row-template .table-row').clone();
            template.find('.time').html(e.timestamp);
            template.find('.message').html(e.message);
            template.find('.machine').html(e.properties.machine);
            tbody.append(template);
        });
    }, 'json');
}