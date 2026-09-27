using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace physics
{
    public static class Collisions
    {
        public static void MoveAndCollide(
            ref Vector2 position,
            int width,
            int height,
            Vector2 direction,
            float speed,
            float deltaTime,
            List<Rectangle> obstacleColliders
        )
        {
            if(direction == Vector2.Zero) return;

            position.X += direction.X * speed * deltaTime;
            Rectangle boundsX = new Rectangle((int)position.X, (int)position.Y, width, height);
            foreach(var collider in obstacleColliders)
            {
                if(boundsX.Intersects(collider))
                {
                    if(direction.X > 0) position.X = collider.Left - width;
                    else if(direction.X < 0) position.X = collider.Right;

                    boundsX.X = (int)position.X;

                }
            }
            position.Y += direction.Y * speed * deltaTime;
            Rectangle boundsY = new Rectangle((int)position.X, (int)position.Y, width, height);

            foreach(var collider in obstacleColliders)
            {
                if(boundsY.Intersects(collider))
                {
                    if(direction.Y > 0) position.Y = collider.Top - height;
                    else if (direction.Y < 0) position.Y = collider.Bottom;

                    boundsY.Y = (int)position.Y;
                }
            }
        }
    }
}