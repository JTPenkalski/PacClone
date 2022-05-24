using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GameEngine;

public class Renderer : Component
{
    private Bitmap? _bitmap;
    public Bitmap? Sprite
    {
        get => _bitmap;
        set
        {
            _bitmap = value;
            GameObject.Invalidate();
        }
    }

    protected BufferedGraphicsContext graphicsContext;

    public Renderer(GameObject gameObject) : base(gameObject)
    {
        graphicsContext = BufferedGraphicsManager.Current;

        gameObject.Paint += Render;
    }

    public virtual void Render(object? sender, PaintEventArgs e)
    {
        if (sender != GameObject)
            throw new ArgumentException("The specified sender does not match this Renderer's attached GameObject.");

        using BufferedGraphics buffer = graphicsContext.Allocate(e.Graphics, GameObject.ClientRectangle);
        using Brush textureBrush = Sprite != null ? new TextureBrush(ResizeImage(Sprite, GameObject.Width, GameObject.Height)) : new SolidBrush(Color.Magenta);

        buffer.Graphics.FillRectangle(textureBrush, GameObject.ClientRectangle);

        buffer.Render();
    }

    protected virtual Image ResizeImage(Image image, int width, int height)
    {
        Rectangle destRect = new(0, 0, width, height);
        Bitmap resizedImage = new(width, height);

        resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics graphics = Graphics.FromImage(resizedImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes attributes = new();
            attributes.SetWrapMode(WrapMode.TileFlipXY);

            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
        }

        return resizedImage;
    }
}