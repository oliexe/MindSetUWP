using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;

namespace FlappyBird
{
    class Statics
    {
        public enum STATE
        {
            Exit,
            Loading,
            Playing,
            Paused,
            GameOver
        }

        public enum STAGE
        {
            None = 0,
            Fire = 1,
            Water = 2,
            Air = 3
        }

        public enum WORLD
        {
            Bullets,
            Paratroopas,
            Pipes
        }

       

        public static int GAME_WIDTH = 1920;
        public static int GAME_HEIGHT = 900;

        public static int GAME_FLOOR = GAME_HEIGHT - 90;

        public static string GAME_TITLE = "MIND BIRD";

        public static STATE GAME_STATE = STATE.GameOver;

        public static DispatcherTimer GAME_CLOCK;

        public static int GAME_SCORE = 0;
        public static WORLD GAME_WORLD;
        public static int GAME_LEVEL = 1;
        public static int EEG_ATTENTION = 0;
        public static int EEG_SIGNAL = 255;
        public static int GAME_HIGHSCORE = 0;
        public static bool GAME_NEWHIGHSCORE = false;

        public static GameTime GAME_GAMETIME;
        public static SpriteBatch GAME_SPRITEBATCH;
        public static ContentManager GAME_CONTENT;
        public static GraphicsDevice GAME_GRAPHICSDEVICE;

        public static bool GAME_USESLOWMODE = false;
        public static float GAME_SLOWMODERATE = .5f;

        public static Layers.Background GAME_BACKGROUND;
        public static Layers.Foreground GAME_FOREGROUND;

        public static TimeSpan TIME_ACTUALGAMETIME = TimeSpan.Zero;

        public static Managers.FontManager MANAGER_FONT;
        public static Managers.InputManager MANAGER_INPUT;
        public static Managers.ScreenManager MANAGER_SCREEN;
        public static Managers.SoundManager MANAGER_SOUND;
        public static Managers.TextureManager MANAGER_TEXTURES;
        public static Managers.UIManager MANAGER_UI;

        public static Random GAME_RANDOM = new Random();
        public static float GAME_SPEED_DIFFICULTY = 1;
        public static int GAME_HITBOX_DIFFICULTY = 20;

        public static Texture2D TEXTURE_PIXEL;

        public static bool COLLISION_USESLOPPY = false;

        public static Color COLOR_HITBOX = new Color(Color.Red, 0.75f);
        public static Color COLOR_DEAD = new Color(Color.Red, 0.5f);
        public static Color COLOR_PAUSED = new Color(Color.Indigo, 0.3f);
        public static Color COLOR_TITLE = new Color(Color.Black, 0.3f);

        public static Screens.Screen SCREEN_CURRENT;

        public static bool DEBUG_SHOWHITBOX = false;
        public static bool DEBUG_SHOWTEXT = false;

        public static int DEBUG_ENTITIES = 0;
        public static Vector2 DEBUG_PLAYER = Vector2.Zero;
        public static float DEBUG_FPS = 0f;
    }
}
