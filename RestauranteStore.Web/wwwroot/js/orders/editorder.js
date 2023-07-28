var OrderDetailsTable = document.getElementById("OrderDetails");

$(document).ready(function () {
    var orderId = document.querySelector('[id="OrderIdSpan"]').textContent;
    $.ajax({
        url: '/Orders/OrderDetails/' + orderId,
        type: "GET",
        typedata: "json",
        success: function (data) {
            OrderDetailsTable.querySelector('[id="DateAdded"]').innerHTML = data.data.dateAdded;
            OrderDetailsTable.querySelector('[id="paymentMethod"]').textContent = data.data.paymentMethod;
            OrderDetailsTable.querySelector('[id="StatusOrder"]').textContent = data.data.statusOrder;

        }
    });
    var tableRestaurant = document.getElementById('RestaurantDetails');
    if (tableRestaurant) {
        $.ajax({
            url: '/Orders/RestaurantDetails/' + orderId,
            type: "GET",
            typedata: "json",
            success: function (data) {
                tableRestaurant.querySelector('[id="Name"]').textContent = data.data.name;
                tableRestaurant.querySelector('[id="Image"]').setAttribute('src', `/images/users/${data.data.image}`);
                tableRestaurant.querySelector('[id="Email"]').textContent = data.data.email;
                tableRestaurant.querySelector('[id="Phone"]').textContent = data.data.phone;

            }
        });
    }
    var tableSupplier = document.getElementById('SupplierDetails');
    if (tableSupplier) {
        $.ajax({
            url: '/Orders/SupplierDetails/' + orderId,
            type: "GET",
            typedata: "json",
            success: function (data) {

                tableSupplier.querySelector('[id="Name"]').innerHTML = data.data.name;
                tableSupplier.querySelector('[id="Image"]').setAttribute('src', `/images/users/${data.data.image}`);
                tableSupplier.querySelector('[id="Email"]').textContent = data.data.email;
                tableSupplier.querySelector('[id="Phone"]').textContent = data.data.phone;

            }
        });
    }

    var PaymentDivs = document.getElementsByName('Payment');
    $.ajax({
        url: '/Orders/PaymentDetails/' + orderId,
        type: "GET",
        typedata: "json",
        success: function (data) {
            var html = '';
            PaymentDivs.forEach(function (PaymentDiv) {
                html = PaymentDiv.innerHTML;
                html += `${data.data.shippingAddress}
                        <br/>
                        ${data.data.shippingCity}`;
                PaymentDiv.innerHTML = html;
            })

        }
    });

    $.ajax({
        url: '/Orders/OrderItems/' + orderId,
        type: "GET",
        typedata: "json",
        success: function (data) {
            var html = '';
            var orderItemsTable = document.getElementById('orderItemsTable');
            var total = 0;
            data.data.forEach(function (item) {
                var totalPrice = item.price * item.qtyRequierd;
                total += totalPrice;
                html += `<tr>
					        <!--begin::Product-->
					        <td>
						        <div class="d-flex align-items-center">
							        <!--begin::Thumbnail-->
							        <a href="#" class="symbol symbol-50px">
								        <span class="symbol-label" style="background-image:url('/images/products/${item.productImage}');"></span>
							        </a>
							        <!--end::Thumbnail-->
							        <!--begin::Title-->
							        <div class="ms-5">
								        <a href="#" class="fw-bold text-gray-600 text-hover-primary">${item.productName}</a>
								        <div class="fs-7 text-muted">Delivery Date: ${item.orderDate}</div>
							        </div>
							        <!--end::Title-->
						        </div>
					        </td>
					        <!--end::Product-->
					        <!--begin::SKU-->
					        <td class="text-end">${item.productId}</td>
					        <!--end::SKU-->
					        <!--begin::Quantity-->
					        <td class="text-end">${item.qtyRequierd}</td>
					        <!--end::Quantity-->
					        <!--begin::Price-->
					        <td class="text-end">$${item.price}</td>
					        <!--end::Price-->
					        <!--begin::Total-->
					        <td class="text-end">$${totalPrice}</td>
					        <!--end::Total-->
				        </tr>
				        `;


            });
            var temp = orderItemsTable.innerHTML;
            orderItemsTable.innerHTML = (html + temp);
            document.getElementById('subtotal').innerText = `$ ${total}`;
            document.getElementById('totalPrice').innerText = ` ${total}`;
        }
    });
});



