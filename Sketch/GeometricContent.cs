using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketch
{
    public static class GeometricContent
    {
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();


        #region line
        public static Texture2D GetLineTexture(this SpriteBatch spriteBatch, Line l)
        {
            string key = l.ToString();
            if (!_textures.Keys.Contains(key))
                _textures.Add(key, ComputeLineTexture(spriteBatch, l));

            return _textures[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static Texture2D ComputeLineTexture(this SpriteBatch spriteBatch, Line l)
        {
            float minX = new float[] { l.Point1.X, l.Point2.X }.Min();
            float minY = new float[] { l.Point1.Y, l.Point2.Y }.Min();
            float maxX = new float[] { l.Point1.X, l.Point2.X }.Max();
            float maxY = new float[] { l.Point1.Y, l.Point2.Y }.Max();

            Vector2 p1 = l.Point1 - new Vector2(minX, minY);
            Vector2 p2 = l.Point2 - new Vector2(minX, minY);
            int width = (int)(maxX - minX);
            width = Math.Max(width, l.StrokeThickness);
            int height = (int)(maxY - minY);

            width = Math.Max(1, width);
            height = Math.Max(1, height) + 1;

            Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }

            if (p2.X < p1.X)
            {
                Vector2 p = p1;
                p1 = p2;
                p2 = p;
            }
            int strokeThickness = l.StrokeThickness;
            int strokeLeft = strokeThickness / 2;
            int strokeRight = strokeThickness = strokeLeft;

            if (p2.X != p1.X) 
            {
                float a = (p2.Y - p1.Y) / (p2.X - p1.X);
                float b = p1.Y - a * p1.X;
                for (int x = 0; x < Math.Max(p1.X, p2.X); x++)
                {
                    float y = a * x + b;
                    for (int xx = x - strokeLeft; xx <= x + strokeRight && xx < width; xx++)
                    {
                        data[(int)(y - 1) * width + (int)xx] = l.FillColor;
                        data[(int)y * width + (int)xx] = l.FillColor;
                        if (y < height - 1)
                            data[(int)(y + 1) * width + (int)xx] = l.FillColor;
                        if (y < height - 2)
                            data[(int)(y + 2) * width + (int)xx] = l.FillColor;
                    }
                }
            }
            else 
                for (int y = 0; y < Math.Max(p1.Y, p2.Y); y++)
                {
                    int x = (int)p1.X;
                    for (int xx = x; xx <= x + strokeThickness && xx < width; xx++)
                        data[(int)y * width + (int)xx] = l.FillColor;
                }

            t.SetData(data);
            return t;
        }


        #endregion

        #region circle
        public static Texture2D GetCircleTexture(SpriteBatch spriteBatch, Circle c)
        {
            string key = c.ToString();
            if (!_textures.Keys.Contains(key))
                _textures.Add(key, ComputeCircleTexture(spriteBatch, c));

            return _textures[key];
        }
        private static Texture2D ComputeCircleTexture(SpriteBatch spriteBatch, Circle c)
        {
            int strokeThickness = 0;
            strokeThickness = Math.Min(strokeThickness, (int)c.Radius);

            int outerRadius = (int)c.Radius * 2 + 2;
            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }

            double angleStep = 1f / (c.Radius * 2);

            for (double angle = 0; angle < System.Math.PI * 2; angle += angleStep)
            {
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);

                for (int i = 1; i <= strokeThickness; i++)
                {
                    int x = (int)System.Math.Round((c.Radius) + (c.Radius - i) * cos);
                    int y = (int)System.Math.Round((c.Radius) + (c.Radius - i) * sin);
                    data[y * outerRadius + x + 1] = Color.Transparent;
                }

                for (int innerRadius = 0; innerRadius <= c.Radius - strokeThickness; innerRadius++)
                {
                    int x = (int)System.Math.Round((c.Radius) + innerRadius * cos);
                    int y = (int)System.Math.Round((c.Radius) + innerRadius * sin);

                    data[y * outerRadius + x + 1] = c.FillColor;
                }
            }

            texture.SetData(data);

            return texture;
        }
        #endregion

        #region triangle
        /// <summary>
        /// gets the boxed triangle (the triangle reaches the left, top, right and bottom of the texture)
        /// one must draw the texture at the correct position or drawingrectangle
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="t">the triangle to draw, the resulting texture encloses the triangle closely</param>
        /// <returns></returns>
        public static Texture2D GetTriangleTexture(SpriteBatch spriteBatch, Triangle t)
        {
            string key = t.ToString();
            if (!_textures.Keys.Contains(key))
                _textures.Add(key, ComputeTriangleTexture(spriteBatch, t));

            return _textures[key];
        }

        //http://www.sunshine2k.de/coding/java/TriangleRasterization/TriangleRasterization.html
        private static Texture2D ComputeTriangleTexture(SpriteBatch spriteBatch, Triangle t)
        {
            Vector2 v1, v2, v3;
            (v1, v2, v3) = OrderPointsHighToLow(t);

            (v1, v2, v3) = MoveToOrigin(ref v1, ref v2, ref v3);

            float x4 = v1.X + ((v2.Y - v1.Y) / (v3.Y - v1.Y)) * (v3.X - v1.X);

            Vector2 v4 = new Vector2(x4, v2.Y);

            int height = (int)(v3.Y - v1.Y) + 1;
            int width = (int)GetWidth(t) + 1;

            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }

            if (v2.Y == v3.Y)
            {
                FillBottomFlatTriangle(v1, v2, v3, data, t.FillColor, width);
            }
            else if (v1.Y == v2.Y)
            {
                FillTopFlatTriangle(v1, v2, v3, data, t.FillColor, width, 0, 0);
            }
            else
            {
                FillBottomFlatTriangle(v1, v2, v4, data, t.FillColor, width);
                FillTopFlatTriangle(v2, v4, v3, data, t.FillColor, width, (int)v4.X, (int)(v2.Y - v1.Y));
            }

            texture.SetData(data);
            return texture;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">top point</param>
        /// <param name="v2">point of the flat bottom</param>
        /// <param name="v3">other point of the flat bottom</param>
        /// <param name="data"></param>
        /// <param name="color"></param>
        /// <param name="width">max width of the triangle</param>
        private static void FillBottomFlatTriangle(Vector2 v1, Vector2 v2, Vector2 v3,
            Color[] data, Color color, int width)
        {
            if (v3.X < v2.X)
            {
                Vector2 v = v2;
                v2 = v3;
                v3 = v;
            }
            float invslope1 = (v2.X - v1.X) / (v2.Y - v1.Y);
            float invslope2 = (v3.X - v1.X) / (v3.Y - v1.Y);

            float curx1 = v1.X;
            float curx2 = v1.X;

            for (int scanlineY = 0; scanlineY < v2.Y; scanlineY++)
            {
                for (int x = (int)curx1; x <= curx2; x++)
                {
                    data[scanlineY * width + x] = color;
                }
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">point of the flat top</param>
        /// <param name="v2">other point of the flat top</param>
        /// <param name="v3">bottom point</param>
        /// <param name="data"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        private static void FillTopFlatTriangle(Vector2 v1, Vector2 v2, Vector2 v3,
            Color[] data, Color color, int width,
            int xOffset, int yOffset)
        {
            if (v2.X < v1.X)
            {
                Vector2 v = v2;
                v2 = v1;
                v1 = v;
            }

            float invslope1 = (v3.X - v1.X) / (v3.Y - v1.Y);
            float invslope2 = (v3.X - v2.X) / (v3.Y - v2.Y);

            float curx1 = v3.X;
            float curx2 = v3.X;

            for (int scanlineY = (int)(v3.Y - v1.Y); scanlineY >= 0; scanlineY--)
            {
                for (int x = (int)curx1; x <= curx2; x++)
                {
                    data[(scanlineY + yOffset) * width + x] = color;
                }
                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }

        private static float GetWidth(Triangle t)
        {
            List<float> xs = new List<float>(3);
            xs.Add(t.Point1.X);
            xs.Add(t.Point2.X);
            xs.Add(t.Point3.X);
            return xs.Max() - xs.Min();
        }

        #region helper functions
        /// <summary>
        /// return the first highest point of the triangle
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector2 TopPoint(Triangle t)
        {
            return TopPoint(new Vector2[] { t.Point1, t.Point2, t.Point3 });
        }
        /// <summary>
        /// neglecting the top of the triangle, return the first highest point of the triangle, 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="top">the highest point of the vector, which will be neglected</param>
        /// <returns></returns>
        public static Vector2 MiddlePoint(Triangle t, Vector2 top)
        {
            if (t.Point1 == top) return TopPoint(new Vector2[] { t.Point2, t.Point3 });
            if (t.Point2 == top) return TopPoint(new Vector2[] { t.Point1, t.Point3 });
            if (t.Point3 == top) return TopPoint(new Vector2[] { t.Point1, t.Point2 });
            return Vector2.Zero; //should never happen
        }

        /// <summary>
        /// returns the (last) lowest point of the triangle
        /// </summary>
        /// <param name="t"></param>
        /// <param name="top">the top point</param>
        /// <param name="middle">the middle point</param>
        /// <returns></returns>
        public static Vector2 BottomPoint(Triangle t, Vector2 top, Vector2 middle)
        {
            List<Vector2> points = new List<Vector2>(3);
            points.Add(t.Point1);
            points.Add(t.Point2);
            points.Add(t.Point3);
            points.Remove(top);
            points.Remove(middle);
            return points[0];
        }
        /// <summary>
        /// returns the first highest point in an array
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static Vector2 TopPoint(Vector2[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Boolean top = true;
                Vector2 v = points[i];
                for (int j = i + 1; j < points.Length; j++)
                    if (points[j].Y < v.Y)
                    {
                        top = false;
                        break;
                    }
                if (top) return v;
            }
            return Vector2.Zero;
        }
        private static (Vector2 v10, Vector2 v20, Vector2 v30) MoveToOrigin(ref Vector2 v1, ref Vector2 v2, ref Vector2 v3)
        {
            float xmin = new float[] { v1.X, v2.X, v3.X }.Min();
            v2 -= new Vector2(xmin, v1.Y);
            v3 -= new Vector2(xmin, v1.Y);
            v1 -= new Vector2(xmin, v1.Y);
            return (v1, v2, v3);
        }

        private static (Vector2 top, Vector2 middle, Vector2 bottom) OrderPointsHighToLow(Triangle t)
        {
            Vector2 v1 = TopPoint(t);
            Vector2 v2 = MiddlePoint(t, v1);
            Vector2 v3 = BottomPoint(t, v1, v2);

            return (v1, v2, v3);
        }
        #endregion
        #endregion


    }
}
