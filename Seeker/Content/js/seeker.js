$(document).ready(function() {
    $.map($("#logs > .card > .card-header > div > span"), function(el) {
        $(el).addClass("label");
        switch($(el).text()) {
            case "Debug":
                $(el).addClass("label-secondary");
                break;
            case "Error":
                $(el).addClass("label-error");
                break;
            case "Warn":
                $(el).addClass("label-warning");
                break;
            default:
                 $(el).addClass("label-primary");
        }
    });
});

$(document).on("click", "#logs > .card > .card-footer > .btn", function () {
    $(this).next().toggleClass("d-hide");
})