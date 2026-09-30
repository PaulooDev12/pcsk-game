using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace pcsk.src.Playeer
{
    public static class PlayerHandlers
    {
        public static void HandleDamage(
        List<Rectangle> damageTiles, 
        Player instance,
        float cooldown, 
        ref float timer)
        {
            if(timer > 0) return; 
            Rectangle touch = new Rectangle(
                instance.Bounds.X - 1, 
                instance.Bounds.Y - 1, 
                instance.Bounds.Width, 
                instance.Bounds.Height);
            
            foreach(var dmg in damageTiles)
            {
                if (dmg.Intersects(touch))
                {
                    timer = cooldown;
                    PlayerProperties.TakeDamage(20);
                    if(PlayerProperties.CurrentLife <= 0)
                    {
                        PlayerProperties.Reset(instance);
                    }
                    break;

                }
            }
        }
    }
}