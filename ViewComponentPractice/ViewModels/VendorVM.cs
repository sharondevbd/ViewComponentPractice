namespace ViewComponentPractice.ViewModels
{
    public class VendorVM
    {
            public string Name { get; set; }
            public string VendorState { get; set; }
            public string Address { get; set; }
            public double Total { get; set; }
            public double Paid { get; set; }
            public decimal Due { get; set; }
        public int TotalInvoice { get; set; }
    }
}
