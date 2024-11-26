(function ($) {
    var _$table = $("#roleTable");

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
            url: "/Admin/Role/GetData",
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
                data: "name",
                width: "20%",
            },
            {
                targets: 1,
                data: "description",
            },
            {
                targets: 2,
                className: "text-center",
                data: null,
                defaultContent: "",
                width: "20%",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info view-role" data-role-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RoleViewModal">`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-role" data-role-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RoleEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-role" data-role-id="${row.id}" data-role-name="${row.name}">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });

    $(document).on("click", ".edit-role", function (e) {
        var roleId = $(this).attr("data-role-id");

        $.ajax({
            url: "/Admin/Role/EditModal?Id=" + roleId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#RoleEditModal div.modal-content").html(result);
                $("#RoleEditModal").find("form").registerInputAmount();
            },
            error: function (e) {
            }
        })
    });
})(jQuery);
