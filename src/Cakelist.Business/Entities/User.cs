using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Entities.CakelistRequestAggregate;

namespace Cakelist.Business.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        //public List<CakeRequest> AssignedCakeRequests { get; set; }
        //public List<CakeRequest> CreatedCakeRequests { get; set; }
        //public List<CakeVote> Votes { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
