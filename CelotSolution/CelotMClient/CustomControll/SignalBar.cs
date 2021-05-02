using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CelotMClient.CustomControll
{
    public class SignalBar : Control
    {
        /// <summary>
        /// Constructor. Initializes a new instance of the DateTimePicker class.
        /// </summary>
        public SignalBar()
        {
            this.Size = new Size(20, 20);
            this.Text = String.Empty;

            minimum = 0;
            maximum = 99;
            currentValue = 99;

            BackColor = Color.LightSteelBlue;
            color1 = Color.Red;
            color2 = Color.DarkOrange;
            color3 = Color.Yellow;
            color4 = Color.LightGreen;
        }

        protected const int barcount = 4;

        protected Color color1;

#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The minimum value of the control.")]
#endif
        public Color Color1
        {
            set
            {
                color1 = value;
                Invalidate();
            }

            get
            {
                return color1;
            }
        }

        protected Color color2;

#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The minimum value of the control.")]
#endif
        public Color Color2
        {
            set
            {
                color2 = value;
                Invalidate();
            }

            get
            {
                return color2;
            }
        }

        protected Color color3;

#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The minimum value of the control.")]
#endif
        public Color Color3
        {
            set
            {
                color3 = value;
                Invalidate();
            }

            get
            {
                return color3;
            }
        }

        protected Color color4;

#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The minimum value of the control.")]
#endif
        public Color Color4
        {
            set
            {
                color4 = value;
                Invalidate();
            }

            get
            {
                return color4;
            }
        }


        protected int minimum;

#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Design")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The minimum value of the control.")]
#endif
        public int Minimum
        {
            get
            {
                return minimum;
            }

            set
            {
                minimum = value;
                Invalidate();
            }
        }


        protected int maximum;
#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Design")]
		[System.ComponentModel.DefaultValue(9)]
		[System.ComponentModel.Description("The maximum value of the control.")]
#endif
        public int Maximum
        {
            get
            {
                return maximum;
            }

            set
            {
                maximum = value;
                Invalidate();
            }
        }

        protected int currentValue;
#if NETCFDESIGNTIME
		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.Category("Design")]
		[System.ComponentModel.DefaultValue(0)]
		[System.ComponentModel.Description("The current value of the control.")]
#endif
        public int CurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                currentValue = value;
                this.Invalidate();
            }
        }

        // offscreen bitmap
        Bitmap m_bmp;
        Graphics m_graphics;

        // drawing methods
        protected void DrawBar(int index, Color color)
        {
            int barWidth = this.Width / barcount;
            int height = Convert.ToInt16(Height * (index + 1) / barcount);
            Rectangle r = new Rectangle(
                (index * barWidth) + 1, Height - height,
                barWidth - 1, height);

            SolidBrush br = new SolidBrush(color);

            m_graphics.FillRectangle(br, r);
        }


        protected Color GetColor(int index)
        {
            switch (index)
            {
                case 0:
                    return color1;
                case 1:
                    return color2;
                case 2:
                    return color3;
                case 3:
                    return color4;
                default:
                    return color1;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // draw to memory bitmap
            CreateMemoryBitmap();

            // init background
            m_graphics.Clear(this.BackColor);

            // determine how many bars should be drawn
            int nBar = (currentValue - minimum) * barcount / (maximum - minimum);

            // draw back bars
            for (int i = 0; i < 4; i++)
            {
                DrawBar(i, Color.FromArgb(150, 255,255,255));
            }

            // draw bars
            for (int i = 0; i < nBar; i++)
            {
                DrawBar(i, GetColor(i));
            }

            // blit memory bitmap to screen
            e.Graphics.DrawImage(m_bmp, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // don't pass to base since we paint everything, avoid flashing
        }


        // events



        // helper methods	
        /// <summary>
        /// Create offsceeen bitmap. This bitmap is used for double-buffering
        /// to prevent flashing.
        /// </summary>
        private void CreateMemoryBitmap()
        {
            if (m_bmp == null || m_bmp.Width != this.Width || m_bmp.Height != this.Height)
            {
                // memory bitmap
                m_bmp = new Bitmap(this.Width, this.Height);
                m_graphics = Graphics.FromImage(m_bmp);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

    }
}
