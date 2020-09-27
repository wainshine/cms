﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Models;

namespace SSCMS.Web.Controllers.Admin.Settings.Logs
{
    public partial class LogsUserController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<PageResult<Log>>> Get([FromBody] SearchRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(Types.AppPermissions.SettingsLogsUser))
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            var userId = user?.Id ?? 0;

            var count = await _logRepository.GetUserLogsCountAsync(userId, request.Keyword, request.DateFrom, request.DateTo);
            var logs = await _logRepository.GetUserLogsAsync(userId, request.Keyword, request.DateFrom, request.DateTo, request.Offset, request.Limit);

            foreach (var log in logs)
            {
                var userName = await _userRepository.GetDisplayAsync(log.UserId);
                log.Set("userName", userName);
            }

            return new PageResult<Log>
            {
                Items = logs,
                Count = count
            };
        }
    }
}
