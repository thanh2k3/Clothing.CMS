(function ($) {
    function updateCartNotify() {
        $.get("/Product/GetCartProductCount", function (response) {
            if (response.productCount !== undefined) {
                if (response.productCount > 0) {
                    $(".wrap-icon-header .icon-cart-shopping").attr("data-notify", response.productCount);
                } else {
                    $(".wrap-icon-header .icon-cart-shopping").removeAttr("data-notify");
                }
            }
        });
    }

	// Lắng nghe sự kiện back, forward (popstate)
	window.addEventListener("popstate", updateCartNotify);

	// Lắng nghe sự kiện thay đổi hash (#)
	window.addEventListener("hashchange", updateCartNotify);

	// Lắng nghe sự kiện load lại trang (F5, Ctrl+R, Refresh)
    window.addEventListener("load", updateCartNotify);

    $(document).on("updateCartNotify", function () {
        updateCartNotify();
    });
})(jQuery)