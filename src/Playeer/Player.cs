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
        public bool isLockedSpeed = false;
        private readonly float _damageCooldown = 1.0f;
        private float _damageTimer = 0f;
        private readonly Texture2D _texture;
        
        public Color PlayerColor { get; private set; }
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
        public bool isFireProteced = false;

        public Player(Vector2 startPosition, Texture2D playerTexture)
        {
            Position = startPosition;
            _texture = playerTexture;
            PlayerColor = Color.Blue;
        }

        /// <summary>
        /// Update refatorado para aceitar o TmxMap do Tiled
        /// </summary>
        public void Update(GameTime gameTime, TiledMap map)
        {
            if (DialogueManager.IsActive) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_damageTimer > 0) _damageTimer -= deltaTime;

            Vector2 moveDirection = PlayerProperties.GetInputDirection();

            // 1. Aplica movimento e colisão com a lista de retângulos gerada pelo Tiled
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

            // 2. Interações e Danos (quando você mapear as Camadas de Objetos do Tiled para Interativos/Npcs)

            // 3. Interação com NPCs (usando a lista do mapa Tiled)
            if (Input.IsKeyPressed(Keys.E))
            {
                Rectangle proximity = new Rectangle(Bounds.X - 16, Bounds.Y - 16, Bounds.Width + 32, Bounds.Height + 32);
                foreach(var npc in map.npcs)
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
            return $"Player X: {Position.X:0} | Player Y: {Position.Y:0} | Velocidade: {PlayerProperties.Speed}f";
        }   
    }
}