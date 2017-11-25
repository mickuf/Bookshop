$(document).ready(function () {
    var options = $.extend({},
        $.datepicker.regional["pl"], {
            dateFormat: "dd/mm/yy"
        }
    );

    $('.datepicker').datepicker(options);
});