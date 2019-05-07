using NUnit.Framework;

namespace Tests
{
    public class MLModelUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNonToxicStatement()
        {
            //ModelInput sampleStatement = new SentimentIssue { Text = "This is a very rude movie" };

            //ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            //// Create prediction engine related to the loaded trained model
            //var predEngine = mlContext.Model.CreatePredictionEngine<SentimentIssue, SentimentPrediction>(trainedModel);

            ////Score
            //var resultprediction = predEngine.Predict(sampleStatement);

            //Console.WriteLine($"=============== Single Prediction  ===============");
            //Console.WriteLine($"Text: {sampleStatement.Text} | Prediction: {(Convert.ToBoolean(resultprediction.Prediction) ? "Toxic" : "Non Toxic")} sentiment | Probability of being toxic: {resultprediction.Probability} ");
            //Console.WriteLine($"==================================================");

            Assert.Pass();
        }
    }
}