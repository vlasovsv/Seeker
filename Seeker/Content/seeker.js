$(document).ready(function() {
    $.map($("#log-table > tbody > tr > td > span"), function(el) {
        $(el).addClass('label');
        switch($(el).text()) {
            case 'Debug':
                $(el).addClass('label-warning');
                break;
            case 'Error':
                $(el).addClass('label-danger');
                break;
            default:
                 $(el).addClass('label-primary');
        }
    });
});

$(document).on('click', '.expander', function () {
    $(this).siblings('div').toggleClass('hidden');
})