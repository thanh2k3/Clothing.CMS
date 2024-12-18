(function ($) {
    var _$modal = $("#UserEditModal"),
        _$form = _$modal.find("form"),
        _$table = $("#userTable");

    _$form.closest("div.modal-content").find(".save-user").click(function (e) {
        e.preventDefault();

        var formData = new FormData();
        var serializedData = _$form.serializeFormToObject();

        // Thêm các dữ liệu từ serializedData vào FormData
        Object.keys(serializedData).forEach(function (key) {
            formData.append(key, serializedData[key]);
        });

        formData.append("AvatarURL", _$form.find("#AvatarURL")[0].files[0]);

        $.ajax({
            url: "/Admin/User/Edit",
            data: formData,
            type: "POST",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) {
                if (result.success === true) {
                    _$modal.modal("hide");
                    _$table.DataTable().ajax.reload();
                    toastr.info(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    });

    // Gắn sự kiện change cho input file
    _$form.find("#AvatarURL").on("change", function (e) {
        var input = e.target;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                _$form.find("#targetImage").attr("src", e.target.result);
            };
            reader.readAsDataURL(input.files[0]);
        }
    });
})(jQuery)