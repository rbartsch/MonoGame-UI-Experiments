using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITests {
    public class ScrollPanel {
        public ScrollMask ScrollMask { get; set; }
        public ScrollContainer ScrollContainer { get; set; }
        public int ScrollIncrements { get; set; } // Best to use an increment of 2/even number, i.e 2, 4, 6, 8, 10, 12 ... not 10, 13, etc
        public Scrollbar Scrollbar { get; set; }

        private float trueScrollLength = 0f;

        public ScrollPanel(ScrollMask scrollMask, int scrollIncrements = 12, Image2D containerBackgroundImage = null, Scrollbar scrollbar = null) {
            ScrollMask = scrollMask;
            ScrollContainer = new ScrollContainer(ScrollMask, containerBackgroundImage);
            ScrollIncrements = scrollIncrements;
            Scrollbar = scrollbar;
        }

        public void UpdateScrollLength() {
            trueScrollLength = ((float)ScrollContainer.Area.Height + (float)ScrollContainer.Area.Y) - ((float)ScrollMask.Area.Height + (float)ScrollMask.Area.Y);
        }

        public void Update(Point mousePos, int prevMouseScrollValue, int currMouseScrollValue) {
            //if (ScrollMask.Area.Contains(mousePos)) {
            //    if (currMouseScrollValue > prevMouseScrollValue) {
            //        if (ScrollContainer.Area.Y < ScrollMask.Area.Y + ScrollMask.Area.Height) {
            //            ScrollContainer.BuildContainer(0, ScrollIncrements);
            //            Scrollbar.Update(-ScrollIncrements);
            //        }
            //    }
            //    else if (currMouseScrollValue < prevMouseScrollValue) {
            //        if (ScrollContainer.Area.Y + ScrollContainer.Area.Height > ScrollMask.Area.Y) {
            //            Console.WriteLine("Test");
            //            ScrollContainer.BuildContainer(0, -ScrollIncrements);
            //            Scrollbar.Update(ScrollIncrements);
            //        }
            //    }
            //}
            if (ScrollMask.Area.Contains(mousePos)) {
                if (currMouseScrollValue > prevMouseScrollValue) {
                    if (ScrollContainer.Area.Y < ScrollMask.Area.Y) {
                        ScrollContainer.BuildContainer(0, ScrollIncrements);
                        Scrollbar.Update(-ScrollIncrements, trueScrollLength, this);
                    }
                }
                else if (currMouseScrollValue < prevMouseScrollValue) {
                    if (ScrollContainer.Area.Y + ScrollContainer.Area.Height > ScrollMask.Area.Y + ScrollMask.Area.Height) {
                        ScrollContainer.BuildContainer(0, -ScrollIncrements);
                        Scrollbar.Update(ScrollIncrements, trueScrollLength, this);
                    }
                }
            }

            if (Scrollbar != null) {
                Scrollbar.UseScrollbar(Mouse.GetState(), ScrollIncrements, trueScrollLength, ScrollContainer);
            }
        }
    }
}