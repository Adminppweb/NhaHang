using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NhaHang.Infrastructure.Web.SmartTable
{

    public class SmartTableParam
    {
        [AllowNull]
        public Pagination Pagination { get; set; } //= new Pagination();
        [AllowNull]
        public Search Search { get; set; } //= new Search();

        [AllowNull]
        public Sort Sort { get; set; } //= new Sort();

        [AllowNull]
        public Filter Filter { get; set; } //= new Filter();
        [AllowNull]
        public List<Filter> FilterCategorys { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterRatings { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterRices { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterAttributes { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterBrands { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterSizes { get; set; } //= new List<Filter>();
        [AllowNull]
        public List<Filter> FilterColors { get; set; }
        [AllowNull]
        public List<Filter> FilterWarrantyPeriods { get; set; }
        [AllowNull]
        public List<Filter> FilterAttributeGroups { get; set; }
        [AllowNull]
        public Dictionary<string, object> Object { get; set; } //= new Dictionary<string, object>();
        [AllowNull]
        public object objectOther { get; set; } //= new object();

    }

    public class SmartTableParam2
    {
        /// <summary>
        public Search Search { get; set; } = new Search();
        /// </summary>
        //public Pagination Pagination { get; set; } = new Pagination();
        //public Filters Filters { get; set; }= new Filters();
        //public object objectOther { get; set; } = new object();
        ///public string KeyString { get; set; }
        ///public JObject PredicateObject { get; set; } = new JObject();
    }
}
