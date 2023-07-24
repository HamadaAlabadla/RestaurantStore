var datatable;
$(document).ready(function () {
    debugger
    datatable = $('#ProductsTable').DataTable({
        "serverSide": true,
        "filter": true,
        "pagination": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Products/GetAllProducts",
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
                    return `<td class ="min-w-70px">
							    <div class="d-flex align-items-center">
								    <!--begin::Thumbnail-->
								    <a href="#" class="symbol symbol-50px">
									    <img src="/images/products/${data.image}" />
								    </a>
								    <!--end::Thumbnail-->
								    <div class="ms-5">
									    <!--begin::Title-->
									    <a href="#" class="text-gray-800 text-hover-primary fs-5 fw-bold" data-kt-ecommerce-product-filter="product_name">${data.name}</a>
									    <!--end::Title-->
								    </div>
							    </div>
						    </td>`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<td class="text-end pe-0 min-w-70px">
								<span class="fw-bold">${data.productNumber}</span>
							</td>`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Last login=-->
							<td class ="min-w-70px" >
								<div class="badge badge-light fw-bold">${data.nameCategory}</div>
							</td>
							<!--end::Last login=-->`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<td class="text-end pe-0 min-w-70px" data-order="18">
						        <span class="fw-bold ms-3">${data.qty} ${data.nameShortenQuantityUnit}</span>
					        </td>`;
                }
            },
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<td class="text-end pe-0 min-w-70px">${data.price} ${data.nameShortenUnitPrice}</td>`;
                }
            }, 
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    var rate = data.rating;
                    var stars = 5;
                    var html = "";
                    var starsFull = 0;
                    switch (rate) {
                        case rate <= 20: starsFull = 1; break;
                        case rate <= 40: starsFull = 2; break;
                        case rate <= 60: starsFull = 3; break;
                        case rate <= 80: starsFull = 4; break;
                        case rate <= 100: starsFull = 5; break;
                    }
                    for (var i = 1; i <= starsFull; i++) {
                        html += `<div class="rating-label checked">
									<!--begin::Svg Icon | path: icons/duotune/general/gen029.svg-->
									<span class="svg-icon svg-icon-2">
										<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
											<path d="M11.1359 4.48359C11.5216 3.82132 12.4784 3.82132 12.8641 4.48359L15.011 8.16962C15.1523 8.41222 15.3891 8.58425 15.6635 8.64367L19.8326 9.54646C20.5816 9.70867 20.8773 10.6186 20.3666 11.1901L17.5244 14.371C17.3374 14.5803 17.2469 14.8587 17.2752 15.138L17.7049 19.382C17.7821 20.1445 17.0081 20.7069 16.3067 20.3978L12.4032 18.6777C12.1463 18.5645 11.8537 18.5645 11.5968 18.6777L7.69326 20.3978C6.99192 20.7069 6.21789 20.1445 6.2951 19.382L6.7248 15.138C6.75308 14.8587 6.66264 14.5803 6.47558 14.371L3.63339 11.1901C3.12273 10.6186 3.41838 9.70867 4.16744 9.54646L8.3365 8.64367C8.61089 8.58425 8.84767 8.41222 8.98897 8.16962L11.1359 4.48359Z" fill="currentColor" />
										</svg>
									</span>
									<!--end::Svg Icon-->
								</div>`;
                    }
                    for (var i = 1; i <= stars - starsFull; i++) {
                        html += `<div class="rating-label">
									<!--begin::Svg Icon | path: icons/duotune/general/gen029.svg-->
									<span class="svg-icon svg-icon-2">
										<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
											<path d="M11.1359 4.48359C11.5216 3.82132 12.4784 3.82132 12.8641 4.48359L15.011 8.16962C15.1523 8.41222 15.3891 8.58425 15.6635 8.64367L19.8326 9.54646C20.5816 9.70867 20.8773 10.6186 20.3666 11.1901L17.5244 14.371C17.3374 14.5803 17.2469 14.8587 17.2752 15.138L17.7049 19.382C17.7821 20.1445 17.0081 20.7069 16.3067 20.3978L12.4032 18.6777C12.1463 18.5645 11.8537 18.5645 11.5968 18.6777L7.69326 20.3978C6.99192 20.7069 6.21789 20.1445 6.2951 19.382L6.7248 15.138C6.75308 14.8587 6.66264 14.5803 6.47558 14.371L3.63339 11.1901C3.12273 10.6186 3.41838 9.70867 4.16744 9.54646L8.3365 8.64367C8.61089 8.58425 8.84767 8.41222 8.98897 8.16962L11.1359 4.48359Z" fill="currentColor" />
										</svg>
									</span>
									<!--end::Svg Icon-->
								</div>`;
                    }
                    return `<td class="min-w-70px text-end pe-0" data-order="rating-3">
								<div class="rating justify-content-end">
                                ${html}
                                </div>
                            </td>`;
                }
            },

            
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    var status = data.status;
                    if (status == "Scheduled")
                        return `<td class="text-end pe-0 min-w-70px" data-order="Scheduled">
								    <!--begin::Badges-->
								    <div class="badge badge-light-primary">${data.status}</div>
								    <!--end::Badges-->
							    </td>`;
                    else if (status == "Inactive")
                        return `<td class="text-end pe-0 min-w-70px" data-order="Inactive">
								    <!--begin::Badges-->
								    <div class="badge badge-light-danger">${data.status}</div>
								    <!--end::Badges-->
							    </td>`;
                    else if (status == "Draft")
                        return `<td class="text-end pe-0 min-w-70px" data-order="Draft">
								    <!--begin::Badges-->
								    <div class="badge badge-light-primary">${data.status}</div>
								    <!--end::Badges-->
							    </td>`;
                    else 
                        return `<td class="text-end pe-0 min-w-70px" data-order="Published">
								    <!--begin::Badges-->
								    <div class="badge badge-light-success">${data.status}</div>
								    <!--end::Badges-->
							    </td>`;
                }
            },
            
            {
                "data": null, "name": null, "autowidth": true,
                "sorting": true,
                "render": function (data, type, row) {
                    return `<!--begin::Action=-->
							<td class="text-end">
                                <span id="productIdSpan"  class="d-none" >${data.productNumber}</span>
							    <a id="editModelLink" href="/Products/Edit/${data.productNumber}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
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

//const filterForm = document.querySelector('[data-kt-user-table-filter="form"]');
//const filterButton = filterForm.querySelector('[data-kt-user-table-filter="filter"]');
const selectOptions = document.querySelectorAll('select');

// Filter datatable on submit
//filterButton.addEventListener('click', function () {
    

//    // Filter datatable --- official docs reference: https://datatables.net/reference/api/search()
//    datatable.draw();
//});

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
//$(document).ready(function () {
    
//    //$('#CategoryId').on('click', function () {
//        var catList = $('#CategoryId');
//        catList.empty();
//        catList.append('<option></option>');
//        var QuantityList = $('#QuantityUnitId');
//        QuantityList.empty();
//        QuantityList.append('<option></option>');
//        var PriceList = $('#UnitPriceId');
//        PriceList.empty();
//        PriceList.append('<option></option>');
//        var SupplierList = $('#UserId');
//        SupplierList.empty();
//        $.ajax({
//            url: "/Categories/IndexAjax",
//            success: function (categories) {
//                $.each(categories, function (i, category) {
//                    catList.append($('<option></option>').attr('value', category.id).text(category.name));
//                });
//            },
//            error: function () {
//                alert: "Error in Category List";
//            }
//        });
//         $.ajax({
//            url: "/QuantityUnits/IndexAjax",
//            success: function (Quantities) {
//                $.each(Quantities, function (i, quantity) {
//                    QuantityList.append($('<option></option>').attr('value', quantity.id).text(quantity.name));
//                });
//            },
//            error: function () {
//                alert: "Error in Quantity List";
//            }
//         });
//         $.ajax({
//            url: "/UnitsPrice/IndexAjax",
//            success: function (UnitsPrice) {
//                $.each(UnitsPrice, function (i, unitPrice) {
//                    PriceList.append($('<option></option>').attr('value', unitPrice.id).text(unitPrice.name));
//                });
//            },
//            error: function () {
//                alert: "Error in Category List";
//            }
//         });
//        $.ajax({
//            url: "/Users/GetSupplier",
//            success: function (supplier) {
//                SupplierList.append($('<option selected="selected"></option>').attr('value', supplier.id).text(supplier.name));
//            },
//            error: function () {
//                alert: "Error in Category List";
//            }
//         });
//    //});
//});


