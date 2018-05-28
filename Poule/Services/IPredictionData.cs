using System;
using System.Collections.Generic;
using Poule.Models;
using Poule.ViewModel;

namespace Poule.Services
{
    public interface IPredictionData
    {
        IEnumerable<Prediction> GetAll();
        Prediction Get(int id);
        Prediction Add(Prediction newPrediction);
        Prediction Update(Prediction prediction);
        PredictionEditModel ToPredictionEditModel(Prediction prediction);
        MyPredictionEditModel ToMyPredictionEditModel(Prediction prediction, DateTime currentTime);
        MyPredictionEditModel ToMyPredictionEditModel(Game game, DateTime currentTime);
        Prediction ToEntity(PredictionEditModel prediction, int id = 0);
        Prediction ToEntity(MyPredictionEditModel prediction, int id = 0);
        Prediction GetForUser(int userId,int gameId);
        void Remove(Prediction prediction);
    }
}