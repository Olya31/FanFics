using BL.Manager.Interface;
using FanFics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FanFics.Controllers
{
    public class FanficsController : Controller
    {
        private readonly ILogger<FanficsController> _logger;
        private readonly ICompositionManager _compositionManager;

        public FanficsController(
            ILogger<FanficsController> logger,
            ICompositionManager compositionManager)
        {
            _logger = logger;
            _compositionManager = compositionManager;
        }

        public async Task<ActionResult> Index(CancellationToken token)
        {
            var compositionViewModel = new CompositionViewModel();
            var compositions = await _compositionManager.GetToMainPageAsync(token);
           
            var compositionViewModels = compositionViewModel.ToCompositionViewModels(compositions);
            return View(compositionViewModels);
        }
    }
}
