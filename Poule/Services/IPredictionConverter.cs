using System;
using Poule.Data;
using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public interface IPredictionConverter
    {
        PredictionEditModel ToPredictionEditModel(Prediction prediction);
        MyPredictionEditModel ToMyPredictionEditModel(Prediction prediction, DateTime currentTime);
        MyPredictionEditModel ToMyPredictionEditModel(Game game, DateTime currentTime);
        Prediction ToEntity(PredictionEditModel prediction, IApplicationDbContext context, int id = 0);
        Prediction ToEntity(MyPredictionEditModel prediction, IApplicationDbContext context, int id = 0);
    }
}  
