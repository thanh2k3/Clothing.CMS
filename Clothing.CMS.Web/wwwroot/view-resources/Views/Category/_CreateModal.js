(function ($) {
    var _$modal = $("#CategoryCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#categoryTable");

    $(document).on("click", ".create-category", function (e) {
        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/Category/Create",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (result) {
                if (result.success === true) {
                    HideCategoryCreateModal();
                    _$table.DataTable().ajax.reload();
                    toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    });

    function HideCategoryCreateModal() {
        ClearTextBoxCategoryCreate();
        _$modal.modal("hide");
    }

    function ClearTextBoxCategoryCreate() {
        _$form.find("input").val("");
        _$form.find("select").prop("selectedIndex", 0);
    }
})(jQuery)