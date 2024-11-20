(function ($) {
    var _$modal = $("#ProductCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#productTable");

    _$form.registerInputAmount();

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
                // Có thể xử lý dữ liệu trước khi hiển thị nếu cần
                return json || [];
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
                    return '<img src="/' + data + '" alt="Image" />';
                }
            },
            {
                targets: 1,
                data: "name",
                width: "20%",
            },
            {
                targets: 2,
                data: "category.title",
                width: "15%",
            },
            {
                targets: 3,
                data: "originalPrice",
                width: "10%",
                render: numberFormatCurrency()
            },
            {
                targets: 4,
                data: "price",
                width: "10%",
                render: numberFormatCurrency()
            },
            {
                targets: 5,
                data: "inventory",
                width: "10%",
                render: numberFormatCurrency()
            },
            {
                targets: 6,
                className: "w-action",
                data: null,
                defaultContent: "",
                width: "20%",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info" data-product-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ProductViewModal">`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-product" data-product-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ProductEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-product" data-product-id="${row.id}" data-title="${row.name}">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });
})(jQuery);
