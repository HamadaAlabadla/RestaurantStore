var datatable;
var searchBox = document.querySelector('[data-kt-user-table-filter="search"]');
var exportBtn = document.querySelector('[id="#kt_modal_export_users"]');
$(document).ready(function () {
    datatable = $('#ProductExcelTable').DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        //order: [[2, 'asc']],
        "ajax": {
            "url": "/ProductExcels/GetAllProducts",
            "type": "POST",
            "datatype": "json",
            "data": function (filterString) {
                var searchContent = searchBox.value;

                filterString.search = searchContent;
                //d.filter = $('#mySelect').val();
                return filterString;
            }

        },
        "columns": [
            {
                "orderable": false,
                "data": null, "name": null, "autowidth": true,
                "sort": false,
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
                "data": "sku", "name": "SKU", "autowidth": true,
            },
            {
                "data": "band", "name": "Band", "autowidth": true,
            },
            {
                "data": "manufacturer", "name": "Manufacturer", "autowidth": true,
            },
            {
                "data": "categoryExcel", "name": "CategoryExcel", "autowidth": true,
            },
            {
                "data": "description", "name": "Description", "autowidth": true,
            },
            {
                "data": "listPrice", "name": "ListPrice", "autowidth": true,
            },
            {
                "data": "minimumDiscount", "name": "MinimumDiscount", "autowidth": true,
            },
            {
                "data": "discountedPrice", "name": "DiscountedPrice", "autowidth": true,
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


searchBox.addEventListener('change', function () {


    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
    datatable.draw();
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
}

//$(document).ready(function () {
//    exportBtn.addEventListener('click', function () {
//        $.ajax({
//            url: '/ProductExcels/ExportExcel',
//            type: "Post",
//            datatype: "json",
//            data: formData,
//            contentType: false,
//            processData: false,
//            success: function (data) {

//            },
//            error: function (data) {

//            },
//        });
//    });
//});

$(document).ready(function () {
    exportBtn.addEventListener('click', exportExcel);
});
async function exportExcel() {
    try {
        debugger
        var searchText = searchBox.value;
        const response = await fetch(`/ProductExcels/ExportExcel?search=${searchText}`, {
            method: 'POST', // or 'POST' depending on your API
        });

        if (response.ok) {
            debugger
            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = 'exported_data.xlsx';
            a.click();
            window.URL.revokeObjectURL(url);

            Swal.fire({
                icon: 'success',
                title: 'Excel Exported',
                text: 'The Excel file has been successfully exported.',
            });
        } else {
            debugger
            Swal.fire({
                icon: 'error',
                title: 'Export Failed',
                text: 'An error occurred while exporting the Excel file.',
            });
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Export Failed',
            text: 'An error occurred while exporting the Excel file.',
        });
    }
}