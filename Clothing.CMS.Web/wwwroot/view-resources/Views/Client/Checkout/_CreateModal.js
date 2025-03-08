(function ($) {
    var _$form = $("#checkoutContainer");

    $(document).on("click", "#checkoutContainer .complete-payment", function (e) {
        e.preventDefault();

        var order = _$form.serializeFormToObject();

        var address = [
            order.Address,
            _$form.find("#selectedWard").val(),
            _$form.find("#selectedDistrict").val(),
            _$form.find("#selectedProvince").val()
        ]

        order.Address = address.concat().join(", ");
        order.Total = parseInt(_$form.find(".order-total").text().replace(/\./g, ""), 10);

        var totalQuantity = 0;

        _$form.find(".item-number").each(function () {
            totalQuantity += parseInt($(this).text(), 10);
        });

        order.Quantity = totalQuantity;

        var orderProducts = [];

        _$form.find(".block-cart-item").each(function () {
            var product = {
                isActive: true,
                productId: parseInt($(this).find(".item-id").val(), 10),
                quantity: parseInt($(this).find(".item-number").text(), 10),
                name: $(this).find(".item-name").text().trim(),
                price: parseInt($(this).find(".item-price").text().replace(/\D/g, ""))
            };

            orderProducts.push(product);
        });

        order.OrderProduct = orderProducts;

        order.UserId = 22

        $.ajax({
            url: "/Checkout/Order",
            type: "POST",
            data: order,
            dataType: "json",
            success: function (response) {
                if (response.success === true) {
                    ClearTextBoxCheckout();

                    $.post("/Cart/ClearSession", function (res) {
                        if (res.success) {
                            $(document).trigger("updateCartItem");
                            $(document).trigger("updateCartNotify");
                            $(document).trigger("updateCartSideBar");
                        }
                    });

                    Swal.fire({
                        icon: "success",
                        title: "Đơn hàng đã được đặt thành công",
                        showConfirmButton: false,
                        customClass: {
                            container: "custom-swal-container",
                            popup: "custom-swal-popup"
                        },
                        timer: 5000
                    });
                }
                else {
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
            }
        });
    });

    function ClearTextBoxCheckout() {
        // Reset các trường nhập văn bản
        _$form.find("input, textarea").val("");

        // Reset các trường select
        _$form.find("#province").empty().append('<option value="">Tỉnh/Thành</option>');
        _$form.find("#district").empty().append('<option value="">Quận/Huyện</option>');
        _$form.find("#ward").empty().append('<option value="">Phường/Xã</option>');

        var provinceSelect = _$form.find("#province").next(".select2").find(".select2-selection__rendered");
        provinceSelect.css("color", "");

        var districtSelect = _$form.find("#district").next(".select2").find(".select2-selection__rendered");
        districtSelect.css("color", "");

        var wardSelect = _$form.find("#ward").next(".select2").find(".select2-selection__rendered");
        wardSelect.css("color", "");
    }
})(jQuery)