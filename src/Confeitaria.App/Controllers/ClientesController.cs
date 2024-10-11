using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Confeitaria.App.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var clienteViewModel = await ObterCliente(id);

            if (clienteViewModel == null) return NotFound();

            return View(clienteViewModel);
           
        }

        public IActionResult Create()
        {
            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);

            await _clienteRepository.Adicionar(_mapper.Map<Cliente>(clienteViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var clienteViewModel = await ObterClientePedidos(id);

            if (clienteViewModel == null) return NotFound();
            
            return View(clienteViewModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(clienteViewModel);

            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            await _clienteRepository.Alterar(cliente);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var clienteViewModel = await ObterClientePedidos(id);

            if (clienteViewModel == null) return NotFound();

            return View(clienteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clienteViewModel = await ObterClientePedidos(id);

            if(clienteViewModel == null) return NotFound(); 

            await _clienteRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<ClienteViewModel> ObterCliente(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterPorID(id));
        }
       
        private async Task<ClienteViewModel> ObterClientePedidos(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterClientePedidos(id));
        }
    }
}
