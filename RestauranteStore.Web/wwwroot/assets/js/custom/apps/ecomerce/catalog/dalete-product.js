

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

                url: '/Products/Delete',
                type: 'Post',
                data: { id: id },
                success: function (result) {
                    datatable.draw();
                    Swal.fire({
                        text: "The product has been deleted successfully",
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
                        text: "An error occurred deleting the product",
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
    var modelId = this.closest('td').querySelector('span[id="productIdSpan"]').textContent;


    deleteUser(modelId);
});

