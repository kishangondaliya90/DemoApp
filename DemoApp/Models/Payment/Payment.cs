namespace DemoApp.Models.Payment
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPayment { get; set; }
        public int PaymentStatusID { get; set; }
        public string PaymentStatus { get; set; }
    }
}
