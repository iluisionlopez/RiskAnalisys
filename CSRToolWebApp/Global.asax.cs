using CSRToolWebApp.Common;
using System;
using AutoMapper;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using CSRToolWebApp.Models;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using EntityFramework = CSRTool.Dependencies.Repositories.EntityFramework;
using Core = CSRTool.Core;
using DataContract = CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Common.ModelBinders;

namespace CSRToolWebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Logging.LogInfo("Application started");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.DefaultBinder = new JsonModelBinder();

            #region Initialize Unity Containter

            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.LoadConfiguration();
            unityContainer.RegisterType<DbContext>(new PerRequestLifetimeManager(),
                new InjectionConstructor("CSRToolContext"));

            Application["unityContainer"] = unityContainer;
            #endregion Initialize Unity Containter

            #region Initialize Automapper mappings

            CreateMapping_CoreToDataContract();
            CreateMapping_DataContractToCore();

            CreateMapping_ModelToDataContract();
            CreateMapping_DataContractToModel();

            CreateMapping_EFToCore();
            CreateMapping_CoreToEF();

            #endregion Initialize Automapper mappings
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {


        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {


        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Application["unityContainer"] = null;
        }


        private void CreateMapping_CoreToDataContract()
        {
            Mapper.CreateMap<Core.Territory, DataContract.Territory>();
            Mapper.CreateMap<Core.Index, DataContract.Index>();
            Mapper.CreateMap<Core.IndexType, DataContract.IndexType>();
            Mapper.CreateMap<Core.Version, DataContract.Version>();
            Mapper.CreateMap<Core.RiskCategory, DataContract.RiskCategory>();
            Mapper.CreateMap<Core.Sector, DataContract.Sector>();
            Mapper.CreateMap<Core.Question, DataContract.Question>();
            Mapper.CreateMap<Core.Answer, DataContract.Answer>();
            Mapper.CreateMap<Core.AssessmentTypeQuestion, DataContract.AssessmentTypeQuestion>();
            Mapper.CreateMap<Core.AssessmentType, DataContract.AssessmentType>();
            Mapper.CreateMap<Core.AssessmentCustomer, DataContract.AssessmentCustomer>();
            Mapper.CreateMap<Core.TransactionType, DataContract.TransactionType>();
            Mapper.CreateMap<Core.Customer, DataContract.Customer>();
            Mapper.CreateMap<Core.OfferType, DataContract.OfferType>();
            Mapper.CreateMap<Core.AssessmentCustomer, DataContract.AssessmentCustomer>();
            Mapper.CreateMap<Core.User, DataContract.User>();
            Mapper.CreateMap<Core.CaseType, DataContract.CaseType>();
            Mapper.CreateMap<Core.AssessmentOfferType, DataContract.AssessmentOfferType>();
            Mapper.CreateMap<Core.AssessmentTransactionType, DataContract.AssessmentTransactionType>();
            Mapper.CreateMap<Core.WebScanType, DataContract.WebScanType>();
            Mapper.CreateMap<Core.WebScan, DataContract.WebScan>();
            Mapper.CreateMap<Core.AssessmentSupplier, DataContract.AssessmentSupplier>();
            Mapper.CreateMap<Core.QuestionAnswer, DataContract.QuestionAnswer>();
            Mapper.CreateMap<Core.AssessmentCustomerQuestionAnswer, DataContract.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<Core.AssessmentSupplierQuestionAnswer, DataContract.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<Core.Supplier, DataContract.Supplier>();
            Mapper.CreateMap<Core.SupplyChain, DataContract.SupplyChain>();
        }

        private void CreateMapping_DataContractToCore()
        {
            Mapper.CreateMap<DataContract.Territory, Core.Territory>();
            Mapper.CreateMap<DataContract.Index, Core.Index>();
            Mapper.CreateMap<DataContract.IndexType, Core.IndexType>();
            Mapper.CreateMap<DataContract.Version, Core.Version>();
            Mapper.CreateMap<DataContract.RiskCategory, Core.RiskCategory>();
            Mapper.CreateMap<DataContract.Sector, Core.Sector>();
            Mapper.CreateMap<DataContract.Question, Core.Question>();
            Mapper.CreateMap<DataContract.Answer, Core.Answer>();
            Mapper.CreateMap<DataContract.AssessmentTypeQuestion, Core.AssessmentTypeQuestion>();
            Mapper.CreateMap<DataContract.AssessmentType, Core.AssessmentType>();
            Mapper.CreateMap<DataContract.AssessmentCustomer, Core.AssessmentCustomer>();
            Mapper.CreateMap<DataContract.TransactionType, Core.TransactionType>();
            Mapper.CreateMap<DataContract.CaseType, Core.CaseType>();
            Mapper.CreateMap<DataContract.Customer, Core.Customer>();
            Mapper.CreateMap<DataContract.OfferType, Core.OfferType>();
            Mapper.CreateMap<DataContract.AssessmentCustomer, Core.AssessmentCustomer>();
            Mapper.CreateMap<DataContract.User, Core.User>();
            Mapper.CreateMap<DataContract.CaseType, Core.CaseType>();
            Mapper.CreateMap<DataContract.AssessmentOfferType, Core.AssessmentOfferType>();
            Mapper.CreateMap<DataContract.AssessmentTransactionType, Core.AssessmentTransactionType>();
            Mapper.CreateMap<DataContract.WebScanType, Core.WebScanType>();
            Mapper.CreateMap<DataContract.WebScan, Core.WebScan>();
            Mapper.CreateMap<DataContract.AssessmentSupplier, Core.AssessmentSupplier>();
            Mapper.CreateMap<DataContract.QuestionAnswer, Core.QuestionAnswer>();
            Mapper.CreateMap<DataContract.AssessmentCustomerQuestionAnswer, Core.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<DataContract.AssessmentSupplierQuestionAnswer, Core.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<DataContract.Supplier, Core.Supplier>();
            Mapper.CreateMap<DataContract.SupplyChain, Core.SupplyChain>();
        }


        #region Mappings_CoreEF
        private void CreateMapping_EFToCore()
        {
            Mapper.CreateMap<EntityFramework.Territory, Core.Territory>();
            Mapper.CreateMap<EntityFramework.TransactionType, Core.TransactionType>();
            Mapper.CreateMap<EntityFramework.Index, Core.Index>();
            Mapper.CreateMap<EntityFramework.IndexType, Core.IndexType>();
            Mapper.CreateMap<EntityFramework.Version, Core.Version>();
            Mapper.CreateMap<EntityFramework.RiskCategory, Core.RiskCategory>();
            Mapper.CreateMap<EntityFramework.Sector, Core.Sector>();
            Mapper.CreateMap<EntityFramework.Question, Core.Question>();
            Mapper.CreateMap<EntityFramework.Answer, Core.Answer>();
            Mapper.CreateMap<EntityFramework.AssessmentType, Core.AssessmentType>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomer, Core.AssessmentCustomer>();
            Mapper.CreateMap<EntityFramework.TransactionType, Core.TransactionType>();
            Mapper.CreateMap<EntityFramework.AssessmentTypeQuestion, Core.AssessmentTypeQuestion>();
            Mapper.CreateMap<EntityFramework.OfferType, Core.OfferType>();
            Mapper.CreateMap<EntityFramework.Customer, Core.Customer>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomer, Core.AssessmentCustomer>();
            Mapper.CreateMap<EntityFramework.User, Core.User>();
            Mapper.CreateMap<EntityFramework.CaseType, Core.CaseType>();
            Mapper.CreateMap<EntityFramework.AssessmentOfferType, Core.AssessmentOfferType>();
            Mapper.CreateMap<EntityFramework.AssessmentTransactionType, Core.AssessmentTransactionType>();
            Mapper.CreateMap<EntityFramework.WebScanType, Core.WebScanType>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomerWebScan, Core.WebScan>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplierWebScan, Core.WebScan>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplier, Core.AssessmentSupplier>();
            Mapper.CreateMap<EntityFramework.QuestionAnswer, Core.QuestionAnswer>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomerQuestionAnswer, Core.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplierQuestionAnswer, Core.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<EntityFramework.Supplier, Core.Supplier>();
            Mapper.CreateMap<EntityFramework.SupplyChain, Core.SupplyChain>();
        }

        private void CreateMapping_CoreToEF()
        {
            Mapper.CreateMap<Core.Territory, EntityFramework.Territory>();
            Mapper.CreateMap<Core.TransactionType, EntityFramework.TransactionType>();
            Mapper.CreateMap<Core.Index, EntityFramework.Index>();
            Mapper.CreateMap<Core.IndexType, EntityFramework.IndexType>();
            Mapper.CreateMap<Core.Version, EntityFramework.Version>();
            Mapper.CreateMap<Core.RiskCategory, EntityFramework.RiskCategory>();
            Mapper.CreateMap<Core.Sector, EntityFramework.Sector>();
            Mapper.CreateMap<Core.Question, EntityFramework.Question>();
            Mapper.CreateMap<Core.Answer, EntityFramework.Answer>();
            Mapper.CreateMap<Core.AssessmentType, EntityFramework.AssessmentType>();
            Mapper.CreateMap<Core.TransactionType, EntityFramework.TransactionType>();
            Mapper.CreateMap<Core.AssessmentTypeQuestion, EntityFramework.AssessmentTypeQuestion>();
            Mapper.CreateMap<Core.CaseType, EntityFramework.CaseType>();
            Mapper.CreateMap<Core.Customer, EntityFramework.Customer>();
            Mapper.CreateMap<Core.AssessmentCustomer, EntityFramework.AssessmentCustomer>();
            Mapper.CreateMap<Core.User, EntityFramework.User>();
            Mapper.CreateMap<Core.AssessmentOfferType, EntityFramework.AssessmentOfferType>();
            Mapper.CreateMap<Core.AssessmentTransactionType, EntityFramework.AssessmentTransactionType>();
            Mapper.CreateMap<Core.WebScanType, EntityFramework.WebScanType>();
            Mapper.CreateMap<Core.WebScan, EntityFramework.AssessmentCustomerWebScan>();
            Mapper.CreateMap<Core.WebScan, EntityFramework.AssessmentSupplierWebScan>();
            Mapper.CreateMap<Core.AssessmentSupplier, EntityFramework.AssessmentSupplier>();
            Mapper.CreateMap<Core.QuestionAnswer, EntityFramework.QuestionAnswer>();
            Mapper.CreateMap<Core.AssessmentCustomerQuestionAnswer, EntityFramework.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<Core.AssessmentSupplierQuestionAnswer, EntityFramework.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<Core.Supplier, EntityFramework.Supplier>();
            Mapper.CreateMap<Core.SupplyChain, EntityFramework.SupplyChain>();
        }
        #endregion

        #region Mappings_DataContractModel
        private void CreateMapping_DataContractToModel()
        {
            //Mapper.CreateMap<Territory, GridMvc.Grid<Territory>>();
            Mapper.CreateMap<DataContract.Territory, TerritoryModel>();
            Mapper.CreateMap<DataContract.Index, IndexModel>();
            Mapper.CreateMap<DataContract.User, UserModel>();
        }

        private void CreateMapping_ModelToDataContract()
        {
            //Mapper.CreateMap<GridMvc.Grid<Territory>, Territory>();
            Mapper.CreateMap<TerritoryModel, DataContract.Territory>();
            Mapper.CreateMap<IndexModel, DataContract.Index>();
            Mapper.CreateMap<UserModel, DataContract.User>();
        }
        #endregion


    }
}
