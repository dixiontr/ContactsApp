using Confluent.Kafka;

namespace ContactsApp.ReportService.Settings
{
    public class KafkaSetting
    {
        public string BoostrapServers { get; set; }
        public string Topic { get; set; }
        public string GroupId { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; }
    }
}