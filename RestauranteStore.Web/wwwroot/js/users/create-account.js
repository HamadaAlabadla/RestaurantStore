"use strict";

var table = document.getElementById('UsersTable');
var toolbarBase;
var toolbarSelected;
var selectedCount;
var checkboxes; 
if (table != null) {
	checkboxes = table.querySelectorAll('[type="checkbox"]');

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


}









// Class definition
var KTCreateAccount = function () {
	// Elements
	var modal;	
	var modalEl;

	let currentStepper;
	var stepper;
	var form;
	var formSubmitButton;
	var formContinueButton;

	// Variables
	var stepperObj;
	var validations = [];

	// Private Functions
	var initStepper = function () {
		// Initialize Stepper
		stepperObj = new KTStepper(stepper);
		var userType;
		let roleVal = null;
		// Stepper change event
		stepperObj.on('kt.stepper.changed', function (stepper) {
			if (stepperObj.getCurrentStepIndex() === 1) {
				
				formSubmitButton.classList.add('d-none');
				formSubmitButton.classList.add('d-inline-block');
				formContinueButton.classList.remove('d-none');
			} else if (stepperObj.getCurrentStepIndex() === 3 ) {
				
				formSubmitButton.classList.remove('d-none');
				formContinueButton.classList.add('d-none');
				formSubmitButton.style.display = 'block';
				formSubmitButton.classList.remove('d-none');
			} else if (stepperObj.getCurrentStepIndex() === 2 && roleVal !== 'restaurant') {
				
				formContinueButton.classList.add('d-none');
				formSubmitButton.style.display = 'block';
				formSubmitButton.classList.remove('d-none');
			} else {
				
				formSubmitButton.classList.add('d-inline-block');
				formSubmitButton.classList.add('d-none');
				formContinueButton.classList.remove('d-none');
			}
		});

		// Validation before going to next page
		stepperObj.on('kt.stepper.next', function (stepper) {
			console.log('stepper.next');
		    currentStepper = stepper.getCurrentStepIndex();
			// Validate form before change stepper step
			var validator = validations[currentStepper - 1]; // get validator for currnt step
			
			if (validator) {
				validator.validate().then(function (status) {
					console.log('validated!');
					if (status == 'Valid') {
						const radioButtons = document.getElementsByName('account_type');
						
					    userType = document.getElementById('UserType');
						// Loop through the radio buttons to find the checked one
						radioButtons.forEach(radioButton => {
							if (radioButton.checked) {
								roleVal = radioButton.value;
							}
						});
						if (roleVal == 'admin' || roleVal == 'supplier') {
							if (roleVal == 'admin')
								userType.value = 2;
							else
								userType.value = 0;
							stepper.goNext();
						}
						else {
							userType.value = 1;
							stepper.goNext();
						}


						KTUtil.scrollTop();
					} else {
						Swal.fire({
							text: "Sorry, looks like there are some errors detected, please try again.",
							icon: "error",
							buttonsStyling: false,
							confirmButtonText: "Ok, got it!",
							customClass: {
								confirmButton: "btn btn-light"
							}
						}).then(function () {
							KTUtil.scrollTop();
						});
					}
				});
			} else {
				stepper.goNext();

				KTUtil.scrollTop();
			}
		});

		// Prev event
		stepperObj.on('kt.stepper.previous', function (stepper) {
			console.log('stepper.previous');

			stepper.goPrevious();
			KTUtil.scrollTop();
		});
	}


	var handleForm = function () {
		
		formSubmitButton.addEventListener('click', function (e) {
			
			//var currentStepper = stepper.getCurrentStepIndex();
			// Validate form before change stepper step
			var validator = validations[currentStepper ]; // get validator for currnt step


			validator.validate().then(function (status) {
				console.log('validated!');
				
				if (status == 'Valid') {
					const fileInput = document.getElementById('Logo');
					const file = fileInput.files[0];
					debugger
					if (file) {
						if (file.type.startsWith('image/')) {
							// It's an image, allow form submission
							e.preventDefault();

							// Disable button to avoid multiple click 
							formSubmitButton.disabled = true;

							// Show loading indication
							formSubmitButton.setAttribute('data-kt-indicator', 'on');
							form.submit();
						} else {
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


				}
				else {
					Swal.fire({
						text: "Sorry, looks like there are some errors detected, please try again.",
						icon: "error",
						buttonsStyling: false,
						confirmButtonText: "Ok, got it!",
						customClass: {
							confirmButton: "btn btn-light"
						}
					});
				}
			});
		});
	}
	var initValidation = function () {
		// Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
		// Step 1
		
		validations.push(FormValidation.formValidation(
			form,
			{
				fields: {
					account_type: {
						validators: {
							notEmpty: {
								message: 'Account type is required'
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
		));
		
		// Step 2
		validations.push(FormValidation.formValidation(
			form,
			{
				fields: {
					UserType: {
						validators: {
							notEmpty: {
								message: 'user type is required'
							}
						}
					},
					Logo: {
						validators: {
							notEmpty: {
								message: 'Logo is required'
							}
						}
					},
					Name: {
						validators: {
							notEmpty: {
								message: 'Name is required'
							}
						}
					},
					UserName: {
						validators: {
							notEmpty: {
								message: 'UserName is required'
							}
						}
					},
					Email: {
						validators: {
							notEmpty: {
								message: 'Email is required'
							}
						}
					},
					PhoneNumber: {
						validators: {
							notEmpty: {
								message: 'PhoneNumber is required'
							}
						}
					},
				},
				//plugins: {
				//	trigger: new FormValidation.plugins.Trigger(),
				//	// Bootstrap Framework Integration
				//	bootstrap: new FormValidation.plugins.Bootstrap5({
				//		rowSelector: '.fv-row',
    //                    eleInvalidClass: '',
    //                    eleValidClass: ''
				//	})
				//}
			}
		));


		// Step 3
		validations.push(FormValidation.formValidation(
			form,
			{
				fields: {
					MainBranchName: {
						validators: {
							notEmpty: {
								message: 'MainBranchName is required'
							}
						}
					},
					MainBranchAddress: {
						validators: {
							notEmpty: {
								message: 'MainBranchAddress is required'
							}
						}
					}
				},
				//plugins: {
				//	trigger: new FormValidation.plugins.Trigger(),
				//	// Bootstrap Framework Integration
				//	bootstrap: new FormValidation.plugins.Bootstrap5({
				//		rowSelector: '.fv-row',
    //                    eleInvalidClass: '',
    //                    eleValidClass: ''
				//	})
				//}
			}
		));
}

	return {
		// Public Functions
		init: function () {
			// Elements
			modalEl = document.querySelector('#kt_modal_create_account');

			if ( modalEl ) {
				modal = new bootstrap.Modal(modalEl);	
			}					

			stepper = document.querySelector('#kt_create_account_stepper');

			if ( !stepper ) {
				return;
			}

			form = stepper.querySelector('#kt_create_account_form');
			formSubmitButton = stepper.querySelector('[data-kt-stepper-action="submit"]');
			formContinueButton = stepper.querySelector('[data-kt-stepper-action="next"]');

			initStepper();
			initValidation();
			handleForm();
		}
	};
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
	KTCreateAccount.init();
});


function handleImage() {
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


$(document).ready(function () {
	handleImage();
});