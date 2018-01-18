using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests {
    public abstract class GUIDrawable {
        public Vector2 ParentPosition { get; set; } = Vector2.Zero;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }
        public SpriteEffects _SpriteEffects { get; set; }
        public float LayerDepth { get; set; }

        public GUIDrawable(Vector2 position, int width, int height, Color color, float rotation = 0f, Vector2? origin = null, Vector2? scale = null, SpriteEffects spriteEffects = SpriteEffects.None, float layerDepth = 0.0f) {
            Position = position;
            Width = width;
            Height = height;
            Color = color;
            Rotation = rotation;
            Origin = origin ?? Vector2.Zero;
            Scale = scale ?? Vector2.One;
            _SpriteEffects = spriteEffects;
            LayerDepth = layerDepth;
            Globals.guiDrawables.Add(this);
        }

        public virtual void Load(ContentManager content) { }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        //public virtual void Unload() { }
    }
}