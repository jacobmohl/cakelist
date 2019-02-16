using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Entities.CakelistRequestAggregate;

namespace Cakelist.Business.Interfaces
{
    public interface ICakeRequestRepository : IAsyncRepository<CakeRequest>
    {
    }
}
