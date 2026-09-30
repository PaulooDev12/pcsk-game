#nullable enable
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace pcsk.src.Data
{
    public class NpcData
    {
        public string Name { get; set; } = string.Empty;
        public int TileX { get; set; }
        public int TileY { get; set; }

        [JsonPropertyName("dialogue")]
        public List<string> Dialogues { get; set; } = new();

        [JsonPropertyName("sprite")]
        public string Sprite {get;set;} = "npctile";

        [JsonPropertyName("healAmount")]
        public int HealAmount {get; set;} = 0;

        [JsonPropertyName("giveItem")]        
        public string? GiveItem {get; set;} = null; 

    }
}