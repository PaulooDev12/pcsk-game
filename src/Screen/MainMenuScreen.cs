using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using pcsk.src.Inputs;
using pcsk.src.Screen.Managers;

namespace pcsk.src.Screen
{
    public class MainMenuScreen : IScreen
    {
        private readonly GameplayScreen _gameplayScreen;
        private readonly SpriteFont _font;

        public MainMenuScreen(GameplayScreen gameplayScreen, SpriteFont spriteFont)
        {
            _gameplayScreen = gameplayScreen;
            _font = spriteFont;
        }
        public void Initialize(){}



        public void Update(GameTime gameTime)
        {
            if (Input.IsKeyPressed(Keys.Enter))
            {
                ScreenManager.ChangeState(_gameplayScreen);
            } 
        }

         public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "Joguinho", new Vector2(200, 150), Color.White);
            spriteBatch.DrawString(_font, "Aperta Enter ae", new Vector2(200, 200), Color.Gold);
            spriteBatch.End();

        
        }
    }
}