namespace GameEngine;

public class Animation
{
    public class Keyframe
    {
        public int Duration { get; init; }
        public Image Image { get; init; }

        public Keyframe(Image image, int duration)
        {
            Image = image;
            Duration = duration;
        }
    }

    private const string ANY_ANIMATION = "ANY";

    private static Animation? _any;
    public static Animation Any
    {
        get
        {
            if (_any == null)
                _any = new Animation(ANY_ANIMATION);

            return _any;
        }
    }

    public bool Loop { get; protected set; }
    public int Duration { get; protected set; }
    public int Keyframes { get; protected set; }
    public string Name { get; protected set; }
    
    protected readonly IList<Keyframe> keyframes = new List<Keyframe>();

    public Animation(string filePath)
    {
        if (filePath == ANY_ANIMATION)
        {
            Name = ANY_ANIMATION;
        }
        else
        {
            string[] values = File.ReadAllText(filePath).Split(',');

            Name = Path.GetFileNameWithoutExtension(filePath);
            Loop = bool.Parse(values[0]);

            for (int i = 1; i < values.Length; i += 2)
            {
                if (Resources.ResourceManager.GetObject(values[i].Trim()) is not Image image)
                    throw new InvalidOperationException($"Cannot create animation with missing resource {values[i]}");

                int duration = int.Parse(values[i + 1]);

                keyframes.Add(new Keyframe(image, duration));

                Duration += duration;
                Keyframes++;
            }
        }
    }

    public Keyframe this[int index] => keyframes[index];

    public override bool Equals(object? obj)
    {
        return obj is Animation animation &&
               Loop == animation.Loop &&
               Duration == animation.Duration &&
               Name == animation.Name;
    }

    public override int GetHashCode() => HashCode.Combine(Loop, Duration, Name);
}