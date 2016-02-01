using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MindSetUWA;

using FlappyBird.Entities;

namespace FlappyBird.Screens
{


    class GameScreen : Screen
    {
        private Bird _entityBird;
        private List<Entity> _entityObstacles;

        public MindSetConnection MindSetConn = new MindSetConnection();

        private TimeSpan _previousRefreshTime;
        private TimeSpan _refreshTime;
        private float _refreshRate = 2500;

        private TimeSpan _previousDifficultyTime;
        private TimeSpan _difficultyTime;
        private float _difficultyRate = 90000;

        private TimeSpan _slowModeTime = TimeSpan.Zero;
        
        private bool _isCheckingCollision = false;

        public GameScreen()
        {
            _previousRefreshTime = TimeSpan.Zero;
            _refreshTime = TimeSpan.FromMilliseconds(_refreshRate);

            _previousDifficultyTime = TimeSpan.Zero;
            _difficultyTime = TimeSpan.FromMilliseconds(_difficultyRate);
        }

        public override void LoadContent()
        {
            _entityBird = new Entities.Bird(Entities.Entity.Type.Bird);

            _entityObstacles = new List<Entity>();
            _entityObstacles.Add(new Entity(Entity.Type.None));

            MindSetConn.ConnectBluetooth("MindWave Mobile");

            base.LoadContent();            
          
        }

        public override void UnloadContent()
        {

            base.UnloadContent();
        }

        public override void Reset()
        {
            _previousRefreshTime = TimeSpan.Zero;
            _refreshTime = TimeSpan.FromMilliseconds(_refreshRate);

            _previousDifficultyTime = TimeSpan.Zero;
            _difficultyTime = TimeSpan.FromMilliseconds(_difficultyRate);

            _entityBird = new Entities.Bird(Entities.Entity.Type.Bird);

            _entityObstacles.Clear();
            _entityObstacles.Add(new Entities.Entity(Entities.Entity.Type.None));
            
            Statics.GAME_SPEED_DIFFICULTY = 1;
            Statics.GAME_LEVEL = 1;
            Statics.GAME_SCORE = 0;
            Statics.GAME_NEWHIGHSCORE = false;
            Statics.TIME_ACTUALGAMETIME = TimeSpan.Zero;
            pathlist.Clear();
            list_position = 0;

            Statics.GAME_BACKGROUND.ResetBackgrounds();
            Statics.GAME_FOREGROUND.ResetBackgrounds();
        }

        List<int> pathlist = new List<int>();
        int list_position = 0;

