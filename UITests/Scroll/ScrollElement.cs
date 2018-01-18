using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace UITests {
    public class ScrollElement {
        private ContentManager contentManager;

        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        private List<GUIDrawable> guiDrawables;

        public ScrollElement(ContentManager contentManager, int width, int height, Vector2? position = null) {
            this.contentManager = contentManager;
            Position = position ?? Vector2.Zero;
            OriginalPosition = Position;
            Width = width;
            Height = height;
            guiDrawables = new List<GUIDrawable>();
        }

        public void UpdatePosition(Vector2 position) {
            Position = position;
            for(int i = 0; i < guiDrawables.Count; i++) {
                guiDrawables[i].ParentPosition = position;
            }
        }

        public int GetGUIDrawableCount() {
            return guiDrawables.Count;
        }

        public void AddGUIDrawable(GUIDrawable guiDrawable) {
            guiDrawables.Add(guiDrawable);
            guiDrawables.Last().Load(contentManager);
            guiDrawables.Last().ParentPosition = Position;
        }

        public void RemoveGUIDrawable(GUIDrawable guiDrawable) {
            guiDrawables.Remove(guiDrawable);
        }

        public void RemoveGUIDrawable(int index) {
            guiDrawables.RemoveAt(index);
        }

        public GUIDrawable GetGUIDrawable(int index) {
            return guiDrawables[index];
        }

        //public GUIDrawable GetGUIDrawable(GUIDrawable guiDrawable) {
        //    return guiDrawables.Find(x => x == guiDrawable);
        //}
    }
}