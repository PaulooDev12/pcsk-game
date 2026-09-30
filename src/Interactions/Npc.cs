#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pcsk.src.Playeer;
using pcsk.src.Interactions.Handlers;

namespace pcsk.src.Interactions
{
    public class Npc : IInteractable
    {
        public Rectangle Bounds {get; private set;}
        public string Name {get; private set;}
        public Texture2D Texture {get; private set;}
        private readonly List<string> _dialogueLines;
        private readonly int _healAmount;
        private readonly string? _giveItem;
        private bool _hasTriggeredAction = false;

        public Npc(
        Rectangle bounds, 
        string name, 
        Texture2D texture, 
        List<string> dialogueLines, 
        int healAmount = 0, 
        string? giveItem = null)
    {
        Bounds = bounds;
        Name = name;
        Texture = texture;
        _dialogueLines = dialogueLines;
        _healAmount = healAmount;
        _giveItem = giveItem;
    }
        public void Interact()
        {
            Console.WriteLine($"Interagiu com {Name}");
            DialogueManager.StartDialogue(Name, _dialogueLines);
            ApplyEffects();
        }
        private void ApplyEffects()
        {
            if(_healAmount > 0)
            {
                PlayerProperties.Heal(_healAmount);
                Console.WriteLine($"Valor curado {_healAmount} | Vida atual do player: {PlayerProperties.CurrentLife}");

            }
            if(!string.IsNullOrEmpty(_giveItem) && !_hasTriggeredAction)
            {
                Console.WriteLine($"[Npc: {Name}] deu o item '{_giveItem}' para o jogador!");
                _hasTriggeredAction = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}