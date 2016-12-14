using System.Data.Entity;
using System.Data.Entity.SqlServer;
using AutoMapper;
using AutoMapper.Mappers;
using CSRTool.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityFramework = CSRTool.Dependencies.Repositories.EntityFramework;


namespace CSRTool.IntegrationTest
{
    public abstract class TestBase
    {
        private IUnityContainer _unityContainer = new UnityContainer().LoadConfiguration();
        private DbContext _dbContext;
        static TestBase()
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
            var ensureAutoMapperCopied = new ListSourceMapper();

        }

        public IUnityContainer UnityContainerForTest {
            get { return _unityContainer; }
        }

        public DbContext DataContext {
            get { return _dbContext; }
        }

        
        
        [TestInitialize]
        public void Setup()
        {
            //Clear mapping
            Mapper.Reset();
            //Create global mapping
            CreateMappingForDataRows();
            CreateMapping_EFToCore();
            CreateMapping_EF_ForUser();
            CreateMapping_Core_EF();

            //Register DbContext if not there already
            if (!_unityContainer.IsRegistered<DbContext>())
            {
                _unityContainer.RegisterType<DbContext>(new PerThreadLifetimeManager(),
                    new InjectionConstructor("CSRToolContext"));                
            }

            //Create a dbcontext 
            _dbContext = _unityContainer.Resolve<DbContext>();

            

        }

        [TestCleanup]
        public void CleanUp()
        {
            if (_dbContext != null)
            {
                _dbContext.Database.Connection.Close();
                _dbContext.Dispose();
            }
        }


        private void CreateMappingForDataRows()
        {
            Mapper.CreateMap<Territory, CSRToolWebApp.DataContracts.Base.Territory>();
            Mapper.CreateMap<Index, CSRToolWebApp.DataContracts.Base.Index>();
            Mapper.CreateMap<IndexType, CSRToolWebApp.DataContracts.Base.IndexType>();
            Mapper.CreateMap<Version, CSRToolWebApp.DataContracts.Base.Version>();
            Mapper.CreateMap<RiskCategory, CSRToolWebApp.DataContracts.Base.RiskCategory>();
            Mapper.CreateMap<Sector, CSRToolWebApp.DataContracts.Base.Sector>();
            Mapper.CreateMap<Question, CSRToolWebApp.DataContracts.Base.Question>();
            Mapper.CreateMap<Answer, CSRToolWebApp.DataContracts.Base.Answer>();
            Mapper.CreateMap<TransactionType, CSRToolWebApp.DataContracts.Base.TransactionType>();
            Mapper.CreateMap<CaseType, CSRToolWebApp.DataContracts.Base.CaseType>();
            Mapper.CreateMap<OfferType, CSRToolWebApp.DataContracts.Base.OfferType>();
            Mapper.CreateMap<AssessmentCustomer, CSRToolWebApp.DataContracts.Base.AssessmentCustomer>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                .ForMember(dest => dest.ChangedByName, opt => opt.MapFrom(src => src.ChangedByUser.UserName));
            Mapper.CreateMap<User, CSRToolWebApp.DataContracts.Base.User>();
            Mapper.CreateMap<AssessmentOfferType, CSRToolWebApp.DataContracts.Base.AssessmentOfferType>();
            Mapper.CreateMap<AssessmentTransactionType, CSRToolWebApp.DataContracts.Base.AssessmentTransactionType>();
            Mapper.CreateMap<WebScan, CSRToolWebApp.DataContracts.Base.WebScan>();
            Mapper.CreateMap<AssessmentSupplier, CSRToolWebApp.DataContracts.Base.AssessmentSupplier>();
            Mapper.CreateMap<AssessmentCustomerQuestionAnswer, CSRToolWebApp.DataContracts.Base.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<AssessmentSupplierQuestionAnswer, CSRToolWebApp.DataContracts.Base.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<Supplier, CSRToolWebApp.DataContracts.Base.Supplier>();
            Mapper.CreateMap<SupplyChain, CSRToolWebApp.DataContracts.Base.SupplyChain>();
        }


