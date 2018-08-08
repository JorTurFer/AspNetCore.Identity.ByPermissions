//Abre una nueva ventana
function openInNewTab(url) {
    var win = window.open(url, '_blank');
    win.focus();
}
//Muestra los tooltip
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
//Llamada AJAX para enviar correo de verificacion desde el profile
function sendVerificationMail(url) {
    $.ajax({
        url: url,
        data: {
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val(),
            Email: $("#Email").val()
        },
        type: "post",
        success: function (data) {
            $("body").html(data);
        },
        error: function () {
            alert("Oops, hemos tenido un problema...");
        }
    });
}

//Añade el evento click de los mensajes modales y envia los datos al controlador
function activateModalActions() {
    $("a[data-modal]").click(function () {
        var targetId = $(this).data("targetid");
        var modal = $(this).data("modal");
        $(modal).data("targetid", targetId)
            .modal({ show: true, backdrop: "static" });
        return false;
    });

    $(".site-modal-remove").each(function (index, element) {
        var destinationUrl = $(element).data("url");
        $(".btn-primary", $(element)).click(function () {
            var targetId = $(element).data("targetid");
            $.post(destinationUrl, { id: targetId, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() })
                .done(function () { location.reload(); })
                .fail(function () { alert("Oops, hemos tenido un problema..."); });
        });
    });
}