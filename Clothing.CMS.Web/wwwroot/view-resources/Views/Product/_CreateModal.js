(function ($) {
    var _$modal = $("#ProductCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#productTable");

    var srcDefault = _$form.find("#targetImage").attr("src");

    $(document).on("click", ".create-product", function (e) {
        var formData = new FormData();

        var serializedData = _$form.serializeFormToObject();

        // Thêm các dữ liệu từ serializedData vào FormData
        Object.keys(serializedData).forEach(function (key) {
            formData.append(key, serializedData[key]);
        });

        formData.append("Image", _$form.find("#ImageURL")[0].files[0]);

        $.ajax({
            url: "/Admin/Product/Create",
            type: "POST",
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) { debugger
                if (result.success === true) {
                    HideProductCreateModal();
                    // Reload lại Datatable
                    _$table.DataTable().ajax.reload();
                    toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    })

    function HideProductCreateModal() {
        ClearTextBoxProductCreate();
        _$modal.modal("hide");
    }

    function ClearTextBoxProductCreate() {
        // Reset các trường nhập văn bản
        _$form.find("input, textarea").val("");
        // Reset các trường select
        _$form.find("select").prop("selectedIndex", 0);
        // Reset hình ảnh về ảnh mặc định
        _$form.find("#targetImage").attr("src", srcDefault);
        // Reset file input
        _$form.find("#ImageURL").val("");
    }

    // Gắn sự kiện change cho input file
    _$form.find("#ImageURL").on("change", function (e) {
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