using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Managers
{
    class TextureManager
    {
        public Dictionary<string, Texture2D> Textures;
        public Dictionary<string, Texture2D> AnimatedTextures;

        public TextureManager()
        {
            Statics.MANAGER_TEXTURES = this;

            Textures = new Dictionary<string, Texture2D>();
            AnimatedTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent()
        {
            #region Entity Textures

            AnimatedTextures.Add("Entity\\Bird", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_bird_animated"));
            AnimatedTextures.Add("Entity\\Paratroopa", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_paratroopa_animated"));

            Textures.Add("Entity\\Bird", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_bird"));
            Textures.Add("Entity\\DeadBird", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_bird_dead"));
            Textures.Add("Entity\\Boomba", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_boomba"));
            Textures.Add("Entity\\Bullet", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_bullet"));
            Textures.Add("Entity\\Paratroopa", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_paratroopa"));
            Textures.Add("Entity\\Pipe", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Entity\\flappy_pipe"));

            #endregion

            #region UI Textures

            Textures.Add("UI\\Button", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_button"));
            Textures.Add("UI\\ButtonExit", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_button_exit"));
            Textures.Add("UI\\ButtonRestart", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_button_restart"));
            Textures.Add("UI\\ButtonPipe", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_level_pipes"));
            Textures.Add("UI\\ButtonBullet", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_level_bullet"));
            Textures.Add("UI\\ButtonParatroopa", Statics.GAME_CONTENT.Load<Texture2D>("Textures\\Button\\flappy_level_paratroopa"));
            
            #endregion
        }
    }
}
