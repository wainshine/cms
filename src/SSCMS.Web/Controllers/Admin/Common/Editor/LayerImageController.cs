﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Dto;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Common.Editor
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class LayerImageController : ControllerBase
    {
        private const string Route = "common/editor/layerImage";
        private const string RouteUpload = "common/editor/layerImage/actions/upload";

        private readonly IPathManager _pathManager;
        private readonly ISiteRepository _siteRepository;

        public LayerImageController(IPathManager pathManager, ISiteRepository siteRepository)
        {
            _pathManager = pathManager;
            _siteRepository = siteRepository;
        }

        public class SubmitRequest : SiteRequest
        {
            public bool IsThumb { get; set; }
            public int ThumbWidth { get; set; }
            public int ThumbHeight { get; set; }
            public bool IsLinkToOriginal { get; set; }
            public List<string> FilePaths { get; set; }
        }

        public class UploadResult
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string Url { get; set; }
        }

        public class SubmitResult
        {
            public string ImageUrl { get; set; }
            public string PreviewUrl { get; set; }
        }
    }
}
