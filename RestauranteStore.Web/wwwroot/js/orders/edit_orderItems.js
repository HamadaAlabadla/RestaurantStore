
var table;
var datatable;
debugger
//var orderId = document.getElementById('OrderIdSpan').textContent;
var orderId = document.querySelector('[id="OrderIdSpan"]').textContent;

function drawTableProducts() {
    debugger
    table = document.querySelector('#Order_Products_table');
    function productList() {
        count = 0;
        datatable.draw();
    }
    datatable = $(table).DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": false,
        "dataSrc": "",
        "ajax": {
            "url": '/Products/GetAllProductsItemDto/' + orderId,
            "type": "POST",
            "datatype": "json",
            "data": function (filterString) {
                //filterString.filter = selectOptions.value;
                return filterString;
            }

        },
        "columns": [
            {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return `<td>
								<div class="form-check form-check-sm form-check-custom form-check-solid">
									<input class="form-check-input productCheckbox" type="checkbox" id="Checked" value="${data.productId}" />
								</div>
							</td>`;
                }
            },
            {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return `<!--begin::Product=-->
						    <td>
							    <div class="d-flex align-items-center" data-kt-ecommerce-edit-order-filter="product" data-kt-ecommerce-edit-order-id="${data.productId}">
								    <!--begin::Thumbnail-->
								    <a href="#" class="symbol symbol-50px">
									    <span class="symbol-label" style="background-image:url('/images/products/${data.image}');"></span>
								    </a>
								    <!--end::Thumbnail-->
								    <div  class="ms-5">
									    <!--begin::Title-->
									    <a href="#" class="text-gray-800 text-hover-primary fs-5 fw-bold">${data.productName}</a>
									    <!--end::Title-->
									    <!--begin::Price-->
									    <div class="fw-semibold fs-7">Price: $
									    <span data-kt-ecommerce-edit-order-filter="price">${data.price}</span></div>
									    <!--end::Price-->
									    <!--begin::NO.-->
									    <div class="text-muted fs-7">NO.: ${data.productId}</div>
									    <!--end::NO.-->
								    </div>
							    </div>
						    </td>
						    <!--end::Product=-->`;
                }
            }, {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return ` <!--begin::Qty=-->
						    <td class="pe-5 " data-order="28">
							   <span class="fw-bold ms-3">${data.supplierName}</span>
							   <span hidden id="supplierId" class="fw-bold ms-3">${data.supplierId}</span>
						    </td>
						    <!--end::Qty=-->`;
                }
            }, {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return ` <!--begin::Qty=-->
						    <td class="text-end pe-5 " data-order="28">
							    <input min="1" type="number" id = "productQTY" name = "productQTY" class="w-50 p-2 fw-bold ms-3 form-control" />
						    </td>
						    <!--end::Qty=-->`;
                }
            },
            {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return ` <!--begin::Qty=-->
						    <td class="text-end pe-5" data-order="28">
							    <span class="fw-bold ms-3">${data.qty}</span>
						    </td>
						    <!--end::Qty=-->`;
                }
            },
        ],
        "drawCallback": function () {
             
            $.ajax({
                url: '/Orders/OrderItems/' + modelId,
                type: "GET",
                typedata: "json",
                success: function (data) {
                    data.data.forEach(function (item) {
                        var target = document.querySelector(`[data-kt-ecommerce-edit-order-id="${item.productId}"]`);
                        var tr = target.closest('td').closest('tr');
                        var checkBox = tr.querySelector('[id="Checked"]');
                        var QTY = tr.querySelector('[id = "productQTY"]');
                        checkBox.checked = true;
                        QTY.value = item.qtyRequierd;
                        addProduct(checkBox, "change")
                    });
                }
            });
        }
    },);

    function handleProductSelect() {
        // Define variables
        const checkboxes = table.querySelectorAll('[type="checkbox"]');
        const target = document.getElementById('kt_ecommerce_edit_order_selected_products');
        const totalPrice = document.getElementById('kt_ecommerce_edit_order_total_price');

        // Loop through all checked products
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener('change', e => {
                // Select parent row element
                const parent = checkbox.closest('tr');

                // Clone parent element as variable
                const product = parent.querySelector('[data-kt-ecommerce-edit-order-filter="product"]').cloneNode(true);

                // Create inner wrapper
                const innerWrapper = document.createElement('div');

                // Store inner content
                const innerContent = product.innerHTML;

                // Add & remove classes on parent wrapper
                const wrapperClassesAdd = ['col', 'my-2'];
                const wrapperClassesRemove = ['d-flex', 'align-items-center'];

                // Define additional classes
                const additionalClasses = ['border', 'border-dashed', 'rounded', 'p-3', 'bg-body'];

                // Update parent wrapper classes
                product.classList.remove(...wrapperClassesRemove);
                product.classList.add(...wrapperClassesAdd);

                // Remove parent default content
                product.innerHTML = '';

                // Update inner wrapper classes
                innerWrapper.classList.add(...wrapperClassesRemove);
                innerWrapper.classList.add(...additionalClasses);

                // Apply stored inner content into new inner wrapper
                innerWrapper.innerHTML = innerContent;

                // Append new inner wrapper to parent wrapper
                product.appendChild(innerWrapper);

                // Get product id
                const productId = product.getAttribute('data-kt-ecommerce-edit-order-id');
                const QTY = parent.getElementsByName('productQTY');

                if (e.target.checked) {
                    // Add product to selected product wrapper
                    target.appendChild(product);
                    QTY.classList.remove('d-none');
                } else {
                    // Remove product from selected product wrapper
                    QTY.classList.add('d-none');
                    const selectedProduct = target.querySelector('[data-kt-ecommerce-edit-order-id="' + productId + '"]');
                    if (selectedProduct) {
                        target.removeChild(selectedProduct);
                    }
                }

                // Trigger empty message logic
                detectEmpty();
            });
        });

        // Handle empty list message
        const detectEmpty = () => {
            // Select elements
            const message = target.querySelector('span');
            const products = target.querySelectorAll('[data-kt-ecommerce-edit-order-filter="product"]');

            // Detect if element is empty
            if (products.length < 1) {
                // Show message
                message.classList.remove('d-none');

                // Reset price
                totalPrice.innerText = '0.00';
            } else {
                // Hide message
                message.classList.add('d-none');

                // Calculate price
                calculateTotal(products);
            }
        }

        // Calculate total cost
        const calculateTotal = (products) => {
            let countPrice = 0;

            // Loop through all selected prodcucts
            products.forEach(product => {
                // Get product price
                const price = parseFloat(product.querySelector('[data-kt-ecommerce-edit-order-filter="price"]').innerText);

                // Add to total
                countPrice = parseFloat(countPrice + price);
            });

            // Update total price
            totalPrice.innerText = countPrice.toFixed(2);
        }
    }

    var count = 0;
    $(document).on('click', function () {

        if (count == 0) {
            handleProductSelect();
            const QTYs = document.querySelectorAll("[name='productQTY']");

            QTYs.forEach(function (item) {
                item.addEventListener("change", function (e) {
                    // Enforce the minimum value of 1
                    if (this.value < 1) {
                        this.value = 0;
                    }
                    var target = item.closest('tr');
                    var check = target.querySelector("[id='Checked']");
                    if (check.checked) {
                        
                        var td = target.getElementsByTagName('td')[1].getElementsByTagName('div')[0];
                        var productId = td.getAttribute('data-kt-ecommerce-edit-order-id');
                        var span = document.querySelector(`[data-id="${productId}"]`);
                        var qtyConst = this.value;
                        span.innerText = this.value;
                        check.checked = false;
                        addProduct(check, "change")
                        this.value = qtyConst;
                        check.checked = true;
                        addProduct(check, "change")
                    }
                });
            });
            count += 1;
        }

    })

    const target = document.getElementById('kt_ecommerce_edit_order_selected_products');
    const totalPrice = document.getElementById('kt_ecommerce_edit_order_total_price');
    function addProduct(checkbox, e) {
        
        // Select parent row element
        const parent = checkbox.closest('tr');
        var QTY = parent.querySelector('[id="productQTY"]');
        if (QTY.value == null || QTY.value == "") {
            QTY.value = '1';
        }
        // Clone parent element as variable
        const product = parent.querySelector('[data-kt-ecommerce-edit-order-filter="product"]').cloneNode(true);

        // Create inner wrapper
        const innerWrapper = document.createElement('div');

        // Store inner content
        var innerContent = product.innerHTML;

        // Add & remove classes on parent wrapper
        const wrapperClassesAdd = ['col', 'my-2'];
        const wrapperClassesRemove = ['d-flex', 'align-items-center'];

        // Define additional classes
        const additionalClasses = ['border', 'border-dashed', 'rounded', 'p-3', 'bg-body'];

        // Update parent wrapper classes
        product.classList.remove(...wrapperClassesRemove);
        product.classList.add(...wrapperClassesAdd);

        // Remove parent default content
        product.innerHTML = '';

        // Update inner wrapper classes
        innerWrapper.classList.add(...wrapperClassesRemove);
        innerWrapper.classList.add(...additionalClasses);
        const productId = product.getAttribute('data-kt-ecommerce-edit-order-id');
        // Apply stored inner content into new inner wrapper
        innerContent += `<div  class="text-muted fs-7">   QTY :
                        <span data-kt-ecommerce-edit-order-filter="QTY" data-id="${productId}">${QTY.value}</span> </div>`;
        innerWrapper.innerHTML = innerContent;

        // Append new inner wrapper to parent wrapper
        product.appendChild(innerWrapper);

        // Get product id


        if (checkbox.checked) {
            // Add product to selected product wrapper
            target.appendChild(product);
        } else {
            // Remove product from selected product wrapper
            const selectedProduct = target.querySelector('[data-kt-ecommerce-edit-order-id="' + productId + '"]');
            if (selectedProduct) {
                target.removeChild(selectedProduct);

            }
            QTY.value = '';
        }

        // Trigger empty message logic
        detectEmpty();
        // Handle empty list message

    }

    const detectEmpty = () => {
        // Select elements
        const message = target.querySelector('span');
        const products = target.querySelectorAll('[data-kt-ecommerce-edit-order-filter="product"]');

        // Detect if element is empty
        if (products.length < 1) {
            // Show message
            message.classList.remove('d-none');

            // Reset price
            totalPrice.innerText = '0.00';
        } else {
            // Hide message
            message.classList.add('d-none');

            // Calculate price
            calculateTotal(products);
        }
    }

    // Calculate total cost
    const calculateTotal = (products) => {
        let countPrice = 0;

        // Loop through all selected prodcucts
        products.forEach(product => {
            // Get product price
            const price = parseFloat(product.querySelector('[data-kt-ecommerce-edit-order-filter="price"]').innerText);
            const QTY = parseFloat(product.querySelector('[data-kt-ecommerce-edit-order-filter="QTY"]').innerText);

            // Add to total
            countPrice = parseFloat(countPrice + (price * QTY));
        });

        // Update total price
        totalPrice.innerText = countPrice.toFixed(2);
    }
    // Handle product select
    function handleProductSelect() {
        // Define variables

        const checkboxes = table.querySelectorAll('[type="checkbox"]');



        // Loop through all checked products
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener('change', e => {
                addProduct(checkbox, e)
            });
        });


    }



}

