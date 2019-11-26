$(document).ready(function () {
    MudarOrdenacao();
    MoverScrollOrdenacao();
});

function MoverScrollOrdenacao() {
    if (window.location.hash.length > 0) {
        var Hash = window.location.hash;

        if (Hash == "#posicao-produto") {
            window.scrollBy(0, 480);
        }
    }
}

function MudarOrdenacao() {
    $("#ordenacao").change(function () {
        //Valores padrão
        var Pagina = 1;
        var Pesquisa = "";
        var Ordenacao = $(this).val();

        //QueryString com os valores da página
        var QueryString = new URLSearchParams(window.location.search);

        //Verifica se existe valores para pagina na QueryString
        if (QueryString.has("pagina")) {
            Pagina = QueryString.get("pagina");
        }

        //Verifica se existe pesquisa para pagina na QueryString
        if (QueryString.has("pesquisa")) {
            Pesquisa = QueryString.get("pesquisa");
        }

        //Pega url da pagina atual
        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        //Monta a url para ordenar
        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao=" + Ordenacao + "#posicao-produto";

        //Redireciona para a URL criada
        window.location.href = URLComParametros;
    });
}