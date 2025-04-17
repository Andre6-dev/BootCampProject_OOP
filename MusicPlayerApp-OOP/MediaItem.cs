namespace MusicPlayerApp_OOP;

public abstract class MediaItem : IPlayable
    {
        // Common properties for all media items
        public string Title { get; protected set; } // Can be set by derived classes
        public TimeSpan Duration { get; protected set; }

        // Abstract property - must be implemented by derived classes
        public abstract string CreatorInfo { get; }

        /// <summary>
        /// Constructor for the base MediaItem class.
        /// Called by constructors in derived classes using 'base(...)'.
        /// </summary>
        /// <param name="title">The title of the media item.</param>
        /// <param name="duration">The duration of the media item.</param>
        protected MediaItem(string title, TimeSpan duration)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "Title cannot be empty.");
            if (duration <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be positive.");

            Title = title;
            Duration = duration;
        }

        /// <summary>
        /// Abstract Play method - must be implemented by derived classes.
        /// This enforces that every specific media type knows how to 'Play' itself.
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// OOP Concept: Inheritance & Polymorphism (Virtual Method).
        /// Provides a 'virtual' default implementation for displaying details.
        /// Derived classes 'can' override this ('override' keyword) to provide
        /// more specific details, but they don't have to if this base
        /// implementation is sufficient.
        /// </summary>
        public virtual void DisplayDetails()
        {
            Console.WriteLine($"  Title: {Title}");
            Console.WriteLine($"  Creator: {CreatorInfo}"); // Uses the abstract property implemented by derived class
            Console.WriteLine($"  Duration: {Duration:mm\\:ss}"); // Format as minutes:seconds
        }
    }