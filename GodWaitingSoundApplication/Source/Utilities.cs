using System;
using System.Drawing;
using System.Windows.Forms;

namespace GodWaitingSoundApplication
{
    class Utilities
    {
        public static Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
                RightToLeft = RightToLeft.Yes,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };
        }

        public static Label CreateLabel(string text, int fontSize, bool bold = true)
        {
            return new Label()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", fontSize, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                RightToLeft = RightToLeft.Yes,
                TabIndex = 0,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
            };
        }

        public static float Clamp(float min, float max, float x)
        {
            if (x <= min)
                return min;

            if (x >= max)
                return max;

            return x;
        }
    }
}