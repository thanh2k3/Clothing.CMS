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
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "Tất cả"]],
        data: response,
        columns: [
            {
                data: 'Id',
                autoWidth: true,
                render: function (data, type, row, meta) {
                    return row.id
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
                defaultContent: "",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info" data-bs-toggle="" data-bs-target="">`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        '   </button>'
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning" data-bs-toggle="" data-bs-target="">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        '   </button>'
                    )
                    return actions.join('');
                }
            },
        ]
    });
}