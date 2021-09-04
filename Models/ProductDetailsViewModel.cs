using System;
using System.Collections.Generic;

namespace DevReviews.API.Models
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel(int id, string title, string desciption, decimal price, DateTime registeredAt, List<ProductReviewViewModel> reviews)
        {
            Id = id;
            Title = title;
            Desciption = desciption;
            Price = price;
            RegisteredAt = registeredAt;
            Reviews = reviews;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Desciption { get; private set; }
        public decimal Price { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public List<ProductReviewViewModel> Reviews { get; set; }


    }

    public class ProductReviewViewModel
    {
        public ProductReviewViewModel(int id, string author, int rating, string comments, DateTime registeredAt)
        {
            Id = id;
            Author = author;
            Rating = rating;
            Comments = comments;
            RegisteredAt = registeredAt;
        }

        public int Id { get; set; }
        public string Author { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}