(function ($) {
    var _$modal = $("#CategoryEditModal"),
        _$form = _$modal.find("form"),
        _$table = $("#categoryTable");

    _$form.closest("div.modal-content").find(".save-category").click(function (e) {
        e.preventDefault();

        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/Category/Edit",
            data: formData,
            type: "POST",
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
})(jQuery)