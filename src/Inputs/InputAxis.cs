using Microsoft.Xna.Framework.Input;
namespace inputs
{
    public static class InputAxis
    {
        public static float get(string axisName)
        {
            string axis = axisName.ToLower();

            if(axis.Equals("horizontal"))
            {
                float value = 0f;
                if(Input.isKeyDown(Keys.D) || Input.isKeyDown(Keys.Right)) value += 1f;
                if(Input.isKeyDown(Keys.A) || Input.isKeyDown(Keys.Left)) value -= 1f;
                return value;
            }

            if(axis.Equals("vertical"))
            {
                float value = 0f;
                if(Input.isKeyDown(Keys.S) || Input.isKeyDown(Keys.Down)) value += 1f;
                if(Input.isKeyDown(Keys.W) || Input.isKeyDown(Keys.Up)) value -= 1f;
                return value;
            }
            return 0f;
        }
    }
}