(function ($) {
    var _$table = $("#categoryTable");

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
            url: "/Admin/Category/GetData",
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
                data: "title",
            },
            {
                targets: 1,
                data: "statusString",
            },
            {
                targets: 2,
                className: "text-center",
                data: null,
                width: "20%",
                defaultContent: "",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-category" data-category-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CategoryEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-category" data-category-id="${row.id}" data-title="${row.title}">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });

    $(document).on("click", ".edit-category", function (e) {
        var cateId = $(this).attr("data-category-id");

        $.ajax({
            url: "/Admin/Category/EditModal?Id=" + cateId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#CategoryEditModal").find(".modal-content").html(result);
            },
            error: function (e) {
            }
        })
    })

    $(document).on("click", ".delete-category", function (e) {
        var cateId = $(this).attr("data-category-id");
        var title = $(this).attr("data-title");

        Swal.fire({
            title: "Bạn có chắc không?",
            text: "Bạn có chắn là muốn xóa danh mục \"" + title + "\" không!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Đồng ý",
            cancelButtonText: "Hủy",
            allowOutsideClick: false,
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Admin/Category/Delete?Id=" + cateId,
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
    })
})(jQuery)
