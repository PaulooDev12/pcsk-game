using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using camera;
using inputs;
using player;
using tileMap;

namespace pcsk
{
 public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Map _map;
    private Player _player;
    private Camera2D _camera;

    private SpriteFont _font;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.SynchronizeWithVerticalRetrace = true;
        _graphics.ApplyChanges();
        _font = Content.Load<SpriteFont>("atwriter");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var tileTextures = new Dictionary<TileType, Texture2D>
        {
            {TileType.Wall, Content.Load<Texture2D>("wall_tile")},
            {TileType.Grass, Content.Load<Texture2D>("grass_tile")},
            {TileType.Chest, Content.Load<Texture2D>("chest")},
            {TileType.Lava, Content.Load<Texture2D>("lava_tile")},
        };

        // TODO: use this.Content to load your game content here
        _map = new Map(tileTextures);
        _player = new Player(GraphicsDevice, new Vector2(100,100));
        _camera = new Camera2D();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        

        Input.Update();
        // TODO: Add your update logic here
        _player.Update(gameTime, _map);
        _camera.Follow(_player.Position, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.Transform);

        _map.Draw(_spriteBatch);

        _player.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        if(_font != null)
        {
            _spriteBatch.DrawString(_font, _player.getPosition(), new Vector2(10,-5), Color.White);
        }

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}  
}

