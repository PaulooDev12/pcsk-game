using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace pcsk.src.debug
{
    public static class DebugMode
    {
        public static Profile GameProfile {get; private set;} = Profile.Game;
        private static Texture2D _pixelTexture;
        public static void ToggleDebugMode()
        {
            if(GameProfile == Profile.Game) GameProfile = Profile.Dev; 
            else GameProfile = Profile.Game;   
        }
        public static Texture2D GetPixelTexture(GraphicsDevice device)
        {
            if (_pixelTexture == null || _pixelTexture.IsDisposed)
            {
                _pixelTexture = new Texture2D(device, 1, 1);
                _pixelTexture.SetData(new[] { Color.White });
            }
            return _pixelTexture;
        }
        public static void DrawHitboxes(SpriteBatch spriteBatch, GraphicsDevice device, Rectangle bounds, Color color)
        {
            Texture2D hitbox = GetPixelTexture(device);
            spriteBatch.Draw(hitbox, bounds, color);
        }

        public static void DrawHitboxesOutLine
        (SpriteBatch spriteBatch, 
        GraphicsDevice graphicsDevice,
        Rectangle bounds, 
        Color color,
        int strokeWidth = 1
        )
        {
         Texture2D pixel = GetPixelTexture(graphicsDevice);
         spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y,bounds.Width, strokeWidth), color);
         spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y + bounds.Height - strokeWidth, bounds.Width, strokeWidth), color);
         spriteBatch.Draw(pixel, new Rectangle(bounds.X, bounds.Y, strokeWidth, bounds.Height), color);
         spriteBatch.Draw(pixel, new Rectangle(bounds.X + bounds.Width - strokeWidth, bounds.Y, strokeWidth, bounds.Height), color);


        }
        public static Texture2D CreateCricleTexture(GraphicsDevice device, int ray)
        {
        int diametro = ray * 2;
        Texture2D texture2D = new(device, diametro, diametro);
        Color[] colorData = new Color[diametro * diametro];
        float squaredRay = ray * ray;
        for(int x = 0; x < diametro; x++)
        {
            for(int y = 0; y < diametro; y++)
            {
                float dx = x - ray;
                float dy = y - ray;
                float squaredDistance = (dx * dx) + (dy * dy);

                if(squaredDistance <= squaredRay)
                    colorData[x + y * diametro] = Color.White;
                else
                    colorData[x + y * diametro] = Color.Transparent;
            }
        }
        texture2D.SetData(colorData);
        return texture2D;
    }
    }
}