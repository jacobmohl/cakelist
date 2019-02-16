using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Entities.CakelistRequestAggregate;

namespace Cakelist.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(CakelistContext cakelistContext)
        {
            cakelistContext.Database.EnsureCreated();

            // Seed user data
            if (cakelistContext.Users.Any()) 
            {
                return; // Database has already Users
            }

            var users = new User[]
            {
                new User { Id = 1, FirstName = "Tony", LastName = "Stark", Email = "tony@avengers.com" },
                new User { Id = 2, FirstName = "Steven", LastName = "Rogers", Email = "steven@avengers.com" },
                new User { Id = 3, FirstName = "Bruce", LastName = "Banner", Email = "bruce@avengers.com"}
            };

            foreach ( User user in users) 
            {
                cakelistContext.Users.Add(user);
            }

            cakelistContext.SaveChanges();

            // Seed request data

            if(cakelistContext.Requests.Any()) 
            {
                return;
            };

            var request = new CakeRequest { Id = 1, CreatedBy = users[0], AssignedTo = users[1], Reason = "New shield" };
            request.AddVote(users[2]);
            cakelistContext.Requests.Add(request);

            cakelistContext.SaveChanges();


        }      
    }
}