        public override void Update()
        {
            CheckForInput();


            Statics.EEG_SIGNAL = 0;
            Statics.EEG_ATTENTION = 100;

            //if (MindSetConn.RealtimeData.Quality > 0)
            //{
            //    Statics.GAME_STATE = Statics.STATE.Paused;
            //}
            //else
            //{
            //    Statics.GAME_STATE = Statics.STATE.Playing;
            //}



            if (Statics.GAME_STATE == Statics.STATE.Playing)
            {
                // MindWave Controller 
                if (pathlist.Count > 0)
                {
                    if (Math.Ceiling(_entityBird.Position.Y) != pathlist[list_position])
                    {
                    if (Math.Ceiling(_entityBird.Position.Y) >= pathlist[list_position])
                    {
                            if (Statics.EEG_ATTENTION > 40) _entityBird._ySpeed = -1.000f;
                            if (Statics.EEG_ATTENTION > 45) _entityBird._ySpeed = -1.000f;
                            if (Statics.EEG_ATTENTION > 50) _entityBird._ySpeed = -1.200f;
                            if (Statics.EEG_ATTENTION > 55) _entityBird._ySpeed = -1.200f;
                            if (Statics.EEG_ATTENTION > 60) _entityBird._ySpeed = -1.400f;
                            if (Statics.EEG_ATTENTION > 65) _entityBird._ySpeed = -1.800f;
                            if (Statics.EEG_ATTENTION > 75) _entityBird._ySpeed = -2.000f;
                            if (Statics.EEG_ATTENTION > 80) _entityBird._ySpeed = -2.400f;
                            if (Statics.EEG_ATTENTION > 85) _entityBird._ySpeed = -3.000f;
                            if (Statics.EEG_ATTENTION > 90) _entityBird._ySpeed = -3.500f;
                            if (Statics.EEG_ATTENTION > 95) _entityBird._ySpeed = -4.000f;
                        }
                    else
                        {
                            if (Statics.EEG_ATTENTION > 40) _entityBird._ySpeed = +1.000f;
                            if (Statics.EEG_ATTENTION > 45) _entityBird._ySpeed = +1.000f;
                            if (Statics.EEG_ATTENTION > 50) _entityBird._ySpeed = +1.200f;
                            if (Statics.EEG_ATTENTION > 55) _entityBird._ySpeed = +1.200f;
                            if (Statics.EEG_ATTENTION > 60) _entityBird._ySpeed = +1.400f;
                            if (Statics.EEG_ATTENTION > 65) _entityBird._ySpeed = +1.800f;
                            if (Statics.EEG_ATTENTION > 75) _entityBird._ySpeed = +2.000f;
                            if (Statics.EEG_ATTENTION > 80) _entityBird._ySpeed = +2.400f;
                            if (Statics.EEG_ATTENTION > 85) _entityBird._ySpeed = +3.000f;
                            if (Statics.EEG_ATTENTION > 90) _entityBird._ySpeed = +3.500f;
                            if (Statics.EEG_ATTENTION > 95) _entityBird._ySpeed = +4.000f;
                        }
                }
                else
                {
                    _entityBird._ySpeed = 0;
                }
                }
               


                // Increase game difficulty
                if (Statics.TIME_ACTUALGAMETIME - _previousDifficultyTime > _difficultyTime)
                {
                    _previousDifficultyTime = Statics.TIME_ACTUALGAMETIME;

                    Statics.GAME_SPEED_DIFFICULTY += 0.5f;
                    Statics.GAME_LEVEL++;

                    foreach (ParallaxBackground layer in Statics.GAME_BACKGROUND.BackgroundLayer_Stack.Values)
                    {
                        layer.MoveSpeed -= Statics.GAME_SPEED_DIFFICULTY;
                    }

                    foreach (ParallaxBackground layer in Statics.GAME_FOREGROUND.ForegroundLayer_Stack.Values)
                    {
                        layer.MoveSpeed -= Statics.GAME_SPEED_DIFFICULTY;
                    }

                    _refreshTime = TimeSpan.FromMilliseconds(_refreshTime.TotalMilliseconds - (Statics.GAME_SPEED_DIFFICULTY * 5f));

                     }

                // Add new obstacle
                if (Statics.TIME_ACTUALGAMETIME - _previousRefreshTime > _refreshTime && Statics.EEG_SIGNAL == 0)
                {
                    _previousRefreshTime = Statics.TIME_ACTUALGAMETIME;

                    switch (Statics.GAME_WORLD)
                    {
                        case Statics.WORLD.Pipes:
                            {
                                // Add Pipes
                                _entityObstacles.Add(new Entities.Pipe(Entity.Type.Pipe, Statics.GAME_SPEED_DIFFICULTY));

                                List<Rectangle> entita = _entityObstacles[_entityObstacles.Count - 1].Bounds;
                                pathlist.Add(entita[entita.Count - 1].Y - 36);

                                break;
                            }
                      
                    }
                }

                foreach (Entities.Entity entity in _entityObstacles.ToList())
                {
                    List<Rectangle> bounds = entity.Bounds;

                    if (!_isCheckingCollision)
                    {
                        foreach (Rectangle bound in bounds)
                        {
                            _isCheckingCollision = true;

                            if (Statics.COLLISION_USESLOPPY)
                            {
                                if (Helpers.Collision.IsSloppyCollision(bound, _entityBird.Bounds[0]))
                                {
                                    SetGameState(Statics.STATE.GameOver);
                                }
                                else if (_entityBird.Position.X > bound.X + bound.Width && !entity.IsScored)
                                {
                                    Statics.MANAGER_SOUND.Play("Player\\Score");
                                    list_position++;
                                    entity.IsScored = true;
                                    Statics.GAME_SCORE++;
                                   
                                }
                            }
                            else
                            {
                                if (Helpers.Collision.IsPixelCollision(bound, _entityBird.Bounds[0], entity.ColorData, _entityBird.ColorData))
                                {
                                    SetGameState(Statics.STATE.GameOver);
                                }
                                else if (_entityBird.Position.X > bound.X + 50 + bound.Width && !entity.IsScored)
                                {
                                    Statics.MANAGER_SOUND.Play("Player\\Score");
                                    list_position++;
                                    entity.IsScored = true;
                                    Statics.GAME_SCORE++;
                                }
                            }

                            if (entity.Position.X <= -entity.Width)
                                this._entityObstacles.Remove(entity);

                            _isCheckingCollision = false;
                        }
                    }
                }

                if (Statics.GAME_SCORE > Statics.GAME_HIGHSCORE)
                {
                    Statics.GAME_HIGHSCORE = Statics.GAME_SCORE;
                    Statics.GAME_NEWHIGHSCORE = true;
                }

                Statics.DEBUG_ENTITIES = _entityObstacles.Count;
                Statics.DEBUG_PLAYER = _entityBird.Position;

                _entityBird.Update();

                foreach (var obstacle in _entityObstacles)
                {
                    obstacle.Update();
                }
            }

            base.Update();
        }

