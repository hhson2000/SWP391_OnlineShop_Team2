namespace NitStore.Models.DTO
{
    public class FeedbackOrderDTO
    {
        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int ProductName { get; set; }

        public byte[] imageBit { get; set; }
        public string Feedback { get; set; }
    }
}
