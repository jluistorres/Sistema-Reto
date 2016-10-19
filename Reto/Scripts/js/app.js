appMaster.Modal = function (options) {
    options = $.extend({
        title: 'Mensaje de la aplicación',
        content: '',
        //width: '500px',
        accept: true,
        callback: null,
        closeAfterCallback: false,
        buttons: '',
        padding: '15px',
        //maxHeight: null,
        fullscreen: false,
        CssClass: '',//modal-sm, modal-lg
        type: 'type-info',
        animation: 'fade'
    }, options);

    var ID_MODAL = 'modal-' + new Date().getTime();
    var objModal = '<div id="' + ID_MODAL + '" class="modal modal-custom ' + options.animation + ' ' + options.type + '" data-backdrop="static" tabindex="-1" role="dialog">' +
                '<div class="modal-dialog ' + options.CssClass + '" role="document">' +
                '<div class="modal-content">' +
                '<div class="modal-header">' +
                '<button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                '<span aria-hidden="true">&times;</span>' +
                '</button>' +
                '<h4 class="modal-title">' + options.title + '</h4>' +
                '</div>' +
                '<div class="modal-body"></div>' +
                '</div>' +
                '</div>' +
                '</div>';

    $('body').append(objModal);
    ID_MODAL = '#' + ID_MODAL;
    var modal = $('body').find(ID_MODAL);
    modal.find('.modal-body').append(options.content).css({ 'padding': options.padding });
    //if (options.maxHeight != null) { modal.find('.modal-body').css({ 'max-height': options.maxHeight, 'overflow': 'auto' }) }

    //Width               
    if (options.fullscreen) modal.addClass('modal-full');

    $(ID_MODAL).on('hidden.bs.modal', function () { modal.remove(); })

    ////More options
    var _footer = $("<div>", { 'class': "modal-footer" });
    if (options.accept || options.buttons != '') {
        _footer.appendTo(modal.find('.modal-content'));
    }

    //Boton Aceptar
    if (options.accept && options.buttons == '') {
        var button = $("<input />", { 'class': 'btn btn-default btn-sm' })
                    .attr({ 'type': 'button', 'value': "Aceptar" }).appendTo(_footer);

        button.click(function () {
            if (typeof options.callback === 'function') {
                options.callback();
                if (options.closeAfterCallback) modal.modal('hide');
            } else { modal.modal('hide'); }
        });
    }

    //Botones personalizados
    if (options.buttons != '') {
        for (var i = 0; i < options.buttons.length; i++) {
            _footer.append(options.buttons[i]);
        }
    }

    return {
        Modal: modal,
        Show: function () { modal.modal('show'); },
        Close: function () { modal.modal('hide'); }
    };
}

//Mensajes de usuario
appMaster.MessageBox = function (title, bodyMessage, callback) {
    return appMaster.Modal({ title: title, content: bodyMessage, callback: callback, CssClass: 'modal-sm' }).Show();
}

//options = { Title: '', Message: '', Yes: function, No: function }
appMaster.Confirm = function (options) {
    options = $.extend({
        Title: 'Mensaje al usuario',
        Message: '¿Desea continuar?',
        Yes: null,
        No: null,
        YesText: 'Aceptar',
        NoText: 'Cancelar'
    }, options);

    var modal, buttons = [];
    var buttonYes = $("<button>", { 'class': 'btn btn-primary btn-sm', 'type': 'button', 'html': options.YesText });
    var buttonNo = $("<button>", { 'class': 'btn btn-default btn-sm', 'type': 'button', 'html': options.NoText });

    buttonYes.click(function () {
        if (typeof options.Yes === 'function') options.Yes();
        modal.Close();
    });

    buttonNo.click(function () {
        if (typeof options.No === 'function') options.No();
        modal.Close();
    });

    buttons.push(buttonYes);
    buttons.push(buttonNo);

    modal = appMaster.Modal({
        title: options.Title,
        content: options.Message,
        buttons: buttons,
        CssClass: 'modal-sm',
        animation: 'scale'
    });

    //Colocamos el foco sobre el boton de Aceptar
    modal.Modal.on('shown.bs.modal', function () { buttonYes.focus(); });

    modal.Show();
}

appMaster.Prompt = function (options) {
    options = $.extend({
        Titulo: 'Valor',
        Mensaje: 'Ingrese un valor',
        Dato: '',
        Tipo: 'text',
        Aceptar: null,
        Cancelar: null
    }, options);

    var modal, buttons = [], txt;
    if (options.Tipo == 'text' || options.Tipo == 'number')
        txt = $('<input type="' + options.Tipo + '" class="form-control textfield" />');
    else if (options.Tipo == 'textarea') {
        txt = $('<textarea>', { 'class': 'form-control' });
        txt.attr('rows', 5);
    }

    txt.val(options.Dato);

    var contenido = $('<div>');
    var label = $('<div>', { 'html': options.Mensaje });
    contenido.append(label).append(txt);

    var buttonYes = $("<input />", { 'class': 'btn btn-primary btn-sm' })
                    .attr({ 'type': 'button', 'value': "Aceptar" });

    var buttonNo = $("<input />", { 'class': 'btn btn-default btn-sm' })
                    .attr({ 'type': 'button', 'value': "Cancelar" });

    txt.keyup(function (e) {
        if (options.Tipo != 'textarea')
            if (e.keyCode == 13 || e.which == 13)
                buttonYes.click();
    });

    buttonYes.click(function () {
        if (!txt.val()) {
            appMaster.MessageBox('Aviso', 'Por favor agregue un texto');
            return;
        }

        if (typeof options.Aceptar === 'function') options.Aceptar(txt.val());
        modal.Close();
    });

    buttonNo.click(function () {
        if (typeof options.Cancelar === 'function') options.Cancelar();
        modal.Close();
    });

    buttons.push(buttonYes);
    buttons.push(buttonNo);

    modal = appMaster.Modal({
        title: options.Titulo,
        content: contenido,
        buttons: buttons,
        CssClass: 'modal-sm'
    });

    modal.Modal.on('shown.bs.modal', function () { if (txt) txt.focus(); });
    modal.Show();
}

appMaster.AddZerosNumber = function (number, length) {
    var my_string = '' + (number || 0);
    while (my_string.length < length) {
        my_string = '0' + my_string;
    }
    return my_string;
}

$(function () {
    $.ajaxSetup({ cache: false });

    var _loading = $('.loading');
    $(document).ajaxStart(function () { _loading.show(); })
               .ajaxComplete(function () { _loading.hide(); })
               .ajaxError(function (a, b, c) {
                    switch (b.status) {
                        case 404: console.log('Error 404: El recurso solicitado no existe o no está disponible'); break;
                        case 403: appMaster.MessageBox('No autorizado', 'Ud. no tiene permisos para ejecutar la acción solicitada'); break;
                    }
                });

    $('body').on('keypress', 'input:text', function (e) {
        if (e.keyCode == 13) return false;
    });

    $(document).on('hidden.bs.modal', '.modal', function () {
        $('.modal:visible').length && $(document.body).addClass('modal-open');
    });
});
