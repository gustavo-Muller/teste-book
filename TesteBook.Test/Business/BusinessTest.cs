using System;
using System.Linq;
using TesteBook.Business.Interface;
using TesteBook.Data;
using TesteBook.Service;
using Xunit;

namespace TesteBook.Test.Data
{
    public class BusinessTest
    {
        private readonly BookRepository _bookRepository = new BookRepository();
        private IBookService _bookService;

        public BusinessTest()
        {
            _bookService = Activator.CreateInstance(typeof(BookService), new BookRepository()) as IBookService;
        }

        [Theory(DisplayName = "Busca livros por parâmetros")]
        [InlineData("Percy")]
        private async void Obtenha_Livros(string parametro)
        {
            var livros = await _bookService.ObtenhaLivros(parametro);
            Assert.NotNull(livros);
            Assert.NotNull(livros.Volumes);
            Assert.NotEmpty(livros.Volumes);
        }

        [Fact(DisplayName = "Valida A inserção, integridade e a exclusão de favoritos")]
        private async void Favoritos_Validos()
        {
            var livros = await _bookService.ObtenhaLivros("{}");

            Assert.NotNull(livros);
            Assert.NotNull(livros.Volumes);
            Assert.NotEmpty(livros.Volumes);

            var moc = livros.Volumes.FirstOrDefault();
            await _bookService.FavoriteLivro(moc.Id);

            var favoritados = await _bookService.ObtenhaFavoritos();
            Assert.True(favoritados.Exists(f => f.Id.Equals(moc.Id)));

            await _bookService.DeleteFavorito(moc.Id);
            Assert.Null(_bookRepository.Obtenha(moc.Id).Result);
        }
    }
}
