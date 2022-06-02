$(function () {
    let serviceUrl = "https://localhost:7029/employees";
    $("#dataGridContainer").dxDataGrid({
        // ...
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: serviceUrl + "/GetAction",
            //insertUrl: serviceUrl + "/InsertAction",
            //updateUrl: serviceUrl + "/UpdateAction",
            //deleteUrl: serviceUrl + "/DeleteAction",

            columns: [{
                dataField: "FirstName"
            }, {
                dataField: "LastName"
            }, {
                dataField: "StartDate",
                dataType: "date",
            }, {
                dataField: "UserName"
            },
            ],
            allowColumnReordering: true,
        })
    })
});