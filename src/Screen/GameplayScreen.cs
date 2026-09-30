using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pcsk.src.Screen.Managers;
using pcsk.src.Playeer;
using pcsk.src.TileMap;
using pcsk.src.Camera;
using pcsk.src.Inputs;
using Microsoft.Xna.Framework.Input;
using pcsk.src.Interactions.Handlers;

namespace pcsk.src.Screen
{
    public class GameplayScreen : IScreen
    {

        private readonly Player _player;
        private readonly Map _map;
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private readonly Texture2D _boxTexture;
        private readonly GraphicsDevice _graphicsDevice;

        public GameplayScreen(Player player, Map map, Camera2D camera, SpriteFont font, Texture2D boxTexture, GraphicsDevice graphicsDevice)
        {
            _player = player;
            _map = map;
            _camera = camera;
            _font = font;
            _boxTexture = boxTexture;
            _graphicsDevice = graphicsDevice;
        }

        public void Initialize(){}

        public void Update(GameTime gameTime)
        {
            if (Input.IsKeyPressed(Keys.Escape))
            {
                ScreenManager.ChangeState(new PauseScreen(this, _font, _graphicsDevice));
                return;
            }
            DialogueManager.Update(gameTime);
            _player.Update(gameTime, _map);
            _camera.Follow(_player.Position, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            
            spriteBatch.Begin(transformMatrix: _camera.Transform);
            _map.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.End();

            if(_font != null)
            {
                spriteBatch.Begin();
                    spriteBatch.DrawString(_font, _player.getPosition(), new Vector2(10, -5), Color.Azure);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            DialogueManager.Draw(spriteBatch,_font, _boxTexture, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            spriteBatch.End();
            
        }
    }
}