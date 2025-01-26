﻿(function ($) {
    var _$modal = $("#OrderCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#orderTable");

    $(document).on("click", ".create-order", function (e) {
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
                ProductId: p.productId,
                Quantity: p.quantity,
                Price: p.price,
                IsActive: p.isActive
            })
        })

        $.ajax({
            url: "/Admin/Order/Create",
            type: "POST",
            data: order,
            dataType: "json",
            success: function (result) {
                if (result.success === true) {
                    HideOrderCreateModal();
                    _$table.DataTable().ajax.reload();
                    toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    });

    function HideOrderCreateModal() {
        ClearTextBoxOrderCreate();
        _$modal.modal("hide");
    }

    function ClearTextBoxOrderCreate() {
        // Reset các trường nhập văn bản
        _$form.find("input, textarea").val("");
        // Reset các trường select
        _$form.find("select").prop("selectedIndex", 0);
        // Set giá trị mặc định của Total
        _$form.find("#Total").val("0");
        // Reset danh sách sản phẩm đã chọn
        selectProducts = [];
        // Reset giá trị cho totalQuantity và totalAmount
        if (typeof updateSummary === "function") {
            updateSummary();
        }
        // Reset các checkbox và input trong bảng sản phẩm
        $("#orderProductTable").find(".select-product").prop("checked", false);
        $("#orderProductTable").find(".input-amount").val(0);
    }
})(jQuery);