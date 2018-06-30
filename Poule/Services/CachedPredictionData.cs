using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poule.Models;

namespace Poule.Services
{
    public class CachedPredictionData : IPredictionData
    {
        private readonly ISqlPredictionData _sqlPredictionData;

        public CachedPredictionData(ISqlPredictionData sqlPredictionData)
        {
            _sqlPredictionData = sqlPredictionData;
        }

        public IEnumerable<Prediction> GetAll()
        {
            // carefully read 
            // https://stackexchange.github.io/StackExchange.Redis/KeysScan
            // to understand that this works as intended
            var result = new List<Prediction>();
            var iserver = Program.LazyConnection.Value.GetEndPoints()[0];
            var server = Program.LazyConnection.Value.GetServer(iserver);
            var cache = Program.LazyConnection.Value.GetDatabase();
            
            foreach (var key in server.Keys(pattern: "p-*"))
            {
                result.Add(JsonConvert.DeserializeObject<Prediction>(cache.StringGet(key)));
              
            }

            return result;
        }

        public Prediction Get(int id)
        {
            return _sqlPredictionData.Get(id);
        }

        public Prediction Add(Prediction prediction)
        {
            var result = _sqlPredictionData.Add(prediction);
            var cache = Program.LazyConnection.Value.GetDatabase();
            cache.StringSet("p-" + prediction.User.Id + "-" + prediction.Game.Id,
                JsonConvert.SerializeObject(prediction));
            return result;
        }

        public Prediction Update(Prediction prediction)
        {
            var p = prediction;
            if (prediction.Game == null)
            {
                p = Get(prediction.Id);
            }

            var result = _sqlPredictionData.Update(p);
            var cache = Program.LazyConnection.Value.GetDatabase();
            cache.StringSet("p-" + p.User.Id + "-" + p.Game.Id,
                    JsonConvert.SerializeObject(p));
            return result;
        }

        public Prediction GetForUser(int userId, int gameId)
        {
            var cache = Program.LazyConnection.Value.GetDatabase();

            var cachedResult = cache.StringGet("p-" + userId + "-" + gameId);

            if (cachedResult.HasValue)
            {
                return JsonConvert.DeserializeObject<Prediction>(cachedResult);
            }
            return null;
        }

        public void Remove(Prediction prediction)
        {
            _sqlPredictionData.Remove(prediction);
            var cache = Program.LazyConnection.Value.GetDatabase();
            cache.KeyDelete("p-" + prediction.User.Id + "-" + prediction.Game.Id);
        }

        public bool InitCache()
        {
            // clear all predictions in cache
                var iserver = Program.LazyConnection.Value.GetEndPoints()[0];
            var server = Program.LazyConnection.Value.GetServer(iserver);
            var cache = Program.LazyConnection.Value.GetDatabase();

            foreach (var key in server.Keys(pattern: "p-*"))
            {
                cache.KeyDelete(key);
            }

            // get all database values
            var predictions = _sqlPredictionData.GetAll();

            // insert into cache
            foreach (var prediction in predictions)
                cache.StringSet("p-" + prediction.User.Id + "-" + prediction.Game.Id,
                    JsonConvert.SerializeObject(prediction));
            return true;
        }
    }
}
