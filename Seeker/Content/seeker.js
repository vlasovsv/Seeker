$(function () {
    $('.search-container input').on('keypress', function (e) {
        if (e.which === 13)
            doSearch();
    });

    $('.search-container .js-search-button').on('click', doSearch);

    $('#startdatepicker').datetimepicker();
    $('#enddatepicker').datetimepicker({
        useCurrent: false //Important! See issue #1075
    });
    $("#startdatepicker").on("dp.change", function (e) {
        $('#enddatepicker').data("DateTimePicker").minDate(e.date);
    });
    $("#enddatepicker").on("dp.change", function (e) {
        $('#startdatepicker').data("DateTimePicker").maxDate(e.date);
    });
});

function doSearch() {
    var tbody = $('.js-log-table-container > table > tbody');
    $('.results-container .js-log-table-container').addClass('hidden');
    tbody.html('');
    $('.results-container .loader').removeClass('hidden');
    $.get('api/v1/search',
        {
            'q': $('.search-container input').val(),
            'start': $('#startdatepicker').data("DateTimePicker").date().format('YYYY-MM-DDTHH:mm:ss'),
            'end': $('#enddatepicker').data("DateTimePicker").date().format('YYYY-MM-DDTHH:mm:ss')
        },
        function (data) {
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