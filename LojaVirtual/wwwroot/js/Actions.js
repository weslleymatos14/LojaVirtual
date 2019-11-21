$(document).ready(function () {
    $(".excluir").click(function (e) {
        var resultado = confirm("Deseja realmente excluir esse registro?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
});