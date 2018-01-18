using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UITests {
    public class Button {
        public Image2D Image { get; set; }
        public Rectangle Area { get; set; }
        public Rectangle MouseArea { get; private set; }
        public Color DefaultColor { get; set; }
        public Color HoverColor { get; set; }
        public Color ClickColor { get; set; }

        public event EventHandler Click;

        public Button(Image2D image, Rectangle area, Color defaultColor, Color hoverColor, Color clickColor) {
            Image = image;
            Area = area;
            Image.ParentPosition = new Vector2(Area.X, Area.Y);
            MouseArea = new Rectangle(0, 0, 2, 2);
            DefaultColor = defaultColor;
            HoverColor = hoverColor;
            ClickColor = clickColor;
        }

        public void OnClick(EventArgs e) {
            Click?.Invoke(this, e);
        }

        private MouseState oldMouseState;
        public void Update(MouseState mouseState) {
            if (Click != null && Click.GetInvocationList().Length > 0) {
                MouseArea = new Rectangle(mouseState.X, mouseState.Y, MouseArea.Width, MouseArea.Height);

                if (Area.Intersects(MouseArea)) {
                    Image.Color = HoverColor;

                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) {
                        Image.Color = ClickColor;
                        OnClick(new EventArgs());
                    }

                    oldMouseState = mouseState;
                }
                else {
                    Image.Color = DefaultColor;
                }
            }
            else {
                // disable button since it has nothing attached
            }
        }
    }
}