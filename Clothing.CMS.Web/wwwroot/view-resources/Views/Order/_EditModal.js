(function ($) {
    var _$modal = $("#OrderEditModal"),
        _$form = _$modal.find("form"),
        _$table = $("#orderTable");

    _$form.closest("div.modal-content").find(".save-order").click(function (e) {
        e.preventDefault();

        // Kiểm tra danh sách sản phẩm
        if (!selectProducts || selectProducts.length === 0) {
            toastr.warning("Vui lòng chọn sản phẩm trước khi tạo đơn hàng", null, {
                timeOut: 3000,
                positionClass: "toast-top-right"
            });

            return;
        }

        var order = _$form.serializeFormToObject();
        order.OrderProduct = [];

        // Thêm dữ liệu vào danh sách OrderProduct
        selectProducts.forEach(p => {
            order.OrderProduct.push({
                OrderId: p.orderId,
                ProductId: p.productId,
                Quantity: p.quantity,
                Price: p.price,
                IsActive: p.isActive
            });
        })

        $.ajax({
            url: "/Admin/Order/Edit",
            type: "POST",
            data: order,
            dataType: "json",
            success: function (result) {
                if (result.success === true) {
                    _$modal.modal("hide");
                    _$table.DataTable().ajax.reload();
                    toastr.info(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    });
})(jQuery);