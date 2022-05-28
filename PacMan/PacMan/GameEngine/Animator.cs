namespace GameEngine;

public class Animator : Component
{
    public enum RenderSettings
    {
        None,
        Rotate90,
        Rotate180,
        Rotate270
    }

    public int Frame { get; protected set; }
    public int Keyframe { get; protected set; }
    public string? DefaultAnimation { get; protected set; }
    public Animation? Animation { get; protected set; }
    public Image? Image { get; protected set; }
    public RenderSettings Settings { get; set; }

    protected int currentFrameCounter;
    protected readonly IDictionary<string, Animation> animations = new Dictionary<string, Animation>();
    protected readonly IDictionary<string, bool> triggers = new Dictionary<string, bool>();
    protected readonly IDictionary<Animation, IDictionary<string, Animation>> transitions = new Dictionary<Animation, IDictionary<string, Animation>>();
    protected readonly Renderer renderer;

    public Animator(GameObject gameObject) : base(gameObject)
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    public Animation this[string name] => animations[name];

    public override void FixedUpdate()
    {
        if (Enabled)
        {
            if (Animation == null)
            {
                // Setup initial animation
                if (DefaultAnimation == null)
                    throw new InvalidOperationException($"Cannot start an {nameof(Animator)} because the default animation is not set.");

                Animation = animations[DefaultAnimation];
                Keyframe = 0;
                GetNextKeyframe();
            }

            if (transitions.ContainsKey(Animation))
            {
                // Check for transitions
                foreach (string trigger in transitions[Animation].Keys)
                {
                    if (triggers[trigger])
                    {
                        triggers[trigger] = false;

                        Animation = animations[transitions[Animation][trigger].Name];
                        Keyframe = 0;
                        GetNextKeyframe();
                    }
                }
            }

            // Update per-frame data
            currentFrameCounter--;
            Frame = (Frame + 1) % Animation.Duration;

            if (Frame == 0 && Animation.Loop)
            {
                // The entire animation finished
                Keyframe = 0;
                GetNextKeyframe();
            }
            else if (currentFrameCounter == 0)
            {
                // The current keyframe finished
                Keyframe++;
                GetNextKeyframe();
            }
        }
    }

    public void AddAnimation(Animation animation, bool defaultAnimation = false)
    {
        animations.Add(animation.Name, animation);

        if (defaultAnimation)
            DefaultAnimation = animation.Name;
    }

    public void RemoveAnimation(Animation animation)
    {
        animations.Remove(animation.Name);
    }

    public void AddTrigger(string name)
    {
        triggers.Add(name, false);
    }

    public void RemoveTrigger(string name)
    {
        triggers.Remove(name);
    }

    public void SetTrigger(string name)
    {
        if (triggers.ContainsKey(name))
            triggers[name] = true;
    }

    public void AddTransition(Animation from, Animation to, string trigger)
    {
        if (!transitions.ContainsKey(from))
        {
            IDictionary<string, Animation> animationTransitions = new Dictionary<string, Animation>();
            transitions.Add(from, animationTransitions);
        }

        transitions[from].Add(trigger, to);
    }

    protected void GetNextKeyframe()
    {
        if (Animation != null)
        {
            Animation.Keyframe nextKeyframe = Animation[Keyframe];
            currentFrameCounter = nextKeyframe.Duration;
            Image = nextKeyframe.Image;

            renderer.Sprite = HandleFlags();
        }
    }

    protected Image HandleFlags()
    {
        if (Image == null)
            throw new InvalidOperationException("Trying to render a null image in an Animator.");

        return Settings switch
        {
            RenderSettings.Rotate90 => renderer.RotateImage(Image, 90f),
            RenderSettings.Rotate180 => renderer.RotateImage(Image, 180f),
            RenderSettings.Rotate270 => renderer.RotateImage(Image, 270f),
            _ => Image
        };
    }
}