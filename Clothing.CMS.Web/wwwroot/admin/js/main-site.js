(function ($) {
    numberFormatCurrency = function numberFormatCurrency(value) {
        return $.fn.dataTable.render.number(".", ",", 0)
    }

    // Định dạng số tiền đầu vào (input)
    $.fn.registerInputAmount = function () {
        var $this = $(this)
        $this.find('.input-amount').toArray().forEach(function (field) {
            new Cleave(field, {
                numeral: true,
                numericOnly: true,
                numeralPositiveOnly: true, //so duong
                numeralDecimalMark: ',',
                delimiter: '.',
                numeralThousandsGroupStyle: 'thousand',
            });
            field.value = field.value;
        });
    };
})(jQuery);