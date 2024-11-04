$(document).on("click", ".create-category", function (e) {
    var formData = $("#formCreateCategory").serialize();

    $.ajax({
        url: "/Admin/Category/Create",
        type: "POST",
        data: formData,
        dataType: "json",
        success: function (result) {
            if (result.success === true) {
                HideCategoryCreateModal();
                GetCategory();
                toastr.success(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
            }
            else {
                toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
            }
        }
    })
})

function HideCategoryCreateModal() {
    ClearTextBoxCategoryCreate();
    $("#CategoryCreateModal").modal("hide");
}

function ClearTextBoxCategoryCreate() {
    $("#formCreateCategory #Title").val("");
    $("#formCreateCategory #Status").val("");
}