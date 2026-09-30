using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pcsk.src.Screen.Managers;
using pcsk.src.Inputs;
using Microsoft.Xna.Framework.Input;

namespace pcsk.src.Screen
{
    public class PauseScreen : IScreen
    {
        private readonly GameplayScreen _gameplayScreen;
        private readonly SpriteFont _font;
        private readonly Texture2D _pixelOverlay;

        private int Width {get; set;} = 700;
        private int Height {get; set;} = 700;

        public PauseScreen(GameplayScreen gameplayScreen, SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            _gameplayScreen = gameplayScreen;
            _font = spriteFont;
            _pixelOverlay = new Texture2D(graphicsDevice, 1, 1);
            _pixelOverlay.SetData(new [] {Color.Black});
            Width = graphicsDevice?.Viewport.Width ?? 500;
            Height = graphicsDevice?.Viewport.Width ?? 500;
            
        }
        public void Initialize(){}

        public void Update(GameTime gameTime)
        {
            if (Input.IsKeyPressed(Keys.Escape))
            {
                ScreenManager.ChangeState(_gameplayScreen);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _gameplayScreen.Draw(spriteBatch);

            spriteBatch.Begin();
            
            Rectangle screenBounds = new Rectangle(0,0, Width, Height);
            spriteBatch.Draw(_pixelOverlay, screenBounds, Color.Black * 0.6f);

            spriteBatch.DrawString(_font, "PAUSADO", new Vector2(300, 150), Color.Yellow);
            spriteBatch.DrawString(_font, "[ESC] Retomar Jogo", new Vector2(300, 220), Color.White);
            spriteBatch.End();
        }
    }
}