(function ($) {
    var _$modal = $("#RoleCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#roleTable");

    $(document).on("click", ".create-role", function (e) {
        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/Role/Create",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (result) {
                if (result.success === true) {
                    HideRoleCreateModal();
                    _$table.DataTable().ajax.reload();
                    toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    });

    function HideRoleCreateModal() {
        ClearTextBoxRoleCreate();
        _$modal.modal("hide");
    }

    function ClearTextBoxRoleCreate() {
        _$form.find("input, textarea").val("");
    }
})(jQuery)