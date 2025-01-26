(function ($) {
    var _$table = $("#orderEditProductTable");

    _$table.DataTable({
        language: {
            lengthMenu: "Hiển thị _MENU_ bản ghi",
            search: "Tìm kiếm:",
            zeroRecords: "Không tìm thấy dòng nào phù hợp",
            infoFiltered: "(được lọc từ _MAX_ mục)",
            info: "Hiển thị _START_ đến _END_ của _TOTAL_ bản ghi",
            infoEmpty: "Chưa có bản ghi nào để hiển thị",
            paginate: {
                previous: "Trước",
                next: "Sau",
            },
            emptyTable: "Chưa có dữ liệu, vui lòng thêm dữ liệu vào",
            processing: "Đang tải...",
        },
        processing: true,
        destroy: true,
        ordering: false,
        autoWidth: false,
        lengthChange: true,
        lengthMenu: [[5], [5]],
        pageLength: 5,
        ajax: {
            url: "/Admin/Product/GetData",
            type: "GET",
            dataType: "json",
            dataSrc: function (json) {
                if (json.success === false) {
                    toastr.error(json.message, null, { timeOut: 3000, positionClass: "toast-top-right" });
                    return []; // Không có dữ liệu để hiển thị
                }
                return json || []; // Xử lý dữ liệu nếu thành công
            },
            error: function () {
                toastr.error("Có lỗi xảy ra khi tải dữ liệu", null, { timeOut: 3000, positionClass: "toast-top-right" });
            }
        },
        columnDefs: [
            {
                targets: 0,
                data: "imageURL",
                width: "10%",
                render: (data, type, row, meta) => {
                    return '<img src="' + data + '" alt="Image" />';
                }
            },
            {
                targets: 1,
                data: "name",
            },
            {
                targets: 2,
                data: "price",
                render: numberFormatCurrency()
            },
            {
                targets: 3,
                data: null,
                render: function (data, type, row, meta) {
                    var product = orderProducts.find(p => p.productId == row.id),
                        quantity = product && product.isActive ? product.quantity : 0;
                    return `<input type="text" class="form-control input-amount" 
                            data-row-id="${meta.row}" value="${quantity}" />`;
                },
            },
            {
                targets: 4,
                data: "id",
                render: function (data, type, row, meta) {
                    var isChecked = orderProducts.some(p => p.productId == data && p.isActive) ? "checked" : "";
                    return `<input type="checkbox" class="text-center form-check-input select-product" 
                            data-product-id="${data}" ${isChecked} />`;
                },
            },
        ],
        initComplete: function () {
            $("#orderEditProductTable").registerInputAmount();
        }
    });

    // Gắn sự kiện khi DataTable vẽ lại giao diện
    $("#orderEditProductTable").on("draw.dt", function () {
        $("#orderEditProductTable").registerInputAmount();
    });

    // Hàm tính tổng
    function updateSummary() {
        const totalQuantity = selectProducts.reduce((sum, product) => sum + product.quantity, 0);
        const totalAmount = selectProducts.reduce((sum, product) => sum + product.price * product.quantity, 0);

        $("#formEditOrder input[name='Quantity']").val(totalQuantity);
        $("#formEditOrder input[name='Total']").val(totalAmount.toLocaleString("vi-VN"));
    }

    // Xử lý khi nhập dữ liệu vào ô input-amount
    $(document).off("blur", "#OrderEditModal .input-amount")
        .on("blur", "#OrderEditModal .input-amount", function () {
        var _$row = $(this).closest("tr"),
            _$dataTable = $("#orderEditProductTable").DataTable(),
            rowData = _$dataTable.row(_$row).data();

        var _$checkbox = _$row.find(".select-product"), // Lấy checkbox DOM element
            isChecked = _$checkbox.is(":checked"),
            orderId = parseInt($("#OrderEditModal").find("#Id").val(), 10);

        var productData = {
            orderId: orderId,
            productId: rowData.id,
            quantity: parseInt(_$row.find(".input-amount").val().replace(/\./g, ""), 10) || 0,
            price: rowData.price,
            isActive: isChecked
        }

        if (productData.quantity > 0) {
            if (isChecked) {
                selectProducts = selectProducts.filter(
                    (p) => p.productId !== productData.productId
                );
                selectProducts.push(productData);
            }
        } else {
            _$checkbox.prop("checked", false);
            selectProducts = selectProducts.filter(
                (p) => p.productId !== productData.productId
            );
            $(this).val(0);
        }

        updateSummary();
    });

    // Xử lý khi thay đổi trạng thái checkbox (tích chọn)
    $(document).off("change", "#OrderEditModal .select-product")
        .on("change", "#OrderEditModal .select-product", function () {
        var _$row = $(this).closest("tr"),
            _$dataTable = $("#orderEditProductTable").DataTable(),
            rowData = _$dataTable.row(_$row).data();

        var isChecked = $(this).is(":checked"),
            orderId = parseInt($("#OrderEditModal").find("#Id").val(), 10);

        var productData = {
            orderId: orderId,
            productId: rowData.id,
            quantity: parseInt(_$row.find(".input-amount").val().replace(/\./g, ""), 10) || 0,
            price: rowData.price,
            isActive: isChecked
        };

        if (isChecked) {
            if (productData.quantity > 0) {
                selectProducts.push(productData);
            } else {
                toastr.warning("Vui lòng nhập số lượng lớn hơn 0 trước khi chọn sản phẩm", null, {
                    timeOut: 3000,
                    positionClass: "toast-top-right"
                });
                $(this).prop("checked", false); // Bỏ chọn checkbox nếu số lượng không hợp lệ
            }
        } else {
            selectProducts = selectProducts.filter(
                (p) => p.productId !== productData.productId
            );
        }

        updateSummary();
    });
})(jQuery);