using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cakelist.Infrastructure.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly ILogger _log;

        public UserNotificationService(ILogger<UserNotificationService> logger)
        {
            _log = logger;
        }

        public async Task NotifyUserAsync(User user, string subject, string message)
        {

            //TODO: Find a way to notify the user
            _log.LogInformation($"Notify user {user.FullName()}");

            await Task.Run(() => { });
        }
    }
}