        private void CreateMapping_EFToCore()
        {
            Mapper.CreateMap<EntityFramework.TerritoryType, TerritoryType>();
            Mapper.CreateMap<EntityFramework.Territory, Territory>();
            Mapper.CreateMap<EntityFramework.Index, Index>();
            Mapper.CreateMap<EntityFramework.IndexType, IndexType>();
            Mapper.CreateMap<EntityFramework.Version, Version>();
            Mapper.CreateMap<EntityFramework.RiskCategory, RiskCategory>();
            Mapper.CreateMap<EntityFramework.Sector, Sector>();
            Mapper.CreateMap<EntityFramework.AssessmentType, AssessmentType>();
            Mapper.CreateMap<EntityFramework.AssessmentTypeQuestion, AssessmentTypeQuestion>();
            Mapper.CreateMap<EntityFramework.Question, Question>();
            Mapper.CreateMap<EntityFramework.Answer, Answer>();
            Mapper.CreateMap<EntityFramework.Customer, Customer>();
            Mapper.CreateMap<EntityFramework.QuestionAnswer, QuestionAnswer>();
            Mapper.CreateMap<EntityFramework.TransactionType, TransactionType>();
            Mapper.CreateMap<EntityFramework.CaseType, CaseType>();
            Mapper.CreateMap<EntityFramework.OfferType, OfferType>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomer, AssessmentCustomer>();
            Mapper.CreateMap<EntityFramework.User, User>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomerWebScan, WebScan>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplierWebScan, WebScan>();
            Mapper.CreateMap<EntityFramework.AssessmentOfferType, AssessmentOfferType>();
            Mapper.CreateMap<EntityFramework.AssessmentTransactionType, AssessmentTransactionType>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomerWebScan, WebScan>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplier, AssessmentSupplier>();
            Mapper.CreateMap<EntityFramework.AssessmentCustomerQuestionAnswer, AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<EntityFramework.AssessmentSupplierQuestionAnswer, AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<EntityFramework.SupplyChain, SupplyChain>();
            Mapper.CreateMap<EntityFramework.Supplier, Supplier>();
        }

        private void CreateMapping_Core_EF()
        {
            Mapper.CreateMap<Territory, EntityFramework.Territory>();
            Mapper.CreateMap<Index, EntityFramework.Index>();
            Mapper.CreateMap<IndexType, EntityFramework.IndexType>();
            Mapper.CreateMap<Version, EntityFramework.Version>();
            Mapper.CreateMap<RiskCategory, EntityFramework.RiskCategory>();
            Mapper.CreateMap<Sector, EntityFramework.Sector>();
            Mapper.CreateMap<AssessmentType, EntityFramework.AssessmentType>();
            Mapper.CreateMap<AssessmentTypeQuestion, EntityFramework.AssessmentTypeQuestion>();
            Mapper.CreateMap<Question, EntityFramework.Question>();
            Mapper.CreateMap<QuestionAnswer, EntityFramework.QuestionAnswer>();
            Mapper.CreateMap<Answer, EntityFramework.Answer>();
            Mapper.CreateMap<Customer, EntityFramework.Customer>();
            Mapper.CreateMap<TransactionType, EntityFramework.TransactionType>();
            Mapper.CreateMap<CaseType, EntityFramework.CaseType>();
            Mapper.CreateMap<OfferType, EntityFramework.OfferType>();
            Mapper.CreateMap<AssessmentCustomer, EntityFramework.AssessmentCustomer>();
            Mapper.CreateMap<WebScan, EntityFramework.AssessmentCustomerWebScan>();
            Mapper.CreateMap<AssessmentOfferType, EntityFramework.AssessmentOfferType>();
            Mapper.CreateMap<AssessmentTransactionType, EntityFramework.AssessmentTransactionType>();
            Mapper.CreateMap<WebScan, EntityFramework.AssessmentCustomerWebScan>();
            Mapper.CreateMap<WebScan, EntityFramework.AssessmentSupplierWebScan>();
            Mapper.CreateMap<AssessmentSupplier, EntityFramework.AssessmentSupplier>();
            Mapper.CreateMap<AssessmentCustomerQuestionAnswer, EntityFramework.AssessmentCustomerQuestionAnswer>();
            Mapper.CreateMap<AssessmentSupplierQuestionAnswer, EntityFramework.AssessmentSupplierQuestionAnswer>();
            Mapper.CreateMap<Supplier, EntityFramework.Supplier>();
            Mapper.CreateMap<SupplyChain, EntityFramework.SupplyChain>();
        }

        private void CreateMapping_EF_ForUser()
        {
            
        }
    }
}
