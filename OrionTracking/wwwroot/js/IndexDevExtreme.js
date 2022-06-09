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
        columns: [{
            dataField: "firstName",
            caption: "Name"
        }, {
            dataField: "lastName",
            caption: "Surname"
        }, {
            dataField: "userName",
            caption: "Username"
        }, {
            dataField: "name",
            caption: "Job title"
        },
        {
            dataField: "email",
            caption: "E-mail"
        },
        {
            dataField: "city",
            caption: "Office"
        },
        {
            dataField: "startDate",
            caption: "Start-Date",
            dataType: "date",
            format: 'dd-MMM-yyyy'
        },
        ],
        allowColumnReordering: true,
    })
});