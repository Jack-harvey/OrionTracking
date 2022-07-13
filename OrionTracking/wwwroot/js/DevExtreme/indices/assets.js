$(function () {
    let serviceUrl = "https://localhost:7029/assets";
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
            dataField: "companyTrackingId",
            caption: "Asset-Id"
        }, {
            dataField: "name",
            caption: "Name"
        }, {
            dataField: "typeName",
            caption: "Type"
        }, {
            dataField: "userName",
            caption: "Employee"
        },
        {
            dataField: "location",
            caption: "Location"
        },
        {
            dataField: "purchaseDate",
            caption: "Purchase-Date",
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
                    const newLink = (`${window.location.origin}/Assets/details/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                hint: 'Edit',
                icon: 'edit',
                onClick(e) {
                    //const editItemLink = (`edit/${e.id}`);
                    const newLink = (`${window.location.origin}/Assets/edit/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                    hint: 'delete',
                    icon: 'trash',
                    onClick(e) {
                        //const editItemLink = (`edit/${e.id}`);
                        const newLink = (`${window.location.origin}/Assets/delete/${e.row.key}`);
                        window.location = newLink;

                    },
                }],
        },
        ],
        allowColumnReordering: true,
    })
});