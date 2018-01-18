using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests {
    public class ScrollContainer {
        public List<ScrollElement> Elements { get; set; }
        public Rectangle OriginalArea { get; set; }
        public Rectangle Area { get; set; }
        public Image2D BackgroundImage { get; set; }

        public ScrollContainer(ScrollMask scrollMask, Image2D backgroundImage = null) {
            Elements = new List<ScrollElement>();
            Area = new Rectangle(scrollMask.Area.X, scrollMask.Area.Y, scrollMask.Area.Width, scrollMask.Area.Height);
            OriginalArea = Area;
            BackgroundImage = backgroundImage == null ? new Image2D(null, Vector2.Zero, Color.White, 0f) : backgroundImage;
            BackgroundImage.ParentPosition = new Vector2(Area.X, Area.Y); // offset drawing the image relative to scrollmask position
            //BuildContainer(Container.X, Container.Y);
        }

        public void AddElement(ScrollElement element) {
            Elements.Add(element);
            BuildContainer(0, 0);
        }

        public void AddElementSequential(ScrollElement element) {
            if(Elements.Count <= 0) {
                element.Position = new Vector2(0, 0);
                element.OriginalPosition = element.Position;
            }
            else {
                element.Position = new Vector2(0, Elements.Last().OriginalPosition.Y + Elements.Last().Height);
                element.OriginalPosition = element.Position;
            }
            Elements.Add(element);
            BuildContainer(0, 0);
        }

        public void RemoveElement(ScrollElement element) {
            Elements.Remove(element);
            BuildContainer(0, 0);
        }

        public void RemoveElement(int index) {
            Elements.RemoveAt(index);
            BuildContainer(0, 0);
        }

        public void RemoveElementSequential() {
            Elements.Remove(Elements.Last());
        }

        public ScrollElement GetElement(int index) {
            return Elements[index];
        }

        public void BuildContainer(int x, int y) {
            int width = 0;
            int height = 0;
            //foreach (ScrollElement e in Elements) {
            //    width = e.Texture2D.Width;
            //    height += e.Texture2D.Height;
            //}

            //width = Elements.Last().Texture2D.Width;
            //height = (int)Elements.Last().RelativePosition.Y + 48;
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y) + Elements.Max(v => v.Height)
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y);
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y) + (int)Math.Round(Elements.Average(v => v.Height));
            width = Elements.Max(v => v.Width);
            height = (int)Elements.Max(v => v.OriginalPosition.Y) + Elements.Last().Height;

            Area = new Rectangle(Area.X + x, Area.Y + y, width, height);
            foreach (ScrollElement e in Elements) {
                e.UpdatePosition(new Vector2(Area.X + e.OriginalPosition.X, Area.Y + e.OriginalPosition.Y));
            }
        }

        public void BuildContainerContinuous(int x, int y) {
            int width = 0;
            int height = 0;
            //foreach (ScrollElement e in Elements) {
            //    width = e.Texture2D.Width;
            //    height += e.Texture2D.Height;
            //}

            //width = Elements.Last().Texture2D.Width;
            //height = (int)Elements.Last().RelativePosition.Y + 48;
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y) + Elements.Max(v => v.Height)
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y);
            //width = Elements.Max(v => v.Width);
            //height = (int)Elements.Max(v => v.OriginalPosition.Y) + (int)Math.Round(Elements.Average(v => v.Height));
            width = Elements.Max(v => v.Width);
            height = (int)Elements.Max(v => v.OriginalPosition.Y) + Elements.Last().Height;

            Area = new Rectangle(Area.X + x, y, width, height);
            foreach (ScrollElement e in Elements) {
                e.UpdatePosition(new Vector2(Area.X + e.OriginalPosition.X, Area.Y + e.OriginalPosition.Y));
            }
        }
    }
}