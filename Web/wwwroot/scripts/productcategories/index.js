$(() => {
    const url = '/ProductCategories';
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
            dataField: 'Name',
            caption: 'Nombre',
            validationRules: [{
                type: 'stringLength',
                message: 'La columna Nombre debe contener entre 1 y 50 caracteres alfanuméricos.',
                max: 50,
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
            mode: 'row',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            useIcons: true,
            texts: {
                confirmDeleteMessage: ''
            },
        },
        grouping: {
            autoExpandAll: false,
        },
    });
});