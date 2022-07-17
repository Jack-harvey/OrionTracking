$(function () {
    let serviceUrl = "https://localhost:7029/CompanyDivisions";
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
            dataField: "companyName",
            caption: "Company"
        }, {
            dataField: "name",
            caption: "Division Name"
        }, {
            dataField: "managerId",
            caption: "Division Manager"
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