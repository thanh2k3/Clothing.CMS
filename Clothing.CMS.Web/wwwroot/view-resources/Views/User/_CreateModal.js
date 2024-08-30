$(document).on("click", ".create-user", function (e) {
    var formData = $("#formCreateUser").serialize();

    $.ajax({
        url: "/Admin/User/Create",
        type: "POST",
        data: formData,
        dataType: "json",
        success: function (result) {
            if (result.success === true) {
                HideUserCreateModal();
                GetUser();
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
    $("#UserCreateModal").modal("hide");
}

function ClearTextBoxUserCreate() {
    $("#formCreateUser #Email").val("");
    $("#formCreateUser #AvatarURL").val("");
    $("#formCreateUser #Password").val("");
    $("#formCreateUser #ConfirmPassword").val("");
    $("#formCreateUser #FirstName").val("");
    $("#formCreateUser #LastName").val("");
}