namespace MvcEx.Models
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string pwd { get; set; }
        public string email { get; set; }

        public Users(int id_, string username_, string pwd_, string email_)
        {
            this.id = id_;
            this.username = username_;
            this.pwd = pwd_;
            this.email = email_;
        }
    }
    public class Books
    {
        public int id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public DateTime written { get; set; }
        public int rating { get; set; }
        public string author { get; set; }
        public string contents { get; set; }
        public List<string> comments { get; set; }

        public Books(int id_, string title_, string genre_, DateTime written_, string author_,int rating_,string contents_, List<string> comments)
        {
            this.id = id_;
            this.title = title_;
            this.genre = genre_;
            this.written = written_;
            this.rating = rating_;
            this.author = author_;
            this.contents = contents_;
            this.comments = comments;
        }
        public Books() { }
    }
}
