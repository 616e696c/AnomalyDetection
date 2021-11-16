using AnomalyDetectionTrainerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.Data;

namespace AnomalyDetectionTrainerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly ILogger<TrainController> _logger;
        private readonly ITrainService _trainService;

        public TrainController(ILogger<TrainController> logger, ITrainService trainService)
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
