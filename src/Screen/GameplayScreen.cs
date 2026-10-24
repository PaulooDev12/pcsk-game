using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pcsk.src.Screen.Managers;
using pcsk.src.Playeer;
using pcsk.src.TileMap;
using pcsk.src.Camera;
using pcsk.src.Inputs;
using Microsoft.Xna.Framework.Input;
using pcsk.src.Interactions.Handlers;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using pcsk.src.debug;
using TiledSharp;

namespace pcsk.src.Screen
{
    public class GameplayScreen : IScreen
    {

        private readonly Player _player;
        private readonly Enemy _enemy;
        private readonly Camera2D _camera;
        private readonly TiledMap _mapa;
        private readonly SpriteFont _font;
        private readonly Texture2D _boxTexture;
        private readonly GraphicsDevice _graphicsDevice;

        public GameplayScreen(Player player, Enemy enemy, Camera2D camera, SpriteFont font, Texture2D boxTexture, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _enemy = enemy;
            _player = player;
            _mapa = new TiledMap(graphicsDevice, "Content/maps/mapa_brabo.tmx", content);
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
            _player.Update(gameTime, _mapa);
            _enemy.Update(gameTime);
            _camera.Follow(_player.Position, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height, (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
           
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: _camera.Transform);
            _mapa.Draw(spriteBatch);
            _player.Draw(spriteBatch);

            if(DebugMode.GameProfile == Profile.Dev)
            {
                DebugMode.DrawHitboxesOutLine(spriteBatch, _graphicsDevice, _player.Bounds, Color.Red, 3);
                foreach(var npc in _mapa.Npcs)
                {
                    DebugMode.DrawHitboxesOutLine(spriteBatch, _graphicsDevice, npc.Bounds, Color.Red,2);
                }
            }
            _enemy.Draw(spriteBatch, _graphicsDevice);
            spriteBatch.End();

            if(_font != null)
            {
                spriteBatch.Begin();
                    spriteBatch.DrawString(_font, _player.getPosition(), new Vector2(10, -5), Color.Azure);
                    if (DebugMode.GameProfile == Profile.Dev) spriteBatch.DrawString(_font, "DEBUG MODE", new Vector2(10, 7), Color.Red, 0, Vector2.Zero, 1.65f, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            DialogueManager.Draw(spriteBatch,_font, _boxTexture, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            spriteBatch.End();
            
        }
    }
}