$(function () {
    let serviceUrl = "https://localhost:7029/EmployeeDocuments";
    DevExpress.localization.locale("en-AU");
    $("#dataGridContainer").dxDataGrid({
        // ...
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: serviceUrl + "/GetAction",
            //insertUrl: serviceUrl + "/InsertAction",
            //updateUrl: serviceUrl + "/UpdateAction",
            //deleteUrl: serviceUrl + "/DeleteAction",
        }),
        showColumnLines: false,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,
        columns: [{
            dataField: "name",
            caption: "Document Name"
        }, {
            dataField: "path",
            caption: "Location"
        },
        {
            dataField: "userName",
            caption: "Employee"
        },
        {
            dataField: "docTypeName",
            caption: "Document Type"
        },
        {
            dataField: "timestamp",
            caption: "Created Date",
            dataType: "date",
            format: 'dd-MMM-yyyy'
        },

        {
            type: 'buttons',
            width: 90,
            buttons: [{
                hint: 'Details',
                icon: 'card',
                onClick(e) {
                    //const editItemLink = (`edit/${e.id}`);
                    const newLink = (`${window.location.origin}/Employees/details/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                hint: 'Edit',
                icon: 'edit',
                onClick(e) {
                    //const editItemLink = (`edit/${e.id}`);
                    const newLink = (`${window.location.origin}/Employees/edit/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                    hint: 'delete',
                    icon: 'trash',
                    onClick(e) {
                        //const editItemLink = (`edit/${e.id}`);
                        const newLink = (`${window.location.origin}/Employees/delete/${e.row.key}`);
                        window.location = newLink;

                    },
                }],
        },
        ],
        allowColumnReordering: true,
    })
});