//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using DetoxModel.Model.DataModels;

namespace DetoxModel.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"../../../../DetoxModel.Model/MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"../../../../../Data/wikiDetoxAnnotated40kRows.tsv";

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            ModelBuilder.CreateModel();

            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Create sample data to do a single prediction with it 
            string inputSampleText = "ML.NET is awesome";
            ModelInput sampleData = CreateSingleDataSample(inputSampleText);

            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(sampleData);

            Console.WriteLine($"Single Prediction --> Prediction for '{inputSampleText}' should be toxic = False | Predicted value was toxic = {predictionResult.Prediction}");
        }

        // Here I create your my sample hard-coded data (Could be coming from an end-user app)
        private static ModelInput CreateSingleDataSample(string inputTextStatement)
        {
            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            ModelInput sampleForPrediction = new ModelInput { Comment = inputTextStatement };
            return sampleForPrediction;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
