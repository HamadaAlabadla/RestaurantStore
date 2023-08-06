"use strict";

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
				debugger
				formSubmitButton.classList.add('d-none');
				formSubmitButton.classList.add('d-inline-block');
				formContinueButton.classList.remove('d-none');
			} else if (stepperObj.getCurrentStepIndex() === 3 ) {
				debugger
				formSubmitButton.classList.remove('d-none');
				formContinueButton.classList.add('d-none');
				formSubmitButton.style.display = 'block';
				formSubmitButton.classList.remove('d-none');
			} else if (stepperObj.getCurrentStepIndex() === 2 && roleVal !== 'restaurant') {
				debugger
				formContinueButton.classList.add('d-none');
				formSubmitButton.style.display = 'block';
				formSubmitButton.classList.remove('d-none');
			} else {
				debugger
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
		debugger
		formSubmitButton.addEventListener('click', function (e) {
			debugger
			//var currentStepper = stepper.getCurrentStepIndex();
			// Validate form before change stepper step
			var validator = validations[currentStepper ]; // get validator for currnt step


			validator.validate().then(function (status) {
				console.log('validated!');
				debugger
				if (status == 'Valid') {
					// Prevent default button action
					e.preventDefault();

					// Disable button to avoid multiple click 
					formSubmitButton.disabled = true;

					// Show loading indication
					formSubmitButton.setAttribute('data-kt-indicator', 'on');
					form.submit();


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
		debugger
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