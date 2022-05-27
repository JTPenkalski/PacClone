﻿namespace GameEngine;

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

    public bool Loop { get; protected set; }
    public int Duration { get; protected set; }
    public int Keyframes { get; protected set; }
    public string Name { get; protected set; }
    
    protected readonly IList<Keyframe> keyframes = new List<Keyframe>();

    public Animation(string filePath)
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

    public Keyframe this[int index] => keyframes[index];
}