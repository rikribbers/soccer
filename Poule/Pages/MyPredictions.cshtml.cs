using System;
using System.Collections.Generic;
using System.Linq;
using Poule.Data;
using Poule.Models;
using Poule.Services;
using Poule.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Poule.Pages
{
    public class MyPredictionsModel : BasePageModel
    {
        private readonly IPredictionData _predictionData;
        private readonly IGameData _gameData;
        private readonly IUserData _userData;

        [BindProperty]
        public List<MyPredictionEditModel> Predictions { get; set; }

        [BindProperty]
        public User MyUser { get; set; }

        public MyPredictionsModel(IPredictionData predictionData, IGameData gameData, IUserData userData,
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _predictionData = predictionData;
            _gameData = gameData;
            _userData = userData;
        }

        public IActionResult OnGet()
        {
            var email = User.Identity.Name;
            var id = _userData.Get(email).Id;
            FillController(id);
            return Page();
        }

        private void FillController(int id)
        {
            Predictions = new List<MyPredictionEditModel>();

            var games = _gameData.GetAll();
            MyUser = _userData.Get(id);
            var currentTime = DateTime.Now;
            foreach (var game in games)
            {
                var prediction = _predictionData.GetForUser(MyUser.Id).FirstOrDefault(p => p.Game.Id == game.Id);

                if (prediction == null)
                {
                    Predictions.Add(_predictionData.ToMyPredictionEditModel(game, currentTime));
                }
                else
                {
                    prediction.User = MyUser;
                    Predictions.Add(_predictionData.ToMyPredictionEditModel(prediction, currentTime));
                }
            }
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            // The trick here is that we always store the data in the database
            // as the user might want to get back to this page
            foreach (var prediction in Predictions)
                if (prediction.HalftimeScore != null || prediction.FulltimeScore != null)
                {
                    var p = _predictionData.ToEntity(prediction);
                    p.User = _userData.Get(MyUser.Id);
                    p.Game = _gameData.Get(prediction.GameId);
                    if (prediction.Id < 1)
                        _predictionData.Add(p);
                    else
                        _predictionData.Update(p);
                }
            return RedirectToPage("MyPredictions");
        }
    }
}