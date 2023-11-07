namespace DeveloperQuestions.LeaveAlone.Models
{
    public enum PaymentTerms { COD, Net20, Net30, Net60 }

    public record Invoice(
        DateTime CreatedOn,
        PaymentTerms PaymentTerms,
        string CreatedById,
        string CustomerId,
        string InvoiceNumber,
        decimal Amount,
        decimal TaxAmount,
        decimal TotalAmount)
    {
        public DateTime PaymentDueDate { get; set; }
    }
}
