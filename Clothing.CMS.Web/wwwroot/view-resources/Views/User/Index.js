(function ($) {
    var _$table = $("#userTable");

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
            url: "/Admin/User/GetData",
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
                data: "avatarURL",
                width: "10%",
                render: (data, type, row, meta) => {
                    return '<img src="' + data + '" alt="Image" />';
                }
            },
            {
                targets: 1,
                data: "email",
            },
            {
                targets: 2,
                data: "firstName",
            },
            {
                targets: 3,
                data: "lastName",
            },
            {
                targets: 4,
                className: "text-center",
                data: null,
                defaultContent: "",
                width: "20%",
                render: function (data, type, row, meta) {
                    var actions = [];
                    if (row.name !== "SuperAdmin") {
                        actions.push(
                            `   <button class="btn btn-sm btn-info view-user" data-user-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserViewModal" >`,
                            `       <i class="fa-solid fa-eye"></i> Xem`,
                            `   </button>`
                        )
                        actions.push(
                            `   <button class="btn btn-sm btn-warning edit-user" data-user-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserEditModal">`,
                            `       <i class="fas fa-pencil-alt"></i> Sửa`,
                            `   </button>`
                        )
                        actions.push(
                            `   <button class="btn btn-sm btn-danger delete-user" data-user-id="${row.id}" data-email="${row.email}">`,
                            `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                            `   </button>`
                        )
                    }
                    return actions.join('');
                }
            },
        ]
    });

    $(document).on("click", ".edit-user", function (e) {
        var userId = $(this).attr("data-user-id");

        $.ajax({
            url: "/Admin/User/EditModal?Id=" + userId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#UserEditModal div.modal-content").html(result);
            },
            error: function (e) {
            }
        })
    });

    $(document).on("click", ".view-user", function (e) {
        var userId = $(this).attr("data-user-id");

        $.ajax({
            url: "/Admin/User/ViewModal?Id=" + userId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#UserViewModal div.modal-content").html(result);
            },
            error: function (e) {
            }
        })
    });

    $(document).on("click", ".delete-user", function (e) {
        var userId = $(this).attr("data-user-id");
        var email = $(this).attr("data-email");

        Swal.fire({
            title: 'Bạn có chắc không?',
            text: "Bạn có chắn là muốn xóa tài khoản \"" + email + "\" không",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Hủy',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Admin/User/Delete?Id=" + userId,
                    type: "POST",
                    dataType: "json",
                    success: function (result) {
                        if (result.success === true) {
                            _$table.DataTable().ajax.reload();
                            toastr.info(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                        }
                        else {
                            Swal.fire({
                                icon: "error",
                                title: "Lỗi",
                                text: result.message
                            });
                        }
                    }
                })
            }
        });
    });
})(jQuery);