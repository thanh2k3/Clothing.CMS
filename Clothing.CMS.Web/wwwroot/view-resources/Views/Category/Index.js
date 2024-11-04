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
            //{
            //    targets: 0,
            //    data: "id",
            //    className: "hidden",
            //},
            {
                targets: 0,
                data: "title",
            },
            {
                targets: 1,
                data: "status",
            },
            {
                targets: 2,
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
                        `   <button class="btn btn-sm btn-warning edit-category" data-category-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-category" data-category-id="${row.id}" data-bs-toggle="" data-bs-target="" >`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });
}