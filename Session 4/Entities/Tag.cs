namespace BookLibrary.Entities;

// TASK 1.1 — Tag entity.
// A tag is a label (e.g. "Fiction", "Classic") that can be attached to many books.
public class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
}
