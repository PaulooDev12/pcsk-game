using Microsoft.Xna.Framework.Input;

namespace pcsk.src.Inputs
{
public static class Input
{
    private static KeyboardState _currentKey;

    private static KeyboardState _previousKey;

    public static void Update()
    {
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();
    }
    public static bool IsKeyDown(Keys key) => _currentKey.IsKeyDown(key);

    public static bool IsKeyPressed(Keys key) => _currentKey.IsKeyDown(key) && _previousKey.IsKeyUp(key);
}    
}