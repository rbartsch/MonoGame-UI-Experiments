using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UITests {
    public class Image2D : GUIDrawable {
        private Texture2D texture2D;
        private string texture2DPath;

        public Image2D(string texture2DPath, Vector2 position, Color color, float layerDepth = 0.0f) : base(position, 0, 0, color, layerDepth) {
            this.texture2DPath = texture2DPath;
        }

        public override void Load(ContentManager content) {
            if (texture2DPath != null) {
                texture2D = content.Load<Texture2D>(texture2DPath);
                Width = texture2D.Width;
                Height = texture2D.Height;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (texture2D != null) {
                //Console.WriteLine("Test");
                //spriteBatch.Draw(texture2D, ParentPosition + Position, Color);
                spriteBatch.Draw(texture2D, ParentPosition + Position, null, Color, Rotation, Origin, Scale, _SpriteEffects, LayerDepth);
            }
        }
    }
}