(function ($) {
    var _$table = $("#orderViewProductTable");

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

                // Lấy ra các sản phẩm có trong orderProducts và có isActive = true
                var filterData = json.filter(product =>
                    orderProducts.some(p => p.productId == product.id && p.isActive)
                );

                return filterData;
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

                    return quantity.toLocaleString("vi-VN");
                },
            },
        ],
        initComplete: function () {
            $("#orderViewProductTable").registerInputAmount();
        }
    });

    // Gắn sự kiện khi DataTable vẽ lại giao diện
    $("#orderViewProductTable").on("draw.dt", function () {
        $("#orderViewProductTable").registerInputAmount();
    });
})(jQuery);