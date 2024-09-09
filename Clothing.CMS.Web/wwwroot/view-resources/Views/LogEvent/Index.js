$(document).ready(function () {
    GetLogEvent();
});

function GetLogEvent() {
    $.ajax({
        url: "/Admin/LogEvent/GetData",
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
    $("#logEventTable").DataTable({
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
                data: "id",
                className: "hidden",
            },
            {
                targets: 1,
                data: "logLevel",
            },
            {
                targets: 2,
                data: "message",
            },
            {
                targets: 3,
                data: "values",
            },
            {
                targets: 4,
                data: "createdTime",
                render: function (data, type, row) {
                    return (data) ? moment(data).format("DD/MM/YYYY HH:mm:ss") : "-";
                },
            },
        ]
    });
}

$(document).on("click", ".delete-logEvent", function (e) {
    Swal.fire({
        title: 'Bạn có chắc không?',
        text: "Bạn có chắn là muốn xóa tất cả nhật ký!",
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
                url: "/Admin/LogEvent/DeleteLogEvent",
                type: "POST",
                dataType: "json",
                success: function (result) {
                    if (result.success === true) {
                        GetLogEvent();
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
