using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Prediction GetForUser(int userId,int gameId);
        void Remove(Prediction prediction);
        bool InitCache();

    }
}