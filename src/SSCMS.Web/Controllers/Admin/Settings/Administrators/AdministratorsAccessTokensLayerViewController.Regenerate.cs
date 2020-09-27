﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Settings.Administrators
{
    public partial class AdministratorsAccessTokensLayerViewController
    {
        [HttpPost, Route(RouteRegenerate)]
        public async Task<ActionResult<RegenerateResult>> Regenerate([FromBody] IdRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(Types.AppPermissions.SettingsAdministratorsAccessTokens))
            {
                return Unauthorized();
            }

            var accessTokenInfo = await _accessTokenRepository.GetAsync(request.Id);

            var accessToken = _settingsManager.Decrypt(await _accessTokenRepository.RegenerateAsync(accessTokenInfo));

            return new RegenerateResult
            {
                AccessToken = accessToken
            };
        }
    }
}
