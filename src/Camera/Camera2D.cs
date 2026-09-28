using Microsoft.Xna.Framework;
namespace camera
{
public class Camera2D
{
    public Matrix Transform {get; private set;}

    public void Follow(Vector2 targetPosition, int screenWidth, int screenHeight)
    {
        var positon = Matrix.CreateTranslation(
            -targetPosition.X,
            -targetPosition.Y,
            0);

        var offset = Matrix.CreateTranslation(
            screenWidth / 2f,
            screenHeight / 2f,
            0);
        
        Transform = positon * offset;
    }
}
}