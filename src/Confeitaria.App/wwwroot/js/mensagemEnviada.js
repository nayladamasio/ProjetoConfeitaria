$(document).ready(function () {
    var sucesso = '@ViewBag.Sucesso';
    if (sucesso === 'true') {
        var myModal = new bootstrap.Modal(document.getElementById('modalSucesso'), {
            backdrop: 'static',
            keyboard: false
        });
        myModal.show();
    }
});