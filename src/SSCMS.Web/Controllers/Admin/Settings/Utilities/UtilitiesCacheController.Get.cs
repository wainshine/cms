﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;

namespace SSCMS.Web.Controllers.Admin.Settings.Utilities
{
    public partial class UtilitiesCacheController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            if (!await _authManager.HasAppPermissionsAsync(Types.AppPermissions.SettingsUtilitiesCache))
            {
                return Unauthorized();
            }

            return new GetResult
            {
                Configuration = _cacheManager.Configuration
            };
        }
    }
}