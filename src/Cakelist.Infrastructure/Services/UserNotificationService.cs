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
        private readonly ILogger _logger;

        public UserNotificationService(ILogger<UserNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task NotifyUserAsync(User user, string subject, string message)
        {

            //TODO: Find a way to notify the user
            _logger.LogInformation($"Notify user {user.FullName()}");

            await Task.Run(() => { });
        }
    }
}
