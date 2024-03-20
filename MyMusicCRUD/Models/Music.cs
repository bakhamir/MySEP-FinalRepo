namespace MyMusicCRUD.Models
{
    public class Music
    {
        public int id { get; set; }
        public string title { get; set; }
        public int catId { get; set; }
        public int duration { get; set; }

    }
    public class Category
    {
        public int id { get; set; }
        public string genre { get; set; }

        public Category(int id_, string genre_)
        {
            this.id = id_;
            this.genre = genre_;
        }
    }

}
