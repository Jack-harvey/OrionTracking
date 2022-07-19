$(function () {
    let serviceUrl = "https://localhost:7029/assets";
    const columnsIncludingMobile = [{
        dataField: "mobileNumber",
        caption: "Number",
        allowHeaderFiltering: false,
    }, {
        dataField: "typeName",
        caption: "Service Type",
        allowHeaderFiltering: false,
    }, {
        dataField: "userName",
        caption: "Username",
        allowHeaderFiltering: false,
    }, {
        dataField: "ParentAssetId",
        caption: "Asset link",
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
        dataField: "isMobileService",
        dataType: "boolean",
        showEditorAlways: false,
        filterValue: true,
        width: 90,
        visible: false,
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
    ]

    const columnsExcludingMobile = [{
        dataField: "companyTrackingId",
        caption: "Asset-Id",
        allowHeaderFiltering: false,
    }, {
        dataField: "name",
        caption: "Name",
        allowHeaderFiltering: false,
    }, {
        dataField: "typeName",
        caption: "Type",
        allowHeaderFiltering: false,
    }, {
        dataField: "userName",
        caption: "Employee",
        allowHeaderFiltering: false,
    },
    {
        dataField: "location",
        caption: "Location",
        allowHeaderFiltering: false,
    },
    {
        dataField: "purchaseDate",
        caption: "Purchase-Date",
        allowHeaderFiltering: false,
        dataType: "date",
        format: 'dd-MMM-yyyy'
    },
    {
        dataField: "active",
        dataType: "boolean",
        showEditorAlways: false,
        filterValue: true,
        width: 90,
    },
    {
        dataField: "isMobileService",
        dataType: "boolean",
        showEditorAlways: false,
        filterValue: false,
        width: 90,
        visible: false,
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
    ]
    DevExpress.localization.locale("en-AU");
    $("#dataGridContainer").dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: "id",
            loadUrl: serviceUrl + "/GetAction",
        }),
        showColumnLines: true,
        showRowLines: true,
        rowAlternationEnabled: true,
        showBorders: true,
        filterSyncEnabled: true,
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
        columns: columnsExcludingMobile,
        allowColumnReordering: true,
    })
});

$('#dataGridContainer').data('currentColumnSet', 'columnsExcludingMobile');
$('#toggleGridColumnSet').on('click', function (e) {
    const currentColumns = $('#grid').data('currentColumnSet');
    let newColumns = null;

    if (currentColumns ==)
})





