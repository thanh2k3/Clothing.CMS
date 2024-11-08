(function ($) {
    var _$modal = $("#CategoryEditModal"),
        _$form = _$modal.find("form");

    function Save() {
        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/Category/Edit",
            data: formData,
            type: "POST",
            success: function (result) {
                if (result.success === true) {
                    _$modal.modal("hide");
                    GetCategory();
                    toastr.info(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
                else {
                    toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
                }
            }
        })
    }

    _$form.closest("div.modal-content").find(".save-button").click(function (e) {
        e.preventDefault();
        Save();
    })

})(jQuery)