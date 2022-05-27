using System.ComponentModel;

namespace GameEngine;

public class GameObject : Control, IEquatable<GameObject>
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
        SetStyle(ControlStyles.UserPaint
            | ControlStyles.AllPaintingInWmPaint
            | ControlStyles.ResizeRedraw
            | ControlStyles.ContainerControl
            | ControlStyles.OptimizedDoubleBuffer
            | ControlStyles.SupportsTransparentBackColor,
            true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;

        Transform = AddComponent<Transform>();

        Game.Instantiate(this);
    }

    public override int GetHashCode() => ID;

    public override bool Equals(object? obj) => Equals(obj as GameObject);

    public bool Equals(GameObject? other)
    {
        if (other is not null)
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
        Renderer?.Render(this, e);

        base.OnPaint(e);
    }
}