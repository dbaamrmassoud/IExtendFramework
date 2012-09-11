using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IExtendFramework.Text
{
    /// <summary>
    /// Contains font info, and can apply it to objects
    /// </summary>
    public class Font
    {
        public FontWeight Weight { get; set; }
        public FontFamily Family { get; set; }
        public double Size { get; set; }
        public FontStretch Stretch { get; set; }
        public FontStyle Style { get; set; }

        public Font()
        {
            Weight = FontWeights.Normal;
            Family = Fonts.SystemFontFamilies.ToArray<FontFamily>()[0];
            Size = 15;
            Stretch = FontStretches.Normal;
            Style = FontStyles.Normal;
        }

        public void ApplyTo(System.Windows.Controls.Control control)
        {
            control.FontWeight = Weight;
            control.FontFamily = Family;
            control.FontSize = Size;
            control.FontStretch = Stretch;
            control.FontStyle = Style;
        }

        public static Font FromControl(System.Windows.Controls.Control control)
        {
            Font f = new Font();
            f.Weight = control.FontWeight;
            f.Family = control.FontFamily;
            f.Size = control.FontSize;
            f.Stretch = control.FontStretch;
            f.Style = control.FontStyle;
            return f;
        }
    }
}
