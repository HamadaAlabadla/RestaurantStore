"use strict";
// Class definition
var table;
var datatable;
table = document.querySelector('#Order_Products_table');
function productList() {
    count = 0;
    datatable.draw();
}
var Rest = document.getElementById('RestaurantId');
$.ajax({
    url: '/Users/GetRestaurant',
    success: function (rest) {
        Rest.value = rest.id;
    },
    error: function () {
        alert: "Something Error !"
    },
});
//$(document).ready(function () {
//    var suppliersList = $('#SupplierId');
//    suppliersList.empty();
//    suppliersList.append('<option></option>')
//    $.ajax({
//        "url": '/Users/GetAllSuppliers',
//        "type": "POST",
//        "datatype": "json",
//        success: function (suppliers) {
//            $.each(suppliers, function (i, supplier) {
//                suppliersList.append($('<option></option>').attr('value', supplier.id).text(supplier.name));
//            });
//        },
//        error: function () {
//            alert: "Something Error !"
//        }
//    });
//});
var KTAppEcommerceSalesSaveOrder = function () {
    // Shared variables
    
    

    // Private functions
    const initSaveOrder = () => {
        // Init flatpickr
        $('#OrderDate').flatpickr({
            altInput: true,
            altFormat: "d F, Y",
            dateFormat: "Y-m-d",
        });

        // Init select2 country options
        // Format options
        const optionFormat = (item) => {
            if ( !item.id ) {
                return item.text;
            }

            var span = document.createElement('span');
            var template = '';

            template += '<img src="' + item.element.getAttribute('data-kt-select2-country') + '" class="rounded-circle h-20px me-2" alt="image"/>';
            template += item.text;

            span.innerHTML = template;

            return $(span);
        }

        // Init Select2 --- more info: https://select2.org/        
        $('#kt_ecommerce_edit_order_billing_country').select2({
            placeholder: "Select a country",
            minimumResultsForSearch: Infinity,
            templateSelection: optionFormat,
            templateResult: optionFormat
        });

        $('#kt_ecommerce_edit_order_shipping_country').select2({
            placeholder: "Select a country",
            minimumResultsForSearch: Infinity,
            templateSelection: optionFormat,
            templateResult: optionFormat
        });

        // Init datatable --- more info on datatables: https://datatables.net/manual/

        const filterForm = document.querySelector('[id="kt_ecommerce_edit_order_form"]');
        //const selectOptions = filterForm.querySelector('[id="SupplierId"]');
        datatable = $(table).DataTable({
                "serverSide": true,
                "filter": true,
                "pagination": false,
                "dataSrc": "",
                "ajax": {
                    "url": "/Products/GetAllProductsItemDto",
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
									    <span class="symbol-label" style="background-image:url('../images/products/${data.image}');"></span>
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
                    },{
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
            },);

        
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-ecommerce-edit-order-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Handle shipping form
    //const handleShippingForm = () => {
    //    // Select elements
    //    const element = document.getElementById('kt_ecommerce_edit_order_shipping_form');
    //    const checkbox = document.getElementById('same_as_billing');

    //    // Show/hide shipping form
    //    checkbox.addEventListener('change', e => {
    //        if (e.target.checked) {
    //            element.classList.add('d-none');
    //        } else {
    //            element.classList.remove('d-none');
    //        }
    //    });
    //}

    // Handle product select
    const handleProductSelect = () => {
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

    // Submit form handler
    const handleSubmit = () => {
        // Define variables
        let validator;

        // Get elements
        const form = document.getElementById('kt_ecommerce_edit_order_form');
        const submitButton = document.getElementById('kt_ecommerce_edit_order_submit');

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'payment_method': {
                        validators: {
                            notEmpty: {
                                message: 'Payment method is required'
                            }
                        }
                    },
                    'shipping_method': {
                        validators: {
                            notEmpty: {
                                message: 'Shipping method is required'
                            }
                        }
                    },
                    'order_date': {
                        validators: {
                            notEmpty: {
                                message: 'Order date is required'
                            }
                        }
                    },
                    'billing_order_address_1': {
                        validators: {
                            notEmpty: {
                                message: 'Address line 1 is required'
                            }
                        }
                    },
                    'billing_order_postcode': {
                        validators: {
                            notEmpty: {
                                message: 'Postcode is required'
                            }
                        }
                    },
                    'billing_order_state': {
                        validators: {
                            notEmpty: {
                                message: 'State is required'
                            }
                        }
                    },
                    'billing_order_country': {
                        validators: {
                            notEmpty: {
                                message: 'Country is required'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Handle submit button
        submitButton.addEventListener('click', e => {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable submit button whilst loading
                        submitButton.disabled = true;

                        setTimeout(function () {
                            
                            submitButton.removeAttribute('data-kt-indicator');
                            var supplierIds = {};
                            var quantities = {};
                            const checkboxes = document.querySelectorAll('input[id="Checked"]');
                            checkboxes.forEach((checkbox) => {
                                
                                if (checkbox.checked) {
                                    var productId = checkbox.value;
                                    var supplierId = checkbox.closest('tr').querySelector('input[id="supplierId"]').value; 
                                    var quantity = checkbox.closest('tr').querySelector('input[id="productQTY"]').value;

                                    supplierIds[productId] = supplierId;
                                    quantities[productId] = quantity;
                                }
                            });

                            var formData = new FormData();
                            formData.append("selectedProductIds", JSON.stringify(supplierIds));
                            formData.append("quantities", JSON.stringify(quantities));
                            formData.append("Id", $("#Id").val());
                            formData.append("Id", $("#Id").val());
                            if (document.getElementById("IsDraft").checked)
                                formData.append("IsDraft", true);
                            else
                                formData.append("IsDraft", false);
                            formData.append("SupplierId", $("#SupplierId").val());
                            formData.append("RestaurantId", $("#RestaurantId").val());
                            formData.append("PaymentMethod", $("#PaymentMethod").val());
                            formData.append("OrderDate", $("#OrderDate").val());
                            formData.append("ShippingAddress", $("#ShippingAddress").val());
                            formData.append("ShippingCity", $("#ShippingCity").val());
                            formData.append("UnitPriceId", $("#UnitPriceId").val());
                            formData.append("Price", $("#Price").val());
                            formData.append("Status", $("#Status").val());
                            $.ajax({
                                url: '/Orders/Create',
                                type: "Post",
                                datatype: "json",
                                data: formData,
                                contentType: false,
                                processData: false,
                                success: function () {
                                    
                                    document.getElementById('kt_ecommerce_edit_order_form').reset();
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
                                            submitButton.disabled = false;

                                            // Redirect to customers list page
                                            window.location = form.getAttribute("data-kt-redirect");
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
                    } else {
                        submitButton.disabled = false;
                        window.location = form.getAttribute("data-kt-redirect");
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
            }
        })
    }


    // Public methods
    return {
        init: function () {

            initSaveOrder();
            handleSearchDatatable();
            //handleShippingForm();
            handleProductSelect();
            handleSubmit();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTAppEcommerceSalesSaveOrder.init();
});

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
                    debugger
                    var td = target.getElementsByTagName('td')[1].getElementsByTagName('div')[0];
                    var productId = td.getAttribute('data-kt-ecommerce-edit-order-id');
                    var span = document.querySelector(`[data-id="${productId}"]`);
                    var qtyConst = this.value;
                    span.innerText = this.value;
                    check.checked = false;
                    addProduct(check, "change")
                    this.value = qtyConst;
                    check.checked = true;
                    addProduct(check , "change")
                }
            });
        });
        count += 1;
    }
    
})

const target = document.getElementById('kt_ecommerce_edit_order_selected_products');
const totalPrice = document.getElementById('kt_ecommerce_edit_order_total_price');
function addProduct(checkbox, e) {
    debugger
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
function handleProductSelect () {
    // Define variables
    
    const checkboxes = table.querySelectorAll('[type="checkbox"]');
    

    
    // Loop through all checked products
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', e => {
            addProduct(checkbox ,e)
        });
    });


}


