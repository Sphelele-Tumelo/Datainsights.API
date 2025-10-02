

using DataInsights.API.Controllers;
using DataInsights.API.DTOs;
using DataInsights.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DataInsights.Tests.ControllerTests
{
    public class SalesControllerTest
    {
        [Fact]

        public async Task GetSales_ReturnOk_IfAdded()
        {
            //Arrange 
            var mockService = new Mock<ISalesService>();
            var fakeSale = new Sale_DTO
            {
               Sales_ID = 1,
                Customer_ID = 1,
                Quantity = 5,
                
            };
            mockService.Setup(service => service.GetSaleByIdAsync(1))
                .ReturnsAsync(fakeSale);


            var controller = new SalesController(mockService.Object);

            //Act

            var result = await controller.GetSalesById(1) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var returnedSale = Assert.IsType<Sale_DTO>(result.Value);
            Assert.Equal(1, returnedSale.Sales_ID);
            Assert.Equal(1, returnedSale.Customer_ID);
        }


        [Fact]
        public async Task GetAllSales_Return_A_List_If_Found()
        {

            //Arrange

            var mockService = new Mock<ISalesService>();

            var listOfSales = new List<Sale_DTO>
            {
                new Sale_DTO { Sales_ID = 1 , Customer_ID = 1, Quantity = 2 },
                new Sale_DTO { Sales_ID = 2 , Customer_ID = 2, Quantity = 4 },
                new Sale_DTO { Sales_ID = 3 , Customer_ID = 3, Quantity = 6 }
            };

            mockService.Setup(service => service.GetAllSalesAsync()).ReturnsAsync(listOfSales);

            var controller = new SalesController(mockService.Object);

            //Act
            var result = await controller.GetAllSales();


            //Assert

            Assert.NotNull(result);


            var returnedReport = Assert.IsType<Sale_DTO>(result.Value);
            Assert.Equal(1, returnedReport.Sales_ID);
            Assert.Equal(1, returnedReport.Customer_ID);
            Assert.Equal(2, returnedReport.Quantity);
        }
    }
}
