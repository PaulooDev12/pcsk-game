using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using inputs;
using tileMap;
using physics;
using interactions;

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

    public Player(Vector2 startPosition, Texture2D playerTexture)
    {
        Position = startPosition;
        _texture = playerTexture;
        PlayerColor = Color.Blue;
       
    }
    public void Update(GameTime gameTime, Map map)
    {
        if(DialogueManager.IsActive) return;
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_damageTimer > 0) _damageTimer -= deltaTime;

        Vector2 moveDirection = PlayerProperties.GetInputDirection();
        Collisions.MoveAndCollide(ref Position, 32, 32, moveDirection, PlayerProperties.Speed, deltaTime, map.WallColliders);
        PlayerProperties.HandleSpeed(Input.isKeyDown(Keys.LeftShift));
        HandleInteraction(map.Interactables);
        PlayerHandlers.HandleDamage(map.Offensives, this, _damageCooldown, ref _damageTimer);
            if (Input.isKeyPressed(Keys.E))
            {
                foreach(var npc in map.Npcs)
                {
                    Rectangle proximity = new Rectangle(Bounds.X - 16, Bounds.Y - 16, Bounds.Width + 24, Bounds.Height + 24);
                    if (proximity.Intersects(npc.Bounds))
                    {
                        npc.Interact();
                        break;
                    }
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