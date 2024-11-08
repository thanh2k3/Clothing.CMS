$(document).ready(function () {
    GetCategory();
});

function GetCategory() {
    $.ajax({
        url: "/Admin/Category/GetData",
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                OnSuccess(response);
            }
            else {
                toastr.error("Có lỗi xảy ra", null, { timeOut: 3000, positionClass: "toast-top-right" })
            }
        }
    })
}

function OnSuccess(response) {
    $("#categoryTable").DataTable({
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
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "Tất cả"]],
        data: response,
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
                className: "w-action",
                data: null,
                defaultContent: "",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info" data-category-id="${row.id}" data-bs-toggle="" data-bs-target="" >`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
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
}

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
        title: 'Bạn có chắc không?',
        text: "Bạn có chắn là muốn xóa danh mục \"" + title + "\" không!",
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
                url: "/Admin/Category/Delete?Id=" + cateId,
                type: "POST",
                dataType: "json",
                success: function (result) {
                    if (result.success === true) {
                        GetCategory();
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