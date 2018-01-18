using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests {
    public class ScrollMask {
        public Rectangle Area { get; set; }
        public Image2D BackgroundImage { get; set; }

        public ScrollMask(Rectangle area, Image2D backgroundImage = null) {
            Area = area;
            BackgroundImage = backgroundImage == null ? new Image2D(null, Vector2.Zero, Color.White, 0f) : backgroundImage;
            BackgroundImage.ParentPosition = new Vector2(Area.X, Area.Y); // offset drawing the image relative to scrollmask position
        }
    }
}