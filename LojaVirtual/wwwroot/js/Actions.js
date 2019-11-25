$(document).ready(function () {
    $(".excluir").click(function (e) {
        var resultado = confirm("Deseja realmente excluir esse registro?");

        if (resultado == false) {
            e.preventDefault();
        }
    });
    //Mascara de Dinheiro
    $('.money').mask('000.000.000.000.000,00', { reverse: true });

    AjaxUploadImagensProdutos();
});

function AjaxUploadImagensProdutos() {
    $(".img-upload").click(function (){
        $(this).parent().parent().find(".input-file").click();
    });

    

    $(".btn-imagem-excluir").click(function () {
        var CampoHidden = $(this).parent().find("input[name=Imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");
        var InputFile = $(this).parent().find(".input-file");

        $.ajax({
            type: "GET",
            url: "/Colaborador/Imagem/Deletar?caminho=" + CampoHidden.val(),
            error: function () {
                alert("Erro ao deletar o arquivo!");
            },
            success: function () {
                Imagem.attr("src", "/img/imagem-padrao.png");
                BtnExcluir.addClass("btn-ocultar");
                CampoHidden.val("");
                InputFile.val("");
            },
        });
    });

    $(".input-file").change(function () {
        //Formulário de dados em JavaScript
        var Formulario = new FormData();
        Formulario.append("file", $(this)[0].files[0]);

        var CampoHidden = $(this).parent().find("input[name=Imagem]");
        var Imagem = $(this).parent().find(".img-upload");
        var BtnExcluir = $(this).parent().find(".btn-imagem-excluir");

        //Mostra imagem de loading
        Imagem.attr("src", "/img/Loading.gif");
        Imagem.addClass("thumb");

        //Requisição Ajax enviando formulário
        $.ajax({
            type: "POST",
            url: "/Colaborador/Imagem/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                Imagem.removeClass("thumb");
                Imagem.attr("src", "/img/imagem-padrao.png");
                alert("Erro no envio do arquivo!");
            },
            success: function (data) {
                var Caminho = data.caminho;
                Imagem.attr("src", Caminho);
                Imagem.removeClass("thumb");
                CampoHidden.val(Caminho);
                BtnExcluir.removeClass("btn-ocultar");
            },
        });
    });
}