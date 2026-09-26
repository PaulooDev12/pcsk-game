using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
namespace pcsk
{
public class Player
{
    public Vector2 Position;
    private readonly float _speed = 200f;

    private readonly float _damageCooldown = 1.0f;
    private float _damageTimer = 0f;
    
    public int life {get; private set;}
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
        life = 100;
    }
    public void Update(GameTime gameTime, Map map)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_damageTimer > 0)
            {
                _damageTimer -= deltaTime;
            }

        Vector2 moveDirection = GetInputDirection();
        MoveAndCollide(moveDirection, deltaTime, map.WallColliders);
        HandleInteraction(map.Interactables);
        handleDamage(map.Offensives, gameTime);
        _texture.SetData(new[] {PlayerColor});
        
    }
    private Vector2 GetInputDirection()
    {
        Vector2 direction = Vector2.Zero;
        if (Input.isKeyDown(Keys.W) || Input.isKeyDown(Keys.Up)) direction.Y -= 1;
        if (Input.isKeyDown(Keys.S) || Input.isKeyDown(Keys.Down)) direction.Y += 1;
        if (Input.isKeyDown(Keys.A) || Input.isKeyDown(Keys.Left)) direction.X -= 1;
        if (Input.isKeyDown(Keys.D) || Input.isKeyDown(Keys.Right)) direction.X += 1;

        if(direction != Vector2.Zero) direction.Normalize();

        return direction;
    }

    private void MoveAndCollide(Vector2 direction, float deltaTime, List<Rectangle> walls)
    {
        Position.X += direction.X * _speed * deltaTime;

        foreach(var wall in walls)
        {
            if(Bounds.Intersects(wall))
            {
                if(direction.X > 0) Position.X = wall.Left - Bounds.Width;
                else if (direction.X < 0) Position.X = wall.Right;
            }
        }
        Position.Y += direction.Y * _speed * deltaTime;

        foreach(var wall in walls)
        {
            if(Bounds.Intersects(wall))
                {
                    if(direction.Y > 0) Position.Y = wall.Top - Bounds.Height;
                    else if (direction.Y < 0) Position.Y = wall.Bottom;
                }        
        }
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

    public void handleDamage(List<Rectangle> dangerItems, GameTime gameTime)
        {
            if(_damageTimer > 0) return;

            Rectangle touch = new Rectangle(Bounds.X - 1, Bounds.Y - 1, Bounds.Width, Bounds.Height);
            int damage = 20;
            foreach(var danger in dangerItems)
            {
                
                if(danger.Intersects(touch))
                {
                    life -= damage;
                    Console.WriteLine($"Vai morrer aí em: ${life}");
                    _damageTimer = _damageCooldown;
                    PlayerColor = Color.Red;

                    if(life <= 0)
                    {
                        Console.WriteLine("Morreu cara Pqp!");
                    }
                    PlayerColor = Color.Blue;
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
            return $"Player X: {Position.X:0} | Player Y: {Position.Y:0}";
        }   
}    
}
