namespace IExtendFramework.Text.RTF
{
    using System.Text;
    
    /// <summary>
    /// A Work in Progress
    /// </summary>
    public class RTFUtil
    {
        #region Public Methods

        public void ParagraphBorderSide(StringBuilder sb, RTFBorderSide rTFBorderSide)
        {
            if (rTFBorderSide == RTFBorderSide.None)
            {
                return;
            }
            if ((rTFBorderSide & RTFBorderSide.Left) == RTFBorderSide.Left)
            {
                sb.Append("\\brdrl");
            }
            if ((rTFBorderSide & RTFBorderSide.Right) == RTFBorderSide.Right)
            {
                sb.Append("\\brdrr");
            }
            if ((rTFBorderSide & RTFBorderSide.Top) == RTFBorderSide.Top)
            {
                sb.Append("\\brdrt");
            }
            if ((rTFBorderSide & RTFBorderSide.Bottom) == RTFBorderSide.Bottom)
            {
                sb.Append("\\brdrb");
            }

            if ((rTFBorderSide & RTFBorderSide.DoubleThickness) == RTFBorderSide.DoubleThickness)
            {
                sb.Append("\\brdrth");
            }
            else
            {
                sb.Append("\\brdrs");
            }
            if ((rTFBorderSide & RTFBorderSide.DoubleBorder) == RTFBorderSide.DoubleBorder)
            {
                sb.Append("\\brdrdb");
            }
            sb.Append("\\brdrw10");
        }

        public void TableRowBorderSide(StringBuilder sb, RTFBorderSide rTFBorderSide)
        {
            if (rTFBorderSide == RTFBorderSide.None)
            {
                return;
            }
            if ((rTFBorderSide & RTFBorderSide.Left) == RTFBorderSide.Left)
            {
                sb.Append("\\trbrdrl");
            }
            if ((rTFBorderSide & RTFBorderSide.Right) == RTFBorderSide.Right)
            {
                sb.Append("\\trbrdrr");
            }
            if ((rTFBorderSide & RTFBorderSide.Top) == RTFBorderSide.Top)
            {
                sb.Append("\\trbrdrt");
            }
            if ((rTFBorderSide & RTFBorderSide.Bottom) == RTFBorderSide.Bottom)
            {
                sb.Append("\\trbrdrb");
            }
            if ((rTFBorderSide & RTFBorderSide.DoubleThickness) == RTFBorderSide.DoubleThickness)
            {
                sb.Append("\\brdrth");
            }
            else
            {
                sb.Append("\\brdrs");
            }
            if ((rTFBorderSide & RTFBorderSide.DoubleBorder) == RTFBorderSide.DoubleBorder)
            {
                sb.Append("\\brdrdb");
            }
            sb.Append("\\brdrw10");
        }

        #endregion
    }
}


