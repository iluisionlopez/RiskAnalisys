using System;
using System.Collections.Generic;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using Version = CSRTool.Core.Version;


namespace CSRTool.Dependencies.ApplicationServices
{
    public class VersionService : IVersionService
    {
        private readonly IVersionRepository _versionRepository;

        public VersionService(IVersionRepository versionRepository)
        {
            _versionRepository = versionRepository;
        }

        public List<Version> GetVersions()
        {
            return _versionRepository.GetVersions();
        }


    }

}