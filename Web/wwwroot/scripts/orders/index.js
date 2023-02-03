$(() => {
    const url = '/Orders';
    $('#gridContainer').dxDataGrid({
        dataSource: DevExpress.data.AspNet.createStore({
            key: 'Id',
            loadUrl: `${url}/GetAll`,
            insertUrl: `${url}/Post`,
            updateUrl: `${url}/Put`,
            deleteUrl: `${url}/Delete`,
            onBeforeSend(method, ajaxOptions) {
                ajaxOptions.xhrFields = { withCredentials: true };
            },
        }),
        remoteOperations: true,
        paging: {
            pageSize: 5,
        },
        pager: {
            showNavigationButtons: true,
            showPageSizeSelector: true,
            allowedPageSizes: [10, 15, 25, 50, 100],
        },
        columns: [{
            dataField: 'CustomerId',
            caption: 'Cliente',
            lookup: {
                dataSource: DevExpress.data.AspNet.createStore({
                    key: 'Id',
                    loadUrl: `/Customer/GetAll`,
                    onBeforeSend(method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    },
                }),
                valueExpr: 'Id',
                displayExpr: 'customer',
            },
        }, {
            dataField: 'ShipAddress',
            caption: 'Dirección',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Dirección debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'ShipAddress2',
            caption: 'Dirección 2',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Dirección 2 debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'City',
            caption: 'Ciudad',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Ciudad debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'State',
            caption: 'Estado',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Estado debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'Zip',
            caption: 'Zip Code',
            validationRules: [{
                type: 'stringLength',
                message: 'La Zip Zip debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'Country',
            caption: 'País',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna País debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'Phone',
            caption: 'Telefono',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Telefono debe contener entre 1 y 20 caracteres alfanuméricos.',
                max: 20,
                min: 1,
            }],
        }, {
            dataField: 'Shipped',
            caption: 'Enviado',
            dataType: 'boolean',
        },
        {
            type: "buttons",
            buttons: [
                {
                    text: "Editar",
                    icon: "edit",
                    onClick: function (e) {
                        onEditarRow(e);
                    },
                },
                {
                    text: "Eliminar",
                    icon: "trash",
                    onClick: function (e) {
                        onEliminarRow(e);
                    },
                }
            ]
        }
        ],
        filterRow: {
            visible: false,
        },
        headerFilter: {
            visible: false,
        },
        groupPanel: {
            visible: false,
        },
        scrolling: {
            mode: 'virtual',
        },
        height: 600,
        showBorders: true,
        hoverStateEnabled: true,
        focusedRowEnabled: true,
        focusedRowIndex: 0,
        searchPanel: {
            visible: true,
            highlightCaseSensitive: false,
            placeholder: "Buscar",
            width: 400,
        },
        editing: {
            mode: 'popup',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            useIcons: true,
            popup: {
                title: 'Clientes',
                showTitle: true,
                width: 1024,
                height: 850,
                toolbarItems: [
                    {
                        toolbar: 'bottom',
                        location: 'after',
                        widget: 'dxButton',
                        options: {
                            onClick: function (e) {
                                $("#gridContainer").dxDataGrid('instance').saveEditData();
                            },
                            text: 'Aceptar',
                            icon: 'save',
                            type: "default",
                        }
                    },
                    {
                        toolbar: 'bottom',
                        location: 'after',
                        widget: 'dxButton',
                        options: {
                            onClick: function (e) {
                                $("#gridContainer").dxDataGrid('instance').cancelEditData();
                            },
                            text: 'Cancelar',
                            icon: 'remove',
                            type: "danger",
                        }
                    }
                ],
            },
            texts: {
                confirmDeleteMessage: ''
            },
            form: {
                colCount: 2,
                name: 'editForm',
                items: [
                    {
                        dataField: 'CustomerId',
                        colSpan: 2,
                    },
                    {
                        dataField: 'ShipAddress',
                        editorType: 'dxTextArea',
                        colSpan: 2,
                        editorOptions: {
                            height: 60,
                        },
                    },
                    {
                        dataField: 'ShipAddress2',
                        editorType: 'dxTextArea',
                        colSpan: 2,
                        editorOptions: {
                            height: 60,
                        },
                    },
                    'City', 'State', 'Zip', 'Country', 'Phone', 'Shipped'
                ]
            },
        },
        grouping: {
            autoExpandAll: false,
        },
        masterDetail: {
            enabled: true,
            template(container, options) {
                const details = options.data.OrderDetails;

                for (var i = 0; i < details.length; i++) {
                    details[i].Total = details[i].Price * details[i].Quantity
                }

                $('<div>')
                    .addClass('master-detail-caption')
                    .text(`Detalle:`)
                    .appendTo(container);

                $('<div id="gridDetailContainer">')
                    .dxDataGrid({
                        columnAutoWidth: true,
                        showBorders: true,
                        expandAll: -1,
                        columns: [
                            {
                                dataField: 'ProductId',
                                caption: 'Producto',
                                lookup: {
                                    dataSource: DevExpress.data.AspNet.createStore({
                                        key: 'Id',
                                        loadUrl: `/Products/GetAll`,
                                        onBeforeSend(method, ajaxOptions) {
                                            ajaxOptions.xhrFields = { withCredentials: true };
                                        },
                                    }),
                                    valueExpr: 'Id',
                                    displayExpr: 'Name',
                                },
                            },
                            {
                                dataField: 'Price',
                                caption: 'Precio',
                                format: 'currency',
                            },
                            {
                                dataField: 'Quantity',
                                caption: 'Cantidad',
                            },
                            {
                                caption: 'Total',
                                dataField: 'Total',
                                format: 'currency',
                                alignment: 'right',
                            },
                        ],
                        summary: {
                            totalItems: [{
                                column: 'Price',
                                summaryType: 'sum',
                                valueFormat: 'currency',
                                customizeText(data) {
                                    return '$' + data.value;
                                },
                            }, {
                                column: 'Quantity',
                                summaryType: 'sum',
                                valueFormat: 'currency',
                                customizeText(data) {
                                    return data.value;
                                },
                            },
                            {
                                column: 'Total',
                                summaryType: 'sum',
                                customizeText(data) {
                                    return '$' + data.value;
                                },
                            },],
                        },
                        dataSource: new DevExpress.data.DataSource({
                            store: new DevExpress.data.ArrayStore({
                                key: 'Id',
                                data: details,
                            }),
                        }),
                        paging: {
                            pageSize: 5,
                        },
                        pager: {
                            showNavigationButtons: true,
                            showPageSizeSelector: true,
                            allowedPageSizes: [10, 15, 25, 50, 100],
                        },
                    }).appendTo(container);
            },
        },
    });
    $('#gridContainer').dxDataGrid('instance').expandAll(-1);
});