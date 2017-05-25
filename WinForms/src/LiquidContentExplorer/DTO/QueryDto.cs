using System;
using System.Web;
using Newtonsoft.Json;

namespace LiquidContentExplorer.DTO
{
    [JsonObject]
    public class QueryDto
    {
        public string SearchText { get; set; }
        public int? StartIndex { get; set; }
        public int? MaxItems { get; set; }
        public string FieldOrder { get; set; }
        public bool OrderAsc { get; set; } = true;
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        public override string ToString()
        {
            return $"SearchText: {SearchText}, StartIndex: {StartIndex}, MaxItems: {MaxItems}, " +
                   $"FieldOrder: {FieldOrder}, OrderAsc: {OrderAsc}, CreatedFrom: {CreatedFrom}" +
                   $"CreatedTo: {CreatedTo}";
        }

        public string ToQueryString()
        {
            return "?" + string.Join("&", 
                ConcatPair("SearchText", SearchText), 
                ConcatPair("StartIndex", StartIndex), 
                ConcatPair("MaxItems", MaxItems), 
                ConcatPair("OrderAsc", OrderAsc ? "true" : "false"), 
                ConcatPair("CreatedFrom", CreatedFrom?.ToString("yyyy-MM-dd") ?? ""), 
                ConcatPair("CreatedTo", CreatedTo?.ToString("yyyy-MM-dd") ?? ""));
        }

        private static string ConcatPair(string name, object value)
        {
            return name + "=" + HttpUtility.UrlEncode((value ?? "").ToString());
        }
    }
}