using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Managers
{
    class UIManager
    {
        public Dictionary<string, Vector2> TextVectors;
        public Dictionary<string, Vector2> TextureVectors;

        private float _screenCenterX;
        private float _screenCenterY;

        private float _screenThirdX;
        private float _screenThirdY;

        private float _screenQuarterX;
        private float _screenQuarterY;

        public UIManager()
        {
            Statics.MANAGER_UI = this;

            TextVectors = new Dictionary<string, Vector2>();
            TextureVectors = new Dictionary<string, Vector2>();

            _screenCenterX = Statics.GAME_WIDTH / 2;
            _screenCenterY = Statics.GAME_HEIGHT / 2;

            _screenThirdX = Statics.GAME_WIDTH / 3;
            _screenThirdY = Statics.GAME_HEIGHT / 3;

            _screenQuarterX = Statics.GAME_WIDTH / 4;
            _screenQuarterY = Statics.GAME_HEIGHT / 4;
        }

        public void LoadContent()
        {
            #region Screen : Title

            TextVectors.Add("Title\\Title", Statics.MANAGER_FONT.Library["Large"].MeasureString(Statics.GAME_TITLE) / 2);
            TextVectors.Add("Title\\Desc1", Statics.MANAGER_FONT.Library["Regular"].MeasureString("Hra zalozena na knihovne MindwaveUWP pro Windows 10") / 2);
            TextVectors.Add("Title\\Desc2", Statics.MANAGER_FONT.Library["Regular"].MeasureString("Hra zalozena na knihovne MindwaveUWP pro Windows 10") / 2);

            TextVectors.Add("Title\\Start", Statics.MANAGER_FONT.Library["Regular"].MeasureString("Start") / 2);
            TextVectors.Add("Title\\Exit", Statics.MANAGER_FONT.Library["Regular"].MeasureString("Exit") / 2);

            TextureVectors.Add("Title\\Start", new Vector2((Statics.GAME_WIDTH / 2) - (Statics.MANAGER_TEXTURES.Textures["UI\\Button"].Width / 2), (Statics.GAME_HEIGHT / 3) * 2 - (Statics.MANAGER_TEXTURES.Textures["UI\\Button"].Height / 2) - 60));
            TextureVectors.Add("Title\\Exit", new Vector2((Statics.GAME_WIDTH / 2) - (Statics.MANAGER_TEXTURES.Textures["UI\\Button"].Width / 2), (Statics.GAME_HEIGHT / 3) * 2 - (Statics.MANAGER_TEXTURES.Textures["UI\\Button"].Height / 2) + 60));

            #endregion

            #region Screen : Level

            TextVectors.Add("Level\\Title", Statics.MANAGER_FONT.Library["Large"].MeasureString("Select level") / 2);

            TextureVectors.Add("Level\\Pipe", new Vector2((float)(_screenQuarterX - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonPipe"].Width / 2), (float)(_screenThirdY * 2 - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonPipe"].Height / 2) - 60));
            TextureVectors.Add("Level\\Bullet", new Vector2((float)(_screenCenterX - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonBullet"].Width / 2), (float)(_screenThirdY * 2 - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonBullet"].Height / 2) - 60));
            TextureVectors.Add("Level\\Paratroopa", new Vector2((float)(Statics.GAME_WIDTH - _screenQuarterX - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonParatroopa"].Width / 2), (float)(_screenThirdY * 2 - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonParatroopa"].Height / 2) - 60));

            #endregion

            #region Screen : Game

            TextVectors.Add("Game\\Score", new Vector2(20, 20));
            TextVectors.Add("Game\\Level", new Vector2(20, 50));
            TextVectors.Add("Game\\Soustredeni", new Vector2(620, 830));
            TextVectors.Add("Game\\kvalita", new Vector2(20, 80));

            #endregion

            #region Screen : Paused

            TextVectors.Add("Pause\\Title", Statics.MANAGER_FONT.Library["Large"].MeasureString("Paused") / 2);
            TextVectors.Add("Pause\\Continue", Statics.MANAGER_FONT.Library["Regular"].MeasureString("Press ENTER to continue") / 2);

            TextureVectors.Add("Pause\\Restart", new Vector2(_screenThirdX, _screenThirdY * 2));
            TextureVectors.Add("Pause\\Exit", new Vector2((float)(Statics.GAME_WIDTH - _screenThirdX - Statics.MANAGER_TEXTURES.Textures["UI\\ButtonExit"].Width), _screenThirdY * 2));

            #endregion

            #region Screen : Game over

            TextVectors.Add("GameOver\\Title", Statics.MANAGER_FONT.Library["Large"].MeasureString("Game Over") / 2);
            TextVectors.Add("GameOver\\Score", Statics.MANAGER_FONT.Library["Extra"].MeasureString(Statics.GAME_SCORE.ToString("00")) / 2);
            TextVectors.Add("GameOver\\HighScore", Statics.MANAGER_FONT.Library["Large"].MeasureString("New high score") / 2);
            TextVectors.Add("GameOver\\Restart", Statics.MANAGER_FONT.Library["Large"].MeasureString("Restart") / 2);
            TextVectors.Add("GameOver\\TimeFlapped", Statics.MANAGER_FONT.Library["Regular"].MeasureString(string.Format("You flapped for {0} seconds", Statics.TIME_ACTUALGAMETIME.TotalSeconds.ToString("00"))) / 2);

            #endregion
        }

        public void Update()
        {
            // Get common dimensions
            _screenCenterX = Statics.GAME_WIDTH / 2;
            _screenCenterY = Statics.GAME_HEIGHT / 2;

            _screenThirdX = Statics.GAME_WIDTH / 3;
            _screenThirdY = Statics.GAME_HEIGHT / 3;

            _screenQuarterX = Statics.GAME_WIDTH / 4;
            _screenQuarterY = Statics.GAME_HEIGHT / 4;

            // Get 'Score' position
            TextVectors["GameOver\\Score"] = Statics.MANAGER_FONT.Library["Extra"].MeasureString(Statics.GAME_SCORE.ToString("00")) / 2;

            // Get 'Total time flapped' position
            TextVectors["GameOver\\TimeFlapped"] = Statics.MANAGER_FONT.Library["Regular"].MeasureString(string.Format("You flapped for {0} seconds", Statics.TIME_ACTUALGAMETIME.TotalSeconds.ToString("00"))) / 2;
            
            // Update cursor
            Statics.MANAGER_SCREEN.Stack["Cursor"].Update();
        }

        public void Draw()
        {
            Statics.GAME_SPRITEBATCH.Begin();

            if (Statics.SCREEN_CURRENT == Statics.MANAGER_SCREEN.Stack["Title"])
            {
                //Statics.GAME_SPRITEBATCH.Draw(Statics.TEXTURE_PIXEL, new Rectangle(0, 0, Statics.GAME_WIDTH, Statics.GAME_HEIGHT), Statics.COLOR_TITLE);
                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Large"], Statics.GAME_TITLE, new Vector2(_screenCenterX, _screenQuarterY), Color.BurlyWood, 0.0f, TextVectors["Title\\Title"], 1.0f, SpriteEffects.None, 1.0f);
                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Small"], "Ukazkova aplikace vyuzivajici knihovnu MindSetUWP pro Windows Universal", new Vector2(_screenCenterX + 10, _screenQuarterY + 70), Color.BlueViolet, 0.0f, TextVectors["Title\\Desc1"], 1.0f, SpriteEffects.None, 1.0f);
                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], "Ukazka Bakalarske Prace - Ondrej Rehacek - REH0063", new Vector2(_screenCenterX, _screenQuarterY + 120), Color.White, 0.0f, TextVectors["Title\\Desc2"], 1.0f, SpriteEffects.None, 1.0f);

                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], "olirehacek.github.io -- Fakulta elektrotechniky a informatiky vsb", new Vector2(_screenCenterX - 50, Statics.GAME_HEIGHT - 50), Color.White, 0.0f, TextVectors["Title\\Desc2"], 1.0f, SpriteEffects.None, 1.0f);

                Statics.GAME_SPRITEBATCH.Draw(Statics.MANAGER_TEXTURES.Textures["UI\\Button"], TextureVectors["Title\\Start"], Color.White);
                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], " Start", new Vector2(_screenCenterX, _screenThirdY * 2 - 60), Color.White, 0.0f, TextVectors["Title\\Start"], 1.0f, SpriteEffects.None, 1.0f);

                Statics.GAME_SPRITEBATCH.Draw(Statics.MANAGER_TEXTURES.Textures["UI\\Button"], TextureVectors["Title\\Exit"], Color.White);
                Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], "Konec", new Vector2(_screenCenterX, _screenThirdY * 2 + 60), Color.White, 0.0f, TextVectors["Title\\Exit"], 1.0f, SpriteEffects.None, 1.0f);

            }
           
            else if (Statics.SCREEN_CURRENT == Statics.MANAGER_SCREEN.Stack["Game"])
            {
                if (Statics.GAME_STATE == Statics.STATE.GameOver)
                {
                    //Statics.GAME_SPRITEBATCH.Draw(Statics.TEXTURE_PIXEL, new Rectangle(0, 0, Statics.GAME_WIDTH, Statics.GAME_HEIGHT), Statics.COLOR_DEAD);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Large"], "Game Over !", new Vector2(_screenCenterX, _screenQuarterY), Color.Black, 0.0f, TextVectors["GameOver\\Title"], 1.0f, SpriteEffects.None, 1.0f);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Extra"], Statics.GAME_SCORE.ToString("00"), new Vector2(_screenCenterX, _screenCenterY - 30), Color.Red, 0.0f, TextVectors["GameOver\\Score"], 1.0f, SpriteEffects.None, 1.0f);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], string.Format("Soustredil {0} sekund !", Statics.TIME_ACTUALGAMETIME.TotalSeconds.ToString("00")), new Vector2(_screenCenterX, _screenCenterY + 160), Color.DarkRed, 0.0f, TextVectors["GameOver\\TimeFlapped"], 1.0f, SpriteEffects.None, 1.0f);

                    if (Statics.GAME_NEWHIGHSCORE)
                        Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Large"], "Novy rekord !!!", new Vector2(_screenCenterX, _screenCenterY + 100), Color.Red, 0.0f, TextVectors["GameOver\\HighScore"], 1.0f, SpriteEffects.None, 1.0f);
                }

                if (Statics.GAME_STATE == Statics.STATE.Paused)
                {
                    //Statics.GAME_SPRITEBATCH.Draw(Statics.TEXTURE_PIXEL, new Rectangle(0, 0, Statics.GAME_WIDTH, Statics.GAME_HEIGHT), Statics.COLOR_PAUSED);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Large"], "Nemas headset na hlave", new Vector2(_screenCenterX - 50, _screenQuarterY), Color.White, 0.0f, TextVectors["Pause\\Title"], 1.0f, SpriteEffects.None, 1.0f);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Regular"], Statics.EEG_ATTENTION.ToString() + " Nasad si eeg headset.", new Vector2(_screenCenterX, _screenThirdY + 50), Color.White, 0.0f, TextVectors["Pause\\Continue"], 1.0f, SpriteEffects.None, 1.0f);
                }

                if (Statics.GAME_STATE == Statics.STATE.Playing)
                {
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Small"], string.Format("Score: {0}", Statics.GAME_SCORE.ToString("00")), TextVectors["Game\\Score"], Color.White);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Small"], string.Format("Level: {0}", Statics.GAME_LEVEL.ToString("00")), TextVectors["Game\\Level"], Color.White);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Large"], string.Format("Soustredeni: {0}", Statics.EEG_ATTENTION.ToString("00")), TextVectors["Game\\Soustredeni"], Color.Red);
                    Statics.GAME_SPRITEBATCH.DrawString(Statics.MANAGER_FONT.Library["Small"], string.Format("Kvalita signalu: {0}", Statics.EEG_SIGNAL.ToString("00")), TextVectors["Game\\kvalita"], Color.Tomato);

                }
            }


            Statics.MANAGER_SCREEN.Stack["Cursor"].Draw();
            
            Statics.GAME_SPRITEBATCH.End();
        }
    }
}
