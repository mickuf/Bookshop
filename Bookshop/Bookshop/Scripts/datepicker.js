$(document).ready(function () {
    var options = $.extend({},
        $.datepicker.regional["pl"], {
            dateFormat: "yy-mm-dd"
        }
    );

    $('.datepicker').datepicker(options);
});