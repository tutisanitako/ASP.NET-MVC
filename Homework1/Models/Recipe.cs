namespace Homework1.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MadeBy { get; set; }
        public string ImageUrl { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string MakingProcess { get; set; }
        public string VideoLink { get; set; }
        public bool IsLiked { get; set; }
        public bool IsBookmarked { get; set; }
    }
}