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
    public class Line : GeometricVisual
    {
        #region Fields
        Vector2 _point1, _point2;
        int _strokeThickness;
        #endregion

        #region Constructors
        public Line(Vector2 point1, Vector2 point2, int strokeThickness, Color fillColor, SpriteBatch spriteBatch)
        {
            Point1 = point1;
            Point2 = point2;
            StrokeThickness = strokeThickness;
            FillColor = fillColor;
            Position = new Vector2(GetSmallestXValue(), GetSmallestYValue());
            Sprite = GeometricContent.GetLineTexture(spriteBatch, this);
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

        public int StrokeThickness
        {
            get { return _strokeThickness; }
            set { _strokeThickness = value; }
        }
        #endregion

        #region Private Methods
        private float GetSmallestXValue()
        {
            float value = Point1.X;

            if (value > Point2.X)
            {
                value = Point2.X;
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

            return value;
        }
        #endregion

        public override string UniqueString()
        {
            return "Line " + Point1.ToString() + " " + Point2.ToString() + " " + FillColor;
        }
    }
}
