using Microsoft.Xna.Framework.Input;
namespace pcsk.src.Inputs
{
    public static class InputAxis
    {
        public static float Get(string axisName)
        {
            string axis = axisName.ToLower();

            if(axis.Equals("horizontal"))
            {
                float value = 0f;
                if(Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)) value += 1f;
                if(Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)) value -= 1f;
                return value;
            }

            if(axis.Equals("vertical"))
            {
                float value = 0f;
                if(Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down)) value += 1f;
                if(Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up)) value -= 1f;
                return value;
            }
            return 0f;
        }
    }
}