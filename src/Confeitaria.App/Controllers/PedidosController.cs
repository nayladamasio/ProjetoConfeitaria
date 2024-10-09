using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Confeitaria.App.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        private readonly IEnderecoPedidoRepository _enderecoPedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository, IMapper mapper,
                                 IEnderecoPedidoRepository enderecoPedidoRepository )
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
            _enderecoPedidoRepository = enderecoPedidoRepository;
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
        public async Task<IActionResult> Create(PedidoViewModel pedidoViewModel)
        {
            if (!ModelState.IsValid) return View(pedidoViewModel);

            pedidoViewModel.DataPedido = DateTime.Now;
            
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            await _pedidoRepository.Adicionar(pedido);

            return RedirectToAction("Index");
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
            var pedidoViewModel = await ObterPedidoProdutos(id);

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
        private async Task<PedidoViewModel> ObterPedidoProdutos(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoProdutos(id));
        }
    }
}
