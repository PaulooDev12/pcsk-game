using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pcsk{
public class Map
{
    private readonly int[,] _mapData = new int[,]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 2, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 3, 0, 0, 1, 0, 0, 0, 3, 0, 1 },
        { 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 3, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };

    private readonly int _tileSize = 64;
    private readonly Texture2D _wallTexture;
    private readonly Texture2D _interactableTexture;

    private readonly Texture2D _lavaTexture;
    public List<Rectangle> WallColliders {get; private set;} = new();
    public List<Rectangle> Interactables {get; private set;} = new();

    public List<Rectangle> Offensives {get; private set;} = new();

    public Map(GraphicsDevice graphicsDevice)
    {
        _wallTexture = CreateColorTexture(graphicsDevice, Color.Gray);
        _interactableTexture = CreateColorTexture(graphicsDevice, Color.Gold);
        _lavaTexture = CreateColorTexture(graphicsDevice, Color.Orange);
        
        BuildColliders();   
    }

    private void BuildColliders()
    {
        for(int row = 0; row < _mapData.GetLength(0); row++)
        {
            for(int col = 0; col < _mapData.GetLength(1); col++)
            {
                Rectangle rect = new Rectangle(col * _tileSize, row * _tileSize, _tileSize, _tileSize);
                if(_mapData[row, col] == 1) WallColliders.Add(rect);

                else if (_mapData[row, col] == 2)
                    Interactables.Add(rect);

                else if (_mapData[row, col] == 3)
                    Offensives.Add(rect); 
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for(int row = 0; row < _mapData.GetLength(0); row++)
        {
            for(int col = 0; col < _mapData.GetLength(1); col++)
            {
                Rectangle tileRect = new Rectangle(col * _tileSize, row * _tileSize, _tileSize, _tileSize);
                if(_mapData[row, col] == 1) 
                    spriteBatch.Draw(_wallTexture, tileRect, Color.White);

                else if(_mapData[row, col] == 2) 
                    spriteBatch.Draw(_interactableTexture, tileRect, Color.White);
                
                else if(_mapData[row, col] == 3)
                    spriteBatch.Draw(_lavaTexture, tileRect, Color.Red);
            }
        }
    }

    private Texture2D CreateColorTexture(GraphicsDevice graphicsDevice, Color color)
    {
        Texture2D rect = new Texture2D(graphicsDevice, 1, 1);
        rect.SetData(new[] { color });
        return rect;
    }
    
} 

}