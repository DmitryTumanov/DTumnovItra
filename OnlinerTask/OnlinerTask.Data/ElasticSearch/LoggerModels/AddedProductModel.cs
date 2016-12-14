using System;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.ElasticSearch.LoggerModels
{
    public class AddedProductModel
    {
        public ProductModel ProductModel { get; set; }

        public DateTime AddedTime { get; set; }

        public AddedProductModel(ProductModel productModel, DateTime addedTime)
        {
            AddedTime = addedTime;
            ProductModel = productModel;
        }
    }
}
