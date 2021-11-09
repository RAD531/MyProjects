using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    public interface Builder
    {
        //builder provides the blueprint for concrete builders to provide their own implementation
        public abstract void Reset();

        public abstract void SetProgressBar(int progressBarValue);

        public abstract void DrawProgressBar();

        public abstract void DrawPictureBox();

        public abstract void orderGenerator();
    }
}
