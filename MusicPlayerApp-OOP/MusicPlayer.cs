namespace MusicPlayerApp_OOP;

/// <summary>
/// Represents the Music Player engine.
/// OOP Concept: Encapsulation.
/// This class bundles the data related to the current playlist/queue
/// (_playlist, _currentIndex) and the player's state (_isPlaying),
/// along with the methods to operate on that data (LoadPlaylist, Play, Stop, Next, Previous).
/// The internal details (like how the current index is managed) are hidden ('private')
/// from the outside world. Interaction happens through public methods.
/// OOP Concept: Composition. The MusicPlayer 'has a' list of IPlayable items.
/// </summary>
public class MusicPlayer
{
    // Encapsulated Data: These fields hold the player's internal state.
    // They are marked 'private' or 'private readonly' so they cannot be directly
    // accessed or modified from outside the class, ensuring data integrity.
    private readonly List<IPlayable> _playlist; // Holds the items to be played
    private int _currentIndex; // Index of the currently playing/selected item
    private bool _isPlaying; // Tracks if the player is currently in 'play' mode
    private string _playlistName; // Name for the loaded content

    /// <summary>
    /// Public property to safely get the currently playing item.
    /// Returns null if the playlist is empty or index is invalid.
    /// OOP Concept: Encapsulation (Controlled Access). Provides read-only access
    /// to derived information without exposing the internal list or index directly.
    /// </summary>
    public IPlayable CurrentItem
    {
        get
        {
            if (_playlist.Any() && _currentIndex >= 0 && _currentIndex < _playlist.Count)
            {
                return _playlist[_currentIndex];
            }

            return null; // Or throw an exception, depending on desired behavior
        }
    }

    /// <summary>
    /// Constructor for the MusicPlayer. Initializes the internal state.
    /// </summary>
    public MusicPlayer()
    {
        _playlist = new List<IPlayable>();
        _currentIndex = -1; // No item selected initially
        _isPlaying = false;
        _playlistName = "No Playlist Loaded";
    }

    /// <summary>
    /// Loads a collection of playable items into the player.
    /// Replaces any existing items.
    /// </summary>
    /// <param name="items">The list of IPlayable items to load.</param>
    /// <param name="name">A name for this collection (e.g., playlist name, album title).</param>
    public void LoadPlaylist(IEnumerable<IPlayable> items, string name)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        Stop(); // Stop playback before loading new list
        _playlist.Clear();
        _playlist.AddRange(items); // Add all items from the provided collection
        _playlistName = string.IsNullOrWhiteSpace(name) ? "Unnamed Collection" : name;
        _currentIndex = _playlist.Any() ? 0 : -1; // Set index to first item if list is not empty
        _isPlaying = false;

        Console.WriteLine($"\n🎶 Loaded '{_playlistName}' with {_playlist.Count} items.");
        if (CurrentItem != null)
        {
            Console.WriteLine($"   Ready to play: {CurrentItem.Title}");
        }
    }

    /// <summary>
    /// Starts or resumes playback from the current item.
    /// If playback reaches the end, it stops.
    /// </summary>
    public void Play()
    {
        if (CurrentItem == null)
        {
            Console.WriteLine("Cannot play. Playlist is empty or no item selected.");
            _isPlaying = false;
            return;
        }

        _isPlaying = true;
        // OOP Concept: Polymorphism.
        // We call the 'Play()' method on the 'CurrentItem'. Because CurrentItem
        // is of type IPlayable, the actual method that gets executed depends
        // on the specific runtime type of the object (_playlist[_currentIndex]).
        // If it's a Song, Song.Play() runs. If it's an AudioBook, AudioBook.Play() runs.
        // The MusicPlayer doesn't need to know the specific type, just that it's 'IPlayable'.
        CurrentItem.Play();
    }

    /// <summary>
    /// Stops playback.
    /// </summary>
    public void Stop()
    {
        if (_isPlaying)
        {
            Console.WriteLine("\n⏹️ Playback Stopped.");
            _isPlaying = false;
        }
        // Note: In a real player, you might pause instead of fully stopping,
        // retaining the position within the current item. This is simplified.
    }

    /// <summary>
    /// Moves to the next item in the playlist and plays it if currently playing.
    /// Stops if it reaches the end.
    /// </summary>
    public void Next()
    {
        if (!_playlist.Any()) return; // No items

        if (_currentIndex < _playlist.Count - 1)
        {
            _currentIndex++;
            Console.WriteLine($"\n⏭️ Skipped to Next: {_playlist[_currentIndex].Title}");
            if (_isPlaying)
            {
                Play(); // Automatically play the next item if player was playing
            }
            else
            {
                // Display info even if stopped
                _playlist[_currentIndex].DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine("\n🚫 Reached end of the playlist.");
            Stop();
            _currentIndex = _playlist.Count - 1; // Stay on last item
        }
    }

    /// <summary>
    /// Moves to the previous item in the playlist and plays it if currently playing.
    /// Does not wrap around.
    /// </summary>
    public void Previous()
    {
        if (!_playlist.Any()) return; // No items

        if (_currentIndex > 0)
        {
            _currentIndex--;
            Console.WriteLine($"\n⏮️ Skipped to Previous: {_playlist[_currentIndex].Title}");
            if (_isPlaying)
            {
                Play(); // Automatically play the previous item if player was playing
            }
            else
            {
                // Display info even if stopped
                _playlist[_currentIndex].DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine("\n🚫 Already at the beginning of the playlist.");
            // Stay on the first item
            if (_isPlaying) Play(); // Re-play current if already playing
            else if (CurrentItem != null) CurrentItem.DisplayDetails();
        }
    }

    /// <summary>
    /// Displays the details of the currently selected item.
    /// </summary>
    public void DisplayCurrentItemInfo()
    {
        if (CurrentItem != null)
        {
            Console.WriteLine($"\nℹ️ Current Item ({_currentIndex + 1}/{_playlist.Count}) in '{_playlistName}':");
            // OOP Concept: Polymorphism.
            // Calling DisplayDetails() on the IPlayable item ensures the correct
            // version (Song.DisplayDetails, AudioBook.DisplayDetails) is called.
            CurrentItem.DisplayDetails();
        }
        else
        {
            Console.WriteLine("\nℹ️ No item currently selected.");
        }
    }

    /// <summary>
    /// Displays the entire playlist content.
    /// </summary>
    public void DisplayPlaylist()
    {
        Console.WriteLine($"\n--- Playlist: {_playlistName} ---");
        if (!_playlist.Any())
        {
            Console.WriteLine(" (Empty)");
            return;
        }

        for (int i = 0; i < _playlist.Count; i++)
        {
            // Using ToString() override for concise list display
            Console.WriteLine($" {i + 1}. {_playlist[i]} {(i == _currentIndex ? "<- Current" : "")}");
        }

        Console.WriteLine("-----------------------------");
    }
}