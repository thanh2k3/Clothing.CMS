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
                    return '<img src="' + data + '" alt="Image" />';
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
                className: "text-center",
                data: null,
                defaultContent: "",
                width: "20%",
                render: function (data, type, row, meta) {
                    var actions = [];
                    actions.push(
                        `   <button class="btn btn-sm btn-info view-product" data-product-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ProductViewModal">`,
                        `       <i class="fa-solid fa-eye"></i> Xem`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-warning edit-product" data-product-id="${row.id}" data-bs-toggle="modal" data-bs-target="#ProductEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> Sửa`,
                        `   </button>`
                    )
                    actions.push(
                        `   <button class="btn btn-sm btn-danger delete-product" data-product-id="${row.id}" data-product-name="${row.name}">`,
                        `       <i class="fa-solid fa-trash-can"></i> Xóa`,
                        `   </button>`
                    )
                    return actions.join('');
                }
            },
        ]
    });

    $(document).on("click", ".edit-product", function (e) {
        var productId = $(this).attr("data-product-id");

        $.ajax({
            url: "/Admin/Product/EditModal?Id=" + productId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#ProductEditModal div.modal-content").html(result);
                $("#ProductEditModal").find("form").registerInputAmount();
            },
            error: function (e) {
            }
        })
    });

    $(document).on("click", ".view-product", function (e) {
        var productId = $(this).attr("data-product-id");

        $.ajax({
            url: "/Admin/Product/ViewModal?Id=" + productId,
            type: "POST",
            dataType: "html",
            success: function (result) {
                $("#ProductViewModal div.modal-content").html(result);
                $("#ProductViewModal").find("form").registerInputAmount();
            },
            error: function (e) {
            }
        })
    });

    $(document).on("click", ".delete-product", function (e) {
        var productId = $(this).attr("data-product-id");
        var name = $(this).attr("data-product-name");

        Swal.fire({
            title: 'Bạn có chắc không?',
            text: "Bạn có chắn là muốn xóa sản phẩm \"" + name + "\" không!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Hủy',
            allowOutsideClick: false,
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Admin/Product/Delete?Id=" + productId,
                    type: "POST",
                    dataType: "json",
                    success: function (result) {
                        if (result.success === true) {
                            _$table.DataTable().ajax.reload();
                            toastr.info(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" });
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
})(jQuery);
