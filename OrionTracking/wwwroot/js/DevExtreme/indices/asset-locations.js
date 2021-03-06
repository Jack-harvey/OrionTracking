$(function () {
    let serviceUrl = "https://localhost:7029/AssetLocations";
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
            caption: "Name"
        }, {
            dataField: "state",
            caption: "State"
        }, {
            dataField: "postCode",
            caption: "Post Code"
        }, {
            dataField: "city",
            caption: "City"
        },
        {
            dataField: "street",
            caption: "Street"
        },
        {
            type: 'buttons',
            width: 90,
            buttons: [{
                hint: 'Details',
                icon: 'card',
                onClick(e) {
                    //const editItemLink = (`edit/${e.id}`);
                    const newLink = (`${window.location.origin}/AssetLocations/details/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                hint: 'Edit',
                icon: 'edit',
                onClick(e) {
                    //const editItemLink = (`edit/${e.id}`);
                    const newLink = (`${window.location.origin}/AssetLocations/edit/${e.row.key}`);
                    window.location = newLink;

                },
            }, {
                    hint: 'delete',
                    icon: 'trash',
                    onClick(e) {
                        //const editItemLink = (`edit/${e.id}`);
                        const newLink = (`${window.location.origin}/AssetLocations/delete/${e.row.key}`);
                        window.location = newLink;

                    },
                }],
        },
        ],
        allowColumnReordering: true,
    })
});