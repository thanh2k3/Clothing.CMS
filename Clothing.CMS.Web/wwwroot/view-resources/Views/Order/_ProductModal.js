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
        lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "Tất cả"]],
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
                data: null, // Không lấy từ dữ liệu, tạo mới
                render: function (data, type, row, meta) {
                    // Đặt giá trị mặc định của ô input là 0
                    return `<input type="text" class="form-control input-amount" 
                            data-row-id="${meta.row}" value="0" />`;
                },
            },
            {
                targets: 4,
                data: null,
                render: function (data, type, row, meta) {
                    // Tạo checkbox với giá trị mặc định là checked
                    return `<input type="checkbox" class="text-center form-check-input select-product" 
                            data-row-id="${meta.row}" />`;
                },
            },
        ],
        initComplete: function () {
            // Áp dụng hàm registerInputAmount sau khi DataTable được render
            $("#orderProductTable").registerInputAmount();
        }
    });
})(jQuery);