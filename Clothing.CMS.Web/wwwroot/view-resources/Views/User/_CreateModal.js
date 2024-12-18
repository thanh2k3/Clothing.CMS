(function ($) {
    var _$modal = $("#UserCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#userTable");

    var srcDefault = _$form.find("#targetImage").attr("src");

    $(document).on("click", ".create-user", function (e) {
        var formData = new FormData();

        var serializedData = _$form.serializeFormToObject();

        // Thêm các dữ liệu từ serializedData vào FormData
        Object.keys(serializedData).forEach(function (key) {
            formData.append(key, serializedData[key]);
        });

        formData.append("avatarURL", _$form.find("#AvatarURL")[0].files[0]);

        $.ajax({
            url: "/Admin/User/Create",
            type: "POST",
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) {
                if (result.success === true) {
                    HideUserCreateModal();
                    _$table.DataTable().ajax.reload();
                    toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    })

    function HideUserCreateModal() {
        ClearTextBoxUserCreate();
        _$modal.modal("hide");
    }

    function ClearTextBoxUserCreate() {
        // Reset các trường nhập văn bản
        _$form.find("input, textarea").val("");
        // Reset hình ảnh về ảnh mặc định
        _$form.find("#targetImage").attr("src", srcDefault);
        // Reset file input
        _$form.find("#AvatarURL").val("");
    }

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
})(jQuery);