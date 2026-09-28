using System.Collections.Generic;
using data;
using interactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace tileMap{
public class Map
{
   private readonly TileType[,] _mapData = new TileType[,]
        {
            { TileType.Wall, TileType.Wall,  TileType.Wall,  TileType.Wall },
            { TileType.Wall, TileType.Grass, TileType.Grass, TileType.Grass },
            { TileType.Wall, TileType.Chest, TileType.Lava,  TileType.Wall },
            { TileType.Wall, TileType.Grass,  TileType.Wall,  TileType.Wall },
        };

    private readonly int _tileSize = 64;
    private readonly Dictionary<TileType, Texture2D> _tileTextures;
    public List<Rectangle> WallColliders {get; private set;} = new();
    public List<Rectangle> Interactables {get; private set;} = new();
    public List<Rectangle> Offensives {get; private set;} = new();

    public List<Npc> Npcs {get; private set;} = new();

    public Map(Dictionary<TileType, Texture2D> tileTextures, ContentManager content)
    {
        _tileTextures = tileTextures;
        Npcs = NpcLoader.LoadNpcDataJson("Content/npcs.json", _tileSize, content);
        BuildColliders();   
    }

    private void BuildColliders()
    {
        for(int row = 0; row < _mapData.GetLength(0); row++)
        {
            for(int col = 0; col < _mapData.GetLength(1); col++)
            {
                Rectangle rect = new Rectangle(col * _tileSize, row * _tileSize, _tileSize, _tileSize);
                TileType type = _mapData[row, col];
                switch (type)
                {
                    case TileType.Wall:
                        WallColliders.Add(rect);
                        break;
                    case TileType.Chest:
                        Interactables.Add(rect);
                        break;
                    case TileType.Lava:
                        Offensives.Add(rect);
                        break;
            }
        }
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for(int row = 0; row < _mapData.GetLength(0); row++)
        {
            for(int col = 0; col < _mapData.GetLength(1); col++)
            {
                TileType type = _mapData[row, col];
                Rectangle destRect = new Rectangle(col * _tileSize, row * _tileSize, _tileSize, _tileSize);
        
                if(_tileTextures.TryGetValue(type, out Texture2D texture))
                {
                      spriteBatch.Draw(texture, destRect, Color.White);  
                }
            }
        }
        foreach(var npc in Npcs)
            {
                npc.Draw(spriteBatch);
                
            }
    }
   
} 

}