using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird.Entities
{
    class Bird : Entity
    {
        private AnimatedSprite _bird_Sprite;
        public float _ySpeed;

        public bool UseSlowFall = false;
        public bool UseJumpBoost = false;
        public bool UseStarPower = false;

        public Bird(Type type) : base(type)
        {
            this.Texture = Statics.MANAGER_TEXTURES.Textures["Entity\\DeadBird"];
            this.Position = new Vector2(300, 500);
            this.Rotation = 0f;
            this.Scale = .75f;
            this.EntityType = type;
            this.Width = this.Texture.Width;
            this.Height = this.Texture.Height;
            this.MoveSpeed = 5f;
            this.ColorData = new Color[this.Width * this.Height];
            this.Texture.GetData(ColorData);

            Texture2D texture = Statics.MANAGER_TEXTURES.AnimatedTextures["Entity\\Bird"];

            _bird_Sprite = new AnimatedSprite();
            _bird_Sprite.Initialize(texture, this.Position, this.Rotation, 128, 128, 4, 60, Color.White, this.Scale, true);

            _ySpeed = 0f;
        }

        public override void Update()
        {
            if (Statics.GAME_STATE == Statics.STATE.Playing)
            {
               // _ySpeed += UseSlowFall ? .1f : .13f;
                

                CheckForInput();

                this.Position.Y =  MathHelper.Clamp(this.Position.Y, (this.Height * this.Scale), Statics.GAME_FLOOR + this.Height * _bird_Sprite.Scale);

                if (this.Position.Y < Statics.GAME_FLOOR)
                {
                    this.Position.Y += _ySpeed;
                }
                else
                {
                    Statics.GAME_STATE = Statics.STATE.GameOver;
                }

                this.Rotation = (float)Math.Atan2(_ySpeed, 10);

                _bird_Sprite.Position = this.Position;
                _bird_Sprite.Rotation = this.Rotation;
                _bird_Sprite.Update(Statics.GAME_GAMETIME);
            }
        }

        public override void Draw()
        {
            if (Statics.GAME_STATE == Statics.STATE.GameOver)
            {
                Statics.GAME_SPRITEBATCH.Begin();
                Statics.GAME_SPRITEBATCH.Draw(this.Texture, _bird_Sprite.Bounds, new Rectangle(0, 0, _bird_Sprite.FrameWidth, _bird_Sprite.FrameHeight), Color.White, _bird_Sprite.Rotation, _bird_Sprite.SourceRotate, SpriteEffects.None, 1.0f);
                Statics.GAME_SPRITEBATCH.End();
            }
            else
            {
                _bird_Sprite.Draw(Statics.GAME_SPRITEBATCH);
            }

            base.Draw();
        }

        private void CheckForInput()
        {
            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.Space) || Statics.MANAGER_INPUT.IsLeftMouseClicked())
                Jump();

            if (Statics.MANAGER_INPUT.CurrentGamePadState().DPad.Up == ButtonState.Pressed)
                Jump();

            if (Statics.MANAGER_INPUT.IsGamepadPressed(Buttons.A))
                Jump();

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.D1))
                UseJumpBoost = UseJumpBoost ? false : true;

            if (Statics.MANAGER_INPUT.IsKeyPressed(Keys.D2))
                UseSlowFall = UseSlowFall ? false : true;

            // Input : Gamepad
            this.Position.X += Statics.MANAGER_INPUT.CurrentGamePadState().ThumbSticks.Left.X * this.MoveSpeed;
        }

        private void Jump()
        {
            Statics.MANAGER_SOUND.Play("Player\\Jump");

           // _ySpeed = UseJumpBoost ? -2 : -3;
        }
    }
}
