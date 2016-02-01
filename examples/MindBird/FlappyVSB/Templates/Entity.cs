using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Entities
{
    class Entity
    {
        public enum Type
        {
            Bird,
            Bullet,
            None,
            Paratroopa,
            Pipe
        }

        public Texture2D Texture;
        public Vector2 Position;
        public float Rotation = 0f;
        public float MoveSpeed = 0f;
        public float Scale = 1f;
        public int Width, Height;
        public Color[] ColorData;

        public Type EntityType;

        public bool IsScored = false;

        public List<Rectangle> Bounds { get { return GetRectangles(); } }

        //public int Width { get { return this.Texture.Width; } }
        //public int Height { get { return this.Texture.Height; } }

        public Entity(Type type)
        {
            this.EntityType = type;
        }

        public virtual void Update() { }
        
        public virtual void Draw() 
        {
            if (Statics.DEBUG_SHOWHITBOX)
                Helpers.Debug.DrawHitboxes(this.Bounds);
        }

        private List<Rectangle> GetRectangles()
        {
            List<Rectangle> rects = new List<Rectangle>();

            switch (this.EntityType)
            {
                case Type.Bird:
                    {
                        // Add bounding rectangles
                        rects.Add(new Rectangle((int)this.Position.X - (int)(this.Width * this.Scale), (int)this.Position.Y - (int)(this.Height * this.Scale), (int)(this.Width * this.Scale), (int)(this.Height * this.Scale)));

                        break;
                    }
                case Type.Bullet:
                    {
                        // Add bounding rectangles
                        rects.Add(new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Width, this.Height));

                        break;
                    }
                case Type.Paratroopa:
                    {
                        // Add bounding rectangles
                        rects.Add(new Rectangle((int)this.Position.X - (int)(this.Width * this.Scale), (int)this.Position.Y - (int)(this.Height * this.Scale), (int)(this.Width * this.Scale), (int)(this.Height * this.Scale)));

                        break;
                    }
                case Type.Pipe:
                    {
                        // Add bounding rectangles
                        rects.Add(new Rectangle((int)this.Position.X, (int)this.Position.Y, 160, 510));
                        rects.Add(new Rectangle((int)this.Position.X, (int)this.Position.Y + this.Height - 510, 160, 510));

                        break;
                    }
            }

            return rects;
        }
    }
}
