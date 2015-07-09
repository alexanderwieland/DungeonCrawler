using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEditor
{
    class TileDisplay : GraphicsDeviceControl
    {
        public event EventHandler OnDraw;
        public event EventHandler OnInizalize;


        protected override void Initialize()
        {
            if (OnInizalize != null)
                OnInizalize(this, null);
        }

        protected override void Draw()
        {
            if (OnDraw != null)
                OnDraw(this, null);
        }
    }
}
