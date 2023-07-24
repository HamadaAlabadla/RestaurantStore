var datatable;
$(document).ready(function () {
    datatable = $('#WarehousesTable').DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Warehouses/GetAllWarehouses",
            "type": "POST",
            "datatype": "json",
            'columnDefs': [
                { orderable: false, targets: 0 }, // Disable ordering on column 0 (checkbox)
                { orderable: false, targets: 5 }, // Disable ordering on column 6 (actions)                
            ],
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
                "sorting": false,
                "render": function (data, type, row) {
                    return `<!--begin::Checkbox-->
							<td>
								<div class="form-check form-check-sm form-check-custom form-check-solid">
									<input class="form-check-input" type="checkbox" onchange="toggleToolbars();" value="1" />
								</div>
							</td>
							<!--end::Checkbox-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Role=-->
							<td style="max-width:50px;">${data.productId}</td>
							<!--end::Role=-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                class: "d-flex align-items-center",
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::User=-->
							<td  class="d-flex align-items-center">
								<!--begin:: Avatar -->
								<div class="symbol symbol-circle symbol-50px overflow-hidden me-3">
									<a href="#">
										<div class="symbol-label">
											<img src="/images/products/${data.image}" alt="${data.name}" class="w-100" />
										</div>
									</a>
								</div>
								<!--end::Avatar-->
								<!--begin::User details-->
								<div class="d-flex flex-column">
									<a href="#" class="text-gray-800 text-hover-primary mb-1">${data.productName}</a>
								</div>
								<!--begin::User details-->
							</td>
							<!--end::User=-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Role=-->
							<td>${data.address}</td>
							<!--end::Role=-->`;
                }
            },

            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Last login=-->
							<td>
								<div class="badge badge-light fw-bold">${data.categoryName}</div>
							</td>
							<!--end::Last login=-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Joined-->
							<td>${data.quantity}  ${data.quantityName}</td>
							<!--begin::Joined-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Joined-->
							<td>${data.price}  ${data.priceName}</td>
							<!--begin::Joined-->`;
                }
            },
            
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
					return `<!--begin::Action=-->
							<td class="text-end">
							
								<div name="Action" class=" menu-row menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
									<!--begin::Menu item-->
										<a href="/Users/Edit/${data.productNumber}" class=" px-3 menu-link px-3">Edit</a>
									<!--end::Menu item-->
									<!--begin::Menu item-->
										<a id="Delete-${data.productNumber}" onclick="Delete(${data.productNumber});"  class=" px-3 menu-link px-3" data-kt-users-table-filter="delete_row">Delete</a>
									<!--end::Menu item-->
								</div>
								<!--end::Menu-->
							</td>
							<!--end::Action=-->`;
                }
            },
        ],
    },);
    
},);

const filterForm = document.querySelector('[data-kt-user-table-filter="form"]');
const filterButton = filterForm.querySelector('[data-kt-user-table-filter="filter"]');
const selectOptions = filterForm.querySelectorAll('select');

// Filter datatable on submit
filterButton.addEventListener('click', function () {
    

    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
    datatable.draw();
});

//var supplierLink = document.getElementById('supplierLink');
//var restoranteLink = document.getElementById('restoranteLink');
//var adminLink = document.getElementById('adminLink');

//supplierLink.addEventListener('click', function () {


//    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
//    datatable.draw();
//});
//restoranteLink.addEventListener('click', function () {


//    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
//    datatable.draw();
//});
//adminLink.addEventListener('click', function () {


//    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
//    datatable.draw();
//});



