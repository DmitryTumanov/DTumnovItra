using System;
using OnlinerTask.Data.DataBaseModels;

namespace OnlinerTask.Data.ElasticSearch.LoggerModels
{
    public class RemovedProduct
    {
        public Product Product { get; set; }

        public DateTime RemovedTime { get; set; }

        public RemovedProduct(Product product, DateTime removedTime)
        {
            RemovedTime = removedTime;
            Product = product;
        }
    }
}