        public override void Draw()
        {
            _entityBird.Draw();

            foreach (var obstacle in _entityObstacles)
            {
                obstacle.Draw();
            }

            base.Draw();
        }

        private void CheckForInput()
        {
            // Input : Keyboard

            if (Statics.MANAGER_INPUT.IsGamepadPressed(Buttons.Back) || Statics.MANAGER_INPUT.IsKeyPressed(Keys.Escape))
            {
                SetGameState(Statics.STATE.Loading);
                Statics.SCREEN_CURRENT = Statics.MANAGER_SCREEN.Stack["Title"];
                Statics.MANAGER_SCREEN.Stack["Game"].Reset();
            }

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.C))
            {
                Statics.EEG_ATTENTION = 100;
                Statics.EEG_SIGNAL = 0;
            }

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.Up))
            {
                _previousDifficultyTime = Statics.TIME_ACTUALGAMETIME;

                Statics.GAME_SPEED_DIFFICULTY += 0.5f;
                Statics.GAME_LEVEL++;

                foreach (ParallaxBackground layer in Statics.GAME_BACKGROUND.BackgroundLayer_Stack.Values)
                {
                    layer.MoveSpeed -= Statics.GAME_SPEED_DIFFICULTY;
                }

                foreach (ParallaxBackground layer in Statics.GAME_FOREGROUND.ForegroundLayer_Stack.Values)
                {
                    layer.MoveSpeed -= Statics.GAME_SPEED_DIFFICULTY;
                }

                _refreshTime = TimeSpan.FromMilliseconds(_refreshTime.TotalMilliseconds - (Statics.GAME_SPEED_DIFFICULTY * 5f));

            }

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.Down))
            {
                _previousDifficultyTime = Statics.TIME_ACTUALGAMETIME;

                Statics.GAME_SPEED_DIFFICULTY -= 0.5f;
                Statics.GAME_LEVEL--;

                foreach (ParallaxBackground layer in Statics.GAME_BACKGROUND.BackgroundLayer_Stack.Values)
                {
                    layer.MoveSpeed += Statics.GAME_SPEED_DIFFICULTY;
                }

                foreach (ParallaxBackground layer in Statics.GAME_FOREGROUND.ForegroundLayer_Stack.Values)
                {
                    layer.MoveSpeed += Statics.GAME_SPEED_DIFFICULTY;
                }

                _refreshTime = TimeSpan.FromMilliseconds(_refreshTime.TotalMilliseconds - (Statics.GAME_SPEED_DIFFICULTY * 5f));

            }

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.F3))
                Statics.GAME_USESLOWMODE = Statics.GAME_USESLOWMODE ? false : true;

            if (Statics.MANAGER_INPUT.IsGamepadPressed(Buttons.Start) || Statics.MANAGER_INPUT.IsRightMouseClicked() || Statics.MANAGER_INPUT.IsKeyPressed(Keys.Enter))
            {
                switch (Statics.GAME_STATE)
                {
                    case Statics.STATE.GameOver:
                        {
                            SetGameState(Statics.STATE.Playing);
                            Statics.MANAGER_SCREEN.Stack["Game"].Reset();
                            break;
                        }
                    case Statics.STATE.Paused:
                        {
                            Statics.GAME_STATE = Statics.STATE.Playing;
                            break;
                        }
                    case Statics.STATE.Playing:
                        {
                            Statics.GAME_STATE = Statics.STATE.Paused;
                            break;
                        }
                }
            }
        }

        private void SetGameState(Statics.STATE newState)
        {
            Statics.GAME_STATE = newState;
        }
    }
}
