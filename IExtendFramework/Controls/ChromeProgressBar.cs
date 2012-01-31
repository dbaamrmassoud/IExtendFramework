using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Drawing.Drawing2D;

namespace IExtendFramework.Controls
{
    [ToolboxItem(true)]
    public class ChromeProgressBar : Control
    {
        #region Fields
        private float _progress = 0F;
        ColorBlend _colorBlend = null; 
        private Image _icon = null;
        const String DEF_ICON_NAME = "IExtendFramework.Controls.DefIcon.jpg";
        #endregion

        #region Properties

        public String FileName
        {
            get;
            set;
        }

        public String ProgressText
        {
            get;
            set;
        }

        public float Progress
        {
            get { return _progress; }
            set 
            {
                if (_progress!= value && value >= 0 && value <= 100)
                {
                    _progress = value;
                    OnProgressChanged();
                }
            }
        }

        public Image Icon
        {
            get { return _icon; }
            set 
            {
                if(value != null)
                _icon = value;
            }
        }

        public bool EnableGradientBackGround
        {
            get;
            set;
        }

        public bool EnableBlendedBackGroud
        {
            get;
            set;
        }

        public Color BackgroundGradientBegin
        {
            get;
            set;
        }

        public Color BackgroundGradientEnd
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get;
            set;
        }

        internal ColorBlend ColorBlend
        {
            get
            {
                if (_colorBlend == null)
                {
                    _colorBlend = new ColorBlend(13);
                    _colorBlend.Positions = new float[] 
                    {
                        0F,
                        0.153F,
                        0.230F,
                        0.307F,
                        0.384F,
                        0.461F,
                        0.538F,
                        0.615F,
                        0.692F,
                        0.769F,
                        0.864F,
                        0.923F,
                        1F,
                    };

                    _colorBlend.Colors = new Color[]
                    {
                        Color.FromArgb(205,255,205),
                        Color.FromArgb(255,255,205),
                        Color.FromArgb(197,250,201),
                        Color.FromArgb(173,243,184),
                        Color.FromArgb(162,239,175),
                        Color.FromArgb(156,237,172),
                        Color.FromArgb(0,210,40),
                        Color.FromArgb(0,210,40),
                        Color.FromArgb(1,211,41),
                        Color.FromArgb(5,214,42),
                        Color.FromArgb(10,217,44),
                        Color.FromArgb(16,222,46),
                        Color.FromArgb(28,225,51)
                    };
                }
                return _colorBlend;

            }
        }

        public Color ProgressColor
        {
            get;
            set;
        }

        public Color SeparatorColor
        {
            get;
            set;
        }

        internal InternalLayout LayoutInternal
        {
            get
            {
                return new InternalLayout(this);
            }
        }
        #endregion

        #region Ctor
        public ChromeProgressBar()
        {
            this.BackColor = Color.FromArgb(223,223,223);
            this.BorderColor = Color.White;
            this.ProgressColor = Color.Green;

            _icon = Image.FromStream(AssemblyHelper.GetEmbeddedResource(DEF_ICON_NAME));

            this.Size = this.LayoutInternal.MinSize;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.FileName = "UnKnown";
            this.ProgressText = "Calculating..." ;

            this.EnableGradientBackGround = false;
            this.BackgroundGradientBegin = Color.FromArgb(87,204,247);
            this.BackgroundGradientEnd = Color.FromArgb(1,160,217);

        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            if(this.EnableGradientBackGround)
                PaintBackground(e);

            PaintProgress(e);

            PaintBorder(e);

            PaintIcon(e);

            PaintText(e);

            DrawSeprator(e);

            base.OnPaint(e);
        }

        #endregion

