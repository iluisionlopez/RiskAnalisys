using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Common
{
    public class Constants
    {
        //RiskCategory
        public static readonly Guid RiskCategoryEnvironment = new Guid("05586a25-cd23-4bc0-8954-2bdb02130e51");
        public static readonly Guid RiskCategoryLabourConditions = new Guid("6395ea2e-63a2-4747-866d-08ec530e90b1");
        public static readonly Guid RiskCategoryCorruption = new Guid("989f7ef7-2068-4d4e-98ca-e08bafed60e6");
        public static readonly Guid RiskCategoryHumanRights = new Guid("77155e4f-fe7e-4a7a-bf72-0d7962b77810");

        //IndexType
        public static readonly Guid IndexTypeDemocraticGovernance = new Guid("82d3904e-6312-4d1e-8896-07615f6f2696");
        public static readonly Guid IndexTypeFreedomOfAssembly = new Guid("675a9f85-d029-4b59-a372-188ffb29be0f");
        public static readonly Guid IndexTypeDiscriminationInTheWorkPlace = new Guid("e3cb5b8d-0708-4331-9408-21186cb3edb9");
        public static readonly Guid IndexTypeForcedOrInvoluntaryLabour = new Guid("5b7b70d1-0ae3-4c07-8fa2-409b1c065ba5");
        public static readonly Guid IndexTypeCorruption = new Guid("f734ffd9-2662-4c2d-82ba-55793ec2a6dc");
        public static readonly Guid IndexTypeChildLabour = new Guid("6b31eb77-5067-474b-aacb-6cfce369eb65");
        public static readonly Guid IndexTypeOccupationalHealthAndSafety = new Guid("04fdd836-6ef5-49d7-95a5-847238b0b81b");
        public static readonly Guid IndexTypeFreedomOfOpinionAndExpression = new Guid("b185766d-ef3d-4224-bfb2-86d0d5188303");
        public static readonly Guid IndexTypeMinorityRights = new Guid("82303d82-6b4b-4860-9ed9-975e40fa012a");
        public static readonly Guid IndexTypeLocalEnvironmentalImpact = new Guid("452b2276-bb3e-4f16-b281-a32be6d43235");
        public static readonly Guid IndexTypeTradeCompliance = new Guid("4ea41714-5c4e-48bd-a40f-bb1407555198");
        public static readonly Guid IndexTypeCorporateGovernance = new Guid("c12531d0-8a3b-47c8-ad8a-d1fdb12476bb");
        public static readonly Guid TerritorySweden = new Guid("050c9ad1-84f8-4447-a173-d49ee5e7fd91");
        public static readonly Guid Version1 = new Guid("3dfc829f-5b09-4c8c-b32d-33d059eeb5e6");
        public static readonly Guid AssessmentTypeCustomer = new Guid("a93fcbeb-ca1d-4ee6-8109-e1d9a3b5aab8");
        public static readonly Guid AssessmentTypeSupplier = new Guid("e3fb40ca-e6ca-4bb6-a102-95cf09e45bc8");
        public static readonly Guid Question01 = new Guid("0289ddb0-dcde-48e6-888c-fca4371099a4");

        public static readonly Guid UserAnna = new Guid("2535fbf9-4de7-4563-99e4-9a11cb2a3fb7");
        public static readonly Guid UserAndreas = new Guid("0F3CE48F-496A-465A-A807-1C7FF3B84606");

        public static readonly Guid CSRTool = new Guid("ea90563c-b707-4862-8e97-a737f7405542");
        public static readonly Guid TheFirstCustomerAssessment = new Guid("d6612f00-3e83-4b8b-9fa6-7b464482aaf6");
        public static readonly Guid AndreasFirstCustomerAssessment = new Guid("540A3E1E-BC92-4EA7-AA41-4F07AB3FB5AF");
        public static readonly Guid TheCustomer = new Guid("27770618-C13F-406A-A8C8-930C72814B47");

        public static readonly Guid OfferTypeFinancing = new Guid("bf0e859b-79aa-478c-804c-346c9f57a724");
        public static readonly Guid OfferTypeService = new Guid("21184a33-228f-478f-97bb-7a79e697a636");
        public static readonly Guid OfferTypeProduct = new Guid("ce461b4e-077f-48d6-8c18-ee65e03b5c5c");

        public static readonly Guid TransactionTypePublic = new Guid("4fa0bc7f-5145-41e5-809d-224d20d790a6");
        public static readonly Guid TransactionTypeAgent = new Guid("88496458-3c4c-4391-93b9-5750c880746f");
        public static readonly Guid TransactionTypeDefense = new Guid("3aa5c5be-7114-4659-be64-9fd451650860");
        public static readonly Guid TransactionTypeDirect = new Guid("eca9bb38-2c89-4cd9-a083-b417553d6909");

        public static readonly Guid WebScan1 = new Guid("61342231-14FA-4977-82B0-4B3FC931AD39");
        public static readonly Guid WebScan2 = new Guid("89C2DB94-9E95-4CB4-9441-E5003AAF8B85");

        public static readonly Guid TerritoryGerman = new Guid("41AC06D6-17AE-4802-8A70-15DBD3A42D8E");

        public static readonly Guid TheSupplier = new Guid("C371F4B9-C61C-4994-B164-07B4BEB6B5BD");
    }
}
