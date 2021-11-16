using Microsoft.ML.Data;

namespace AnomalyDetection.Common
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public float Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
