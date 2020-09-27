﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Settings.Utilities
{
    public partial class UtilitiesCacheController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> ClearCache()
        {
            if (!await _authManager.HasAppPermissionsAsync(Types.AppPermissions.SettingsUtilitiesCache))
            {
                return Unauthorized();
            }

            _cacheManager.Clear();
            await _dbCacheRepository.ClearAsync();
            _cacheManager.Clear();

            return new BoolResult
            {
                Value = true
            };
        }
    }
}