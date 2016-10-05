using System.Collections.Generic;

namespace OnlinerTask.Models
{
    public class SearchResult
    {
        public List<ProductModel> products { get; set; }
    }
    public class Responce
    {
        public string SearchString { get; set; }
    }
}