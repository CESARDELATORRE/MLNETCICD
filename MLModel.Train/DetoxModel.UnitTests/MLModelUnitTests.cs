using DetoxModel.Model.DataModels;
using Microsoft.ML;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System;
using System.IO;

namespace Tests
{
    public class MLModelUnitTests
    {
        MLContext _mlContext;
        ITransformer _trainedModel;

        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"../../../../DetoxModel/DetoxModel.Model/MLModel.zip";

        [SetUp]
        public void Setup()
        {
            _mlContext = new MLContext();

            _trainedModel = _mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out var modelInputSchema);
        }

        [Test]
        public void TestNonToxicStatement()
        {
            ModelInput sampleStatement = new ModelInput { Comment = "ML.NET is awesome!" };

            var predEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_trainedModel);

            var resultprediction = predEngine.Predict(sampleStatement);

            //Should be toxic=false since "ML.NET is awesome!" is not toxic
            Assert.AreEqual(false, Convert.ToBoolean(resultprediction.Prediction));

            //Assert.Pass();
        }

        [Test]
        public void TestToxicStatement()
        {
            string testStatament = "This movie sucks";
            ModelInput sampleStatement = new ModelInput { Comment = testStatament };

            var predEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_trainedModel);

            var resultprediction = predEngine.Predict(sampleStatement);

            //Should be toxic=true since "This movie sucks" is toxic
            Assert.AreEqual(true, Convert.ToBoolean(resultprediction.Prediction));

            //Assert.Pass();
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(MLModelUnitTests).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}