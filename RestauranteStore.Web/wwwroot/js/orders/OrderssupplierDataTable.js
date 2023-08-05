var datatable;

var table = $('[id="OrdersTable"]');

$(document).ready(function () {
    
    datatable = table.DataTable(
        {
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Orders/GetAllOrdersForSupplier",
            "type": "POST",
            "datatype": "json",
            "data": function (filterString) {
                
                filterString.filter = '';

                // Get filter values
                selectOptions.forEach((item, index) => {
                    
                    if (item.value && item.value !== '') {
                        
                        if (index !== 0) {
                            filterString.filter += ' ';
                        }

                        // Build filter value options
                        filterString.filter += item.value;
                    }
                });
                //var tempFilter = document.getElementById('tempFilter');
                //var buttonFilter = document.getElementById('buttonFilter');
                //if (tempFilter.textContent !== '' && tempFilter.textContent !== null) {
                //    filterString.filter = tempFilter.textContent;
                //    buttonFilter.classList.add('d-none');
                //} else {
                //    buttonFilter.classList.remove('d-none');
                //}
                //d.filter = $('#mySelect').val();
                return filterString;
            }

        },
        "columns": [
            {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "render": function (data, type, row) {
                    return `<!--begin::Order ID=-->
							<td data-kt-ecommerce-order-filter="order_id">
								<a  id="OrderIdA" href="#" class="text-gray-800 text-hover-primary fw-bold">${data.id}</a>
							</td>
							<!--end::Order ID=-->`;
                }
            },
            {
                "data": null, "name": "Restaurant", "autowidth": true,
              
                "render": function (data, type, row) {
                    var html = '';
                    if (data.restaurantImage === null || data.restaurantImage === "" || data.restaurantImage === " ") {
                        html = `<div class="symbol-label fs-3 bg-light-warning text-warning" > ${data.restaurantName[0]}</div>`;
                    } else {
                        html = `<div class="symbol-label">
									<img src="/images/users/${data.restaurantImage}" alt="Max Smith" class="w-100" />
								</div>`;
                    }
                    return `<!--begin::Customer=-->
									<td>
										<div class="d-flex align-items-center">
											<!--begin:: Avatar -->
											<div class="symbol symbol-circle symbol-50px overflow-hidden me-3">
												<a href="#">
													${html}
												</a>
											</div>
											<!--end::Avatar-->
											<div class="ms-5">
												<!--begin::Title-->
												<a href="#" class="text-gray-800 text-hover-primary fs-5 fw-bold">${data.restaurantName}</a>
												<!--end::Title-->
											</div>
										</div>
									</td>
									<!--end::Customer=-->`;
                }
            },
            {
                "data": null, "name": "StatusOrder", "autowidth": true,
                "render": function (data, type, row) {
                    var color = '';
                    if (data.statusOrder == 'Completed' || data.statusOrder == 'Delivered') {
                        color = 'success';
                    } else if (data.statusOrder == 'Denied' || data.statusOrder == 'Expired' || data.statusOrder == 'Failed' || data.statusOrder == 'Cancelled') {
                        color = 'danger';
                    } else if (data.statusOrder == 'Pending') {
                        color = 'warning';
                    } else if (data.statusOrder == 'Processing' || data.statusOrder == 'Delivering') {
                        color = 'primary';
                    } else if (data.statusOrder == 'Refunded') {
                        color = 'info';
                    }

                    return `<!--begin::Status=-->
							<td class="text-end pe-0" data-order="${data.statusOrder}">
								<!--begin::Badges-->
								<div class="badge badge-light-${color}">${data.statusOrder}</div>
								<!--end::Badges-->
							</td>
							<!--end::Status=-->`;
                }
            },
            {
                "data": null, "name": "TotalPrice", "autowidth": true,
                "render": function (data, type, row) {
                    return `<!--begin::Total=-->
							<td class="text-end pe-0">
								<span class="fw-bold">${data.totalPrice}</span>
							</td>
							<!--end::Total=-->`;
                }
            },
            {
                "data": null, "name": "DateCreate", "autowidth": true,
                "render": function (data, type, row) {
                    return `<!--begin::Date Added=-->
							<td class="text-end" data-order="2023-01-29">
								<span class="fw-bold">${data.dateCreate}</span>
							</td>
							<!--end::Date Added=-->`;
                }
            },
            {
                "data": null, "name": "DateModified", "autowidth": true,
                "render": function (data, type, row) {
                    return `<!--begin::Date Modified=-->
							<td class="text-end" data-order="2023-02-03">
								<span class="fw-bold">${data.dateModified}</span>
							</td>
							<!--end::Date Modified=-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "orderable": false,
                "render": function (data, type, row) {
                    var deleteHtml = ``;
                    var completeHtml = ``;
                    var deleveredHtml = ``;
                    if (data.statusOrder == 'Pending' || data.statusOrder == 'Processing' ) {
                        deleteHtml = `<a id="deleteLink" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm">
								        <!--begin::Svg Icon | path: icons/duotune/general/gen027.svg-->
								        <span class="svg-icon svg-icon-3">
									        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
										        <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="currentColor" />
										        <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="currentColor" />
										        <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="currentColor" />
									        </svg>
								        </span>
								        <!--end::Svg Icon-->
							        </a>`;
                        deleveredHtml = `<a id="confirmOrderLink" class="btn btn-icon btn-bg-light  btn-active-color-light btn-active-success btn-sm me-1" >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-truck" viewBox="0 0 16 16">
                                              <path d="M0 3.5A1.5 1.5 0 0 1 1.5 2h9A1.5 1.5 0 0 1 12 3.5V5h1.02a1.5 1.5 0 0 1 1.17.563l1.481 1.85a1.5 1.5 0 0 1 .329.938V10.5a1.5 1.5 0 0 1-1.5 1.5H14a2 2 0 1 1-4 0H5a2 2 0 1 1-3.998-.085A1.5 1.5 0 0 1 0 10.5v-7zm1.294 7.456A1.999 1.999 0 0 1 4.732 11h5.536a2.01 2.01 0 0 1 .732-.732V3.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .294.456zM12 10a2 2 0 0 1 1.732 1h.768a.5.5 0 0 0 .5-.5V8.35a.5.5 0 0 0-.11-.312l-1.48-1.85A.5.5 0 0 0 13.02 6H12v4zm-9 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm9 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2z"/>
                                            </svg>
                                        </a>`
                    }
                    if (data.statusOrder == 'Delivered') {
                        completeHtml = `<a id="confirmMoneyLink" class="btn btn-icon btn-bg-light  btn-active-color-light btn-active-success btn-sm me-1" >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cash-coin" viewBox="0 0 16 16">
                                              <path fill-rule="evenodd" d="M11 15a4 4 0 1 0 0-8 4 4 0 0 0 0 8zm5-4a5 5 0 1 1-10 0 5 5 0 0 1 10 0z"/>
                                              <path d="M9.438 11.944c.047.596.518 1.06 1.363 1.116v.44h.375v-.443c.875-.061 1.386-.529 1.386-1.207 0-.618-.39-.936-1.09-1.1l-.296-.07v-1.2c.376.043.614.248.671.532h.658c-.047-.575-.54-1.024-1.329-1.073V8.5h-.375v.45c-.747.073-1.255.522-1.255 1.158 0 .562.378.92 1.007 1.066l.248.061v1.272c-.384-.058-.639-.27-.696-.563h-.668zm1.36-1.354c-.369-.085-.569-.26-.569-.522 0-.294.216-.514.572-.578v1.1h-.003zm.432.746c.449.104.655.272.655.569 0 .339-.257.571-.709.614v-1.195l.054.012z"/>
                                              <path d="M1 0a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h4.083c.058-.344.145-.678.258-1H3a2 2 0 0 0-2-2V3a2 2 0 0 0 2-2h10a2 2 0 0 0 2 2v3.528c.38.34.717.728 1 1.154V1a1 1 0 0 0-1-1H1z"/>
                                              <path d="M9.998 5.083 10 5a2 2 0 1 0-3.132 1.65 5.982 5.982 0 0 1 3.13-1.567z"/>
                                            </svg>
                                        </a>`;
                    }
                    return `<!--begin::Action=-->
							<td class="text-end">
                                <span id="OrderIdSpan"  class="d-none" >${data.id}</span>
                                ${deleveredHtml}
                                <a href="/Orders/Details/${data.id}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
								    <!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
								    <span class="svg-icon svg-icon-3">
									    <i class="fas fa-eye"></i>
								    </span>
								    <!--end::Svg Icon-->
							    </a>
                                ${deleteHtml}
                                ${completeHtml}
							</td>
							<!--end::Action=-->`;
                }
            }, 
            
            ],
            "drawCallback": function () {
                handleDeleteLink();
                handleConfirmMoney();
                handleConfirmOrderLink();
             }
        },
    );

    
},);


function handleDeleteLink() {
    var deleteLinks = document.querySelectorAll('[id="deleteLink"');
    deleteLinks.forEach(function (deleteLink) {
        debugger

        deleteLink.addEventListener('click', function () {
            var tr = this.closest('td').closest('tr');
            var orderId = tr.querySelector('[id="OrderIdA"]').textContent;
            debugger

            Swal.fire({
                title: "Confirm Delete",
                text: "Are you sure you want to delete this order?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "Delete",
                cancelButtonText: "Cancel",
            }).then((result) => {

                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Orders/DeniedOrder',
                        type: 'POST',
                        data: { id: orderId },
                        success: function (data) {

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
                            debugger
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
                }
            });
        })

    });

}

function handleConfirmMoney() {
    var confirmMoneyLinks = document.querySelectorAll('[id="confirmMoneyLink"');
    confirmMoneyLinks.forEach(function (confirmMoneyLink) {
        debugger

        confirmMoneyLink.addEventListener('click', function () {
            var tr = this.closest('td').closest('tr');
            var orderId = tr.querySelector('[id="OrderIdA"]').textContent;
            debugger

            Swal.fire({
                title: "Confirm Delivering Money",
                text: "Are you sure you want to confirm delivering money from the customer?",
                icon: "question",
                showCancelButton: true,
                confirmButtonColor: "#4CAF50",
                cancelButtonColor: "#d33",
                confirmButtonText: "Confirm",
                cancelButtonText: "Cancel",
            }).then((result) => {

                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Orders/DeliverMoney',
                        type: 'POST',
                        data: { id: orderId },
                        success: function (data) {

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
                            debugger
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
                }
            });
        })

    });
}


function handleConfirmOrderLink() {
    var confirmOrderLinks = document.querySelectorAll('[id="confirmOrderLink"');
    if (confirmOrderLinks !== null) {
        confirmOrderLinks.forEach(function (confirmOrderLink) {
            confirmOrderLink.addEventListener('click', function () {
                var tr = this.closest('td');
                var orderId = tr.querySelector('[id="OrderIdSpan"]').textContent;

                Swal.fire({
                    title: "Confirm Delivering",
                    text: "Are you sure you want to deliver this order?",
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonColor: "#4CAF50",
                    cancelButtonColor: "#d33",
                    confirmButtonText: "Confirm",
                    cancelButtonText: "Cancel",
                }).then((result) => {

                    if (result.isConfirmed) {
                        $.ajax({
                            url: '/Orders/Delivering',
                            type: 'POST',
                            data: { id: orderId },
                            success: function (data) {
                                Swal.fire({
                                    title: "Order Delivereing!",
                                    text: "The order has been delivereing successfully.",
                                    icon: "success",
                                    confirmButtonColor: "#4CAF50",
                                }).then(function (result) {
                                    if (result.isConfirmed) {
                                        datatable.draw();
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
                    } else {
                        Swal.fire({
                            title: "Order Delivery Canceled",
                            text: "You canceled the order delivery.",
                            icon: "info",
                            confirmButtonColor: "#4CAF50",
                        });
                    }
                });
            });
        });
    }

}


$(document).ready(() => {
    
    const element = document.querySelector('#kt_ecommerce_sales_flatpickr');
    flatpickr = $(element).flatpickr({
        altInput: true,
        altFormat: "d/m/Y",
        dateFormat: "Y-m-d",
        mode: "range",
        onChange: function (selectedDates, dateStr, instance) {
            
            //handleFlatpickr(selectedDates, dateStr, instance);
            datatable.draw();
        },
    });

    // Handle clear flatpickr
    const clearButton = document.querySelector('#kt_ecommerce_sales_flatpickr_clear');
    clearButton.addEventListener('click', e => {
        flatpickr.clear();
    });
});





const filterForm = document.querySelector('[data-kt-user-table-filter="form"]');
const filterButton = filterForm.querySelector('[data-kt-user-table-filter="filter"]');
const selectOptions = filterForm.querySelectorAll('select');
filterButton.addEventListener('click', function () {


    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
    datatable.draw();
});
