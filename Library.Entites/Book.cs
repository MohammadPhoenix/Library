namespace Library.Entites
{
    public class Book {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public short BookCategoryId { get; set; }
        public BookCategory BookCategory { get; set; }
        public byte MinimumAge { get; set; }
        public byte MaximumAge { get; set; }
    }
}
