#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using Windows.System.Threading;
using Windows.UI.Xaml;
using SharpDX;
#endregion

namespace FlappyBird
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FlappyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private DispatcherTimer _gameClock = new DispatcherTimer();
        
        private TimeSpan _gameClockTick = new TimeSpan(0, 0, 1);
       

        public FlappyGame() : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferHeight = Statics.GAME_HEIGHT;
            _graphics.PreferredBackBufferWidth = Statics.GAME_WIDTH;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            _gameClock.Interval = new TimeSpan(0, 0, 0, 1);
            this.Window.Title = Statics.GAME_TITLE;

            Statics.GAME_CONTENT = Content;
            Statics.GAME_GRAPHICSDEVICE = GraphicsDevice;

            Statics.GAME_BACKGROUND = new Layers.Background();
            Statics.GAME_FOREGROUND = new Layers.Foreground();

            Statics.MANAGER_FONT = new Managers.FontManager();
            Statics.MANAGER_INPUT = new Managers.InputManager();
            Statics.MANAGER_SCREEN = new Managers.ScreenManager();
            Statics.MANAGER_SOUND = new Managers.SoundManager();
            Statics.MANAGER_TEXTURES = new Managers.TextureManager();
            Statics.MANAGER_UI = new Managers.UIManager();

            Statics.GAME_CLOCK = _gameClock;
            _gameClock.Tick += OnGameClock_Event;
            _gameClock.Stop();

            base.Initialize();
        }

        private void OnGameClock_Event(object sender, object e)
        {
            Statics.TIME_ACTUALGAMETIME += _gameClockTick;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Statics.GAME_SPRITEBATCH = _spriteBatch;

            Statics.GAME_BACKGROUND.LoadContent();
            Statics.GAME_FOREGROUND.LoadContent();

            Statics.MANAGER_FONT.LoadContent();
            Statics.MANAGER_TEXTURES.LoadContent();
            Statics.MANAGER_SCREEN.LoadContent();
            Statics.MANAGER_SOUND.LoadContent();
            Statics.MANAGER_UI.LoadContent();

            foreach (Screens.Screen screen in Statics.MANAGER_SCREEN.Stack.Values)
            {
                screen.LoadContent();
            }

            Statics.SCREEN_CURRENT = Statics.MANAGER_SCREEN.Stack["Title"];
            //Statics.GAME_STATE = Statics.STATE.Playing;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.F12))
                _graphics.ToggleFullScreen();

            // Check the current game state
            switch (Statics.GAME_STATE)
            {
                case Statics.STATE.Exit:
                    {
                        Statics.GAME_CLOCK.Stop();
                        

                        break;
                    }
                case Statics.STATE.GameOver:
                    {
                        Statics.GAME_CLOCK.Stop();
                        SetBackgroundLayerScrolling(false);

                        break;
                    }
                case Statics.STATE.Loading:
                    {
                        Statics.GAME_CLOCK.Stop();
                        SetBackgroundLayerScrolling(true);

                        break;
                    }
                case Statics.STATE.Paused:
                    {
                        Statics.GAME_CLOCK.Stop();
                        SetBackgroundLayerScrolling(false);

                        break;
                    }
                case Statics.STATE.Playing:
                    {
                        if (!Statics.GAME_CLOCK.IsEnabled)
                            Statics.GAME_CLOCK.Start();

                        Statics.GAME_GAMETIME = gameTime;
                        SetBackgroundLayerScrolling(true);

                        break;
                    }
            }

            Statics.MANAGER_INPUT.Update();
            Statics.MANAGER_UI.Update();

            Statics.SCREEN_CURRENT.Update();
                    Statics.GAME_BACKGROUND.Update();
            Statics.GAME_FOREGROUND.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            Statics.GAME_BACKGROUND.Draw();
            Statics.SCREEN_CURRENT.Draw();
            Statics.GAME_FOREGROUND.Draw();

            Statics.MANAGER_UI.Draw();

           

            base.Draw(gameTime);
        }

        private void SetBackgroundLayerScrolling(bool isScrolling)
        {
            foreach (ParallaxBackground layer in Statics.GAME_BACKGROUND.BackgroundLayer_Stack.Values)
            {
                layer.IsScrolling = isScrolling;
            }

            foreach (ParallaxBackground layer in Statics.GAME_FOREGROUND.ForegroundLayer_Stack.Values)
            {
                layer.IsScrolling = isScrolling;
            }
        }
    }
}
