using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pcsk.src.Screen.Managers
{
    public interface IScreen
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}