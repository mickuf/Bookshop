$(document).ready(function () {
    var options = $.extend({},
        $.datepicker.regional["pl"], {
            dateFormat: "yy-mm-dd",
            maxDate: "+5y",
            minDate: "-300y"
        }
    );

    $('.datepicker').datepicker(options);
});