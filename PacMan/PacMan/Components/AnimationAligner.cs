﻿using GameEngine;

namespace PacMan.Components;

public class AnimationAligner : Component
{
    private readonly Animator animator;
    private readonly KeyboardController keyboardController;

    public AnimationAligner(GameObject gameObject) : base(gameObject)
    {
        Animator? anim = GameObject.GetComponent<Animator>();
        if (anim == null)
            throw new Exception($"Component {nameof(AnimationAligner)} expects component {nameof(Animator)}.");
        animator = anim;

        KeyboardController? kbc = GameObject.GetComponent<KeyboardController>();
        if (kbc == null)
            throw new Exception($"Component {nameof(AnimationAligner)} expects component {nameof(KeyboardController)}.");
        keyboardController = kbc;
    }

    public override void Update()
    {
        if (keyboardController.AxialInput.X < 0)
            animator.Settings = Animator.RenderSettings.Rotate180;
        else if (keyboardController.AxialInput.X > 0)
            animator.Settings = Animator.RenderSettings.None;
        else if (keyboardController.AxialInput.Y > 0)
            animator.Settings = Animator.RenderSettings.Rotate90;
        else if (keyboardController.AxialInput.Y < 0)
            animator.Settings = Animator.RenderSettings.Rotate270;
    }
}