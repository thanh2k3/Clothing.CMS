(function ($) {
    numberFormatCurrency = function numberFormatCurrency(value) {
        return $.fn.dataTable.render.number(".", ",", 0)
    }
})(jQuery);