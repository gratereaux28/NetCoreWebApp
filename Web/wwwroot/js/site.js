async function onEditarRow(s) {
    var RowIndex = s.row.rowIndex;
    s.component.editRow(RowIndex);
};

async function onEliminarRow(s) {
    Swal.fire({
        title: 'Eliminar',
        text: `¿Está seguro que desea eleminar este registro?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Eliminar',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.isConfirmed) {
            s.component.deleteRow(s.row.rowIndex);
        }
    })
};