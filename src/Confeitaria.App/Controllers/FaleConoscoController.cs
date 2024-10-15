using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Interfaces;
using Confeitaria.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Confeitaria.App.Controllers
{
    public class FaleConoscoController : Controller
    {
        private readonly IFaleConoscoRepository _faleConoscoRepository;
        private readonly IMapper _mapper;


        public FaleConoscoController(IFaleConoscoRepository faleConoscoRepository, IMapper mapper)
        {
            _faleConoscoRepository = faleConoscoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TBFaleConosco()
        {
            return View(_mapper.Map<IEnumerable<FaleConoscoViewModel>>(await _faleConoscoRepository.ObterTodos()));

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var faleConoscoViewModel = await ObterMensagem(id);        
            
            if(faleConoscoViewModel == null) return NotFound();

            return View(faleConoscoViewModel);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaleConoscoViewModel faleConoscoViewModel)
        {
            if (!ModelState.IsValid) return View(faleConoscoViewModel);

            faleConoscoViewModel.DataEnvio = DateTime.Now;

            await _faleConoscoRepository.Adicionar(_mapper.Map<FaleConosco>(faleConoscoViewModel));

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var faleConoscoViewModel = await ObterMensagem(id);

            if (faleConoscoViewModel == null) return NotFound();

            return View(faleConoscoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_faleConoscoRepository == null) return NotFound();

            await _faleConoscoRepository.Remover(id);

            return RedirectToAction("TBFaleConosco");
        }

        private async Task<FaleConoscoViewModel> ObterMensagem(Guid id)
        {
            return _mapper.Map<FaleConoscoViewModel>(await _faleConoscoRepository.ObterPorId(id));
        }
    }
}
