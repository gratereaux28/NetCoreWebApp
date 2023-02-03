let id = '';
let rowIndex = null;
let isNewRow = false;

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
                    loadMode: 'raw',
                }),
                valueExpr: 'Id',
                displayExpr: 'customer',
            },
            validationRules: [{
                type: 'required',
                message: 'El campo Cliente es requerido.',
            }],
        }, {
            dataField: 'ShipAddress',
            caption: 'Dirección',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Dirección debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Dirección es requerido.',
            }
            ],
        }, {
            dataField: 'ShipAddress2',
            caption: 'Dirección 2',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Dirección 2 debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Dirección 2 es requerido.',
            }],
        }, {
            dataField: 'City',
            caption: 'Ciudad',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Ciudad debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Ciudad es requerido.',
            }],
        }, {
            dataField: 'State',
            caption: 'Estado',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Estado debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Estado es requerido.',
            }],
        }, {
            dataField: 'Zip',
            caption: 'Zip Code',
            validationRules: [{
                type: 'stringLength',
                message: 'La Zip Zip debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Zip Code es requerido.',
            }],
        }, {
            dataField: 'Country',
            caption: 'País',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna País debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo País es requerido.',
            }],
        }, {
            dataField: 'Phone',
            caption: 'Telefono',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Telefono debe contener entre 1 y 20 caracteres alfanuméricos.',
                max: 20,
                min: 1,
            }, {
                type: 'required',
                message: 'El campo Telefono es requerido.',
            }],
        }, {
            dataField: 'stringDetalle',
            visible: false,
        }, {
            dataField: 'Shipped',
            caption: 'Enviado',
            dataType: 'boolean',
        }, ,
        {
            type: "buttons",
            buttons: [
                {
                    text: "Editar",
                    icon: "edit",
                    onClick: async function (e) {
                        rowIndex = e.row.rowIndex;
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
        height: 720,
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
                title: 'Ordenes',
                showTitle: true,
                width: 1024,
                height: 850,
                toolbarItems: [
                    {
                        toolbar: 'bottom',
                        location: 'after',
                        widget: 'dxButton',
                        options: {
                            onClick: async function (e) {
                                const gridc = $("#gridContainer").dxDataGrid('instance');
                                const gridcDetalle = $("#gridEditDetailContainer").dxDataGrid('instance');
                                const detalles = gridcDetalle.getDataSource().items();
                                await gridc.cellValue(rowIndex, "stringDetalle", JSON.stringify(detalles));
                                await gridcDetalle.option("dataSource", detalles);
                                await gridc.saveEditData();
                                isNewRow = false;
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
                                isNewRow = false;
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
                    'City', 'State', 'Zip', 'Country', 'Phone', 'Shipped',
                    {
                        colSpan: 2,
                        template: async function (editor, container) {

                            const response = await fetch(`/OrderDetails/GetAll?orderID=${id}`);
                            const details = await response.json();

                            $('<div>')
                                .addClass('master-detail-caption')
                                .text(`Detalle:`)
                                .appendTo(container);

                            $('<div id="gridEditDetailContainer">')
                                .dxDataGrid({
                                    dataSource: details,
                                    //dataSource: DevExpress.data.AspNet.createStore({
                                    //    key: 'Id',
                                    //    loadUrl: `OrderDetails/GetAll`,
                                    //    onBeforeSend(method, ajaxOptions) {
                                    //        ajaxOptions.data.orderID = id;
                                    //        ajaxOptions.xhrFields = { withCredentials: true };
                                    //    },
                                    //}),
                                    remoteOperations: true,
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
                                                    loadMode: 'raw',
                                                }),
                                                valueExpr: 'Id',
                                                displayExpr: 'Name',
                                            },
                                            validationRules: [{
                                                type: 'required',
                                                message: 'El campo Producto es requerido.',
                                            }],
                                        },
                                        {
                                            dataField: 'Price',
                                            caption: 'Precio',
                                            format: 'currency',
                                        },
                                        {
                                            dataField: 'Quantity',
                                            caption: 'Cantidad',
                                            validationRules: [{
                                                type: 'required',
                                                message: 'El campo Cantidad es requerido.',
                                            }],
                                        },
                                        {
                                            caption: 'Total',
                                            dataField: 'Total',
                                            format: 'currency',
                                            alignment: 'right',
                                            cellTemplate(container, options) {
                                                console.log(options);
                                                console.log(container);
                                                container.html('$' + (options.data.Price * options.data.Quantity));
                                            },
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
                                        },
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
                                    height: 300,
                                    showBorders: true,
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
                                            title: 'Orden Detalle',
                                            showTitle: true,
                                            width: 500,
                                            height: 300,
                                            toolbarItems: [
                                                {
                                                    toolbar: 'bottom',
                                                    location: 'after',
                                                    widget: 'dxButton',
                                                    options: {
                                                        onClick: async function (e) {
                                                            $("#gridEditDetailContainer").dxDataGrid('instance').saveEditData();
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
                                                            $("#gridEditDetailContainer").dxDataGrid('instance').cancelEditData();
                                                        },
                                                        text: 'Cancelar',
                                                        icon: 'remove',
                                                        type: "danger",
                                                    }
                                                }
                                            ],
                                        },

                                        form: {
                                            colCount: 1,
                                            name: 'editForm',
                                            items: [
                                                {
                                                    dataField: 'ProductId',
                                                },
                                                {
                                                    dataField: 'Price',
                                                    editorOptions: {
                                                        readOnly: true,
                                                    },
                                                },
                                                {
                                                    dataField: 'Quantity',
                                                },
                                            ]
                                        },
                                        texts: {
                                            confirmDeleteMessage: ''
                                        },
                                    },
                                    grouping: {
                                        autoExpandAll: false,
                                    },
                                    onEditorPreparing: function (row) {
                                        let component = row.component;

                                        if (row.dataField === "ProductId") {
                                            row.editorOptions.onValueChanged = async function (e) {
                                                const response = await fetch(`/Products/Get?id=${e.value}`)
                                                const product = await response.json();
                                                if (isNewRow)
                                                    await component.getCellElement(row.row.rowIndex, 'Price').find(".dx-texteditor").dxTextBox("instance").option("value", product.Price);
                                                else
                                                    await component.getCellElement(row.row.rowIndex, 'Price').find(".dx-numberbox").dxNumberBox("instance").option("value", product.Price);
                                                row.setValue(e.value);
                                            }
                                        }

                                    },
                                })
                                .appendTo(container);

                            //$("#gridEditDetailContainer").dxDataGrid('instance').option("dataSource", details);
                        }
                    },
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
                                        loadMode: 'raw',
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
        onInitNewRow: function (e) {
            rowIndex = 0;
            id = "00000000-0000-0000-0000-000000000000";
            isNewRow = true;
        },
        onEditingStart: function (e) {
            id = e.data.Id;
        },
    });
    $('#gridContainer').dxDataGrid('instance').expandAll(-1);
});