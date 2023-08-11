"use strict";

// Class definition
var KTSignupGeneral = function() {
    // Elements
    var form;

    var validator;
    var passwordMeter;

    // Handle form
    var handleForm  = function(e) {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
			form,
			{
				fields: {
                    'Password': {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            },
                            callback: {
                                message: 'Please enter valid password',
                                callback: function (input) {

                                    if (input.value.length > 0) {
                                        return validatePassword();
                                    }
                                }
                            }
                        }
                    },
                    'confirm-password': {
                        validators: {
                            notEmpty: {
                                message: 'The password confirmation is required'
                            },
                            callback: {
                                //compare: function () {
                                //    debugger
                                //    var val = form.querySelector('[name="Password"]').value;
                                //    return val;
                                //},
                                message: 'The password and its confirm are not the same',
                                callback: function (input) {
                                    debugger
                                    if (input.value.length > 0) {
                                        var pass = form.querySelector('[name="Password"]').value;
                                        debugger
                                        return confirmPassword(pass, input);
                                       
                                    }
                                }
                            }
                        }
                    },
				},
				plugins: {
					trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: false
                        }  
                    }),
					bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',  // comment to enable invalid state icons
                        eleValidClass: '' // comment to enable valid state icons
                    })
				}
			}
		);

        // Handle password input
        form.querySelector('input[name="password"]').addEventListener('input', function() {
            if (this.value.length > 0) {
                validator.updateFieldStatus('password', 'NotValidated');
            }
        });
    }

    // Password input validation
    var validatePassword = function() {
        return (passwordMeter.getScore() === 100);
    }

    var confirmPassword = function (pass , input) {
        return (pass === input.value);
    }

    // Public functions
    return {
        // Initialization
        init: function() {
            // Elements
            debugger
            form = document.querySelector('#kt_create_account_form');
            passwordMeter = KTPasswordMeter.getInstance(form.querySelector('[data-kt-password-meter="true"]'));

            handleForm ();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function() {
    KTSignupGeneral.init();
});
