﻿$(function () {
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
        showColumnLines: false,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,
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

        {
            type: 'buttons',
            width: 75,
            buttons: [{
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