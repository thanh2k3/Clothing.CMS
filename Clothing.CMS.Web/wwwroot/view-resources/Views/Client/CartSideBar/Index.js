(function ($) {
    var _$wrap = $("#cartSideBar");

    loadCartSideBar();

    function loadCartSideBar() {
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
            error: function () { }
        });
    }

    function renderCart(cartItemVM) {
        var cartWrap = _$wrap.find(".header-cart-wrapitem"),
            cartTotal = _$wrap.find(".header-cart-total"),
            cartHTML = "",
            totalPrice = 0;

        if (cartItemVM.length === 0) {
            cartHTML += `
                <li class="header-cart-item">Giỏ hàng trống</li>
            `;
        } else {
            cartItemVM.forEach(function (item) {
                var itemTotal = item.quantity * item.price;
                totalPrice += itemTotal;

                cartHTML += `
                    <li class="header-cart-item flex-w flex-t m-b-18">
					    <div class="header-cart-item-img">
						    <img src="${item.imageURL}" alt="IMG">
					    </div>
					    <div class="header-cart-item-txt">
						    <div class="header-cart-item-name m-b-8 trans-04">${item.name}</div>
						    <span class="header-cart-item-info">
                                <div>${item.size}, ${item.color}</div>
							    <div>${item.quantity} x ${item.price.toLocaleString()}₫</div>
						    </span>
					    </div>
				    </li>
                `;
            });
        }

        cartHTML = cartHTML.trim();
        cartWrap.html(cartHTML);
        cartTotal.text(`Tổng: ${totalPrice.toLocaleString()}₫`);
    }

    $(document).on("updateCartSideBar", function () {
        loadCartSideBar();
    });
})(jQuery)