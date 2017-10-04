$(document).ready(function() {
    $.map($("#log-table > tbody > tr > td > span"), function(el) {
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

$(document).on("click", "#log-table > tbody > tr.expanded-row", function () {
    $(this).next().toggleClass("d-hide");
})