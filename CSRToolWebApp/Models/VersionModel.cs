using CSRToolWebApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Version = CSRToolWebApp.DataContracts.Base.Version;

namespace CSRToolWebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionModel
    {
        #region Properties
        public Guid VersionID { get; set; }
        public string Name { get; set; }
        public IRestService Service { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public VersionModel()
        {
            Service = new RestService("versions");    
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        internal static IEnumerable<SelectListItem> GetAssessmentVersion(IRestService service)
        {
            var json = service.Get(new List<KeyValuePair<string, string>>());
            var dataContractIndex = JsonConvert.DeserializeObject<List<Version>>(json);
            var selectList =
                dataContractIndex.Select(x => new SelectListItem()
                {
                    Text = x.Name.ToString(),
                    Value = x.Id.ToString()
                }).ToList();

            return selectList;
        }
        #endregion

    }
}