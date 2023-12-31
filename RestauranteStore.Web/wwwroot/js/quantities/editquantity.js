﻿function loadEditForm(id) {
    
    $.ajax({
        
        url: '/QuantityUnits/Edit',
        type: 'GET',
        data: { id: id },
        success: function (result) {
            
            $('#EditQuantityModal .modal-body').html(result);
           // $('#editUserModal').modal('show');
        },
        error: function (error) {
            console.error('Error while loading the edit form:', error);
        }
    });
}

$(document).on('click', '#editModelLink', function (e) {
    e.preventDefault();
    debugger
    // Replace "modelId" with the ID of the model you want to edit
    var modelId = this.closest('td').querySelector('span[id="QuantityIdSpan"]').textContent;
    

    loadEditForm(modelId);
});

function deleteUser(id) {
    debugger
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then(function (result) {
        debugger
        if (result.isConfirmed) {
            debugger
            $.ajax({

                url: '/QuantityUnits/Delete',
                type: 'POST',
                data: { id: id },
                success: function (result) {
                    debugger
                    Swal.fire({
                        text: "The user has been deleted successfully",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                    // $('#editUserModal').modal('show');
                },
                error: function (error) {
                    debugger
                    Swal.fire({
                        text: "An error occurred deleting the user",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        }
        if (result.dismiss === 'cancel') {
            Swal.fire({
                text: "Your cancelled!.",
                icon: "error",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary",
                }
            });
        }
    });

}

$(document).on('click', '#deleteLink', function (e) {
    e.preventDefault();
    debugger
    // Replace "modelId" with the ID of the model you want to edit
    var modelId = this.closest('td').querySelector('span[id="QuantityIdSpan"]').textContent;


    deleteUser(modelId);
});
$(document).on('submit', '#kt_modal_edit_user_form', function (e) {
    debugger
    e.preventDefault();
    var formEdit = this;
    var formData = $(this).serialize();

    $.ajax({
        url: '/QuantityUnits/Edit',
        type: 'POST',
        data: formData,
        success: function (result) {
            debugger
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
                debugger
                if (result.isConfirmed) {
                    debugger
                    // Enable submit button after loading
                    $('#EditQuantityModal').modal('hide');
                    formEdit.reset(); // Reset form			
                    $('#EditQuantityModal').hide();
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