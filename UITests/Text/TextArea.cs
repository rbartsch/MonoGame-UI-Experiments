using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UITests {
    public class TextArea : Text2D {
        public Rectangle Area { get; set; }
        public TextAlignment Alignment { get; set; }
        public bool GrowVertically { get; set; }

        private int maxLineWidth;

        public TextArea(Rectangle area, TextAlignment alignment, bool growVertically, string spriteFontPath, string text, Vector2 position, Color color) : base(spriteFontPath, text, position, color) {
            Area = area;
            ParentPosition = new Vector2(Area.X, Area.Y);
            Alignment = alignment;
            GrowVertically = growVertically;
            maxLineWidth = Area.Width;
        }

        public void SetText(string text, bool wordWrap = true, TextAlignment? alignment = null) {
            if (alignment.HasValue) {
                Alignment = alignment.Value;
            }

            Text = text;
            AlignText(Alignment);
            WrapText(wordWrap);
        }

        public void WrapText(bool wordWrap) {
            if (MeasureString().X < maxLineWidth) {
                return;
            }

            if (!wordWrap) {
                char[] letters = Text.ToCharArray();
                float lineWidth = 0f;
                float spaceWidth = GetSpacing();
                StringBuilder wrappedText = new StringBuilder();

                for (int i = 0; i < letters.Length; i++) {
                    Vector2 letterSize = MeasureString(letters[i].ToString());

                    if (lineWidth + letterSize.X < maxLineWidth) {
                        lineWidth += letterSize.X + spaceWidth;
                    }
                    else {
                        wrappedText.Append("\n");
                        lineWidth = letterSize.X + spaceWidth;
                    }

                    wrappedText.Append(letters[i]);
                }

                Text = wrappedText.ToString();
            }
            else {
                String line = String.Empty;
                String returnString = String.Empty;
                String[] wordArray = Text.Split(' ');

                foreach (String word in wordArray) {
                    if (MeasureString(line + word).X > Area.Width) {
                        returnString = returnString + line + '\n';
                        line = String.Empty;
                    }

                    line = line + word + ' ';
                }

                Text = returnString + line;
            }

            if (GrowVertically) {
                Area = new Rectangle(Area.X, Area.X, Area.Width, (int)Math.Round(MeasureString().Y));
            }
        }

        public void AlignText(TextAlignment alignment) {
            switch (alignment) {
                case TextAlignment.Left: {
                        Origin = new Vector2(0, 0);
                    }
                    break;
                case TextAlignment.Centre: {
                        Origin = new Vector2((float)Math.Round(MeasureString().X / 2), Origin.Y);
                    }
                    break;
                case TextAlignment.Right: {
                        Origin = new Vector2(MeasureString().X, Origin.Y);
                    }
                    break;
            }
        }

        public void ChangePosition(int x, int y) {
            Area = new Rectangle(x, y, Area.Width, Area.Height);
        }
    }
}