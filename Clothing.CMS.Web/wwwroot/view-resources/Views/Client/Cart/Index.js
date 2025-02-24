(function ($) {
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
                        title: "Oops...",
                        text: "Lỗi khi lấy giỏ hàng",
                        customClass: {
                            confirmButton: "btn-swal2"
                        }
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
            cartHTML += `
                <tr class="table_row">
                    <td colspan="8" class="text-center">
                        Chưa có sản phẩm nào trong giỏ hàng
                    </td>
                </tr>
            `;
        } else {
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
                                <div class="btn-num-product-down cl8 hov-btn3 trans-04 flex-c-m">
                                    <i class="fa-solid fa-minus"></i>
                                </div>

                                <input class="mtext-104 cl3 txt-center num-product" type="number" name="num-product1" value="${item.quantity}">

                                <div class="btn-num-product-up cl8 hov-btn3 trans-04 flex-c-m">
                                    <i class="fa-solid fa-plus"></i>
                                </div>
                            </div>
                        </td>
                        <td class="column-7">${formattedTotal}</td>
                        <td class="column-8">
                            <i class="fa-solid fa-trash-can"></i>
                        </td>
                    </tr>
                `;
            });
        }

        // Cập nhật danh sách giỏ hàng
        $("#cartContainer tbody").html(cartHTML);
        $("#cartContainer .order-total").text(totalPrice.toLocaleString("vi-VN") + "₫");
    }
})(jQuery)