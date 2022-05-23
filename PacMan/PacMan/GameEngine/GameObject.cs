using System.ComponentModel;

namespace GameEngine;

public class GameObject : Control
{
    public int ID { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(false)]
    [Browsable(false)]
    public Renderer? Renderer { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(false)]
    [Browsable(false)]
    public Transform Transform { get; init; }

    protected readonly IDictionary<Type, Component> components = new Dictionary<Type, Component>(2);

    public GameObject()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.UserPaint
            | ControlStyles.AllPaintingInWmPaint
            | ControlStyles.ResizeRedraw
            | ControlStyles.ContainerControl
            | ControlStyles.OptimizedDoubleBuffer
            | ControlStyles.SupportsTransparentBackColor,
            true);

        Transform = AddComponent<Transform>();

        Game.Instantiate(this);
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object? obj)
    {
        if (obj is GameObject other)
            return ID == other.ID;

        return false;
    }

    public T AddComponent<T>() where T : Component
    {
        if (!components.ContainsKey(typeof(T)) && Activator.CreateInstance(typeof(T), this) is T newComponent)
        {
            components.Add(typeof(T), newComponent);

            if (newComponent is Renderer renderer)
                Renderer = renderer;

            return newComponent;
        }

        throw new InvalidOperationException($"Cannot add component of type {typeof(T)}");
    }

    public T? GetComponent<T>() where T : Component => components.ContainsKey(typeof(T)) ? (T)components[typeof(T)] : null;

    public bool RemoveComponent<T>() where T : Component => components.Remove(typeof(T));

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Renderer?.Render(this, e);
    }
}