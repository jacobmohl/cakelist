using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Entities.CakelistRequestAggregate;

namespace Cakelist.Business.Interfaces
{
    interface ICakeRequestRepository : IAsyncRepository<CakeRequest>
    {
    }
}
