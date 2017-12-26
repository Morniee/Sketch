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
    public class Circle : GeometricVisual
    {
        #region Fields
        Vector2 _centerPoint;
        float _radius;
        #endregion

        #region Constructors
        public Circle(Vector2 centerPoint, float radius, Color fillColor, SpriteBatch spriteBatch)
        {
            FillColor = fillColor;
            Radius = radius;
            CenterPoint = centerPoint;
            Position = CenterPoint - new Vector2(Radius, Radius);
            Sprite = GeometricContent.GetCircleTexture(spriteBatch, this);
        }
        #endregion

        #region Properties
        public Vector2 CenterPoint
        {
            get { return _centerPoint; }
            set { _centerPoint = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        public override string UniqueString()
        {
            return "Circle " + CenterPoint.ToString() + " " + Radius + " " + FillColor;
        }
    }
}
