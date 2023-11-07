using AutoFixture;
using DeveloperQuestions.LeaveAlone.Models;
using InvoicingApp.PleaseModify.Repositories;
using InvoicingApp.PleaseModify.Services;
using InvoicingApp.PleaseModify.Validators;
using NSubstitute;

namespace InvoicingApp.UnitTests
{
    public class InvoiceServiceUnitTest
    {
        private readonly InvoiceService _invoiceService;
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IInvoiceRepository _invoiceRepository = Substitute.For<IInvoiceRepository>();
        private readonly IFixture _fixture = new Fixture();

        public InvoiceServiceUnitTest() 
        {
            _invoiceService = new InvoiceService(_dateTimeProvider, _invoiceRepository, new InvoiceValidator());
        }

        [Fact]
        public void InvoiceAdd_ShouldAddInvoice_WhenInvoiceIsValid()
        {
            // Arrange
            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2023, 11, 6));
            var invoice = _fixture.Build<Invoice>()
                .With(i => i.TaxAmount, 100)
                .With(i => i.Amount, 1000)
                .With(i => i.TotalAmount, 1100)
                .With(i => i.CreatedOn, new DateTime(2023, 11, 1))
                .With(i => i.InvoiceNumber, "0123456789")
                .With(i => i.CreatedById, "111")
                .With(i => i.CustomerId, "1")
                .With(i => i.PaymentTerms, PaymentTerms.COD)
                .Create();

            // Act
            _invoiceService.Add(invoice);


            // Assert
            _invoiceRepository.Received(1).Add(invoice);
        }

        [Theory]
        [InlineData(100, 1000, 1000, "2023-11-06", "0123456789", "111", "1", PaymentTerms.COD)]
        [InlineData(100, 1000, 1100, "2023-11-07", "0123456789", "111", "1", PaymentTerms.COD)]
        [InlineData(100, 1000, 1100, "2023-11-07", "", "111", "1", PaymentTerms.COD)]
        [InlineData(100, 1000, 1100, "2023-11-07", "0123456789", "", "1", PaymentTerms.COD)]
        [InlineData(100, 1000, 1100, "2023-11-07", "0123456789", "111", "", PaymentTerms.COD)]
        public void InvoiceAdd_ShouldNotAddInvoice_WhenInputAreInvalid(int taxAmount, int amount, int totalAmount,
            string createdOn, string invoiceNumber, string createdById, string customerId, PaymentTerms paymentTerms)
        {
            // Arrange
            var expectedCreatedOn = DateTime.Parse(createdOn);

            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2023, 11, 6));

            var invoice = _fixture.Build<Invoice>()
                .With(i => i.TaxAmount, taxAmount)
                .With(i => i.Amount, amount)
                .With(i => i.TotalAmount, totalAmount)
                .With(i => i.CreatedOn, expectedCreatedOn)
                .With(i => i.InvoiceNumber, invoiceNumber)
                .With(i => i.CreatedById, createdById)
                .With(i => i.CustomerId, customerId)
                .With(i => i.PaymentTerms, paymentTerms)
                .Create();

            // Act
            Action act = () => _invoiceService.Add(invoice);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}