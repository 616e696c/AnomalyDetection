using Microsoft.ML.Data;

namespace AnomalyDetectionTrainerAPI.Services
{
    public interface ITrainService
    {
        MulticlassClassificationMetrics Train(CancellationToken ct);
    }
}
