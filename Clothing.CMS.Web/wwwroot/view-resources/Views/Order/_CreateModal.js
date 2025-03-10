(function ($) {
    var _$modal = $("#OrderCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#orderTable"),
        _$orderProductTable = $("#orderProductTable");

    GenerateOrderCode();

    var selectProducts = [];

    _$orderProductTable.DataTable({
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
                    return `<input type="text" class="form-control input-amount" 
                            data-row-id="${meta.row}" value="0" />`;
                },
            },
            {
                targets: 4,
                data: "id",
                render: function (data, type, row, meta) {
                    return `<input type="checkbox" class="text-center form-check-input select-product" 
                            data-product-id="${data}" />`;
                },
            },
        ],
        initComplete: function () {
            // Áp dụng hàm registerInputAmount sau khi DataTable được render
            _$orderProductTable.registerInputAmount();
        }
    });

    // Gắn sự kiện khi DataTable vẽ lại giao diện
    _$orderProductTable.on("draw.dt", function () {
        _$orderProductTable.registerInputAmount();
    });

    // Hàm tính tổng
    function updateSummary() {
        var totalQuantity = selectProducts.reduce((sum, product) => sum + product.quantity, 0),
            totalAmount = selectProducts.reduce((sum, product) => sum + product.price * product.quantity, 0);

        _$form.find("input[name='Quantity']").val(totalQuantity);
        _$form.find("input[name='Total']").val(totalAmount.toLocaleString("vi-VN"));
    }

    // Xử lý khi nhập dữ liệu vào ô input-amount
    $(document).on("blur", "#OrderCreateModal .input-amount", function () {
        var _$row = $(this).closest("tr"),
            _$dataTable = _$orderProductTable.DataTable(),
            rowData = _$dataTable.row(_$row).data(); // Dữ liệu của dòng hiện tại

        var quantity = parseInt($(this).val().replace(/\./g, ""), 10) || 0, // Loại bỏ dấu chấm để xử lý đúng
            checkbox = _$row.find(".select-product"),
            price = rowData.price,
            name = rowData.name;

        if (quantity > 0) {
            // Chỉ cập nhật dữ liệu khi checkbox được tích chọn
            if (checkbox.is(":checked")) {
                selectProducts = selectProducts.filter((p) => p.name !== name);
                selectProducts.push({ name, price, quantity });
            }
        } else {
            checkbox.prop("checked", false);
            selectProducts = selectProducts.filter((p) => p.name !== name);

            // Đặt giá trị mặc định của ô input quantity là 0
            $(this).val(0);
        }

        updateSummary();
    });

    // Xử lý khi thay đổi trạng thái checkbox (tích chọn)
    $(document).on("change", "#OrderCreateModal .select-product", function () {
        var _$row = $(this).closest("tr"),
            _$dataTable = _$orderProductTable.DataTable(),
            rowData = _$dataTable.row(_$row).data(),
            isChecked = $(this).is(":checked");

        var productData = {
            productId: rowData.id,
            name: rowData.name,
            price: rowData.price,
            quantity: parseInt(_$row.find(".input-amount").val().replace(/\./g, ""), 10) || 0,
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
                $(this).prop("checked", false);
            }
        } else {
            selectProducts = selectProducts.filter(
                (p) => p.name !== productData.name
            );
        }

        updateSummary();
    });

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
                    GenerateOrderCode();
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

        // Reset tab mặc định của modal
        $("#orderCreateTab").tab("show");

        // Reset trang mặc định của modal - product là trang 1
        _$orderProductTable.DataTable().page(0).draw(false);
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

        // Reset tất cả checkbox và input-amount trên mọi trang của DataTable
        var dataTable = _$orderProductTable.DataTable();
        dataTable.rows().every(function () {
            var rowNode = this.node();
            $(rowNode).find(".select-product").prop("checked", false);
            $(rowNode).find(".input-amount").val(0);
        })
    }

    function GenerateOrderCode() {
        $.ajax({
            url: "/Admin/Order/GenerateOrderCode",
            type: "GET",
            success: function (data) {
                _$form.find("#Code").val(data);
            }
        });
    }
})(jQuery);