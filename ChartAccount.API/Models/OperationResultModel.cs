namespace ChartAccountAPI.Models
{
    public class OperationResultModel
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
