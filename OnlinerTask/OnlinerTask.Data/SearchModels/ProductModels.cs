using System;

namespace OnlinerTask.DAL.SearchModels
{
    public class ProductModel
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public Image images { get; set; }
        public string description { get; set; }
        public string html_url { get; set; }
        public Review reviews { get; set; }
        public string review_url { get; set; }
        public Price prices { get; set; }
    }

    public class Image
    {
        public string header { get; set; }
    }

    public class Review
    {
        public int rating { get; set; }
        public int count { get; set; }
        public string html_url { get; set; }
    }

    public class Price
    {
        public PriceAmmount price_min { get; set; }
        public PriceAmmount price_max { get; set; }
        public string html_url { get; set; }
        public Offers offers { get; set; }
    }

    public class Offers
    {
        public int count { get; set; }
    }

    public class PriceAmmount
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
}