namespace BookDatabase.Models
{
    public class BookModel
    {
        public string BookTile { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public int Rating { get; set; }
        public bool IsFinished { get; set; }
    }
}
