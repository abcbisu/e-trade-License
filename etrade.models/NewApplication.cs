using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade.models
{
    class NewApplication : TranslatedData<NewApplication_Trans>
    {
        public int AuthorityId { get; set; }
        public string Description { get; set; }
        public int RegionId { get; set; }
        public int WardId { get; set; }
        public int LocationId { get; set; }
        public int MarketId { get; set; }
        public string FloorNo { get; set; }
        public string ShopNo { get; set; }
        public string NatureOfBusiness { get; set; }
        public int BusinessTypeId { get; set; }
        public int BusinessSubTypeId { get; set; }
        public int SignboardLength { get; set; }
        public int SignboardWidth { get; set; }
        public bool IsIndustry { get; set; }
        public bool ChemicalsOrExplosive { get; set; }
        public string PlotType { get; set; }
        public string PlotSubType { get; set; }
        public string BusinessPlace { get; set; }
        public string BusinessActivity { get; set; }
        public int DocTypeId { get; set; }
        public int DocNameId { get; set; }
        public List<File> DocUpload { get; set; }
    }
    class File
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Value { get; set; }
    }
    class NewApplication_Trans
    {
        public string TradeName { get; set; }
        public string HoldingNo { get; set; }
        public string Address { get; set; }
        public string Road { get; set; }
    }
}
