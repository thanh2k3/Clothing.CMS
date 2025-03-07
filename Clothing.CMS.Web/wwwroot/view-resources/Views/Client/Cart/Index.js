(function ($) {
    var _$form = $("#cartContainer");

    loadCart();

    function loadCart() {
        $.ajax({
            url: "/Cart/GetCart",
            type: "GET",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    renderCart(response.cartItemVM);
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...!",
                        text: "Lỗi khi lấy giỏ hàng",
                        customClass: {
                            container: "custom-swal-container",
                            popup: "custom-swal-popup"
                        },
                        timer: 3000
                    });
                }
            },
            error: function () {
            }
        });
    }

    function renderCart(cartItemVM) {
        let cartHTML = "";
        let totalPrice = 0;

        if (cartItemVM.length === 0) {
            _$form.find(".btn-checkout").addClass("button-disabled");

            cartHTML += `
                <tr class="table_row">
                    <td colspan="8" class="text-center">
                        Chưa có sản phẩm nào trong giỏ hàng
                    </td>
                </tr>
            `;
        } else {
            _$form.find(".btn-checkout").removeClass("button-disabled");

            cartItemVM.forEach(item => {
                let formattedPrice = item.price.toLocaleString("vi-VN") + "₫";
                let formattedTotal = (item.price * item.quantity).toLocaleString("vi-VN") + "₫";

                totalPrice += item.price * item.quantity;

                cartHTML += `
                    <tr class="table_row">
				        <td class="column-1">
                            <div class="how-itemcart1">
                                <img src="${item.imageURL}" alt="${item.name}">
                            </div>
                        </td>
                        <td class="column-2">
                            ${item.name}
                        </td>
                        <td class="column-3">${formattedPrice}</td>
                        <td class="column-4">${item.size}</td>
                        <td class="column-5">${item.color}</td>
                        <td class="column-6">
                            <div class="wrap-num-product flex-w m-r-0">
                                <div class="btn-product-minus cl8 hov-btn3 trans-04 flex-c-m stext-102">
                                    <i class="fa-solid fa-minus"></i>
                                </div>

                                <input class="stext-102 cl3 txt-center num-product product-quantity input-amount" type="text" name="num-product1" value="${item.quantity}" autocomplete="off">

                                <div class="btn-product-plus cl8 hov-btn3 trans-04 flex-c-m stext-102">
                                    <i class="fa-solid fa-plus"></i>
                                </div>
                            </div>
                        </td>
                        <td class="column-7">${formattedTotal}</td>
                        <td class="column-8">
                            <button class="remove-from-cart" data-product-id="${item.productId}" data-product-name="${item.name}" 
                                                             data-product-size="${item.size}" data-product-color="${item.color}">
                                <i class="fa-solid fa-trash-can"></i>
                            </button>
                        </td>
                    </tr>
                `;
            });
        }

        // Cập nhật danh sách giỏ hàng
        _$form.find("tbody").html(cartHTML);
        _$form.find(".order-total").text(totalPrice.toLocaleString("vi-VN") + "₫");
    }

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

    // Hàm định dạng số với dấu "." ngăn cách phần nghìn
    function formatNumberWithDot(value) {
        return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    function updateTotalPrice() {
        var totalPrice = 0;
        $(".table_row").each(function () {
            var itemTotal = parseInt($(this).find(".column-7").text().replace(/\D/g, ""));
            if (!isNaN(itemTotal)) {
                totalPrice += itemTotal;
            }
        });

        _$form.find(".order-total").text(totalPrice.toLocaleString("vi-VN") + "₫");
    }

    function updateCartSession(productData) {
        $.ajax({
            url: "/Cart/UpdateCart",
            type: "POST",
            data: productData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $(document).trigger("updateCartSideBar");
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...!",
                        text: response.message,
                        showConfirmButton: false,
                        customClass: {
                            container: "custom-swal-container",
                            popup: "custom-swal-popup"
                        },
                        timer: 3000
                    });
                }
            },
            error: function () { }
        })
    }

    $(document).on("click", "#cartContainer .remove-from-cart", function (event) {
        event.preventDefault();

        var productId = $(this).attr("data-product-id"),
            productName = $(this).attr("data-product-name"),
            productSize = $(this).attr("data-product-size"),
            productColor = $(this).attr("data-product-color");

        var productData = {
            productId: productId,
            name: productName,
            size: productSize,
            color: productColor
        };

        $.ajax({
            url: "/Cart/RemoveFromCart",
            type: "POST",
            data: productData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    loadCart();
                    updateCartNotify();
                    $(document).trigger("updateCartSideBar");
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...!",
                        text: response.message,
                        showConfirmButton: false,
                        customClass: {
                            container: "custom-swal-container",
                            popup: "custom-swal-popup"
                        },
                        timer: 3000
                    });
                }
            },
            error: function () { }
        });
    });

    $(document).on("click", "#cartContainer .btn-product-plus, #cartContainer .btn-product-minus", function () {
        var _$row = $(this).closest(".table_row"); // Lấy đúng dòng chứa sản phẩm
        var _$input = _$row.find(".product-quantity");
        var quantity = parseInt(_$input.val().replace(/\./g, ""), 10) || 1;

        if ($(this).hasClass("btn-product-plus")) {
            quantity++;
        } else if ($(this).hasClass("btn-product-minus")) {
            quantity = Math.max(1, quantity - 1); // Không cho số lượng < 1
        }

        quantity = Math.max(1, Math.min(quantity, 999999));
        _$input.val(quantity).trigger("change"); // Gọi sự kiện thay đổi số lượng

        var formatValue = formatNumberWithDot(quantity);
        _$input.val(formatValue);
        _$input.attr("value", quantity);
    });

    $(document).on("input", "#cartContainer .product-quantity", function (event) {
        var value = $(this).val().replace(/\D/g, "");

        // Ngăn nhập số 0
        if (value === "0") {
            value = "1"; // Nếu nhập số 0, đổi thành 1 ngay lập tức
        }

        if (value.length > 6) {
            value = value.slice(0, 6);
        }

        var quantity = parseInt(value) || "";
        var formatValue = formatNumberWithDot(quantity);

        $(this).val(formatValue);
        $(this).attr("value", quantity);
    });

    $(document).on("change", "#cartContainer .product-quantity", function () {
        var _$row = $(this).closest(".table_row");
        var newQuantity = parseInt($(this).val().replace(/\./g, ""), 10);
        if (isNaN(newQuantity) || newQuantity <= 0) {
            newQuantity = 1;
            $(this).val(newQuantity);
        }

        var productId = _$row.find(".remove-from-cart").attr("data-product-id"),
            productName = _$row.find(".remove-from-cart").attr("data-product-name"),
            productSize = _$row.find(".remove-from-cart").attr("data-product-size"),
            productColor = _$row.find(".remove-from-cart").attr("data-product-color");

        var productData = {
            productId: productId,
            name: productName,
            size: productSize,
            color: productColor,
            quantity: newQuantity
        }

        var price = parseInt(_$row.find(".column-3").text().replace(/\D/g, ""));
        var newTotal = price * newQuantity;

        _$row.find(".column-7").text(newTotal.toLocaleString("vi-VN") + "₫");

        updateTotalPrice();

        updateCartSession(productData);
    });
})(jQuery)