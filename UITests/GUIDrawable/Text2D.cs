using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UITests {
    public class Text2D : GUIDrawable {
        public string Text { get; set; }

        private SpriteFont spriteFont;
        private string spriteFontPath;

        public Text2D(string spriteFontPath, string text, Vector2 position, Color color) : base(position, 0, 0, color) {
            this.spriteFontPath = spriteFontPath;
            Text = text;
        }

        public Vector2 MeasureString(string text = null) {
            if (text != null) {
                return spriteFont.MeasureString(text);
            }
            else {
                return spriteFont.MeasureString(Text);
            }
        }

        public int GetLineSpacing() {
            return spriteFont.LineSpacing;
        }

        public float GetSpacing() {
            return spriteFont.Spacing;
        }

        public float GetMaxCharacterWidth() {
            List<Vector2> sizes = new List<Vector2>();
            for(int i = 0; i < spriteFont.Characters.Count; i++) {
                sizes.Add(MeasureString(spriteFont.Characters[i].ToString()));
            }

            return sizes.Max(x => x.X);
        }

        public override void Load(ContentManager content) {
            if (spriteFontPath != null) {
                spriteFont = content.Load<SpriteFont>(spriteFontPath);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (spriteFont != null && Text != null) {
                spriteBatch.DrawString(spriteFont, Text, ParentPosition + Position, Color, Rotation, Origin, Scale, _SpriteEffects, LayerDepth);
            }
        }
    }
}