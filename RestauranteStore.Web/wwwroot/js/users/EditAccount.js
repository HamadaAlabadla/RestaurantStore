var userId = document.getElementById('userId').textContent;
$.ajax({
    url: '/Users/EditUser/' + userId,
    type: "GET",
    typedata: "json",
    success: function (data) {
        
        document.getElementById('kt_account_settings_profile_details').innerHTML = data;
        KTUtil.onDOMContentLoaded(function () {
            KTAccountSettingsProfileDetails.init();
        });
    }
});

$.ajax({
    url: '/Users/DeleteUser/' + userId,
    type: "GET",
    typedata: "json",
    success: function (data) {
        
        document.getElementById('kt_account_settings_deactivate').innerHTML = data;
        KTUtil.onDOMContentLoaded(function () {
            KTAccountSettingsDeactivateAccount.init();
        });
    }
});
$(document).on('click', '#kt_signin_email_button', function (e) {
    e.preventDefault();
    
    $.ajax({
        url: '/Users/EditEmail/' + userId,
        type: "GET",
        typedata: "json",
        success: function (data) {

            document.getElementById('kt_signin_email_edit').innerHTML = data;
            KTUtil.onDOMContentLoaded(function () {
                KTAccountSettingsSigninMethods.init();
            });
        }
    });
});



$(document).on('click', '#kt_signin_password_button', function (e) {
    e.preventDefault();
    
    $.ajax({
        url: '/Users/EditPassword/' + userId,
        type: "GET",
        typedata: "json",
        success: function (data) {
            
            document.getElementById('kt_signin_password_edit').innerHTML = data;
            KTUtil.onDOMContentLoaded(function () {
                KTAccountSettingsSigninMethods.init();
            });
        }
    });
});
