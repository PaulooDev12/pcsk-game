using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using pcsk.src.Inputs;
using pcsk.src.TileMap;
using pcsk.src.Physics;
using pcsk.src.Interactions.Handlers;

namespace pcsk.src.Playeer
{
public class Player
    {
        public Vector2 Position;
        
        private readonly float _damageCooldown = 1.0f;
        private float _damageTimer = 0f;
        private readonly Texture2D _texture;
        
        public Color PlayerColor { get; private set; }
        public Rectangle Bounds => new((int)Position.X, (int)Position.Y, 64, 64);
        public bool isFireProteced = false;

        public Player(Vector2 startPosition, Texture2D playerTexture)
        {
            Position = startPosition;
            _texture = playerTexture;
            PlayerColor = Color.Blue;
        }

        public void Update(GameTime gameTime, TiledMap map)
        {
            if (DialogueManager.IsActive) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_damageTimer > 0) _damageTimer -= deltaTime;

            Vector2 moveDirection = PlayerProperties.GetInputDirection();

           
            Collisions.MoveAndCollide(
                ref Position, 
                64, 
                64, 
                moveDirection, 
                PlayerProperties.Speed, 
                deltaTime, 
                map.CollisionsRect
            );

            PlayerProperties.HandleSpeed(Input.IsKeyDown(Keys.LeftShift));

            // adicionei o reset
            if(PlayerProperties.CurrentLife <= 0) PlayerProperties.Reset(this);

            if (Input.IsKeyPressed(Keys.E))
            {
                Rectangle proximity = new Rectangle(Bounds.X - 16, Bounds.Y - 16, Bounds.Width + 32, Bounds.Height + 32);
                foreach(var npc in map.Npcs)
                {
                    if (proximity.Intersects(npc.Bounds))
                    {
                        npc.Interact();
                    }
                }
            
            }        
        }

        private void HandleInteraction(List<Rectangle> interactables)
        {
            if (interactables == null) return;

            if (Input.IsKeyPressed(Keys.E))
            {
                Rectangle proximity = new Rectangle(Bounds.X - 8, Bounds.Y - 8, Bounds.Width + 16, Bounds.Height + 16);

                foreach (var item in interactables)
                {
                    if (proximity.Intersects(item))
                    {
                        Console.WriteLine("Interagiu com o objeto do mapa!");
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
            return $"Player X: {Position.X:0} | Player Y: {Position.Y:0} | Vida: {PlayerProperties.CurrentLife} | Velocidade: {PlayerProperties.Speed}f";
        }   
    }
}