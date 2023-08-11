var form;
function loadEditForm(id) {
    
    $.ajax({
        
        url: '/Users/Edit',
        type: 'GET',
        data: { id: id },
        success: function (result) {
            debugger
            $('#editUserModal .modal-body').html(result);
            form = document.getElementById('kt_modal_edit_user_form');
            var fileInput = document.getElementById('Logo');
            fileInput.addEventListener('change', validateImage);
            function validateImage() {
                
                const file = fileInput.files[0];
                if (file) {
                    if (file.type.startsWith('image/')) {
                        // It's an image, proceed with the upload
                        console.log('Valid image selected.');
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Please select a valid image file.',
                        });
                        fileInput.value = ''; // Clear the input
                    }
                }
            }
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
    var modelId = this.closest('td').querySelector('span[id="userIdSpan"]').textContent;
    

    loadEditForm(modelId);
});

function deleteUser(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({

                url: '/Users/Delete',
                type: 'Post',
                data: { id: id },
                success: function (result) {
                    datatable.draw();
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
    var modelId = this.closest('td').querySelector('span[id="userIdSpan"]').textContent;


    deleteUser(modelId);
});
$(document).on('submit', '#kt_modal_edit_user_form', function (e) {
    e.preventDefault();
    e.stopPropagation(); // Prevent event from bubbling up
    debugger
    form.classList.add("was-validated");

    if (form.checkValidity() === false) {
        // Form is invalid, do not proceed with submission
        return;
    }
    const fileInput = document.getElementById('Logo');
    const file = fileInput.files[0];

    if (!file) {
        if (true) {
            var formEdit = this;
            formEdit.submit();
        }
        else {
                // Show a SweetAlert2 popup for non-image files
                e.preventDefault(); // Prevent form submission
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Please select a valid image file.',
                });
            }
        }
    else {
        // Show a SweetAlert2 popup for non-image files
        e.preventDefault(); // Prevent form submission
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please select a valid image file.',
        });
    }
});



function handleImage() {
    debugger
    document.getElementById('Logo').addEventListener('change', validateImage);
    function validateImage() {
        const fileInput = document.getElementById('Logo');
        const file = fileInput.files[0];
        if (file) {
            if (file.type.startsWith('image/')) {
                // It's an image, proceed with the upload
                console.log('Valid image selected.');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Please select a valid image file.',
                });
                fileInput.value = ''; // Clear the input
            }
        }
    }
}
