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