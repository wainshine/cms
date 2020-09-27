﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Enums;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Cms.Templates
{
    public partial class TemplatesPreviewController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<StringResult>> Submit([FromBody] SubmitRequest request)
        {
            if (!await _authManager.HasSitePermissionsAsync(request.SiteId, Types.SitePermissions.TemplatesPreview))
            {
                return Unauthorized();
            }

            var site = await _siteRepository.GetAsync(request.SiteId);
            if (site == null) return NotFound();

            var contentId = 0;
            if (request.TemplateType == TemplateType.ContentTemplate)
            {
                var channel = await _channelRepository.GetAsync(request.ChannelId);
                var count = await _contentRepository.GetCountAsync(site, channel);
                if (count > 0)
                {
                    var tableName = _channelRepository.GetTableName(site, channel);
                    contentId = await _contentRepository.GetFirstContentIdAsync(tableName, request.ChannelId);
                }

                if (contentId == 0)
                {
                    return this.Error("所选栏目下无内容，请选择有内容的栏目");
                }
            }

            _cacheManager.AddOrUpdateSliding(CacheKey, request.Content, 60);

            //var cacheItem = new CacheItem<string>(CacheKey, request.Content, ExpirationMode.Sliding, TimeSpan.FromHours(1));
            //_cacheManager.AddOrUpdate(cacheItem, _ => request.Content);

            var templateInfo = new Template
            {
                TemplateType = request.TemplateType
            };

            await _parseManager.InitAsync(site, request.ChannelId, contentId, templateInfo);

            var parsedContent = await _parseManager.ParseTemplatePreviewAsync(request.Content);

            return new StringResult
            {
                Value = parsedContent
            };
        }
    }
}
