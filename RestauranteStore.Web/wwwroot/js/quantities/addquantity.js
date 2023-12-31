﻿
var table = document.getElementById('UsersTable');
debugger
// Close button handler
const element = document.getElementById('AddQuantityModal');
//let closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
const form = element.querySelector('#kt_modal_add-quantity');
const modal = new bootstrap.Modal(element);

//closeButton.addEventListener('click', e => {
//    e.preventDefault();

//    Swal.fire({
//        text: "Are you sure you would like to cancel?",
//        icon: "warning",
//        showCancelButton: true,
//        buttonsStyling: false,
//        confirmButtonText: "Yes, cancel it!",
//        cancelButtonText: "No, return",
//        customClass: {
//            confirmButton: "btn btn-primary",
//            cancelButton: "btn btn-active-light"
//        }
//    }).then(function (result) {
//        if (result.value) {
//            form.reset(); // Reset form			
//            modal.hide();
//            var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
//            allHide.forEach(x => {
//                x.classList.add('d-none');
//            }
               
//            );
//        } else if (result.dismiss === 'cancel') {
//            Swal.fire({
//                text: "Your form has not been cancelled!.",
//                icon: "error",
//                buttonsStyling: false,
//                confirmButtonText: "Ok, got it!",
//                customClass: {
//                    confirmButton: "btn btn-primary",
//                }
//            });
//        }
//    });
//});

// Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
var validator = FormValidation.formValidation(
    form,
    {
        fields: {
            'user_name': {
                validators: {
                    notEmpty: {
                        message: 'Full name is required'
                    }
                }
            },
            'user_email': {
                validators: {
                    notEmpty: {
                        message: 'Valid email address is required'
                    }
                }
            },
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

// Submit button handler
        //const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        //submitButton.addEventListener('click', e => {
        //    e.preventDefault();

        //    // Validate form before submit
        //    if (validator) {
        //        validator.validate().then(function (status) {
        //            console.log('validated!');

        //            if (status == 'Valid') {
        //                // Show loading indication
        //                submitButton.setAttribute('data-kt-indicator', 'on');

        //                // Disable button to avoid multiple click 
        //                submitButton.disabled = true;

        //                // Simulate form submission. For more info check the plugin's official documentation: https://sweetalert2.github.io/
        //                setTimeout(function () {
        //                    // Remove loading indication
        //                    submitButton.removeAttribute('data-kt-indicator');

        //                    // Enable button
        //                    submitButton.disabled = false;

        //                    // Show popup confirmation 
        //                    Swal.fire({
        //                        text: "Form has been successfully submitted!",
        //                        icon: "success",
        //                        buttonsStyling: false,
        //                        confirmButtonText: "Ok, got it!",
        //                        customClass: {
        //                            confirmButton: "btn btn-primary"
        //                        }
        //                    }).then(function (result) {
        //                        if (result.isConfirmed) {
        //                            modal.hide();
        //                            var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
        //                            allHide.forEach(x => {
        //                                x.classList.add('d-none');
        //                            });
        //                        }
        //                    });

        //                    //form.submit(); // Submit form
        //                }, 2000);
        //            } else {
        //                // Show popup warning. For more info check the plugin's official documentation: https://sweetalert2.github.io/
        //                Swal.fire({
        //                    text: "Sorry, looks like there are some errors detected, please try again.",
        //                    icon: "error",
        //                    buttonsStyling: false,
        //                    confirmButtonText: "Ok, got it!",
        //                    customClass: {
        //                        confirmButton: "btn btn-primary"
        //                    }
        //                });
        //            }
        //        });
        //    }
        //});

// Cancel button handler
const cancelButtons = element.querySelectorAll('[data-kt-users-modal-action="cancel"]');

cancelButtons.forEach((cancelButton) => {
    cancelButton.addEventListener('click', e => {
        e.preventDefault();

        Swal.fire({
            text: "Are you sure you would like to cancel?",
            icon: "warning",
            showCancelButton: true,
            buttonsStyling: false,
            confirmButtonText: "Yes, cancel it!",
            cancelButtonText: "No, return",
            customClass: {
                confirmButton: "btn btn-primary",
                cancelButton: "btn btn-active-light"
            }
        }).then(function (result) {
            if (result.value) {
                form.reset(); // Reset form			
                modal.hide();
                var allHide = document.querySelectorAll('[class="modal-backdrop show"]');
                allHide.forEach(x => {
                    x.classList.add('d-none');
                });
            } else if (result.dismiss === 'cancel') {
                Swal.fire({
                    text: "Your form has not been cancelled!.",
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-primary",
                    }
                });
            }
        });
    });

});





    

