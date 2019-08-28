using LojaVirtual.Data;
using LojaVirtual.Migrations;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        private readonly LojaVirtualContext _banco;

        public NewsLetterRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }
        public void Cadastrar(NewsLetterEmail newsLetter)
        {
            _banco.NewsLetterEmails.Add(newsLetter);
            _banco.SaveChanges();
        }

        public List<NewsLetterEmail> ObterEmailsCadastrados()
        {
            return _banco.NewsLetterEmails.ToList();
        }
    }
}
