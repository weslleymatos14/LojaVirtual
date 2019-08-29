$(document).ready(function () {
    $(".excluir").click(function (e) {
        var resultado = confirm("Deseja realmente excluir esse registro?");

        if (resultado == false) {
            e.preventDefault();
        }
    })
})