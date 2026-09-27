using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using inputs;
using tileMap;
using physics;

namespace player
{
public class Player
{
    public Vector2 Position;
    public bool isLockedSpeed = false;
    private readonly float _damageCooldown = 1.0f;
    private float _damageTimer = 0f;
    private readonly Texture2D _texture;
    public Color PlayerColor {get; private set;}
    public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
    public bool isFireProteced = false;

    public Player(GraphicsDevice graphicsDevice, Vector2 startPosition)
    {
        Position = startPosition;
        _texture = new Texture2D(graphicsDevice, 1, 1);
        PlayerColor = Color.Blue;
        _texture.SetData(new[] {PlayerColor});
    }
    public void Update(GameTime gameTime, Map map)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_damageTimer > 0) _damageTimer -= deltaTime;

        Vector2 moveDirection = PlayerProperties.GetInputDirection();
        Collisions.MoveAndCollide(ref Position, 32, 32, moveDirection, PlayerProperties.Speed, deltaTime, map.WallColliders);
        PlayerProperties.HandleSpeed(Input.isKeyDown(Keys.LeftShift));
        HandleInteraction(map.Interactables);
        handleDamage(map.Offensives);
        _texture.SetData(new[] {PlayerColor});
        
    }

    private void HandleInteraction(List<Rectangle> interactables)
    {
        if (Input.isKeyPressed(Keys.E))
        {
            Rectangle proximity = new Rectangle(Bounds.X - 8, Bounds.Y - 8, Bounds.Width, Bounds.Height);

            foreach(var item in interactables)
            {
                if(proximity.Intersects(item))
                {
                    Console.WriteLine("Interagiu com essa porra");
                }
            }
        }            
    }

    public void handleDamage(List<Rectangle> dangerItems)
        {
            if(_damageTimer > 0) return;

            Rectangle touch = new Rectangle(Bounds.X - 1, Bounds.Y - 1, Bounds.Width, Bounds.Height);
            
            foreach(var danger in dangerItems)
            {
                
                if(danger.Intersects(touch))
                {
                    _damageTimer = _damageCooldown;

                    PlayerProperties.TakeDamage(20);
                    if(PlayerProperties.CurrentLife <= 0)
                    {
                        PlayerProperties.Reset(this);
                    }
                    break;
                }
                
            }
        }
    public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
        }
    public string getPosition()
        {
            return $"Player X: {Position.X:0} | Player Y: {Position.Y:0} | Velocidade: {PlayerProperties.Speed}f";
        }   
}    
}
