using System;
using System.Collections.Generic;

using System.IO;
using System.Text.Json;
using interactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace data
{
    public static class NpcLoader
    {
        public static List<Npc> LoadNpcDataJson(string jsonFile, int tileSize, ContentManager content)
        {
            List<Npc> npcs = new List<Npc>();
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine("Arquivo não existente");
                return npcs;  
            } 

            string jsonText = File.ReadAllText(jsonFile);

            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var npcDataList = JsonSerializer.Deserialize<List<NpcData>>(jsonText, options);

            if(npcDataList == null)
            {
                Console.WriteLine("A lista de dados é nula");
                return npcs; 
            } 

            foreach(var data in npcDataList)
            {
                Rectangle rect = new Rectangle(
                    data.TileX * tileSize,
                    data.TileY * tileSize,
                    tileSize,
                    tileSize
                );
                
                Texture2D npcTexture;
                try
                {
                    npcTexture = content.Load<Texture2D>(data.Sprite);
                }
                catch
                {
                    npcTexture = content.Load<Texture2D>("npctile");
                }

                List<string> lines = data.Dialogues ?? new List<string>();
                npcs.Add(
                new Npc(
                rect, 
                data.Name, 
                npcTexture,
                lines,
                data.HealAmount,
                data.GiveItem           
                ));

                Console.WriteLine($"[DEBUG NPC] Nome: {data.Name} | Total de Falas: {lines.Count}");
            }
          
            Console.WriteLine("Npcs carregados com sucesso!");
            return npcs;
        }
    }
}