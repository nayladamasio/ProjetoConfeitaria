using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;

namespace Confeitaria.App.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        private readonly IEnderecoPedidoRepository _enderecoPedidoRepository;
        private readonly IPedidoProdutoRepository _pedidoProdutoRepository;
        private readonly IClienteRepository _clienteRepository;

        public PedidosController(IPedidoRepository pedidoRepository, IMapper mapper,
                                 IEnderecoPedidoRepository enderecoPedidoRepository,
                                  IPedidoProdutoRepository pedidoProdutoRepository,
                                  IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
            _enderecoPedidoRepository = enderecoPedidoRepository;
            _pedidoProdutoRepository = pedidoProdutoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var pedidoViewModel = await ObterPedidoEndereco(id);

            if (pedidoViewModel == null) return NotFound();

            return View(pedidoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoViewModel pedidoViewModel, int quantidade)
        {
            if (!ModelState.IsValid) return View(pedidoViewModel);

            pedidoViewModel.DataPedido = DateTime.Now;
            
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            await _pedidoRepository.Adicionar(pedido);

            foreach (var produto in ProdutosController._carrinho)
            {
                var pedidoProduto = new PedidoProduto
                {
                    PedidoId = pedido.Id,
                    ProdutoId = produto.Id,
                    Quantidade = produto.Quantidade
                };
                
                await _pedidoProdutoRepository.Adicionar(pedidoProduto);
            }

            ProdutosController._carrinho.Clear();

            return RedirectToAction("PedidoConcluido", new { id = pedido.Id });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var pedidoViewModel = await ObterEndereco(id);

            if (pedidoViewModel == null) return NotFound();

            return View(pedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PedidoViewModel pedidoViewModel)
        {
            if (id != pedidoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(pedidoViewModel);

            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            await _pedidoRepository.Alterar(pedido);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var pedidoViewModel = await ObterPedidoEndereco(id);

            if (pedidoViewModel == null) return NotFound();

            return View(pedidoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pedidoViewModel = await ObterPedidoEndereco(id);

            if (pedidoViewModel == null) return NotFound();

            await _pedidoRepository.Remover(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var pedido = await ObterPedidoEndereco(id);

            if (pedido == null) return NotFound();

            return PartialView("_EnderecoEdit", new PedidoViewModel { Endereco = pedido.Endereco });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(PedidoViewModel pedidoViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_EnderecoEdit", pedidoViewModel.Endereco);

            await _enderecoPedidoRepository.Alterar(_mapper.Map<EnderecoPedido>(pedidoViewModel.Endereco));

            var url = Url.Action("ObterEndereco", "Pedidos", new { id = pedidoViewModel.Endereco.Pedido.Id });

            return Json(new { sucess = true, url });
        }

        public async Task<IActionResult> PedidoConcluido(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPedidoCliente(id);
            if (pedido == null) return NotFound();

            var endereco = await _enderecoPedidoRepository.ObterEnderecoPorPedido(id);

            var pedidoViewModel = _mapper.Map<PedidoViewModel>(pedido);

            ViewBag.Endereco = endereco.Cep;
            ViewBag.NomeCliente = pedido.Cliente.Nome;
            ViewBag.EmailCliente = pedido.Cliente.Email;
            return View(pedidoViewModel);
        }


        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var pedido = await ObterPedidoEndereco(id);

            if (pedido == null) return NotFound();

            return PartialView("_EnderecoDetails", pedido);
        }

        private async Task<PedidoViewModel> ObterPedidoEndereco(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoEndereco(id));
        }
        //private async Task<PedidoViewModel> ObterPedidoProdutos(Guid id)
        //{
        //    return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoProdutos(id));
        //}
    }
}
