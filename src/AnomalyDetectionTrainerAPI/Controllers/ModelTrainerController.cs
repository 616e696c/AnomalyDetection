using AnomalyDetectionTrainerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.Data;

namespace AnomalyDetectionTrainerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelTrainerController : ControllerBase
    {
        private readonly ILogger<ModelTrainerController> _logger;
        private readonly IModelTrainerService _trainService;

        public ModelTrainerController(ILogger<ModelTrainerController> logger, IModelTrainerService trainService)
        {
            _logger = logger;
            _trainService = trainService;   
        }
        [HttpPost(Name = "Train")]
        public MulticlassClassificationMetrics Train(CancellationToken cancellationToken)
        {
            return _trainService.Train(cancellationToken);
        }
    }
}
