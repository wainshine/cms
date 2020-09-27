﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesAssetsController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> List([FromQuery] SiteRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, Types.SitePermissions.TemplatesAssets))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return NotFound();

            var directories = new List<Cascade<string>>();
            var files = new List<KeyValuePair<string, string>>();
            await GetDirectoriesAndFilesAsync(directories, files, site, site.TemplatesAssetsIncludeDir, ExtInclude);
            await GetDirectoriesAndFilesAsync(directories, files, site, site.TemplatesAssetsCssDir, ExtCss);
            await GetDirectoriesAndFilesAsync(directories, files, site, site.TemplatesAssetsJsDir, ExtJs);

            var siteUrl = (await _pathManager.GetSiteUrlAsync(site, string.Empty, true)).TrimEnd('/');

            return new GetResult
            {
                Directories = directories,
                Files = files,
                SiteUrl = siteUrl,
                IncludeDir = site.TemplatesAssetsIncludeDir,
                CssDir = site.TemplatesAssetsCssDir,
                JsDir = site.TemplatesAssetsJsDir
            };
        }
    }
}
