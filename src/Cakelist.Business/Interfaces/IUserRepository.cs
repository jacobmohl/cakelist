using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Entities;

namespace Cakelist.Business.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
