// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using ContactsApp.BackgroundService;
using ContactsApp.BackgroundService.Clients;
using ContactsApp.BackgroundService.Entities;
using ContactsApp.BackgroundService.Helpers;
using ContactsApp.BackgroundService.Services;
using ContactsApp.Core.DTOs;

var config = new ConsumerConfig()
{
    BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVER"),
    GroupId = "report-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    
};

var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("report-request");

var reportClient = new ReportClient();
var contactClient = new ContactClient();

CancellationTokenSource cancellationTokenSource =new();

try
{
    while (true)
    {
        var response = consumer.Consume(cancellationTokenSource.Token);
        if (response.Message != null)
        {
            var reportId = response.Message.Value;
            
            var items = await contactClient.GetContactInformationsAsync();

            items = items == null ? items : new List<ContactInformationDetailDTO>();


            Console.WriteLine("Durum Raporlandı (DataPulled)");

            List<ReportDataDTO> reportDataDtos = items.ToReportData();
            

            Console.WriteLine("Durum Raporlandı (InProgress)");

            var filePath = ExcelReportFileService.BuildFile(reportDataDtos,reportId);
            Console.WriteLine(filePath);
            reportClient.Ready(Guid.Parse(reportId),filePath);
            Console.WriteLine("Durum Raporlandı (Ready)");
        }
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}