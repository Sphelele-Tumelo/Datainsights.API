
using DataInsights.API.Controllers;
using DataInsights.API.DTOs;
using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DataInsights.Tests.ControllerTests
{
    public class ReportControllerTest
    {
        [Fact]

        public async Task GetMonthlySalesReport_ReturnsOK_WithReportAswell()
        {
            //Arrange 
            var mockService = new Mock<IReportService>();
            var fakeReport = new MonthlySalesReportDTO
            {
                Month = "September 2025",
                TotalSales = 10000,
                TotalOrders = 5,
                TopProducts = new List<ProductSalesDTO> {
                 new ProductSalesDTO {ProductName = "Nike air max", QuantitySold = 3, Revenue = 5000},
                 new ProductSalesDTO {ProductName = "Nocta", QuantitySold = 5, Revenue = 60000}
                }
            };
            mockService.Setup(service => service.GetMonthlySales(9, 2025))
                .ReturnsAsync(fakeReport);

            var controller = new ReportController(mockService.Object);

            //Act
            var result = await controller.GetMonthlySalesReport(9, 2025) as OkObjectResult;

            //Assert

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var returnedReport = Assert.IsType<MonthlySalesReportDTO>(result.Value);
            Assert.Equal("September 2025", returnedReport.Month);
            Assert.Equal(10000, returnedReport.TotalSales);
            Assert.Equal(2, returnedReport?.TopProducts?.Count);

        }
    }
}
