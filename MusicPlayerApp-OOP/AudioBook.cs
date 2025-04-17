namespace MusicPlayerApp_OOP;

/// <summary>
/// Represents an AudioBook.
/// OOP Concept: Inheritance & Polymorphism.
/// Like Song, this class inherits from MediaItem. It demonstrates how the same
/// base class can be used for different types of media.
/// It provides its own specific implementations for abstract members (CreatorInfo, Play)
/// and potentially overrides virtual ones (DisplayDetails).
/// </summary>
public class AudioBook : MediaItem // Inherits from MediaItem
{
    public string Author { get; private set; }
    public string Narrator { get; private set; }

    /// <summary>
    /// Provides the implementation for the abstract CreatorInfo property from MediaItem.
    /// </summary>
    public override string CreatorInfo => $"{Author} (Narrated by: {Narrator})";

    /// <summary>
    /// Constructor for the AudioBook class.
    /// </summary>
    public AudioBook(string title, string author, string narrator, TimeSpan duration)
        : base(title, duration) // Call the base class constructor
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentNullException(nameof(author), "Author cannot be empty.");
        if (string.IsNullOrWhiteSpace(narrator))
            throw new ArgumentNullException(nameof(narrator), "Narrator cannot be empty.");

        Author = author;
        Narrator = narrator;
    }

    /// <summary>
    /// OOP Concept: Polymorphism (Method Overriding).
    /// Provides the specific implementation for the 'abstract Play' method for an AudioBook.
    /// </summary>
    public override void Play()
    {
        Console.WriteLine($"\n📖 Playing AudioBook: {Title} by {Author}");
        Console.WriteLine($"   Narrated by: {Narrator}, Duration: {Duration:hh\\:mm\\:ss}"); // Use hours format
    }

    /// <summary>
    /// OOP Concept: Polymorphism (Method Overriding).
    /// Overrides the DisplayDetails method to show audiobook-specific information.
    /// </summary>
    public override void DisplayDetails()
    {
        base.DisplayDetails(); // Call base method first
        Console.WriteLine($"  Author: {Author}");
        Console.WriteLine($"  Narrator: {Narrator}");
    }

    /// <summary>
    /// Override ToString for simpler representation.
    /// </summary>
    public override string ToString()
    {
        return $"{Title} - {Author} ({Duration:hh\\:mm\\:ss})";
    }
}