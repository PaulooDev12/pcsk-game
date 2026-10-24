
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pcsk;
using pcsk.src.debug;
using pcsk.src.Physics;
using pcsk.src.Playeer;

public class Enemy
{
    public Vector2 positon;
    public Rectangle Bounds => new((int)positon.X, (int)positon.Y, 64, 64);
    private bool IntersectsPlayer => Bounds.Intersects(_target.Bounds);
    public string Name {get; set;}
    private readonly Player _target;
    private float Speed {get; set;} = 180.0f;    
    private readonly float _cooldown = 1.0f;
    private float _timer = 0f;

    private readonly float _ray = 450f;

    public Enemy(Vector2 startPosition, Texture2D enemyTexture, Player player)
    {
        _target = player;
        positon = startPosition;
        _texture = enemyTexture;

    }

  
    private readonly Texture2D _texture;
    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        BoundingSphere enemyPoint = new(new Vector3(positon,0), _ray);
        BoundingSphere proximityPoint = new(new Vector3(positon,0), _ray / 2f);
        BoundingSphere playerPoint = new(new Vector3(_target.Position, 0), 1f);
        
        if(enemyPoint.Intersects(playerPoint)) {
            Follow(deltaTime);
            if(proximityPoint.Intersects(playerPoint)) Speed = 350f;
            else Speed = 150f;
        }
        
        if(_timer > 0f) _timer -= deltaTime;
        if(_timer <= 0f) 
        {
            Attack();
            _timer = _cooldown;
        }
        
    }

    private void Attack()
    {
        Rectangle touch = new(Bounds.X - 4, Bounds.Y - 4, Bounds.Width, Bounds.Height);
        if (touch.Intersects(_target.Bounds))
        {
            PlayerProperties.TakeDamage(5);
               
        }
       
    }

    private void Follow(float deltaTime)
    {
        Vector2 direction = _target.Position - positon;

        if(direction != Vector2.Zero) direction.Normalize();

        Collisions.MoveAndCollide(ref positon, 64,64, direction, Speed, deltaTime, [_target.Bounds]);       
    }
    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        
        // Texture2D texturAlert = CreateCricleTexture(device, (int)_ray);
        // Vector2 originCircle = new(texturAlert.Width / 2f, texturAlert.Height / 2f);
        if(DebugMode.GameProfile == Profile.Dev)
        {
            Texture2D alertPointTexture = DebugMode.CreateCricleTexture(device, (int)_ray);
            Vector2 originCircle = new(alertPointTexture.Width / 2f, alertPointTexture.Height /2f);
             spriteBatch.Draw(alertPointTexture, positon, null, Color.Red * 0.55f,0f, originCircle, 1f, SpriteEffects.None, 0f);
        }
        
        spriteBatch.Draw(_texture, Bounds, Color.White);
    }


}