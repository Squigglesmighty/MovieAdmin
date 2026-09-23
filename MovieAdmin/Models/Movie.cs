namespace MovieAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int RunTime { get; set; }

        public DateTime ReleaseDate { get; set; } //First put string then forgot i learned about DateTime

        public bool NewRelease { get; set; } // First put string AGAIN but remembered its only a true or false so bool was the correct type

        public float Rating { get; set; } //First put int but didnt consider ratings being outside of whole numbers so changed to float


    }
}
