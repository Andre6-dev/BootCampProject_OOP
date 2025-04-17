namespace MusicPlayerApp_OOP;

/// <summary>
/// Represents the concept of something that can be played and displayed.
/// OOP Concept: Abstraction.
/// This interface defines a 'contract' for playable items without specifying
/// the exact implementation details. Any class implementing IPlayable
/// guarantees it can provide this functionality.
/// </summary>
public interface IPlayable
{
    /// <summary>
    /// Gets the title of the playable item.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets information about the creator(s) (Artist, Author, etc.).
    /// </summary>
    string CreatorInfo { get; } // Renamed from Artist for generality

    /// <summary>
    /// Method to simulate playing the item.
    /// </summary>
    void Play();

    /// <summary>
    /// Method to display information about the item.
    /// </summary>
    void DisplayDetails();
}