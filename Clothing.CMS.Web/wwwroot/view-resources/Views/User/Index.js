$(document).ready(function () {
    GetUser();
});

function GetUser() {
    $.ajax({
        url: "/Admin/User/GetData",
        type: "GET",
        dataType: "json",
        success: OnSuccess
    })
}

function OnSuccess(response) {
    $("#userTable").DataTable({
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
        lengthChange: true,
        lengthMenu: [[10, 20, 50, -1], [10, 20, 50, "Tất cả"]],
        data: response,
        columns: [
            {
                data: 'Email',
                autoWidth: true,
                render: function (data, type, row, meta) {
                    return row.email
                }
            },
            {
                data: 'FirstName',
                autoWidth: true,
                render: function (data, type, row, meta) {
                    return row.firstName
                }
            },
            {
                data: 'LastName',
                autoWidth: true,
                render: function (data, type, row, meta) {
                    return row.lastName
                }
            },
            {
                data: null,
                className: "w-action",
                defaultContent: "",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info" data-user-id="${row.id}" data-bs-toggle="" data-bs-target="" >`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-user" data-user-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger" data-user-id="${row.id}" data-bs-toggle="" data-bs-target="">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });
}

$(document).on("click", ".edit-user", function (e) {
    var userId = $(this).attr("data-user-id");

    $.ajax({
        url: "/Admin/User/EditModal?Id=" + userId,
        type: "POST",
        dataType: "html",
        success: function (result) {
            $("#UserEditModal").find(".modal-content").html(result);
        },
        error: function (e) {
        }
    })
})