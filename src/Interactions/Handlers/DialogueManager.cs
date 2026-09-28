using System.Collections.Generic;
using inputs;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using System;

namespace interactions
{
    public static class DialogueManager
    {
        public static bool IsActive {get; private set;} = false;
        private static string _speakerName;
        private static List<string> _lines;
        private static int _currentLineIndex;

        public static void StartDialogue(string name, List<string> lines)
        {
         if(lines == null || lines.Count == 0)
        {
            Console.WriteLine("Erro: Os dialogos do json não foram carregados corretamente");  
            return;  
        } 

         _speakerName = name;
         _lines = lines;
         _currentLineIndex = 0;
         IsActive = true;
        }

        public static void Update()
        {
            if(!IsActive) return;

            if(Input.isKeyPressed(Keys.E) || Input.isKeyPressed(Keys.Space))
            {
                
                if(_currentLineIndex + 1 >= _lines.Count)
                {
                    IsActive = false;
                }

                _currentLineIndex++;
            }
        }
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D boxTexture, int screenWidth, int screenHeight)
        {
            if(!IsActive) return;

            Rectangle boxRect = new Rectangle(50, screenHeight - 150, screenWidth - 150, 120);

            spriteBatch.Draw(boxTexture, boxRect, Color.Black * 0.85f);

            string nameText = $"[{_speakerName}]";
            string currentText = _lines[_currentLineIndex];
            spriteBatch.DrawString(font, nameText, new Vector2(boxRect.X + 15, boxRect.Y + 10), Color.Gold);
            spriteBatch.DrawString(font, currentText, new Vector2(boxRect.X + 15, boxRect.Y + 40), Color.White);
                
        }
    }
}