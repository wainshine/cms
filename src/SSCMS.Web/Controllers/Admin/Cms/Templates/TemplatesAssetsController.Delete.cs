﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesAssetsController
    {
        [HttpDelete, Route(Route)]
        public async Task<ActionResult<BoolResult>> Delete([FromBody] FileRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, Types.SitePermissions.TemplatesAssets))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return NotFound();

            FileUtils.DeleteFileIfExists(await _pathManager.GetSitePathAsync(site, request.DirectoryPath, request.FileName));
            await _authManager.AddSiteLogAsync(request.SiteId, "删除资源文件", $"{request.DirectoryPath}:{request.FileName}");

            return new BoolResult
            {
                Value = true
            };
        }
    }
}
