$(document).ready(function () {
    $('#AdminsTable').DataTable({
        "serverSide": true,
        "filter": true,
        "dataSrc": "",
        "ajax": {
            "url": "/Admins/GetAllAdmins",
            "type": "POST",
            "datatype": "json",
            "columnDefs": [{
                "defaultContent": "-",
                "targets": "_all",
                "targets": [0],
                "visible": false,
                "searchable": false
            },],

        },
        "columns": [
            { "data": "id", "name": "ID", "autowidth": true },
            {
                "data": "adminTypeText", "name": "Admin Type", "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return `<th> ${data} </th >`;
                }
            },
            { "data": "email", "name": "Email", "autowidth": true },
            { "data": "dateCreateText", "name": "Date Create", "autowidth": true },
            { "data": "userName", "name": "UserName", "autowidth": true },
            { "data": "phoneNumber", "name": "Phone Number", "autowidth": true, },
            {
                "data": "logo", "name": "Logo", "autowidth": true,
                "sorting": false,
                "render": function (data, type, row) {
                    return '<th><img style="max-width:150px;max-height:150px" src="images/Admin/' + data + '" /></th>';
                }
            },
            {
                "data": null,
                "name": null,
                "sorting": false,
                "render": function (data, type, row) {
                    debugger
                    return `<div><form action="/Admins/Delete" method="post">
                            <input type="hidden" class="form-control" data-val="true"
                            data-val-required="The Id field is required." id="Id" name="Id" value="${row.id}">

                                <input type="submit" value="Create" class="btn btn-danger" />
                                <a href="/Admins/Edit?id=${row.id}" class = "btn btn-info" > Edit </a >
                    </form> </div>`;
                },
                "orderable": false,
            },
        ],
    },);
},);