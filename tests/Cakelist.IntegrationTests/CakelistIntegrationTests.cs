using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cakelist.Business.Entities;
using Cakelist.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cakelist.IntegrationTests
{
    public class CakelistIntegrationTests
    {
        [Fact]
        public void CreateRequestToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CakelistContext>()
                .UseInMemoryDatabase(databaseName: "CakelistTestDB")
                .Options;

            // Act
            using (var context = new CakelistContext(options)) 
            {
                context.Users.Add(new User() {
                    Id = 1,
                    FirstName = "Tony",
                    LastName = "Stark",
                    Email = "tony@avengers.com"
                });

                context.SaveChangesAsync();
            }

            // Assert
            using (var context = new CakelistContext(options)) 
            {
                Assert.Equal(1, context.Users.Count());
                Assert.Equal("Tony", context.Users.Single().FirstName);

            }


        }
    }
}
