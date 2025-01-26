(function ($) {
    var _$table = $("#orderTable");

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
            url: "/Admin/Order/GetData",
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
                data: "code",
            },
            {
                targets: 1,
                data: "userEmail",
            },
            {
                targets: 2,
                data: "createdTime",
                render: function (data, type, row) {
                    return (data) ? moment(data).format("DD/MM/YYYY HH:mm:ss") : "-";
                },
            },
            {
                targets: 3,
                data: "total",
                render: numberFormatCurrency(),
            },
            {
                targets: 4,
                data: "statusString",
            },
            {
                targets: 5,
                className: "text-center",
                data: null,
                defaultContent: "",
                width: "20%",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info view-order" data-order-id="${row.id}" data-bs-toggle="modal" data-bs-target="#OrderViewModal">`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-order" data-order-id="${row.id}" data-bs-toggle="modal" data-bs-target="#OrderEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-order" data-order-id="${row.id}">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });

    $(document).on("click", ".edit-order", function (e) {
        var orderId = $(this).attr("data-order-id");

        $.ajax({
            url: "/Admin/Order/EditModal?Id=" + orderId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#OrderEditModal div.modal-content").html(result);
                $("#OrderEditModal").find("form").registerInputAmount();

                orderProducts.forEach(function (product) {
                    if (product.isActive) {
                        selectProducts.push(product);
                    }
                });
            },
            error: function (e) {
            }
        });
    });

    // Lắng nghe sự kiện "hidden.bs.modal" đóng modal
    $("#OrderEditModal").on("hidden.bs.modal", function () {
        selectProducts = [];
    });
})(jQuery);
