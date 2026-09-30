using System.Collections.Generic;
using pcsk.src.Inputs;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using System;

namespace pcsk.src.Interactions.Handlers
{
    public static class DialogueManager
    {
        public static bool IsActive {get; private set;} = false;
        private static string _speakerName;
        private static List<string> _lines = new();
        private static int _currentLineIndex = 0;
        private static string _fullText = "";
        private static string _visibleText = "";
        private static double _timePerChar = 0.03;
        private static double _charTimer = 0;
        private static int _currentCharIndex = 0;

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

         SetupCurrentLine();
        }

        private static void SetupCurrentLine()
        {
            _fullText = _lines[_currentLineIndex];
            _visibleText = "";
            _currentCharIndex = 0;
            _charTimer = 0;
        }

        public static void Update(GameTime gameTime)
        {
            if(!IsActive) return;
            
            if(_currentCharIndex < _fullText.Length)
            {
                _charTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if(_charTimer >= _timePerChar)
                {
                    _currentCharIndex++;
                    _visibleText = _fullText.Substring(0, _currentCharIndex);
                    _charTimer -= _timePerChar;
                }
            }
            if(Input.IsKeyPressed(Keys.E) || Input.IsKeyPressed(Keys.Space))
            {
                if(_currentCharIndex < _fullText.Length)
                {
                    _currentCharIndex = _fullText.Length;
                    _visibleText = _fullText;
                }
                else
                {
                    _currentLineIndex++;
                    if(_currentLineIndex >= _lines.Count)
                    {
                        
                        IsActive = false;
                    }
                    else
                    {
                        SetupCurrentLine();
                    }
                }
            }
        }
        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D boxTexture, int screenWidth, int screenHeight)
        {
            if(!IsActive) return;

            Rectangle boxRect = new Rectangle(50, screenHeight - 150, screenWidth - 150, 120);

            spriteBatch.Draw(boxTexture, boxRect, Color.Black * 0.85f);

            string nameText = $"[{_speakerName}]";
            spriteBatch.DrawString(font, nameText, new Vector2(boxRect.X + 15, boxRect.Y + 10), Color.Gold);
            spriteBatch.DrawString(font, _visibleText, new Vector2(boxRect.X + 15, boxRect.Y + 40), Color.White);
                
        }
    }
}