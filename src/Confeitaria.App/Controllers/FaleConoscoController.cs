using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Confeitaria.App.Data;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Interfaces;
using Confeitaria.Data.Repositories;
using AutoMapper;
using Confeitaria.Business.Models;

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

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FaleConoscoViewModel>>(await _faleConoscoRepository.ObterTodos()));
        }

        public IActionResult FaleConosco2()
        {
            return View();
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

            await _faleConoscoRepository.Adicionar(_mapper.Map<FaleConosco>(faleConoscoViewModel));

            return RedirectToAction("FaleConosco2");

        }

        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var faleConoscoViewModel = await _faleConoscoRepository.ObterPorId(id);
        //    if (faleConoscoViewModel == null) return NotFound();
        //    return View(faleConoscoViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, FaleConoscoViewModel faleConoscoViewModel)
        //{
        //    if (id != faleConoscoViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(faleConoscoViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FaleConoscoViewModelExists(faleConoscoViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(faleConoscoViewModel);
        //}

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
            if (ObterMensagem(id) == null) return NotFound();

            await _faleConoscoRepository.Remover(id);

            return RedirectToAction("Index");
        }

        private async Task<FaleConoscoViewModel> ObterMensagem(Guid id)
        {
            return _mapper.Map<FaleConoscoViewModel>(await _faleConoscoRepository.ObterPorId(id));
        }
    }
}
