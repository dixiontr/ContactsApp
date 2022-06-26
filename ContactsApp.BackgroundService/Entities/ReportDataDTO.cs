namespace ContactsApp.BackgroundService.Entities
{

    public record ReportDataDTO
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }

}