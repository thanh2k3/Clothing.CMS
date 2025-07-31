(function ($) {
	var _$form = $("#loginForm");

	$("#loginForm").validate({
		rules: {
			Email: {
				required: true,
				email: true
			},
			Password: {
				required: true,
				minlength: 6
			}
		},
		messages: {
			Email: {
				required: "Vui lòng nhập Email.",
				email: "Email không hợp lệ."
			},
			Password: {
				required: "Vui lòng nhập mật khẩu.",
				minlength: "Mật khẩu phải có ít nhất 6 ký tự."
			}
		},
		errorClass: "text-danger",
		errorPlacement: function (error, element) {
			error.insertAfter(element);
		},
		highlight: function (element) {
			$(element).addClass("is-invalid");
		},
		unhighlight: function (element) {
			$(element).removeClass("is-invalid");
		}
	});


	$(document).on("click", ".login-admin", function (e) {
		e.preventDefault();

		if (!_$form.valid()) {
			return;
		}

		var formData = _$form.serialize();

		$.ajax({
			url: "/Admin/Account/Login",
			type: "POST",
			data: formData,
			dataType: "json",
			success: function (result) {
				if (result.success === true) {
					window.location.href = "/admin/home";
				}
				else {
					toastr.error(result.message, null, { timeOut: 3000, positionClass: "toast-top-right" })
				}
			}
		})
	});
})(jQuery)