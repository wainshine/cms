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
    public partial class LayerWordController : ControllerBase
    {
        private const string Route = "common/editor/layerWord";
        private const string RouteUpload = "common/editor/layerWord/actions/upload";

        private readonly IPathManager _pathManager;
        private readonly ISiteRepository _siteRepository;

        public LayerWordController(IPathManager pathManager, ISiteRepository siteRepository)
        {
            _pathManager = pathManager;
            _siteRepository = siteRepository;
        }

        public class SubmitRequest : SiteRequest
        {
            public bool IsClearFormat { get; set; }
            public bool IsFirstLineIndent { get; set; }
            public bool IsClearFontSize { get; set; }
            public bool IsClearFontFamily { get; set; }
            public bool IsClearImages { get; set; }
            public List<NameTitle> Files { get; set; }
        }

        public class NameTitle
        {
            public string FileName { get; set; }
            public string Title { get; set; }
        }
    }
}
