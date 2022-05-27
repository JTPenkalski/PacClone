using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GameEngine;

public class Renderer : Component
{
    private Image? _bitmap;
    public Image? Sprite
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
        using Brush brush = Sprite != null ? new TextureBrush(ResizeImage(Sprite, GameObject.Width, GameObject.Height)) : new SolidBrush(Color.Magenta);

        buffer.Graphics.FillRectangle(brush, GameObject.ClientRectangle);

        buffer.Render();
    }

    public virtual Image ResizeImage(Image image, int width, int height)
    {
        Rectangle destRect = new(0, 0, width, height);
        Bitmap resizedImage = new(width, height);

        resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics graphics = Graphics.FromImage(resizedImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes attributes = new();
            attributes.SetWrapMode(WrapMode.TileFlipXY);

            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
        }

        return resizedImage;
    }

    public virtual Image RotateImage(Image image, float angle)
    {
        // Create an empty Bitmap image
        Bitmap bitmap = new(image.Width, image.Height);

        // Turn the Bitmap into a Graphics object
        using Graphics graphics = Graphics.FromImage(bitmap);

        // Set the rotation point to the center of our image
        graphics.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);

        // Rotate the image
        graphics.RotateTransform(angle);

        // Reset the rotation point
        graphics.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);

        // Set the InterpolationMode to HighQualityBicubic so to ensure a high
        // quality image once it is transformed to the specified size
        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        // Draw our new image onto the Graphics object
        graphics.DrawImage(image, new Point(0, 0));

        // Return the final image
        return bitmap;
    }
}