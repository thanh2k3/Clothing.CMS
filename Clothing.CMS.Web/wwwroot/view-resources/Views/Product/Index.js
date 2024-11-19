$(document).ready(function () {
    GetProduct();
});

function GetProduct() {
    $.ajax({
        url: "/Admin/Product/GetData",
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
    $("#productTable").DataTable({
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
                data: "name",
            },
            {
                targets: 1,
                data: "imageURL",
                render: (data, type, row, meta) => {
                    return '<img src="/' + data + '" alt="Image" />';
                }
            },
            {
                targets: 2,
                data: "category.title",
            },
            {
                targets: 3,
                data: "description",
            },
            {
                targets: 4,
                data: "originalPrice",
                render: numberFormatCurrency()
            },
            {
                targets: 5,
                data: "price",
                render: numberFormatCurrency()
            },
            {
                targets: 6,
                data: "inventory",
                render: numberFormatCurrency()
            },
            {
                targets: 7,
                className: "w-action",
                data: null,
                defaultContent: "",
                render: function (data, type, row, meta) {
                    var actions = [];
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
}
