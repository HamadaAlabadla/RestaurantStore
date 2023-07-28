function loadEditForm(id) {
    
    $.ajax({
        
        url: '/Orders/EditStatus',
        type: 'GET',
        data: { id: id },
        success: function (result) {
            
            $('#EditOrderModal .modal-content').html(result);
           // $('#editUserModal').modal('show');
        },
        error: function (error) {
            console.error('Error while loading the edit form:', error);
        }
    });
}

$(document).on('click', '#editModelLink', function (e) {
    e.preventDefault();
    
    // Replace "modelId" with the ID of the model you want to edit

    var modelId = null;
    if (this.closest('td') !== null) {
        modelId = this.closest('td').querySelector('span[id="OrderIdSpan"]').textContent;
    } else {
        modelId = document.querySelector('span[id="OrderIdSpan"]').textContent;
    }
    

    loadEditForm(modelId);
});


$(document).on('submit', '#kt_modal_edit_status_order_form', function (e) {
    
    e.preventDefault();
    var formEdit = this;
    var formData = $(this).serialize();

    $.ajax({
        url: '/Orders/EditStatus',
        type: 'POST',
        data: formData,
        success: function (result) {
            
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
                    
                    datatable.draw();
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