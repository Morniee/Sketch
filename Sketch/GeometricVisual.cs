using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sketch
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GeometricVisual
    {

        #region Properties
        public Color FillColor { get; set; }
        public virtual Vector2 Position { get; protected set; }
        public virtual Texture2D Sprite { get; protected set; }
        protected virtual Rectangle DrawingRectangle
        {
            get { return new Rectangle(Position.ToPoint(), new Point(Sprite.Width, Sprite.Height)); }
        }
        #endregion

        #region Public/Protected Methods
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, DrawingRectangle, Color.White);
        }

        public abstract string UniqueString();
        public override string ToString()
        {
            return UniqueString();
        }
        #endregion
    }
}