        #region Implementation
        private void PaintIcon(PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.Icon,LayoutInternal.IconRectangle);
        }

        private void DrawSeprator(PaintEventArgs e)
        {
            
        }

        private void PaintText(PaintEventArgs e)
        {
            e.Graphics.DrawString(this.FileName, this.Font, Brushes.Black, LayoutInternal.TextRectangle);

            int height = TextRenderer.MeasureText(e.Graphics,this.ProgressText, this.Font, Size.Empty, TextFormatFlags.NoClipping).Height;

            e.Graphics.DrawString(this.ProgressText, this.Font, Brushes.Black,new Point( LayoutInternal.TextRectangle.Left ,LayoutInternal.TextRectangle.Bottom - height - 4 ));
        }

        private void PaintBorder(PaintEventArgs e)
        {
            Region clip = e.Graphics.Clip;

            GraphicsPath borderPath = new GraphicsPath();

            Rectangle progressRect = LayoutInternal.ProgressRectangle;
            progressRect.Inflate(-1, -1);
            progressRect.Width -= 2; progressRect.Height -= 2;

            Rectangle region = LayoutInternal.ProgressRectangle;
            region.Inflate(1, 1);
            region.Width -= 2; region.Height -= 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(region, 0, 360);
            e.Graphics.Clip = new Region(path);
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            borderPath.AddArc(progressRect, 0, 360);

            using (Pen borderPen = new Pen(this.BorderColor, 2))
            {
                e.Graphics.DrawPath(borderPen, borderPath);

                e.Graphics.DrawLine(borderPen,
                    new Point(progressRect.Left + progressRect.Width / 2, progressRect.Top),
                    new Point(progressRect.Left + progressRect.Width / 2, progressRect.Bottom));

                e.Graphics.DrawLine(borderPen,
                    new Point(progressRect.Left, progressRect.Top + progressRect.Height / 2),
                    new Point(progressRect.Right, progressRect.Top + progressRect.Height / 2));

                e.Graphics.DrawLine(borderPen,
                     new Point(progressRect.Left ,progressRect.Top),
                     new Point(progressRect.Right,progressRect.Bottom));

                e.Graphics.DrawLine(borderPen,
                    new Point(progressRect.Left, progressRect.Bottom),
                    new Point(progressRect.Right, progressRect.Top ));
            }

            e.Graphics.Clip = clip;
        }

        private void PaintProgress(PaintEventArgs e)
        {
           using( SolidBrush progressBrush = new SolidBrush(this.ProgressColor))
           {
               Rectangle rect = LayoutInternal.ProgressRectangle;

               rect.Inflate(-2, -2);
               rect.Height -= 2; rect.Width -= 2;

               float startAngle = -90;
               float sweepAngle = Progress / 100 * 360;
               
               e.Graphics.FillPie(progressBrush, rect, startAngle, sweepAngle);
           }
        }

        private void PaintBackground(PaintEventArgs e)
        {
            if (this.EnableGradientBackGround && this.EnableBlendedBackGroud)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Green, Color.Green, LinearGradientMode.Vertical))
                {
                    brush.InterpolationColors = this.ColorBlend;
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
            else
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this.BackgroundGradientBegin,this.BackgroundGradientEnd, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
        }

        public void UpdateProgress()
        {
            this.Progress++;
        }

        protected virtual void OnProgressChanged()
        {
            this.Invalidate();
        }
        #endregion

        #region InternalLayout
        internal class InternalLayout
        {
            #region Fields
            const int MIN_WIDTH = 32;
            const int MIN_HEIGHT = 32;
            const int SEPARATOR_WIDTH = 2;
            const int DEF_PADDING = 2;

            private ChromeProgressBar _progressBar;
            #endregion

            #region Ctor
            public InternalLayout(ChromeProgressBar progressBar)
            {
                this._progressBar = progressBar;
            }
            #endregion

            #region Properties

            public ChromeProgressBar ProgressBar
            {
                get { return _progressBar; }
            }

            public Rectangle ProgressRectangle
            {
                get
                {
                    Rectangle progressRect = new Rectangle(DEF_PADDING, DEF_PADDING, this.ProgressBar.Height - DEF_PADDING, this.ProgressBar.Height - DEF_PADDING);

                    if (progressRect.Height < MIN_HEIGHT)
                        progressRect = new Rectangle(DEF_PADDING, DEF_PADDING, MIN_HEIGHT, MIN_WIDTH);

                    return progressRect;
                }
            }

            public Rectangle TextRectangle
            {
                get
                {
                    Rectangle txtRect = new Rectangle(SeparatorLocation + DEF_PADDING,2 * DEF_PADDING,
                        ProgressBar.Width - ProgressRectangle.Width - 4 * DEF_PADDING, ProgressBar.Height - 2 * DEF_PADDING);
                    
                    return txtRect;
                }
            }

            public int SeparatorLocation
            {
                get
                {
                    return this.ProgressRectangle.Right + DEF_PADDING + SEPARATOR_WIDTH;
                }
            }

            public Rectangle IconRectangle
            {
                get 
                {
                    Size sz = ProgressBar.Icon.Size;
                    Point loc = Point.Empty;
                    Rectangle iconRect = Rectangle.Empty;

                    if (sz.Height > 16 || sz.Width > 16)
                        sz = new Size(16, 16);

                    loc = new Point(ProgressRectangle.Left + (ProgressRectangle.Width - sz.Width) / 2,
                           ProgressRectangle.Top + (ProgressRectangle.Height - sz.Height) / 2);

                    return new Rectangle(loc, sz);
                }
            }

            public Size MinSize
            {
                get 
                {
                    return new Size(this.ProgressRectangle.Width + SEPARATOR_WIDTH + this.TextRectangle.Width + 4 * DEF_PADDING,
                        this.ProgressBar.Height);
                }
            }
            
            #endregion
        }
        #endregion
    }
}