$(document).on('click', '#submitOrderItems', function (e) {
    e.preventDefault();
    // Replace "modelId" with the ID of the model you want to edit
    //var modelId = document.querySelector('span[id="OrderIdSpan"]').textContent;
    Swal.fire({
        title: "Confirm Edit",
        text: "Are you sure you want to edit this item?",
        icon: "warning",
        buttons: ["Cancel", "Edit"],
        dangerMode: true,
    }).then((result) => {
        if (result.isConfirmed) {
            setTimeout(function () {


                
                var quantities = {};
                const checkboxes = document.querySelectorAll('input[id="Checked"]');
                checkboxes.forEach((checkbox) => {

                    if (checkbox.checked) {
                        var productId = checkbox.value;
                        var quantity = checkbox.closest('tr').querySelector('input[id="productQTY"]').value;

                        quantities[productId] = quantity;
                    }
                });
                debugger
                var formData = new FormData();
                formData.append("OrderId", orderId);
                formData.append("quantities", JSON.stringify(quantities));

                $.ajax({
                    url: '/Orders/EditOrderItems',
                    type: "Post",
                    datatype: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        var html = '';
                        var orderItemsTable = document.getElementById('orderItemsTable');
                        var total = 0;
                        data.data.forEach(function (item) {
                            debugger
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
                        var temp = `<!--begin::Subtotal-->
									<tr>
										<td colspan="4" class="text-end">Subtotal</td>
										<td id="subtotal" class="text-end">$</td>
									</tr>
									<!--end::Subtotal-->
									<!--begin::VAT-->
									<tr>
										<td colspan="4" class="text-end">VAT (0%)</td>
										<td class="text-end">$0.00</td>
									</tr>
									<!--end::VAT-->
									<!--begin::Shipping-->
									<tr>
										<td colspan="4" class="text-end">Shipping Rate</td>
										<td class="text-end">$0.00</td>
									</tr>
									<!--end::Shipping-->
									<!--begin::Grand total-->
									<tr>
										<td colspan="4" class="fs-3 text-dark text-end">Grand Total</td>
										<td  class="text-dark fs-3 fw-bolder text-end">$
											<span id="totalPrice"></span>
										</td>
									</tr>
									<!--end::Grand total-->`;
                        orderItemsTable.innerHTML = (html + temp);
                        document.getElementById('subtotal').innerText = `$ ${total}`;
                        document.getElementById('totalPrice').innerText = ` ${total}`;
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
                                $('#EditOrderModal').modal('hide');
                                $('#EditOrderModal').hide();
                                var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
                                allHide.forEach(x => {
                                    x.classList.add('d-none');
                                });
                                // Enable submit button after loading

                                // Redirect to customers list page
                                
                            }
                        });
                    },
                    error: function () {
                        submitButton.disabled = false;
                        Swal.fire({
                            html: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    },
                });
            }, 2000);
        }
    });
});