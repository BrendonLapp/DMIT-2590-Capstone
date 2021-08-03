$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "m/d/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+200",

    });

});