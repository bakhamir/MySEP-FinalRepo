namespace ParentChildrenSeasons.Models
{
    public class Children
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int ParentId { get; set; }
    }

    public class Parent
    {
        public int ParentId { get; set; }
        public string ParentName { get; set; }
    }

}
