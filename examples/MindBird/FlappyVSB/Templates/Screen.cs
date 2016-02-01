using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Screens
{
    public abstract class Screen
    {
        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual void Update() { }

        public virtual void Draw() { }

        public virtual void Reset() { }
    }
}
