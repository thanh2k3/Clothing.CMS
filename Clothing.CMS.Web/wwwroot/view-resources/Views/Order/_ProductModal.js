let selectProducts = [];

(function ($) {
    var _$table = $("#orderProductTable");

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
            $("#orderProductTable").registerInputAmount();
        }
    });

    // Gắn sự kiện khi DataTable vẽ lại giao diện
    $("#orderProductTable").on("draw.dt", function () {
        $("#orderProductTable").registerInputAmount();
    });

    // Hàm tính tổng
    function updateSummary() {
        const totalQuantity = selectProducts.reduce((sum, product) => sum + product.quantity, 0);
        const totalAmount = selectProducts.reduce((sum, product) => sum + product.price * product.quantity, 0);

        $("#formCreateOrder input[name='Quantity']").val(totalQuantity);
        $("#formCreateOrder input[name='Total']").val(totalAmount.toLocaleString("vi-VN"));
    }

    // Xử lý khi nhập dữ liệu vào ô input-amount
    $(document).on("blur", ".input-amount", function () {
        const _$row = $(this).closest("tr");
        const dataTable = $("#orderProductTable").DataTable();
        const rowData = dataTable.row(_$row).data(); // Dữ liệu của dòng hiện tại

        const quantity = parseInt($(this).val().replace(/\./g, ""), 10) || 0; // Loại bỏ dấu chấm để xử lý đúng
        const checkbox = _$row.find(".select-product");
        const price = rowData.price;
        const name = rowData.name;

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
    $(document).on("change", ".select-product", function () {
        const _$row = $(this).closest("tr");
        const dataTable = $("#orderProductTable").DataTable();
        const rowData = dataTable.row(_$row).data();

        const isChecked = $(this).is(":checked");

        const productData = {
            name: rowData.name,
            price: rowData.price,
            quantity: parseInt(_$row.find(".input-amount").val().replace(/\./g, ""), 10) || 0
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
                (p) => p.name !== productData.name
            );
        }

        updateSummary();
    });
})(jQuery);