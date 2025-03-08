(function ($) {
    var _$form = $("#checkoutContainer");

    loadItemSession();

    function loadItemSession() {
        $.ajax({
            url: "/Cart/GetCart",
            type: "GET",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    renderItem(response.cartItemVM);
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...!",
                        text: "Lỗi khi lấy giỏ hàng",
                        customClass: {
                            container: "custom-swal-container",
                            popup: "custom-swal-popup"
                        },
                        timer: 3000
                    });
                }
            },
            error: function () { }
        });
    }

    function renderItem(cartItemVM) {
        var itemHTML = "",
            totalTemporary = 0,
            totalPrice = 0;

        if (cartItemVM.length === 0) {
            _$form.find(".complete-payment").addClass("button-disabled");

            itemHTML += `
                    <li class="header-cart-item flex-w flex-t p-t-18">
                        <span>Giỏ hàng đang trống</span>
                    </li>
                `;
        } else {
            _$form.find(".complete-payment").removeClass("button-disabled");

            cartItemVM.forEach(item => {
                var formattedPrice = item.price.toLocaleString("vi-VN") + "₫";

                totalTemporary += item.price * item.quantity;

                itemHTML += `
                    <li class="header-cart-item flex-w flex-t p-t-18 block-cart-item">
                        <input class="item-id" value="${item.productId}" hidden />
                        <div class="header-cart-item-img">
                            <span class="item-number">${item.quantity}</span>
                            <img class="bor10" src="${item.imageURL}" alt="${item.name}" />
                        </div>
                        <div class="header-cart-item-txt">
                            <div class="header-cart-item-name m-b-8 trans-04 item-name">${item.name}</div>
                            <span class="header-cart-item-info">
                                <div class="item-txt">${item.size}, ${item.color}</div>
                                <div class="item-price">${formattedPrice}</div>
                            </span>
                        </div>
                    </li>
                `;
            });
        }

        totalPrice = totalTemporary;

        var value = _$form.find(".shipping-fee").val();

        if (value.trim() === "") {
            totalPrice = totalTemporary + 0;
        } else {
            value = parseInt(value);
            totalPrice = totalTemporary + value;
        }

        itemHTML = itemHTML.trim();
        _$form.find(".list-item-cart").html(itemHTML);
        _$form.find(".total-temporary").text(totalTemporary.toLocaleString("vi-VN") + "₫");
        _$form.find(".order-total").text(totalPrice.toLocaleString("vi-VN") + "₫");
    }

    var jsonData;

    // Tải dữ liệu từ JSON
    $.getJSON("/data/vietnam_units.json", function (data) {
        jsonData = data;

        var provinces = data.provinces;
        $.each(provinces, function (index, province) {
            _$form.find("#province").append('<option value="' + province.code + '" data-name="' + province.name + '">' + province.name + '</option>');
        });
    });

    // Khi chọn tỉnh/thành phố
    _$form.find("#province").change(function () {
        var selectedOption = $(this).find(":selected"),
            provinceCode = selectedOption.val(),
            provinceName = selectedOption.attr("data-name");

        _$form.find("#selectedProvince").val(provinceName);

        _$form.find("#district").empty().append('<option value="">Quận/Huyện</option>');
        _$form.find("#ward").empty().append('<option value="">Phường/Xã</option>');


        var provinceSelect = _$form.find("#province").next(".select2").find(".select2-selection__rendered");
        if (provinceCode) {
            provinceSelect.css("color", "#333");
        } else {
            provinceSelect.css("color", "");
        }

        if (provinceCode) {
            var districts = jsonData.districts.filter(d => d.province_code === provinceCode);
            $.each(districts, function (index, district) {
                _$form.find("#district").append('<option value="' + district.code + '" data-name="' + district.name + '">' + district.name + '</option>');
            });
        }

        _$form.find("#selectedDistrict").val("");
        _$form.find("#selectedWard").val("");

        var districtSelect = _$form.find("#district").next(".select2").find(".select2-selection__rendered");
        var wardSelect = _$form.find("#ward").next(".select2").find(".select2-selection__rendered");
        districtSelect.css("color", "");
        wardSelect.css("color", "");
    });

    // Khi chọn quận/huyện
    _$form.find("#district").change(function () {
        var selectedOption = $(this).find(":selected"),
            districtCode = selectedOption.val(),
            districtName = selectedOption.attr("data-name");

        _$form.find("#selectedDistrict").val(districtName);

        _$form.find("#ward").empty().append('<option value="">Phường/Xã</option>');

        var districtSelect = _$form.find("#district").next(".select2").find(".select2-selection__rendered");
        if (districtCode) {
            districtSelect.css("color", "#333");
        } else {
            districtSelect.css("color", "");
        }

        if (districtCode) {
            var wards = jsonData.wards.filter(w => w.district_code === districtCode);
            $.each(wards, function (index, ward) {
                _$form.find("#ward").append('<option value="' + ward.code + '" data-name="' + ward.name + '">' + ward.name + '</option>');
            });
        }

        _$form.find("#selectedWard").val("");

        var wardSelect = _$form.find("#ward").next(".select2").find(".select2-selection__rendered");
        wardSelect.css("color", "");
    });

    _$form.find("#ward").change(function () {
        var selectedOption = $(this).find(":selected"),
            wardCode = selectedOption.val(),
            wardName = selectedOption.attr("data-name");

        _$form.find("#selectedWard").val(wardName);

        var wardSelect = _$form.find("#ward").next(".select2").find(".select2-selection__rendered");
        if (wardCode) {
            wardSelect.css("color", "#333");
        } else {
            wardSelect.css("color", "");
        }
    });

    $(document).on("updateCartItem", function () {
        loadItemSession();
    })
})(jQuery)