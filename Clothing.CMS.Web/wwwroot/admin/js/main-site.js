(function ($) {
    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function (camelCased = false) {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        if (camelCased && camelCased === true) {
            return convertToCamelCasedObject(obj);
        }

        //những input có format là tiền thì phải convert lại thành số
        this.find('.input-amount').toArray().forEach(function (field) {
            obj[field.name] = field.value.replaceAll(".", "")
        });

        return obj;
    };

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