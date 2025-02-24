(function ($) {
    var _$section = $("#productDetailContainer");

    _$section.registerInputAmount();

    $(document).on("click", "#productDetailContainer .size-btn", function () {
        _$section.find(".size-btn").removeClass("active");
        $(this).addClass("active");
        _$section.find("#selectedSize").val($(this).data("size"));
    });

    $(document).on("click", "#productDetailContainer .color-btn", function () {
        _$section.find(".color-btn").removeClass("active");
        $(this).addClass("active");
        _$section.find("#selectedColor").val($(this).data("color"));
    });

    // Hàm lấy giá trị số lượng từ input
    function getProductQuantity() {
        var quantity = parseInt(_$section.find(".input-amount").val().replace(/\./g, ""), 10) || 0;

        return quantity;
    }

    // Hàm định dạng số với dấu "." ngăn cách phần nghìn
    function formatNumberWithDot(value) {
        return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    // Hàm cập nhật giá trị hiển thị trên input
    function updateQuantityInput(quantity) {
        quantity = Math.max(0, Math.min(quantity, 999999));
        var formatValue = formatNumberWithDot(quantity);
        _$section.find(".product-quantity").val(formatValue);
        _$section.find(".product-quantity").attr("value", quantity);
    }

    // Hàm cập nhật thông báo của giỏ hàng
    function updateCartNotify() {
        $.get("/Product/GetCartProductCount", function (response) {
            if (response.productCount !== undefined && response.productCount > 0) {
                $(".wrap-icon-header .icon-cart-shopping").attr("data-notify", response.productCount);
            }
        });
    }

    // Sự kiện thay đổi số lượng bằng nhập trực tiếp
    $(document).on("input", "#productDetailContainer .product-quantity", function () {
        var value = $(this).val().replace(/\D/g, "");

        if (value.length > 6) {
            value = value.slice(0, 6);
        }

        var quantity = parseInt(value) || 0;
        var formatValue = formatNumberWithDot(quantity);

        $(this).val(formatValue);
        _$section.find(".product-quantity").attr("value", formatValue);
    });

    // Khi nhấn nút tăng hoặc giảm
    $(document).on("click", "#productDetailContainer .btn-product-minus, #productDetailContainer .btn-product-plus", function () {
        var quantity = getProductQuantity();

        if ($(this).hasClass("btn-product-plus")) {
            quantity++;
        } else if ($(this).hasClass("btn-product-minus")) {
            quantity = Math.max(0, quantity - 1);
        }

        quantity = Math.max(0, Math.min(quantity, 999999));
        updateQuantityInput(quantity);
    });

    $(document).on("click", "#productDetailContainer .add-to-cart", function () {
        var productId = parseInt(_$section.find(".product-id").val()),
            selectedSize = _$section.find("#selectedSize").val(),
            selectedColor = _$section.find("#selectedColor").val(),
            quantity = parseInt(_$section.find(".product-quantity").val().replace(/\./g, ""), 10) || 0,
            productName = _$section.find(".product-name").text().trim(),
            productPrice = parseInt(_$section.find(".product-price").text().replace(/\D/g, ""), 10) || 0,
            productImage = _$section.find(".product-img").attr("src");

        // Kiểm tra xem các giá trị có hợp lệ không
        if (!selectedSize || !selectedColor || isNaN(quantity) || quantity <= 0) {
            Swal.fire({
                icon: "error",
                title: "Oops...!",
                text: "Vui lòng chọn size, màu sắc và số lượng hợp lệ",
                customClass: {
                    confirmButton: "btn-swal2"
                }
            });
            return;
        }

        var productData = {
            productId: productId,
            name: productName,
            imageURL: productImage,
            price: productPrice,
            size: selectedSize,
            color: selectedColor,
            quantity: quantity
        };

        $.ajax({
            url: "/Product/AddToCart",
            type: "POST",
            data: productData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    updateCartNotify();

                    Swal.fire({
                        icon: "success",
                        title: productData.name,
                        text: response.message,
                        customClass: {
                            confirmButton: "btn-swal2"
                        }
                    });
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...!",
                        text: response.message,
                        customClass: {
                            confirmButton: "btn-swal2"
                        }
                    });
                }
            },
            error: function () {
            }
        });
    });
})(jQuery);