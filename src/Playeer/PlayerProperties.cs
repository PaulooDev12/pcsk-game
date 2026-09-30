using System;
using pcsk.src.Inputs;
using Microsoft.Xna.Framework;
namespace pcsk.src.Playeer
{
    public static class PlayerProperties
    {
        public static int MaxLife {get; private set;} = 100;
        public static float Speed {get; private set;} = 200f;
        private static readonly float _runningSpeed = 700f;
        private static readonly float _walkSpeed = 200f;
        public static int CurrentLife {get; private set;} = 100;
        public static float DamageTaked {get; private set;} = 20;

        public static void TakeDamage(int amount)
        {
            CurrentLife -= amount;
            if(CurrentLife < 0)
                CurrentLife = 0;
            Console.WriteLine($"Player tomou {amount} de dano! Vida restante: {CurrentLife}");
        }

        public static void SetSpeed(float amount)
        {
            Speed = amount;
        }
        public static void Reset(Player instance)
        {
            CurrentLife = MaxLife;
            instance.Position = new Vector2(100,100);
        }
        public static void HandleSpeed(bool isRunning)
        {
            Speed = isRunning ? _runningSpeed : _walkSpeed;
        } 

        public static Vector2 GetInputDirection()
        {
            Vector2 direction = new Vector2(
                InputAxis.Get("horizontal"),
                InputAxis.Get("vertical")
            );
            if(direction != Vector2.Zero)
                direction.Normalize();

            if(direction.LengthSquared() > 1)
                direction.Normalize();
            
            return direction;
        }
        public static void Heal(int healAmount)
        {
            int newHeal = Math.Clamp(CurrentLife += healAmount,0, 100);
            CurrentLife = newHeal;
            
        }
    }
}