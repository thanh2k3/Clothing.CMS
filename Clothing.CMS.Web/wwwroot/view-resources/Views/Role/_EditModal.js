(function ($) {
    var _$modal = $("#RoleEditModal"),
        _$form = _$modal.find("form"),
        _$table = $("#roleTable");

    _$form.closest("div.modal-content").find(".save-role").click(function (e) {
        e.preventDefault();

        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/Role/Edit",
            type: "POST",
            data: formData,
            dataType: "json",
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