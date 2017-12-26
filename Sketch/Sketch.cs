using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Sketch
{
    /// <summary>
    /// 
    /// </summary>
    public class Sketch
    {
        #region Fields
        List<GeometricVisual> _geometricVisuals;
        #endregion

        #region Constructors
        public Sketch() {
            GeometricVisuals = new List<GeometricVisual>();
        }
        #endregion

        #region Properties
        public List<GeometricVisual> GeometricVisuals
        {
            get { return _geometricVisuals; }
            set { _geometricVisuals = value; }
        }
        #endregion

        #region Public Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(GeometricVisual geometricVisual in _geometricVisuals)
            {
                geometricVisual.Draw(spriteBatch);
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
