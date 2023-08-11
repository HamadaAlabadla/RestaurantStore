
var table = document.getElementById('UsersTable');

// Close button handler
const element = document.getElementById('AddCategoryModal');
let closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
const form = element.querySelector('#kt_modal_add-category_form');
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





    

