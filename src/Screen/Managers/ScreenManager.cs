using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pcsk.src.Screen.Managers
{
    public static class ScreenManager
    {
        public static IScreen CurrentScreen {get; private set;} 

        public static void ChangeState(IScreen newScreen)
        {
            CurrentScreen = newScreen;
        }
        public static void Update(GameTime gameTime)
        {
            CurrentScreen?.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen?.Draw(spriteBatch);
        }
    }
}