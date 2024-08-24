function CreateUser() { debugger
    var formData = $("#formCreateUser").serialize();

    $.ajax({
        url: "/Admin/User/Create",
        type: "POST",
        data: formData,
        dataType: "json",
        success: function (result) { debugger
            if (result.success === true) {
                GetUser();
                HideUserCreateModal();
            }
            else {
                alert(result.message);
            }
        },
        error: function (result) {
            alert(result.message);
        }
    })
}

function HideUserCreateModal() {
    ClearTextBoxUserCreate();
    $("#UserCreateModal").modal("hide");
}

function ClearTextBoxUserCreate() {
    $("#formCreateUser #Email").val("");
    $("#formCreateUser #Avatar").val("");
    $("#formCreateUser #Password").val("");
    $("#formCreateUser #ConfirmPassword").val("");
    $("#formCreateUser #FirstName").val("");
    $("#formCreateUser #LastName").val("");
}