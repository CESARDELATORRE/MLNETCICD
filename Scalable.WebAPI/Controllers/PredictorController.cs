using System;
using Microsoft.Extensions.ML;
using Microsoft.AspNetCore.Mvc;
using Scalable.WebAPI.ML.DataModels;

namespace Scalable.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictorController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public PredictorController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            // Get the ML Model Engine injected, for scoring
            _predictionEnginePool = predictionEnginePool;
        }

        // GET api/predictor/sentimentprediction?sentimentText=ML.NET is awesome!
        [HttpGet]
        [Route("sentimentprediction")]
        public ActionResult<string> PredictSentiment([FromQuery]string sentimentText)
        {
            ModelInput sampleData = new ModelInput() { Comment = sentimentText };

            //Predict sentiment
            ModelOutput prediction = _predictionEnginePool.Predict(sampleData);

            bool isToxic = prediction.IsToxic;
            float probability = CalculatePercentage(prediction.Score);
            string retVal = $"Prediction: Is Toxic?: '{isToxic.ToString()}' with {probability.ToString()}% probability of toxicity  for the text '{sentimentText}'";

            return retVal;

        }

        public static float CalculatePercentage(double value)
        {
            return 100 * (1.0f / (1.0f + (float)Math.Exp(-value)));
        }
    }
}