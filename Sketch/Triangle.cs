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
    public class Triangle : GeometricVisual
    {
        #region Fields
        Vector2 _point1, _point2, _point3;
        #endregion

        #region Constructors
        public Triangle(Vector2 point1, Vector2 point2, Vector2 point3, Color fillColor, SpriteBatch spriteBatch)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            FillColor = fillColor;
            Position = new Vector2(GetSmallestXValue(), GetSmallestYValue());
            Sprite = GeometricContent.GetTriangleTexture(spriteBatch, this);
        }
        #endregion

        #region Properties
        public Vector2 Point1
        {
            get { return _point1; }
            set { _point1 = value; }
        }

        public Vector2 Point2
        {
            get { return _point2; }
            set { _point2 = value; }
        }

        public Vector2 Point3
        {
            get { return _point3; }
            set { _point3 = value; }
        }
        #endregion

        #region Private Methods
        private float GetSmallestXValue()
        {
            float value = Point1.X;

            if(value > Point2.X)
            {
                value = Point2.X;
            }

            if(value > Point3.X)
            {
                value = Point3.X;
            }

            return value;
        }

        private float GetSmallestYValue()
        {
            float value = Point1.Y;

            if (value > Point2.Y)
            {
                value = Point2.Y;
            }

            if (value > Point3.Y)
            {
                value = Point3.Y;
            }

            return value;
        }
        #endregion
        public override string UniqueString()
        {
            return "Triangle " + Point1.ToString() + " " + Point2.ToString() + " " + Point3.ToString() + " " + FillColor;
        }
    }
}
