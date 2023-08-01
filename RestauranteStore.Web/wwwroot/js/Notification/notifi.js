var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
var contentNotifi = document.getElementById('notificationList');
var notificationNumber = document.getElementById('notifiNum');
$(document).ready(function () {

    $.ajax({
        url: '/Notifications/GetListNotification' ,
        type: "GET",
        typedata: "json",
        success: function (data) {
            
            contentNotifi.innerHTML = data;
            
            var countUnRead = document.getElementById('NotifiCountUnRead').textContent;
            if (countUnRead === '0')
                notificationNumber.classList.add('d-none');
            else {
                notificationNumber.classList.remove('d-none');
                notificationNumber.textContent = countUnRead;
            }

            handelLinks();
        }
    });
});

function handelLinks() {
    
    var Links = contentNotifi.querySelectorAll('[id="NotifiLink"]');
    Links.forEach(function (link) {
        var notifiId = link.querySelector('#NotifiIdSpan').textContent;
        
        var divClosest = link.parentNode.parentNode.parentNode;
        if (divClosest.classList.contains('bg-light-primary')) {
            
            link.addEventListener('click', function () {
                
                var orderId = link.querySelector('#OrderIdSpan').textContent;
                $.ajax({
                    url: '/Notifications/setRead',
                    type: "POST",
                    typedata: "json",
                    data: { id: notifiId },
                    success: function () {
                        
                        divClosest.classList.remove('bg-light-primary');
                        window.location.href = `/Orders/DetailsForRestaurant/${orderId}`
                    },
                });

            });
        }
    });
}
connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveNotification", function (notifi) {
    
    var data = contentNotifi.innerHTML;
    var html = `<!--begin::Item-->
		        <div href=""  class="d-flex flex-stack py-4 bg-light-primary">
			        
			        <!--begin::Section-->
			        <div class="d-flex align-items-center">
				        <!--begin::Symbol-->
				        <div class="symbol symbol-35px me-4">
					        <span class="symbol-label bg-light-primary">
						        <!--begin:: Avatar -->
						        <div class="symbol symbol-circle symbol-50px overflow-hidden me-3">
							        <a href="#">
								        <div class="symbol-label">
									        <img src="~/images/users/${notifi.fromUserImage}" alt="${notifi.fromUserName}" class="w-100" />
								        </div>
							        </a>
						        </div>
						        <!--end::Avatar-->
					        </span>
				        </div>
				        <!--end::Symbol-->
				        <!--begin::Title-->
				        <div class="mb-0 me-2">
					        <a href="" id="NotifiLink" class="fs-6 text-gray-800 text-hover-primary fw-bold">
                            <span hidden id="NotifiIdSpan">${notifi.id}</span>
			                <span hidden id="OrderIdSpan">${notifi.orderId}</span>
                            ${notifi.fromUserName}
                            </a>
					        <div class="text-gray-400 fs-7">${notifi.header}</div>
				        </div>
				        <!--end::Title-->
			        </div>
			        <!--end::Section-->
			        <!--begin::Label-->
			        <span class="badge badge-light fs-8">${notifi.dateAdded} mins ago</span>
			        <!--end::Label-->
		        </div>
		        <!--end::Item-->`;

    contentNotifi.innerHTML = (html + data);
    handelLinks();
    if (notificationNumber.classList.contains('d-none')) {
        notificationNumber.classList.remove('d-none');
        notificationNumber.textContent = 1;
    } else {
        notificationNumber.textContent = parseInt(notificationNumber.textContent) + 1;
    }

});





//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});