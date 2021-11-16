using AnomalyDetection.Common;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.Data.SchemaDefinition;

namespace AnomalyDetectionPredictionAPI.Services
{
    public static class PredictionEngineService
    {
        private static PredictionEngine<ModelInput, ModelOutput> _instance;
       
        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            CreatePredictEngine();
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            if(_instance != null)
                return _instance;   
            var mlContext = new MLContext();
            //Load model from file
            ITransformer mlModel = mlContext.Model.Load("../data/Models/model.zip", out var _);

            //Specify input schema
            var inputSchemaDefinition = SchemaDefinition.Create(typeof(ModelInput), SchemaDefinition.Direction.Both);

            //Create a prediction engine using trained model.
            _instance = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel, inputSchemaDefinition: inputSchemaDefinition);
            return _instance;
        }
    }
}
