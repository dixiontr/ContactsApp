// See https://aka.ms/new-console-template for more information

using ContactsApp.BackgroundService;
using ContactsApp.BackgroundService.Clients;
using ContactsApp.BackgroundService.Entities;
using ContactsApp.BackgroundService.Helpers;
using ContactsApp.BackgroundService.Services;

var consumer = KafkaConsumer.RaiseConsumer();

CancellationTokenSource cancellationTokenSource =new();

try
{
    while (true)
    {
        var response = consumer.Consume(cancellationTokenSource.Token);
        if (response.Message != null)
        {
            var reportId = response.Message.Value;
            
            var items = await ContactClient.GetContactInformationsAsync();

            ReportClient.DataPulled(Guid.Parse(reportId));

            List<ReportDataDTO> reportDataDtos = items.ToReportData();
            
            Console.WriteLine("*******Started1st*******");
            foreach (var item in reportDataDtos)
            {
                Console.WriteLine(item);
            }

            ReportClient.InProgress(Guid.Parse(reportId));

            var filePath = ExcelReportFileService.BuildFile(reportDataDtos,reportId);
            
            Console.WriteLine(filePath);
            ReportClient.Ready(Guid.Parse(reportId));
        }
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}