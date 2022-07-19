$(function () {
    let serviceUrl = "https://localhost:7029/employees";
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
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,
        searchPanel: {
            visible: true,
            width: 240,
            placeholder: 'Search...',
        },
        headerFilter: {
            visible: false,
        },
        filterRow: {
            visible: true,
            applyFilter: 'auto',
        },
        columns: [{
            dataField: "firstName",
            caption: "Name",
            allowHeaderFiltering: false,
        }, {
            dataField: "lastName",
            caption: "Surname",
            allowHeaderFiltering: false,
        }, {
            dataField: "userName",
            caption: "Username",
            allowHeaderFiltering: false,
        }, {
            dataField: "name",
            caption: "Job title",
            allowHeaderFiltering: false,
        },
        {
            dataField: "email",
            caption: "E-mail",
            allowHeaderFiltering: false,
        },
        {
            dataField: "city",
            caption: "Office",
            allowHeaderFiltering: false,
        },
        {
            dataField: "startDate",
            caption: "Start-Date",
            dataType: "date",
            format: 'dd-MMM-yyyy',
            allowHeaderFiltering: false,
        },
        {
            dataField: "active",
            dataType: "boolean",
            showEditorAlways: false,
            filterValue: true,
            width: 90,
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