using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using pcsk.src.Camera;
using pcsk.src.Inputs;
using pcsk.src.Playeer;
using pcsk.src.TileMap;
using pcsk.src.Interactions.Handlers;
using pcsk.src.Screen.Managers;
using pcsk.src.Screen;
using pcsk.src.Screen.MainMenu;
using System.Diagnostics;
using pcsk.src.debug;

namespace pcsk
{
 public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;

    private Enemy _enemy;
    private SpriteBatch _spriteBatch;
    private Map _map;
    private Player _player;
    private Camera2D _camera;
    private Texture2D _blankTextue;

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
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.SynchronizeWithVerticalRetrace = true;
        _graphics.ApplyChanges();
        _font = Content.Load<SpriteFont>("atwriter");
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _blankTextue = new Texture2D(GraphicsDevice, 1, 1);
        _blankTextue.SetData(new[] {Color.White});

        Texture2D playerTexture = Content.Load<Texture2D>("ch");
        
        _player = new Player(new Vector2(600,600), playerTexture);
        _camera = new Camera2D();
        _enemy = new Enemy(new Vector2(600, 600), playerTexture, _player);

        var gameplayScreen = new GameplayScreen(_player, _enemy ,_camera, _font, _blankTextue, GraphicsDevice, Content);

        ScreenManager.ChangeState(new MainMenuScreen(gameplayScreen, _font));
    }

    protected override void Update(GameTime gameTime)
    {
        // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //     Exit();
        
        Input.Update();
        ScreenManager.Update(gameTime);

        // TODO: Add your update logic here
        if(Input.IsKeyPressed(Keys.LeftControl))
            {
                DebugMode.ToggleDebugMode();
            }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        ScreenManager.Draw(_spriteBatch);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}  
}

