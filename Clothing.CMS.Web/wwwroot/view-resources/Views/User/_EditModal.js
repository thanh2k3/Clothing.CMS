(function ($) {
    var _$modal = $("#UserEditModal"),
        _$form = _$modal.find("form");

    function Save() {
        var formData = _$form.serialize();

        $.ajax({
            url: "/Admin/User/Edit",
            data: formData,
            type: "POST",
            success: function (result) {
                if (result.success === true) {
                    GetUser();
                    _$modal.modal("hide");
                }
            }
        })
    }

    _$form.closest("div.modal-content").find(".save-button").click(function (e) {
        e.preventDefault();
        Save();
    })

})(jQuery)