$(() => {
    const url = '/Products';
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
        columns: [{
            dataField: 'Sku',
            caption: 'SKU',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna SKU debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'Name',
            caption: 'Nombre',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Nombre debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'Price',
            caption: 'Precio',
        }, {
            dataField: 'ShortDesc',
            caption: 'Descripción Corta',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Descripción Corta debe contener entre 1 y 1000 caracteres alfanuméricos.',
                max: 1000,
                min: 1,
            }],
        }, {
            dataField: 'LongDesc',
            caption: 'Descripción larga',
        }, {
            dataField: 'Image',
            caption: 'Imagen',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Imagen debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'CategoryId',
            caption: 'Categoría',
            lookup: {
                dataSource: DevExpress.data.AspNet.createStore({
                    key: 'Id',
                    loadUrl: `/ProductCategories/GetAll`,
                    onBeforeSend(method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    },
                }),
                valueExpr: 'Id',
                displayExpr: 'Name',
            },
        }, {
            dataField: 'Stock',
            caption: 'Stock',
        }, {
            dataField: 'Location',
            caption: 'Locación',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Locación debe contener entre 1 y 250 caracteres alfanuméricos.',
                max: 250,
                min: 1,
            }],
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
                title: 'Productos',
                showTitle: true,
                width: 900,
                height: 600,
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
                        dataField: 'Name',
                        colSpan: 2,
                    },
                    'Sku', 'Price',
                    {
                        dataField: 'ShortDesc',
                        editorType: 'dxTextArea',
                        colSpan: 2,
                        editorOptions: {
                            height: 100,
                        },
                    },
                    {
                        dataField: 'LongDesc',
                        editorType: 'dxTextArea',
                        colSpan: 2,
                        editorOptions: {
                            height: 100,
                        },
                    },
                    'Image', 'CategoryId', 'Stock', 'Location']
            },
        },
        grouping: {
            autoExpandAll: false,
        },
    });
});