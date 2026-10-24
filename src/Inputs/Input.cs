using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace pcsk.src.Inputs
{
public static class Input
{
    private static KeyboardState _currentKey;
    private static KeyboardState _previousKey;
    
    private static MouseState _currentMouse;
    private static MouseState _previousMouse;

    public static void Update()
    {
        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();
    }

    public static bool IsKeyDown(Keys key) => _currentKey.IsKeyDown(key);

    public static bool IsKeyPressed(Keys key) => _currentKey.IsKeyDown(key) && _previousKey.IsKeyUp(key);


    public static Point MousePosition => new Point(_currentMouse.X, _currentMouse.Y);

    public static bool IsLeftButtonDown() => _currentMouse.LeftButton == ButtonState.Pressed;
    public static bool IsLeftButtonPressed() => _currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

}    
}