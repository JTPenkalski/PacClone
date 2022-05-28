using GameEngine;

namespace PacMan.Components;

public class AnimationAligner : Component
{
    private readonly Animator animator;
    private readonly KeyboardController keyboardController;

    public AnimationAligner(GameObject gameObject) : base(gameObject)
    {
        animator = GameObject.GetComponent<Animator>();
        keyboardController = GameObject.GetComponent<KeyboardController>();
    }

    public override void FixedUpdate()
    {
        if (keyboardController.Direction.X < 0)
            animator.Settings = Animator.RenderSettings.Rotate180;
        else if (keyboardController.Direction.X > 0)
            animator.Settings = Animator.RenderSettings.None;
        else if (keyboardController.Direction.Y > 0)
            animator.Settings = Animator.RenderSettings.Rotate90;
        else if (keyboardController.Direction.Y < 0)
            animator.Settings = Animator.RenderSettings.Rotate270;
    }
}