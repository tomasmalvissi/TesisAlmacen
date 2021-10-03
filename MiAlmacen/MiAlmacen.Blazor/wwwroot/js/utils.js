function confirmar(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#254A71',
            cancelButtonColor: '#B6374A',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(result.isConfirmed);
            }
        })
    })
}

function simple(title, text, icon) {
    Swal.fire({
        title,
        text,
        icon
    })
}

function levantaModal(id) {
    $(id).modal('show');
}

function ocultaModal(id) {
    $(id).modal('hide');
}


function levantaTooltips() {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    });
}

function levantaAlerta() {
    $('.alert').alert()
}

function ocultaAlerta() {
    $('.alert').alert('close')
}

function levantaCollapse(id) {
    $('#collapseFiltros').collapse('toggle');
}