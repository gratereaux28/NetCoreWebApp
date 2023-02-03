$(() => {
    const url = '/Customer';
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
            dataField: 'customer',
            caption: 'Cliente',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Cliente debe contener entre 1 y 500 caracteres alfanuméricos.',
                max: 500,
                min: 1,
            }],
        }, {
            dataField: 'Country',
            caption: 'País',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna País debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'City',
            caption: 'Ciudad',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Ciudad debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'ContactManager',
            caption: 'Administrador',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Administrador debe contener entre 1 y 100 caracteres alfanuméricos.',
                max: 100,
                min: 1,
            }],
        }, {
            dataField: 'ContactNumber',
            caption: 'Número de Contacto',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Número debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
                min: 1,
            }],
        }, {
            dataField: 'Status',
            caption: 'Estado',
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
                width: 700,
                height: 450,
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
                colCount: 1,
                name: 'editForm',
                items: ['customer', 'Country', 'City', 'ContactManager', 'ContactNumber', 'Status']
            },
        },
        grouping: {
            autoExpandAll: false,
        },
    });
});