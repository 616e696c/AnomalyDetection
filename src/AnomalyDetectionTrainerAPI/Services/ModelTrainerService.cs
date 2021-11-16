using AnomalyDetection.Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using MulticlassClassificationMetrics = Microsoft.ML.Data.MulticlassClassificationMetrics;

namespace AnomalyDetectionTrainerAPI.Services
{
    public class ModelTrainerService: IModelTrainerService
    {
        public MulticlassClassificationMetrics Train(CancellationToken ct)
        {
            var mlContext = new MLContext();
            //Load all files from directory
            IDataView data = mlContext.Data.LoadFromTextFile("../data/TrainData/*.csv", 
            //Define input columns
            columns: new[] 
            {
                new TextLoader.Column("Product ID", DataKind.String,1),
                new TextLoader.Column("Type", DataKind.String,2),
                new TextLoader.Column("Air temperature", DataKind.Single,3),
                new TextLoader.Column("Process temperature", DataKind.Single,4),
                new TextLoader.Column("Rotational speed", DataKind.Single, 5),
                new TextLoader.Column("Torque", DataKind.Single,6),
                new TextLoader.Column("Tool wear", DataKind.Single,7),
                new TextLoader.Column("Machine failure", DataKind.Single,8),
            }, 
                hasHeader: true, separatorChar: ',');
            //Split data as train and test for further operations
            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            IDataView trainData = dataSplit.TrainSet;
            IDataView testData = dataSplit.TestSet;

            //We will use MulticlassClassification so use respective types
            //Those lines below AutoML's configurations
            var experimentSettings = new MulticlassExperimentSettings();
            experimentSettings.MaxExperimentTimeInSeconds = 60;// Increasing this may cause better prediction
            experimentSettings.OptimizingMetric = MulticlassClassificationMetric.LogLoss;//Indicates efficiency metric
            experimentSettings.CancellationToken = ct;

            //Create and experiment and execute it
            MulticlassClassificationExperiment experiment = mlContext.Auto().CreateMulticlassClassificationExperiment(experimentSettings);
            ExperimentResult<MulticlassClassificationMetrics> experimentResult = experiment
                            .Execute(trainData, labelColumnName: "Machine failure");

            //When AutoML finishes, we can get best fit with this property
            ITransformer model = experimentResult.BestRun.Model;
            //Test the trained model with test data
            var predictions = model.Transform(testData);
            //Evaluate the test results
            var metrics = mlContext.MulticlassClassification.Evaluate(predictions, labelColumnName: "Machine failure", scoreColumnName: "Score");

            //Save model for prediction engine usage
            mlContext.Model.Save(model, data.Schema, "../data/Models/model.zip");
            return metrics;
        }
    }
}