function loadEditForm(id) {
    $.ajax({

        url: '/Orders/EditOrderDetails',
        type: 'GET',
        data: { id: id },
        success: function (result) {

            $('#EditOrderModal .modal-content').html(result);
            // Init flatpickr
            $('#OrderDate').flatpickr({
                altInput: true,
                altFormat: "d F, Y",
                dateFormat: "Y-m-d",
            });
            // $('#editUserModal').modal('show');
        },
        error: function (error) {
            console.error('Error while loading the edit form:', error);
        }
    });
}

function loadEditPaymentForm(id) {
    $.ajax({

        url: '/Orders/EditPaymentDetails',
        type: 'GET',
        data: { id: id },
        success: function (result) {

            $('#EditOrderModal .modal-content').html(result);
            // Init flatpickr
            // $('#editUserModal').modal('show');
        },
        error: function (error) {
            console.error('Error while loading the edit form:', error);
        }
    });
}

$(document).on('click', '#editOrderDetailsLink', function (e) {
    e.preventDefault();
    // Replace "modelId" with the ID of the model you want to edit
    var modelId = document.querySelector('span[id="OrderIdSpan"]').textContent;


    loadEditForm(modelId);
});


$(document).on('click', '#editPaymentDetailsLink', function (e) {
    e.preventDefault();
    // Replace "modelId" with the ID of the model you want to edit
    var modelId = document.querySelector('span[id="OrderIdSpan"]').textContent;


    loadEditPaymentForm(modelId);
});


$(document).on('submit', '#kt_modal_edit_order_details', function (e) {
    e.preventDefault();
    var formEdit = this;
    var formData = $(this).serialize();

    $.ajax({
        url: '/Orders/EditOrderDetails',
        type: 'POST',
        data: formData,
        success: function (data) {
            OrderDetailsTable.querySelector('[id="DateAdded"]').textContent = data.data.dateAdded;
            OrderDetailsTable.querySelector('[id="paymentMethod"]').textContent = data.data.paymentMethod;
            OrderDetailsTable.querySelector('[id="StatusOrder"]').textContent = data.data.statusOrder;

            // Handle the success response
            // For example, you can close the modal and update the displayed data
            Swal.fire({
                text: "order has been successfully submitted!",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            }).then(function (result) {
                if (result.isConfirmed) {
                    
                    // Enable submit button after loading
                    $('#EditOrderModal').modal('hide');
                    formEdit.reset(); // Reset form			
                    $('#EditOrderModal').hide();
                    var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
                    allHide.forEach(x => {
                        x.classList.add('d-none');
                    });
                    // Redirect to customers list page
                    //window.location = form.getAttribute("data-kt-redirect");
                }
            });

            // Perform any other action on success
        },
        error: function (error) {
            Swal.fire({
                html: "Sorry, looks like there are some errors detected, please try again.",
                icon: "error",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
        }
    });
});
(document).on('submit', '#kt_modal_edit_payment_details', function (e) {
    e.preventDefault();
    var formEdit = this;
    var formData = $(this).serialize();

    $.ajax({
        url: '/Orders/EditPaymentDetails',
        type: 'POST',
        data: formData,
        success: function (data) {
            var PaymentDivs = document.getElementsByName('Payment');
            var html = '';
            PaymentDivs.forEach(function (PaymentDiv) {
                html = PaymentDiv.innerHTML;
                html += `${data.data.shippingAddress}
                            <br/>
                            ${data.data.shippingCity}`;
                PaymentDiv.innerHTML = html;
            })

            // Handle the success response
            // For example, you can close the modal and update the displayed data
            Swal.fire({
                text: "order has been successfully submitted!",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            }).then(function (result) {
                if (result.isConfirmed) {
                    
                    // Enable submit button after loading
                    $('#EditOrderModal').modal('hide');
                    formEdit.reset(); // Reset form			
                    $('#EditOrderModal').hide();
                    var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
                    allHide.forEach(x => {
                        x.classList.add('d-none');
                    });
                    // Redirect to customers list page
                    //window.location = form.getAttribute("data-kt-redirect");
                }
            });

            // Perform any other action on success
        },
        error: function (error) {
            Swal.fire({
                html: "Sorry, looks like there are some errors detected, please try again.",
                icon: "error",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
        }
    });
});