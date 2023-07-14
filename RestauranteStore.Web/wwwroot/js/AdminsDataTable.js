$(document).ready(function () {
    $('#AdminsTable').DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Admins/GetAllAdmins",
            "type": "POST",
            "datatype": "json",
            'columnDefs': [
                { orderable: false, targets: 0 }, // Disable ordering on column 0 (checkbox)
                { orderable: false, targets: 5 }, // Disable ordering on column 6 (actions)                
            ],

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
											<img src="/images/${data.logo}" alt="${data.name}" class="w-100" />
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
								<a href="#" class="btn btn-light btn-active-light-primary btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
									Actions
									<!--begin::Svg Icon | path: icons/duotune/arrows/arr072.svg-->
									<span class="svg-icon svg-icon-5 m-0">
										<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
											<path d="M11.4343 12.7344L7.25 8.55005C6.83579 8.13583 6.16421 8.13584 5.75 8.55005C5.33579 8.96426 5.33579 9.63583 5.75 10.05L11.2929 15.5929C11.6834 15.9835 12.3166 15.9835 12.7071 15.5929L18.25 10.05C18.6642 9.63584 18.6642 8.96426 18.25 8.55005C17.8358 8.13584 17.1642 8.13584 16.75 8.55005L12.5657 12.7344C12.2533 13.0468 11.7467 13.0468 11.4343 12.7344Z" fill="currentColor" />
										</svg>
									</span>
									<!--end::Svg Icon-->
								</a>
								<!--begin::Menu-->
								<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
									<!--begin::Menu item-->
									<div class="menu-item px-3">
										<a href="/${data.role}s/Edit/${data.id}" class="menu-link px-3">Edit</a>
									</div>
									<!--end::Menu item-->
									<!--begin::Menu item-->
									<div class="menu-item px-3">
										<a href="/${data.role}s/Delete/${data.id}" class="menu-link px-3" data-kt-users-table-filter="delete_row">Delete</a>
									</div>
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

var table = document.getElementById('AdminsTable');
var datatable;
var toolbarBase;
var toolbarSelected;
var selectedCount;

const checkboxes = table.querySelectorAll('[type="checkbox"]');

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


function toggleToolbars () {
    debugger
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

function modalView() {
    
    const admin = document.getElementById('kt_modal_add_admin');
    const radioAdmin = document.querySelector('[name = "user_role"]');
    //document.getElementById('radio_add_admin');
    if (radioAdmin.value == 1) {
        admin.classList.remove('d-none');
    } else {
        admin.classList.add('d-none');
    }
}
