using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.WebScanType;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class WebScanTypeService : IWebScanTypeService
    {
        private readonly IWebScanTypeRepository _webScanTypeRepository;

        public WebScanTypeService(IWebScanTypeRepository WebScanTypeRepository)
        {
            _webScanTypeRepository = WebScanTypeRepository;
        }

        public List<WebScanType> GetWebScanTypes()
        {
            return _webScanTypeRepository.GetWebScanTypes();
        }


    }

}