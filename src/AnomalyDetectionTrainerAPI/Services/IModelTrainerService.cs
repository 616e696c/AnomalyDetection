using Microsoft.ML.Data;

namespace AnomalyDetectionTrainerAPI.Services
{
    public interface IModelTrainerService
    {
        MulticlassClassificationMetrics Train(CancellationToken ct);
    }
}
