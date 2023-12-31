"use strict";

// Class definition
var KTAppEcommerceSaveProduct = function () {

    // Private functions

    // Init quill editor
    const initQuill = () => {
        // Define all elements for quill editor
        const elements = [
            '#kt_ecommerce_add_product_description',
            '#kt_ecommerce_add_product_meta_description'
        ];

        // Loop all elements
        elements.forEach(element => {
            // Get quill element
            let quill = document.querySelector(element);

            // Break if element not found
            if (!quill) {
                return;
            }

            // Init quill --- more info: https://quilljs.com/docs/quickstart/
            quill = new Quill(element, {
                modules: {
                    toolbar: [
                        [{
                            header: [1, 2, false]
                        }],
                        ['bold', 'italic', 'underline'],
                        ['image', 'code-block']
                    ]
                },
                placeholder: 'Type your text here...',
                theme: 'snow' // or 'bubble'
            });
        });
    }

    // Init tagify
    const initTagify = () => {
        // Define all elements for tagify
        const elements = [
            '#kt_ecommerce_add_product_category',
            '#kt_ecommerce_add_product_tags'
        ];

        // Loop all elements
        elements.forEach(element => {
            // Get tagify element
            const tagify = document.querySelector(element);

            // Break if element not found
            if (!tagify) {
                return;
            }

            // Init tagify --- more info: https://yaireo.github.io/tagify/
            new Tagify(tagify, {
                whitelist: ["new", "trending", "sale", "discounted", "selling fast", "last 10"],
                dropdown: {
                    maxItems: 20,           // <- mixumum allowed rendered suggestions
                    classname: "tagify__inline__suggestions", // <- custom classname for this dropdown, so it could be targeted
                    enabled: 0,             // <- show suggestions on focus
                    closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
                }
            });
        });
    }

    //// Init form repeater --- more info: https://github.com/DubFriend/jquery.repeater
    //const initFormRepeater = () => {
    //    $('#kt_ecommerce_add_product_options').repeater({
    //        initEmpty: false,

    //        defaultValues: {
    //            'text-input': 'foo'
    //        },

    //        show: function () {
    //            $(this).slideDown();

    //            // Init select2 on new repeated items
    //            initConditionsSelect2();
    //        },

    //        hide: function (deleteElement) {
    //            $(this).slideUp(deleteElement);
    //        }
    //    });
    //}

    // Init condition select2
    const initConditionsSelect2 = () => {
        // Tnit new repeating condition types
        const allConditionTypes = document.querySelectorAll('[data-kt-ecommerce-catalog-add-product="product_option"]');
        allConditionTypes.forEach(type => {
            if ($(type).hasClass("select2-hidden-accessible")) {
                return;
            } else {
                $(type).select2({
                    minimumResultsForSearch: -1
                });
            }
        });
    }


    // Init noUIslider
    //const initSlider = () => {
    //    var slider = document.querySelector("#kt_ecommerce_add_product_discount_slider");
    //    var value = document.querySelector("#kt_ecommerce_add_product_discount_label");

    //    noUiSlider.create(slider, {
    //        start: [10],
    //        connect: true,
    //        range: {
    //            "min": 1,
    //            "max": 100
    //        }
    //    });

    //    slider.noUiSlider.on("update", function (values, handle) {
    //        value.innerHTML = Math.round(values[handle]);
    //        if (handle) {
    //            value.innerHTML = Math.round(values[handle]);
    //        }
    //    });
    //}

    // Init DropzoneJS --- more info:
    //const initDropzone = () => {
    //    var myDropzone = new Dropzone("#kt_ecommerce_add_product_media", {
    //        url: "https://keenthemes.com/scripts/void.php", // Set the url for your upload script location
    //        paramName: "file", // The name that will be used to transfer the file
    //        maxFiles: 10,
    //        maxFilesize: 10, // MB
    //        addRemoveLinks: true,
    //        accept: function (file, done) {
    //            if (file.name == "wow.jpg") {
    //                done("Naha, you don't.");
    //            } else {
    //                done();
    //            }
    //        }
    //    });
    //}

    // Handle discount options
    const handleDiscount = () => {
        const discountOptions = document.querySelectorAll('input[name="discount_option"]');
        const percentageEl = document.getElementById('kt_ecommerce_add_product_discount_percentage');
        const fixedEl = document.getElementById('kt_ecommerce_add_product_discount_fixed');

        discountOptions.forEach(option => {
            option.addEventListener('change', e => {
                const value = e.target.value;

                switch (value) {
                    case '2': {
                        percentageEl.classList.remove('d-none');
                        fixedEl.classList.add('d-none');
                        break;
                    }
                    case '3': {
                        percentageEl.classList.add('d-none');
                        fixedEl.classList.remove('d-none');
                        break;
                    }
                    default: {
                        percentageEl.classList.add('d-none');
                        fixedEl.classList.add('d-none');
                        break;
                    }
                }
            });
        });
    }

    //// Shipping option handler
    //const handleShipping = () => {
    //    const shippingOption = document.getElementById('kt_ecommerce_add_product_shipping_checkbox');
    //    const shippingForm = document.getElementById('kt_ecommerce_add_product_shipping');

    //    shippingOption.addEventListener('change', e => {
    //        const value = e.target.checked;

    //        if (value) {
    //            shippingForm.classList.remove('d-none');
    //        } else {
    //            shippingForm.classList.add('d-none');
    //        }
    //    });
    //}

    // Category status handler
    const handleStatus = () => {
        const target = document.getElementById('kt_ecommerce_add_product_status');
        const select = document.querySelector('[data-id = "kt_ecommerce_add_product_status_select"]');
        const statusClasses = ['bg-success', 'bg-warning', 'bg-danger'];

        $(select).on('change', function (e) {
            debugger
            const value = e.target.value;

            switch (value) {
                case "0": {
                    target.classList.remove(...statusClasses);
                    target.classList.add('bg-success');
                    hideDatepicker();
                    break;
                }
                case "1": {
                    target.classList.remove(...statusClasses);
                    target.classList.add('bg-warning');
                    showDatepicker();
                    break;
                }
                case "2": {
                    target.classList.remove(...statusClasses);
                    target.classList.add('bg-danger');
                    hideDatepicker();
                    break;
                }
                case "3": {
                    target.classList.remove(...statusClasses);
                    target.classList.add('bg-primary');
                    hideDatepicker();
                    break;
                }
                default:
                    break;
            }
        });


        // Handle datepicker
        const datepicker = document.getElementById('kt_ecommerce_add_product_status_datepicker');

        // Init flatpickr --- more info: https://flatpickr.js.org/
        $('#kt_ecommerce_add_product_status_datepicker').flatpickr({
            enableTime: true,
            dateFormat: "Y-m-d H:i",
        });

        const showDatepicker = () => {
            datepicker.parentNode.classList.remove('d-none');
        }

        const hideDatepicker = () => {
            datepicker.parentNode.classList.add('d-none');
        }
    }

    // Condition type handler
    const handleConditions = () => {
        const allConditions = document.querySelectorAll('[name="method"][type="radio"]');
        const conditionMatch = document.querySelector('[data-kt-ecommerce-catalog-add-category="auto-options"]');
        allConditions.forEach(radio => {
            radio.addEventListener('change', e => {
                if (e.target.value === '1') {
                    conditionMatch.classList.remove('d-none');
                } else {
                    conditionMatch.classList.add('d-none');
                }
            });
        })
    }

    // Submit form handler
    const handleSubmit = () => {
        // Define variables
        let validator;

        // Get elements
        const form = document.getElementById('kt_ecommerce_add_product_form');
        const submitButton = document.getElementById('kt_ecommerce_add_product_submit');


        // Handle submit button
        submitButton.addEventListener('click', e => {
            e.preventDefault();
            e.stopPropagation(); // Prevent event from bubbling up

            form.classList.add("was-validated");

            if (form.checkValidity() === false) {
                // Form is invalid, do not proceed with submission
                return;
            }

            submitButton.setAttribute('data-kt-indicator', 'on');

            // Disable submit button whilst loading
            submitButton.disabled = true;

            setTimeout(function () {
                submitButton.removeAttribute('data-kt-indicator');
                var des = document.getElementById('kt_ecommerce_add_product_description').textContent;
                document.getElementById('Description').value = des;
                            
                var formData = new FormData();
                if ($("#Image")[0].files !== null) {
                    formData.append("Image", $("#Image")[0].files[0]);
                }
                formData.append("Name", $("#Name").val());
                formData.append("QTY", $("#QTY").val());
                formData.append("Description", $("#Description").val());
                formData.append("ProductNumber", $("#ProductNumber").val());
                formData.append("UserId", $("#UserId").val());
                formData.append("CategoryId", $("#CategoryId").val());
                formData.append("QuantityUnitId", $("#QuantityUnitId").val());
                formData.append("UnitPriceId", $("#UnitPriceId").val());
                formData.append("Price", $("#Price").val());
                formData.append("Status", $("#Status").val());
                $.ajax({
                    url: '/Products/Edit',
                    type: "Post",
                    datatype: "json",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function () {
                        debugger
                        document.getElementById('kt_ecommerce_add_product_form').reset();
                        Swal.fire({
                            text: "Product has been successfully submitted!",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            if (result.isConfirmed) {
                                debugger
                                // Enable submit button after loading
                                submitButton.disabled = false;

                                // Redirect to customers list page
                                window.location = form.getAttribute("data-kt-redirect");
                                window.location.href = "/Products/Index";
                            }
                        });
                    },
                    error: function () {
                        submitButton.disabled = true;
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
        })
    }

    // Public methods
    return {
        init: function () {
            // Init forms
            initQuill();
            initTagify();
            //initSlider();
            //initFormRepeater();
            //initDropzone();
            initConditionsSelect2();

            // Handle forms
            handleStatus();
            handleConditions();
            handleDiscount();
            //handleShipping();
            handleSubmit();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTAppEcommerceSaveProduct.init();
});

$(document).ready(function () {
    var CatList = $('#CategoryId');
    CatList.empty();
    CatList.append('<option style="display:none;"></option>')
    $.ajax({
        url: '/Categories/IndexAjax',
        success: function (categories) {
            $.each(categories, function (i, category) {
                CatList.append($('<option></option>').attr('value', category.id).text(category.name));
            });
        },
        error: function () {
            alert: "Something Error !"
        },
    });

    var UnitPriceList = $('#UnitPriceId');
    UnitPriceList.empty();
    UnitPriceList.append('<option></option>')
    $.ajax({
        url: '/UnitsPrice/IndexAjax',
        success: function (UnitsPrice) {
            $.each(UnitsPrice, function (i, unitprice) {
                UnitPriceList.append($('<option></option>').attr('value', unitprice.id).text(unitprice.name));
            });
        },
        error: function () {
            alert: "Something Error !"
        },
    });

    var QuantityUnitList = $('#QuantityUnitId');
    QuantityUnitList.empty();
    QuantityUnitList.append('<option></option>')
    $.ajax({
        url: '/QuantityUnits/IndexAjax',
        success: function (QuantityUnits) {
            $.each(QuantityUnits, function (i, QuantityUnit) {
                QuantityUnitList.append($('<option></option>').attr('value', QuantityUnit.id).text(QuantityUnit.name));
            });
        },
        error: function () {
            alert: "Something Error !"
        },
    });

    var Users = document.getElementById('UserId');
    $.ajax({
        url: '/Users/GetSupplier',
        success: function (User) {
            debugger
            Users.value = User.id;
        },
        error: function () {
            alert: "Something Error !"
        },
    });
});

$(document).ready(function () {
    document.getElementById('kt_ecommerce_add_product_description').textContent = document.getElementById('Description').value
});

