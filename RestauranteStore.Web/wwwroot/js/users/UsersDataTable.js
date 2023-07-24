var datatable;
var searchBox = document.querySelector('[data-kt-user-table-filter="search"]');
$(document).ready(function () {
    datatable =  $('#UsersTable').DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Users/GetAllUsers",
            "type": "POST",
            "datatype": "json",
            'columnDefs': [
                { orderable: false, targets: 0 }, // Disable ordering on column 0 (checkbox)
                { orderable: false, targets: 5 }, // Disable ordering on column 6 (actions)                
            ],
            "data": function (filterString) {
                debugger
                filterString.filter = '';
                var searchContent = searchBox.value;
                
                filterString.search = searchContent;
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
                var tempFilter = document.getElementById('tempFilter');
                var buttonFilter = document.getElementById('buttonFilter');
                if (tempFilter.textContent !== '' && tempFilter.textContent !== null) {
                    filterString.filter = tempFilter.textContent;
                    buttonFilter.classList.add('d-none');
                } else {
                    buttonFilter.classList.remove('d-none');
                }
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
                "data": null, "name": "User", "autowidth": true,
                class: "d-flex align-items-center",
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::User=-->
							<td  class="d-flex align-items-center">
								<!--begin:: Avatar -->
								<div class="symbol symbol-circle symbol-50px overflow-hidden me-3">
									<a href="#">
										<div class="symbol-label">
											<img src="/images/users/${data.logo}" alt="${data.name}" class="w-100" />
										</div>
									</a>
								</div>
								<!--end::Avatar-->
								<!--begin::User details-->
								<div class="d-flex flex-column">
									<a href="#" class="text-gray-800 text-hover-primary mb-1">${data.name}</a>
									<span>${data.email}</span>
								</div>
								<!--begin::User details-->
							</td>
							<!--end::User=-->`;
                }
            },
            {
                "data": null, "name": "Role", "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Role=-->
							<td>${data.role}</td>
							<!--end::Role=-->`;
                }
            },
            {
                "data": null, "name": "Last login", "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Last login=-->
							<td>
								<div class="badge badge-light fw-bold">${data.lastLogin}</div>
							</td>
							<!--end::Last login=-->`;
                }
            },
            {
                "data": null, "name": "Joined Date", "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Joined-->
							<td>${data.dateCreateText}</td>
							<!--begin::Joined-->`;
                }
            },
            {
                "data": null, "name": "Actions", "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
					return `<!--begin::Action=-->
							<td class="text-end">
                                <span id="userIdSpan" class="d-none" >${data.id}</span>
							    <a id="editModelLink" data-bs-toggle="modal" data-bs-target="#editUserModal" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
								    <!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
								    <span class="svg-icon svg-icon-3">
									    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
										    <path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="currentColor" />
										    <path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="currentColor" />
									    </svg>
								    </span>
								    <!--end::Svg Icon-->
							    </a>
							    <a id="deleteLink" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm">
								    <!--begin::Svg Icon | path: icons/duotune/general/gen027.svg-->
								    <span class="svg-icon svg-icon-3">
									    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
										    <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="currentColor" />
										    <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="currentColor" />
										    <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="currentColor" />
									    </svg>
								    </span>
								    <!--end::Svg Icon-->
							    </a>
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
searchBox.addEventListener('change', function () {
    

    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
    datatable.draw();
});

