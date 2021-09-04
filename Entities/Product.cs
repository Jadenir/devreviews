using System;
using System.Collections.Generic;

namespace DevReviews.API.Entities
{
    public class Product
    {
        public Product(string title, string desciption, decimal price)
        {
            Title = title;
            Desciption = desciption;
            Price = price;

            RegisteredAt = DateTime.Now;
            Reviews = new List<ProductReview>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Desciption { get; private set; }
        public decimal Price { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public List<ProductReview> Reviews { get; private set; }

        public void AddReview(ProductReview review)
        {
            Reviews.Add(review);
        }
        
        public void Update(string descrition, decimal price)
        {
            Desciption = descrition;
            Price = price;
        }
    }
}