(function ($) {
    $('#manage-item').on('click', function () {
        $(this).find('.nav-link').toggleClass('active');

        $(this).find('#forms-nav').slideToggle(350);
    });

    $('#role-item').on('click', function () {
        $(this).find('.nav-link').toggleClass('active');

        $(this).find('#forms-nav').slideToggle(350);
    });
})(jQuery)