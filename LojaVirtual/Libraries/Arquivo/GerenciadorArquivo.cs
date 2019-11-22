using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            //Adiciona as imagens temporárias do produto
            var NomeArquivo = Path.GetFileName(file.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", NomeArquivo).Replace("\\", "/");
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            //Excluir imagens temporárias do produto
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Imagem> MoverImagemProduto(List<string> listaCaminhoTemp, int produtoId)
        {
            //Verifica e cria pasta definitiva das imagens do produto
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());
            if (!Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }

            List<Imagem> ListaImagemDef = new List<Imagem>();
            //Move as imagens da pasta Temp para a pasta definitiva
            foreach (var caminhoTemp in listaCaminhoTemp)
            {
                if (!string.IsNullOrEmpty(caminhoTemp))
                {


                    //Pega os caminhos
                    var NomeArquivo = Path.GetFileName(caminhoTemp);
                    var CaminhoDef = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/");

                    if (CaminhoDef != caminhoTemp)
                    {
                        var CaminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\temp", NomeArquivo);
                        var CaminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", produtoId.ToString(), NomeArquivo);

                        //Move as imagens
                        if (File.Exists(CaminhoAbsolutoTemp))
                        {
                            //Deleta arquivo no destino
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(CaminhoAbsolutoDef);
                            }

                            //Move para pasta de destino e deleta temporário
                            File.Copy(CaminhoAbsolutoTemp, CaminhoAbsolutoDef);
                            if (File.Exists(CaminhoAbsolutoDef))
                            {
                                File.Delete(CaminhoAbsolutoTemp);
                            }

                            ListaImagemDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        ListaImagemDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), NomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });
                    }
                }
            }

            return ListaImagemDef;
        }
    }
}
