using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using pcsk.src.Data;
using pcsk.src.debug;
using pcsk.src.Interactions;
using TiledSharp;

namespace pcsk.src.TileMap
{
    public class TiledMap
    {
        private TmxMap _map;
        private Texture2D _tilesetTexture;

        private int _tileWidth;
        private int _tileHeight;
        public List<Rectangle> CollisionsRect { get; private set; } = new();
        private Dictionary<int, Texture2D> _tilesetTextures = new();

        public List<Npc> Npcs {get; private set;} = new();

        public TiledMap(GraphicsDevice graphicsDevice, string tileMapPath, ContentManager content)
        {
            _map = new TmxMap(tileMapPath);
            

            Npcs = NpcLoader.LoadNpcDataJson("Content/npcs.json", 64, content);

            _tileWidth = _map.TileWidth;   
            _tileHeight = _map.TileHeight; 

            string mapDirectory = Path.GetDirectoryName(tileMapPath) ?? "Content/maps";

            foreach(var tileset in _map.Tilesets)
            {
                string imageFileName = Path.GetFileName(tileset.Image.Source);
                string imagePath = Path.Combine(mapDirectory, imageFileName);
                using (var stream = File.OpenRead(imagePath))
                {
                    Texture2D texture = Texture2D.FromStream(graphicsDevice, stream);
                    _tilesetTextures.Add(tileset.FirstGid, texture);
                }
            }
            

            GenerateCollisions();
        }

        private TmxTileset GetTilesetForGid(int gid)
        {
            for(int i = _map.Tilesets.Count - 1; i>=0; i--)
            {
                if(gid >= _map.Tilesets[i].FirstGid) return _map.Tilesets[i];
            }
            return null;
        }

        private void GenerateCollisions()
        {
            CollisionsRect.Clear();
           

            foreach (var layer in _map.Layers)
            {
                foreach (var tile in layer.Tiles)
                {
                    if (tile.Gid == 0) continue;

                    var tileset = GetTilesetForGid(tile.Gid);

                    int tileFrame = tile.Gid - tileset.FirstGid;

                    if (tileset.Tiles.TryGetValue(tileFrame, out var tilesetTile))
                    {
                        if (tilesetTile.Properties.TryGetValue("Solid", out string solidValue) || 
                            tilesetTile.Properties.TryGetValue("solid", out solidValue))
                        {
                            if (bool.TryParse(solidValue, out bool isSolid) && isSolid)
                            {
                                int x = tile.X * _tileWidth;
                                int y = tile.Y * _tileHeight;
                                CollisionsRect.Add(new Rectangle(x, y, _tileWidth, _tileHeight));
                                
                            }
                        }
                    }
                }
            }

            if (_map.ObjectGroups.Contains("Collisions"))
            {
                foreach (var obj in _map.ObjectGroups["Collisions"].Objects)
                {
                    CollisionsRect.Add(new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height));
                }
            }
            foreach(var npc in Npcs)
            {
                CollisionsRect.Add(npc.Bounds);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_map == null)
                return;

            foreach (var layer in _map.Layers)
            {
                if (!layer.Visible)
                    continue;

                foreach (var tile in layer.Tiles)
                {
                    int gid = tile.Gid;

                    if (gid == 0)
                        continue;

                    var tileset = GetTilesetForGid(gid);
                    if(tileset == null) continue;

                    if(!_tilesetTextures.TryGetValue(tileset.FirstGid, out Texture2D texture)) continue;

                    int tileFrame = gid - tileset.FirstGid;
                    
                    if (tileFrame < 0)
                        continue;

                    int tilesetColumns = tileset.Columns ?? (texture.Width / _tileWidth);
                    int sourceColumn = tileFrame % tilesetColumns;
                    int sourceRow = tileFrame / tilesetColumns;

                    int sourceX =
                        tileset.Margin +
                        sourceColumn * (tileset.TileWidth + tileset.Spacing);

                    int sourceY =
                        tileset.Margin +
                        sourceRow * (tileset.TileHeight + tileset.Spacing);

                    Rectangle sourceRect = new(
                        sourceX,
                        sourceY,
                        _tileWidth,
                        _tileHeight
                    );

                    int mapX = tile.X * _tileWidth;
                    int mapY = tile.Y * _tileHeight;

                    Rectangle destRect = new(
                        mapX,
                        mapY,
                        _tileWidth,
                        _tileHeight
                    );

                    spriteBatch.Draw(
                        texture,
                        destRect,
                        sourceRect,
                        Color.White
                    );
                    
                }
            }
            foreach (var npc in Npcs)
            {
                
                npc.Draw(spriteBatch);
            }
        }
    }
}