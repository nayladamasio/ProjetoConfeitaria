using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Enums;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Confeitaria.App.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public static List<ProdutoViewModel> _carrinho = new List<ProdutoViewModel>();

        public ProdutosController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
        }

        public async Task<IActionResult> IndexBolos()
        {
            var produtos = await _produtoRepository.ObterTodos();

            var bolos = produtos.Where(p => p.Categoria == Categoria.Bolos);

            var bolosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(bolos);

            return View(bolosViewModel);

        }
        public async Task<IActionResult> IndexTortas()
        {
            var produtos = await _produtoRepository.ObterTodos();

            var tortas = produtos.Where(p => p.Categoria == Categoria.Tortas);

            var tortasViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(tortas);

            return View(tortasViewModel);

        }
        public async Task<IActionResult> IndexCupCakes()
        {
            var produtos = await _produtoRepository.ObterTodos();

            var cupcakes = produtos.Where(p => p.Categoria == Categoria.CupCakes);

            var cupcakesViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(cupcakes);

            return View(cupcakesViewModel);

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(produtoViewModel);

            produtoViewModel.DataCadastro = DateTime.Now;

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivo(produtoViewModel.ImageUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImageUpload.FileName;

            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var atualizar = await ObterProdutoId(id);

            if (atualizar == null) return NotFound();

            produtoViewModel.Imagem = atualizar.Imagem;

            if (!ModelState.IsValid) return View(produtoViewModel);

            if (produtoViewModel.ImageUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";

                if (!await UploadArquivo(produtoViewModel.ImageUpload, imgPrefixo)) return View(produtoViewModel);

                atualizar.Imagem = imgPrefixo + produtoViewModel.ImageUpload.FileName;
            }

            atualizar.Nome = produtoViewModel.Nome;
            atualizar.Descricao = produtoViewModel.Descricao;
            atualizar.Peso = produtoViewModel.Peso;
            atualizar.Valor = produtoViewModel.Valor;
            atualizar.Categoria = produtoViewModel.Categoria;
            atualizar.Disponivel = produtoViewModel.Disponivel;
            atualizar.DataCadastro = produtoViewModel.DataCadastro;

            await _produtoRepository.Alterar(_mapper.Map<Produto>(atualizar));

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_produtoRepository == null) return NotFound();

            await _produtoRepository.Remover(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Informacoes(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }


        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorID(id));
        }

        private async Task<ProdutoViewModel> ObterProdutoId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoId(id));
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo == null || arquivo.Length <= 0) return false;

            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(caminho))
            {
                ModelState.AddModelError(String.Empty, "Já existe um arquivo com este nome");
                return false;
            }

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarAoCarrinho(Guid id, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorID(id);
            if (produto == null) return NotFound();
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
            var produtocarrinho = _carrinho.FirstOrDefault(p => p.Id == id);
            if (produtocarrinho != null)
            {
                produtocarrinho.Quantidade += quantidade;
            }
            else
            {
                produtoViewModel.Quantidade = quantidade;
                _carrinho.Add(produtoViewModel);
            }
            return RedirectToAction("Carrinho");

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AdicionarAoCarrinho(Guid id)
        //{
        //    var produto = await _produtoRepository.ObterPorID(id);
        //    if (produto == null) return NotFound();
        //    var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

        //        _carrinho.Add(produtoViewModel);
        //    return RedirectToAction("Carrinho");

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AtualizarCarrinho(Guid produtoId, int quantidade)
        {
            var produtocarrinho = _carrinho.FirstOrDefault(p => p.Id == produtoId);
            if (produtocarrinho != null) produtocarrinho.Quantidade = quantidade;

            return RedirectToAction("carrinho");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverDoCarrinho(Guid id)
        {
            var produtocarrinho = _carrinho.FirstOrDefault(p => p.Id == id);

            if (produtocarrinho != null) _carrinho.Remove(produtocarrinho);

            return RedirectToAction("Carrinho");

        }
        public IActionResult Carrinho()
        {
            return View(_carrinho);
        }

    }
}
