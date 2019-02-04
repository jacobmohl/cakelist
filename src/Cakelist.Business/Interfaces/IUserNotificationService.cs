using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities;

namespace Cakelist.Business.Interfaces
{
    interface IUserNotificationService
    {
        Task NotifyUserAsync(User user, string subject, string message);
    }
}
