namespace Library.Services.Books.Contracts
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Writer { get; set; }
        public short BookCategoryId { get; set; }
        public byte MinimumAge { get; set; }
        public byte MaximumAge { get; set; }
    }
}
