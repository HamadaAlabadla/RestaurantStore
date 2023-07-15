
var table = document.getElementById('UsersTable');
var datatable;
var toolbarBase;
var toolbarSelected;
var selectedCount;

const checkboxes = table.querySelectorAll('[type="checkbox"]');

// Select elements
toolbarBase = document.querySelector('[data-kt-user-table-toolbar="base"]');
toolbarSelected = document.querySelector('[data-kt-user-table-toolbar="selected"]');
selectedCount = document.querySelector('[data-kt-user-table-select="selected_count"]');
const deleteSelected = document.querySelector('[data-kt-user-table-select="delete_selected"]');

// Toggle delete selected toolbar
checkboxes.forEach(c => {
    // Checkbox on click event
    c.addEventListener('click', function () {
        setTimeout(function () {
            toggleToolbars();
        }, 50);
    });
});





// Deleted selected rows
deleteSelected.addEventListener('click', function () {
    // SweetAlert2 pop up --- official docs reference: https://sweetalert2.github.io/
    Swal.fire({
        text: "Are you sure you want to delete selected customers?",
        icon: "warning",
        showCancelButton: true,
        buttonsStyling: false,
        confirmButtonText: "Yes, delete!",
        cancelButtonText: "No, cancel",
        customClass: {
            confirmButton: "btn fw-bold btn-danger",
            cancelButton: "btn fw-bold btn-active-light-primary"
        }
    }).then(function (result) {
        if (result.value) {
            Swal.fire({
                text: "You have deleted all selected customers!.",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn fw-bold btn-primary",
                }
            }).then(function () {
                // Remove all selected customers
                checkboxes.forEach(c => {
                    if (c.checked) {
                        datatable.row($(c.closest('tbody tr'))).remove().draw();
                    }
                });

                // Remove header checked box
                const headerCheckbox = table.querySelectorAll('[type="checkbox"]')[0];
                headerCheckbox.checked = false;
            }).then(function () {
                toggleToolbars(); // Detect checked checkboxes
                initToggleToolbar(); // Re-init toolbar to recalculate checkboxes
            });
        } else if (result.dismiss === 'cancel') {
            Swal.fire({
                text: "Selected customers was not deleted.",
                icon: "error",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn fw-bold btn-primary",
                }
            });
        }
    });
});


function toggleToolbars() {
    debugger
    // Select refreshed checkbox DOM elements 
    const allCheckboxes = table.querySelectorAll('tbody [type="checkbox"]');

    // Detect checkboxes state & count
    let checkedState = false;
    let count = 0;

    // Count checked boxes
    allCheckboxes.forEach(c => {
        if (c.checked) {
            checkedState = true;
            count++;
        }
    });

    // Toggle toolbars
    if (checkedState) {
        selectedCount.innerHTML = count;
        toolbarBase.classList.add('d-none');
        toolbarSelected.classList.remove('d-none');
    } else {
        toolbarBase.classList.remove('d-none');
        toolbarSelected.classList.add('d-none');
    }
};

function modalView () {

    const user = document.getElementById('kt_modal_add_user');
    const restorante = document.getElementById('kt_modal_add_restorante');
    const radioAdmin = document.getElementsByName('user_role');
    //document.getElementById('radio_add_admin');
    radioAdmin.forEach(x => {
        if (x.checked) {
            if (x.value == 1 )
            {
                document.getElementById('usertype').value = 2;
                user.classList.remove('d-none');
                restorante.classList.add('d-none');
            } else if(x.value == 2)
            {
                document.getElementById('usertype').value = 0;
                user.classList.remove('d-none');
                restorante.classList.add('d-none');
            }
            else if (x.value == 3)
            {
                document.getElementById('restorantetype').value = 1;
                restorante.classList.remove('d-none');
                user.classList.add('d-none');
            }
            else
            {
                user.classList.add('d-none');
                restorante.classList.add('d-none');
            }
        }
    });
}

// Close button handler
const element = document.getElementById('kt_modal_select_role');
const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
const form = element.querySelector('#kt_modal_add_user_form');
const modal = new bootstrap.Modal(element);

closeButton.addEventListener('click', e => {
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
            }
               
            );
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
const cancelButton = element.querySelector('[data-kt-users-modal-action="cancel"]');
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

function MenuShow(id) {
    var Actions = document.getElementById('Action-' + id);
    var ActionsList = document.getElementsByName('Action');
    if (Actions.classList.contains('menu')
        || Actions.classList.contains('menu-sub-dropdown')
        || Actions.classList.contains('menu-sub'))
    {
        Actions.classList.remove('menu');
        Actions.classList.remove('menu-sub');
        Actions.classList.remove('menu-sub-dropdown');
        ActionsList.forEach(x => {
            if (x != Actions) {
                x.classList.add('menu');
                x.classList.add('menu-sub');
                x.classList.add('menu-sub-dropdown');
            }
        });
        
    } else {
        Actions.classList.add('menu');
        Actions.classList.add('menu-sub');
        Actions.classList.add('menu-sub-dropdown');
    }
}

function Delete (id) {
    $('#Delete-'+id).ready(function () {
        // Data to be sent in the request
        var data = {
            id: id
        };

        // Perform AJAX request
        $.ajax({
            url: '/Admins/Delete',
            type: 'POST',
            data: data,
            success: function (response) {
                // Handle the response after the request is successful
                console.log('POST request successful');
                console.log(response);
            },
            error: function (xhr, status, error) {
                // Handle the error if the request fails
                console.error('POST request error:', error);
            }
        });
    });
}


    

