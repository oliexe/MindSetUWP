using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Managers
{
    public class FontManager
    {
        private Dictionary<string, SpriteFont> _library;
        public Dictionary<string, SpriteFont> Library { get { return _library; } }

        public FontManager()
        {
            Statics.MANAGER_FONT = this;

            _library = new Dictionary<string, SpriteFont>();
        }

        public void LoadContent()
        {
            
            _library.Add("Extra", Statics.GAME_CONTENT.Load<SpriteFont>("gameFont_ExtraLarge"));
            _library.Add("Large", Statics.GAME_CONTENT.Load<SpriteFont>("gameFont_Large"));
            _library.Add("Regular", Statics.GAME_CONTENT.Load<SpriteFont>("gameFont"));
            _library.Add("Small", Statics.GAME_CONTENT.Load<SpriteFont>("gameFont_Small"));
        }
    }
}
