using GameEngine;

namespace PacMan.Components;

public class AspectRatioFitter : Component
{
    public float AspectRatio { get; set; } = 1;

    private bool doAspectRatio = true;
    private Size oldSize;

    public AspectRatioFitter(GameObject gameObject) : base(gameObject) { }

    ~AspectRatioFitter()
    {
        GameObject.SizeChanged -= Resize;
    }

    public override void Initialize()
    {
        oldSize = GameObject.Size;

        GameObject.SizeChanged += Resize;
    }

    private void Resize(object? sender, EventArgs e)
    {
        if (doAspectRatio)
        {
            doAspectRatio = false;
            Size newSize = GameObject.Size;

            if (newSize.Width == oldSize.Width && newSize.Height != oldSize.Height)
            {
                GameObject.Width = (int)(newSize.Height * AspectRatio);
            }
            else if (newSize.Width != oldSize.Width && newSize.Height == oldSize.Height)
            {
                GameObject.Height = (int)(newSize.Width * AspectRatio);
            }
            else if (newSize.Width != oldSize.Width && newSize.Height != oldSize.Height)
            {
                if (newSize.Height - oldSize.Height > newSize.Width - oldSize.Width)
                {
                    GameObject.Width = (int)(newSize.Height * AspectRatio);
                }
                else
                {
                    GameObject.Height = (int)(newSize.Width * AspectRatio);
                }
            }

            oldSize = GameObject.Size;
        }
        else
        {
            doAspectRatio = true;
        }
    }
}