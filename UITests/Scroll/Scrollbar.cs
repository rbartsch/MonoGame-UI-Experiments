using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UITests {
    public class Scrollbar {
        public Rectangle Area { get; set; }
        public Image2D BackgroundImage { get; set; }
        public Image2D HandleImage { get; set; }

        private Vector2 handleMin;
        private Vector2 handleMax;
        private float scrollPosition = 0;
        private bool dragging = false;
        private bool scrolling = false;
        private float convertedScrollPosition = 0;
        private int increment = 0;
        private int prevIncrement = 0;

        public Scrollbar(Rectangle area, Image2D handleImage, Image2D backgroundImage = null) {
            Area = area;
            HandleImage = handleImage;
            HandleImage.ParentPosition = new Vector2(Area.X, Area.Y);
            BackgroundImage = backgroundImage == null ? new Image2D(null, Vector2.Zero, Color.White, 0f) : backgroundImage;
            BackgroundImage.ParentPosition = new Vector2(Area.X, Area.Y); // offset drawing the image relative to scrollmask position
            handleMin = HandleImage.Position;
            handleMax = new Vector2(handleMin.X, Area.Height);
        }

        public void UseScrollbar(MouseState mouseState, int scrollIncrement, float trueScrollLength, ScrollContainer scrollContainer) {
            Rectangle mouseArea = new Rectangle(mouseState.X, mouseState.Y, 2, 2);
            Rectangle handleImageArea = new Rectangle((int)HandleImage.ParentPosition.X + (int)HandleImage.Position.X, (int)HandleImage.ParentPosition.Y + (int)HandleImage.Position.Y, HandleImage.Width, HandleImage.Height);

            if (handleImageArea.Contains(mouseArea)) {
                if (mouseState.LeftButton == ButtonState.Pressed) {
                    dragging = true;
                    scrolling = false;
                }
            }

            if (dragging) {
                if (mouseState.LeftButton == ButtonState.Pressed) {
                    increment = (mouseState.Position.Y - Area.Top);
                }
                else {
                    dragging = false;
                }
            }

            if (increment <= 0) {
                increment = 0;
            }
            else if (increment > Area.Bottom - Area.Top - HandleImage.Height) {
                increment = Area.Bottom - Area.Top - HandleImage.Height;
            }

            if (increment > prevIncrement) {
                //Console.WriteLine("down");
                convertedScrollPosition = LinearConversion.Float(increment, handleMin.Y, handleMax.Y - HandleImage.Height, -scrollContainer.OriginalArea.Y, trueScrollLength - scrollContainer.OriginalArea.Y);
                convertedScrollPosition = (float)Math.Round(convertedScrollPosition);
                scrollContainer.BuildContainerContinuous(0, -(int)convertedScrollPosition);
                scrollPosition = LinearConversion.Float(increment, handleMin.Y, handleMax.Y - HandleImage.Height, handleMin.Y, scrollIncrement * ((int)Math.Round(trueScrollLength / scrollIncrement)));
                scrollPosition = (float)Math.Round(scrollPosition);
                Console.WriteLine(scrollPosition);

            }
            else if (prevIncrement > increment) {
                //Console.WriteLine("up");
                convertedScrollPosition = LinearConversion.Float(increment, handleMin.Y, handleMax.Y, -scrollContainer.OriginalArea.Y, trueScrollLength - scrollContainer.OriginalArea.Y);
                convertedScrollPosition = (float)Math.Round(convertedScrollPosition);
                scrollContainer.BuildContainerContinuous(0, -(int)convertedScrollPosition);
                scrollPosition = LinearConversion.Float(increment, handleMin.Y, handleMax.Y, handleMin.Y, scrollIncrement * ((int)Math.Round(trueScrollLength / scrollIncrement)));
                scrollPosition = (float)Math.Round(scrollPosition);
                Console.WriteLine(scrollPosition);
            }
            else {
                //Console.WriteLine("idle");
            }

            if (!scrolling) {
                HandleImage.Position = new Vector2(HandleImage.Position.X, increment);
            }

            prevIncrement = increment;
        }

        public void Update(int scrollIncrement, float trueScrollLength, ScrollPanel scrollPanel) {
            scrolling = true;
            scrollPosition += scrollIncrement;
            scrollPosition = (float)Math.Round(scrollPosition);
            if (scrollPosition <= 0) {
                scrollPosition = 0;
                scrollPanel.ScrollContainer.BuildContainerContinuous(0, scrollPanel.ScrollMask.Area.Top);
            }
            else if (scrollPosition > scrollIncrement * ((int)Math.Round(trueScrollLength / scrollIncrement))) {
                scrollPosition = scrollIncrement * ((int)Math.Round(trueScrollLength / scrollIncrement));
                scrollPanel.ScrollContainer.BuildContainerContinuous(0, (int)-scrollPosition + scrollPanel.ScrollMask.Area.Top);
            }
            // oldMax is the total value of each increment by 12 to scroll the container.
            HandleImage.Position = new Vector2(HandleImage.Position.X, (float)Math.Round(LinearConversion.Float(scrollPosition, handleMin.Y, scrollIncrement * ((int)Math.Round(trueScrollLength / scrollIncrement)), handleMin.Y, handleMax.Y - HandleImage.Height)));
        }
    }
}