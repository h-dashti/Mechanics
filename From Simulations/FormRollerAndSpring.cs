using System;
using System.Drawing;
using System.Windows.Forms;

namespace MechanicalSimulations
{
    public partial class FormRollerAndSpring : Form
    {
        //---------------------------- Variables
        private double x, y, s, v, t, dt = 0.005;
        private int currenti;
        
        private int n = 5;
        private Size margin = new Size(55, 35);

        private RollerAndSpring rollerAndSpring;
        private double scaleAnimate = 50;
       
        private PlotClass plotClass;
        private int transX, transY;
        private bool isCatchBall, isCatchSp;
        //Point ptBehind_sp;
        int widthBall = 20;
        Rectangle recSpOrg = new Rectangle(-10, -10, 20, 20);
        private double xSpring, ySpring;

        Bitmap bitNow;
        //------------------------------------------

        public FormRollerAndSpring()
        {
            InitializeComponent();
            InitializeFirst();
        }
        private void PrintResults()
        {
            string data =
            "x   = " + x.ToString() + "\r\n" +
            "y  = " + y.ToString() + "\r\n" +
            "v  = " + v.ToString() + "\r\n" +
            "s  = " + s.ToString() + "\r\n" +
            "Transverse Acc =" + "\r\n" +
            rollerAndSpring.F_By_Currenti(s, v, currenti).ToString() + "\r\n" + "\r\n" +

            "t     = " + t.ToString() + "\r\n" + "\r\n" +

            "Mechanical Energy  = " + "\r\n" +
            rollerAndSpring.MechanicalEnergy(y, v, x) + "\r\n" + "\r\n" + "\r\n" +
            "F Spring =" + "\r\n" +
            rollerAndSpring.F_Spring(currenti).ToString() + "\r\n" +
            "x_Sp = " + xSpring.ToString() + "\r\n" +
            "y_Sp = " + ySpring.ToString();

            labelResult.Text = data;

        }

        private void InitializeFirst()
        {
            
            labelX.Text = comboBoxX.Text;
            labelY.Text = comboBoxY.Text;
            
            textBoxv0.Text = "0";
            textBoxm.Text = "0.5";
            textBoxb.Text = "0";
            textBoxg.Text = "9.8";
            textBoxk.Text = "3";
            textBoxR0.Text = "1";

            timer1.Interval = 10;

            InitializeHump();
            

            ResetData();

        }// End Initialize1

        private void InitializeHump()
        {
            xSpring = 0.7;
            ySpring = 0.5;
            scaleAnimate = 60;
            widthBall = 20;
            transY = pictureBoxAnimate.Height;
            transX = pictureBoxAnimate.Width / 2;
            labelShape.Text = "Hump";
            s = 27;
           
        }
        private void InitializeInfinity()
        {
            xSpring = 0.5;
            ySpring = 0.2;
            scaleAnimate = 150;
            widthBall = 20;
            transY = pictureBoxAnimate.Height / 2;
            transX = pictureBoxAnimate.Width / 2;
            labelShape.Text = "Infinity";

            s = 1;
            //RollerAndSpring = new RollerAndSpring();
           // rollerAndSpring.CreateInfinteArrays();
           
        }
        private void InitializeCardiod()
        {
            xSpring = -0.2;
            ySpring = 0.5;
            scaleAnimate = 100;
            widthBall = 20;
            transY = pictureBoxAnimate.Height / 2 ;
            transX = pictureBoxAnimate.Width / 8;
            labelShape.Text = "Cardiod";
            s = 1;
        }

        private void InitializeButterfly()
        {
            xSpring = 0.0;
            ySpring = 2.5;
            widthBall = 14;
            scaleAnimate = 55;
            transY = pictureBoxAnimate.Height / 2;
            transX = pictureBoxAnimate.Width / 2 - 40;
            labelShape.Text = "Butterfly";
            s = 3;

        }

        private void InitializeOval()
        {
            xSpring = 0.5;
            ySpring = 1.2;
            scaleAnimate = 150;
            widthBall = 20;
            transY =  pictureBoxAnimate.Height / 2 + 10;
            transX = pictureBoxAnimate.Width / 2;
            labelShape.Text = "Oval";
            s = 1;

        }
        private void InitializeSpiral()
        {
            xSpring = -5.5;
            ySpring = 1;
            scaleAnimate = 30;
            widthBall = 20;
            transY = pictureBoxAnimate.Height / 2 + 60;
            transX = pictureBoxAnimate.Width / 2;
            labelShape.Text = "Spiral";
            s = 2.5;
        }

        private void InitializeCircle()
        {
            xSpring = 0.3;
            ySpring = 0.2;
            scaleAnimate = 150;
            widthBall = 20;
            transY = pictureBoxAnimate.Height / 2 + 10;
            transX = pictureBoxAnimate.Width / 2;
            labelShape.Text = "Circle";
            s = 1.5;
        }

        // End Initialize Shapes

        private void ResetData()
        {
            if (buttonPlay.Enabled)
                numericUpDownPath.Enabled = true;
            isCatchBall = isCatchSp = false;
            v = double.Parse(textBoxv0.Text);
            t = 0.0;
            rollerAndSpring = new RollerAndSpring();
            rollerAndSpring.m = double.Parse(textBoxm.Text);
            rollerAndSpring.b = double.Parse(textBoxb.Text);
            rollerAndSpring.g = double.Parse(textBoxg.Text);
            rollerAndSpring.R0 = double.Parse(textBoxR0.Text);
            rollerAndSpring.k = double.Parse(textBoxk.Text);
            rollerAndSpring.x_s = xSpring;
            rollerAndSpring.y_s = ySpring;
            switch (labelShape.Text)
            {
                case "Hump" :
                    
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateHumpArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = false;
                    break;
                case "Infinity":
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateInfinteArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    break ;
                case "Cardiod" :
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateCardiodArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    break;
                case "Butterfly" :
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateButterflyArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    break;
                case "Oval":
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateOvalArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    
                    break;

                case "Spiral":
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateSpiralArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    
                    break;
                case "Circle":
                    numericUpDownPath.Value = 0.0001M;
                    rollerAndSpring.CreateCircleArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    
                    break;
                default  :
                    break;

            }
            rollerAndSpring.Findxyi(s, out x, out y, out currenti);

            plotClass = new PlotClass(pictureBoxCurve.Size, checkBoxConnectDots.Checked,
                 pictureBoxAnimate.BackColor, 1, 1, 2, 2, margin);

            plotClass.AddFirstPoint(
                ValueString(comboBoxX.Text, s, v, x, y, t, currenti),
                ValueString(comboBoxY.Text, s, v, x, y, t, currenti));
            pictureBoxCurve.Invalidate();

            PrintResults();
            if(buttonPlay.Enabled)
                TurnOnMouseHandles();
            SetBitMaps(pictureBoxAnimate.Width, pictureBoxAnimate.Height, transX, transY, pictureBoxAnimate.BackColor);
            pictureBoxAnimate.Invalidate();

        }// End ResetData
        private double ValueString(string st, double s, double v, double x, double y, double t, int currenti)
        {
            switch (st)
            {
                case "s":
                    return s;
                case "v":
                    return v;
                case "x" :
                    return x;
                case "y" :
                    return y;

                case "At":
                    return rollerAndSpring.F_By_Currenti(s, v, currenti);
                case "F_spring" :
                    return rollerAndSpring.F_Spring(currenti);
                
                case "Mechanical E":
                    return rollerAndSpring.MechanicalEnergy(y, v, x);

                case "t":
                    return t;
                default:
                    return 0;

            }// End ValueString
        }


        private void ChangeMainShapes(object sender, EventArgs e)
        {
            if (sender == toolStripButtonInfinity)
            {
                labelShape.Text = "Infinity";
                InitializeInfinity();
            }
            else if (sender == toolStripButtonHump)
            {
                labelShape.Text = "Hump";
                InitializeHump();
            }
            else if (sender == toolStripButtonCardiod)
            {
                labelShape.Text = "Cardiod";
                InitializeCardiod();
            }
            else if (sender == toolStripButtonButterfly)
            {
                labelShape.Text = "Butterfly";
                InitializeButterfly();
            }
            else if (sender == toolStripButtonOval)
            {
                labelShape.Text = "Oval";
                InitializeOval();
            }

            else if (sender == toolStripButtonSpiral)
            {
                labelShape.Text = "Spiral";
                InitializeSpiral();
            }
            else if (sender == toolStripButtonCircle)
            {
                labelShape.Text = "Circle";
                InitializeCircle();
            }



            TurnOffMouseHandles();
            ResetData();
            pictureBoxAnimate.Invalidate();
        }
        private void TurnOnMouseHandles()
        {
            pictureBoxAnimate.MouseDown += new MouseEventHandler(pictureBoxAnimate_MouseDown);
            pictureBoxAnimate.MouseMove += new MouseEventHandler(pictureBoxAnimate_MouseMove);
            pictureBoxAnimate.MouseUp += new MouseEventHandler(pictureBoxAnimate_MouseUp);
        }
        private void TurnOffMouseHandles()
        {
            pictureBoxAnimate.MouseDown -= new MouseEventHandler(pictureBoxAnimate_MouseDown);
            pictureBoxAnimate.MouseMove -= new MouseEventHandler(pictureBoxAnimate_MouseMove);
            pictureBoxAnimate.MouseUp -= new MouseEventHandler(pictureBoxAnimate_MouseUp);
        }
        void pictureBoxAnimate_MouseUp(object sender, MouseEventArgs e)
        {
            isCatchBall = isCatchSp = false;
            pictureBoxAnimate.Invalidate();

        } //pictureBoxAnimate_MouseUp


        private void FindNearest_I_toMouse(Point pt, double s, double ds , ref int currenti)
        {
            int indexUp = rollerAndSpring.GetIndexWithPereviousI(s + ds, currenti);
            int indexDo = rollerAndSpring.GetIndexWithPereviousI(s - ds, currenti);

            double dUp =
                (rollerAndSpring.X[indexUp] * scaleAnimate - pt.X) * (rollerAndSpring.X[indexUp] * scaleAnimate - pt.X) +
                (rollerAndSpring.Y[indexUp] * scaleAnimate - pt.Y) * (rollerAndSpring.Y[indexUp] * scaleAnimate - pt.Y);

            double dDo =
                (rollerAndSpring.X[indexDo] * scaleAnimate - pt.X) * (rollerAndSpring.X[indexDo] * scaleAnimate - pt.X) +
                (rollerAndSpring.Y[indexDo] * scaleAnimate - pt.Y) * (rollerAndSpring.Y[indexDo] * scaleAnimate - pt.Y);

            if (dUp <= dDo)
                currenti = indexUp;
            else
                currenti = indexDo;


        }// End FindNearestItoMouse

        void pictureBoxAnimate_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCatchBall)
            {
                Point ptBall = new Point(e.X - transX, -(e.Y - transY));
                FindNearest_I_toMouse(ptBall, s, 0.01, ref currenti);
                x = rollerAndSpring.X[currenti];
                y = rollerAndSpring.Y[currenti];
                s = rollerAndSpring.S[currenti];

                plotClass.AddNewPoint(
                ValueString(comboBoxX.Text, s, v, x, y, t, currenti),ValueString(comboBoxY.Text, s, v, x, y, t, currenti));
                pictureBoxCurve.Invalidate();

                PrintResults();
                
               
            }
            else if (isCatchSp)
            {
                int transX_sp = e.X - transX - (int)(rollerAndSpring.x_s * scaleAnimate);
                int transY_sp = -(e.Y - transY) - (int)(rollerAndSpring.y_s * scaleAnimate);
                
                double x_sp = (transX_sp) / scaleAnimate;
                double y_sp = (transY_sp) / scaleAnimate;
                xSpring += x_sp;
                ySpring += y_sp;
                rollerAndSpring.x_s = xSpring;
                rollerAndSpring.y_s = ySpring;

                plotClass.AddNewPoint(
               ValueString(comboBoxX.Text, s, v, x, y, t, currenti),
               ValueString(comboBoxY.Text, s, v, x, y, t, currenti));

                PrintResults();
    
            }
            
            pictureBoxAnimate.Invalidate();

        } // pictureBoxAnimate_MouseMove

        void pictureBoxAnimate_MouseDown(object sender, MouseEventArgs e)
        {
            int transX_sp = e.X - transX - (int)(rollerAndSpring.x_s * scaleAnimate);
            int transY_sp = -(e.Y - transY) - (int)(rollerAndSpring.y_s * scaleAnimate);
            Point ptBehind_sp = new Point(transX_sp, transY_sp);

           Point ptBehind = new Point(e.X - transX, -(e.Y - transY));
            int x0 = (int)Math.Round(x * scaleAnimate);
            int y0 = (int)Math.Round(y * scaleAnimate);

            if ((x0 - ptBehind.X) * (x0 - ptBehind.X) + (y0 - ptBehind.Y) * (y0 - ptBehind.Y) <= widthBall/2 * widthBall/2)  // catchin ball
            {
                isCatchBall = true;
                //Cursor.Position = pictureBoxAnimate.PointToScreen(new Point(x0 + transX, transY  - y0));
              
            }
            else if (recSpOrg.Contains(ptBehind_sp))
            {
                isCatchSp = true;
            }
            pictureBoxAnimate.Invalidate();


        }// pictureBoxAnimate_MouseDown


        private void buttonPlay_Click(object sender, EventArgs e)
        {
            numericUpDownPath.Enabled = false;
            buttonPlay.Enabled = false;
            buttonStop.Enabled = true;
            buttonStop.Focus();
            TurnOffMouseHandles();

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

        }
        void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 1; i <= n; i++)
            {
                rollerAndSpring.UpDate(dt, ref s, ref v, ref x, ref y, ref currenti);
            }
            t += n * dt;

            plotClass.AddNewPoint(
                 ValueString(comboBoxX.Text, s, v, x, y, t, currenti),
                 ValueString(comboBoxY.Text, s, v, x, y, t, currenti));
            pictureBoxCurve.Invalidate();

            PrintResults();

            pictureBoxAnimate.Invalidate();
        }// End Timer1_Tick

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonPlay.Enabled = true;

            timer1.Tick -= new EventHandler(timer1_Tick);
            timer1.Stop();
            //plotClass.RedrawPoints();
            pictureBoxAnimate.Invalidate();

        }

        private void buttonResetData_Click(object sender, EventArgs e)
        {
            TurnOffMouseHandles();
            ResetData();
           
            pictureBoxAnimate.Invalidate();
        }


        private void DoingClear()
        {
            plotClass.ClearData();
            plotClass.AddNewPoint(
                  ValueString(comboBoxX.Text, s, v, x, y, t, currenti),
                  ValueString(comboBoxY.Text, s, v, x, y, t, currenti));
            pictureBoxCurve.Invalidate();

            pictureBoxAnimate.Invalidate();
        }
        private void buttonClearGraph_Click(object sender, EventArgs e)
        {
            DoingClear();
        }

        private void buttonRedrawPoints_Click(object sender, EventArgs e)
        {
            plotClass.RepeatDrawingPointsMap();
            pictureBoxAnimate.Invalidate();
            pictureBoxCurve.Invalidate();
        }

        private void buttonMoreDetail_Click(object sender, EventArgs e)
        {
            FormMoreDetail formMoreDetail = new FormMoreDetail(plotClass, scaleAnimate, dt, timer1.Interval / 1000.0, n);

            DialogResult dialog = formMoreDetail.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                scaleAnimate = formMoreDetail.scaleAnimate;
                this.n = formMoreDetail.n;
                this.dt = formMoreDetail.h;
                timer1.Interval = (int)(formMoreDetail.timerInterval * 1000);
            }
            pictureBoxCurve.Invalidate();
            pictureBoxAnimate.Invalidate();

        }

        private void checkBoxConnectDots_CheckedChanged(object sender, EventArgs e)
        {
            plotClass.isConnectDots = checkBoxConnectDots.Checked;
            pictureBoxAnimate.Invalidate();
        }

        private void comboBoxY_TextChanged(object sender, EventArgs e)
        {
            ResetData();
           
            pictureBoxAnimate.Invalidate();

            labelX.Text = comboBoxX.Text;
            labelY.Text = comboBoxY.Text;

        }

        private void pictureBoxAnimate_Paint(object sender, PaintEventArgs e)
        {
            
            //----------------------

            Graphics g = e.Graphics;
            System.Drawing.Drawing2D.GraphicsState gs = g.Save();
            g.TranslateTransform(0, pictureBoxAnimate.Height);
            g.ScaleTransform(1, -1);
            g.DrawImage(bitNow, 0, 0);
            g.Restore(gs);


            // Drawin mass
            g.TranslateTransform(transX, transY);
            g.ScaleTransform(1, -1);
            Rectangle rectMass = new Rectangle(
                (int)Math.Round(x * scaleAnimate) - widthBall/2,
                (int)Math.Round(y * scaleAnimate) - widthBall/2, widthBall, widthBall);

            g.FillEllipse(new SolidBrush(Color.DarkBlue), rectMass);



            // Drawing Spring
            g.TranslateTransform((int)(rollerAndSpring.x_s * scaleAnimate), (int)(rollerAndSpring.y_s * scaleAnimate));
            g.DrawRectangle(new Pen(Color.Blue),recSpOrg);


            double alfa = Math.Atan2(y - ySpring, x - xSpring);
            g.RotateTransform((float)(alfa * 180 / Math.PI));
            Color colorSpring = Color.Green;
            double r = Math.Sqrt((x - xSpring) * (x - xSpring) + (y - ySpring) * (y - ySpring));
            if (r < rollerAndSpring.R0)
                colorSpring = Color.Red;

            int lspring = (int)Math.Round(r * scaleAnimate);
            int peaks = 13;
            int temp = -8;
            Point[] pts = new Point[peaks + 4];
            for (int i = 2; i < pts.Length - 2; i++)
            {
                pts[i] = new Point(5 + (2 * i - 3) * (lspring - 10) / (2 * peaks), temp);
                temp = -temp;
            }
            pts[0] = new Point(0, 0);
            pts[1] = new Point(5, 0);
            pts[pts.Length - 2] = new Point(lspring - 5, 0);
            pts[pts.Length - 1] = new Point(lspring, 0);

            g.DrawLines(new Pen(colorSpring), pts);



        }// End PainAnimate

        private void SetBitMaps(int width, int height, int transx, int transy, Color backColor)
        {
            bitNow = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitNow);
            g.Clear(backColor);

            // Drawing path Curve
            System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();

            int lenghtArr = rollerAndSpring.X.Length;
            int nums = lenghtArr / 1000;
            for (int i = 0; i < rollerAndSpring.S.Length; i++)
            {
                if (i % nums == 0 || nums == 0)
                    gPath.AddLine(
                        (int)Math.Round(scaleAnimate * rollerAndSpring.X[i]) + transx,
                        (int)Math.Round(scaleAnimate * rollerAndSpring.Y[i]) - transy + height,
                        (int)Math.Round(scaleAnimate * rollerAndSpring.X[i]) + transx,
                        (int)Math.Round(scaleAnimate * rollerAndSpring.Y[i]) - transy + height);

            }

            g.DrawPath(new Pen(Color.Black), gPath);
            g.DrawRectangle(new Pen(Color.White, 2),
                (int)Math.Round(scaleAnimate * rollerAndSpring.X[0]) - 1 + transx,
                (int)Math.Round(scaleAnimate * rollerAndSpring.Y[0]) - 1 - transy + height, 2, 2);  // Draw point s=0
            // Draw Arrow

            int locate0 = lenghtArr / 5;
            int locate = locate0;
            for (int i = 1; i <= 4; i++)
            {
                System.Drawing.Drawing2D.GraphicsState gState = g.Save();

                if (locate >= lenghtArr - 2)
                    break;
                int xArrow = (int)Math.Round(scaleAnimate * rollerAndSpring.X[locate]) + transx;
                int yArrow = (int)Math.Round(scaleAnimate * rollerAndSpring.Y[locate]) - transy + height;
                Point[] ptsArrow = new Point[3]{
                new Point(- 8, +4), new Point(0, 0), new Point(-8, -4)};

                float angle = (float)(Math.Atan2(rollerAndSpring.Y[locate + 1] - rollerAndSpring.Y[locate],
                    rollerAndSpring.X[locate + 1] - rollerAndSpring.X[locate]));

                g.TranslateTransform(xArrow, yArrow);
                g.RotateTransform((float)(angle * 180 / Math.PI));
                g.DrawLines(new Pen(Color.Blue, 2), ptsArrow);
                g.Restore(gState);
                locate += locate0;
            }

            //----------------------


        }// End SetBitmap
        //---------------------------------------------------------------
        private void numericUpDownPath_ValueChanged(object sender, EventArgs e)
        {
            switch (labelShape.Text)
            {
                case "Hump":

                    rollerAndSpring.CreateHumpArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = false;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                case "Infinity":

                    rollerAndSpring.CreateInfinteArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                case "Cardiod":

                    rollerAndSpring.CreateCardiodArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                case "Butterfly":

                    rollerAndSpring.CreateButterflyArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                case "Oval":

                    rollerAndSpring.CreateOvalArrays((double)numericUpDownPath.Value);

                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;

                case "Spiral":

                    rollerAndSpring.CreateSpiralArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                case "Circle":

                    rollerAndSpring.CreateCircleArrays((double)numericUpDownPath.Value);
                    rollerAndSpring.curveisClosed = true;
                    rollerAndSpring.Findxyi(s, out x, out y, out currenti);
                    break;
                default:
                    break;
            }
            pictureBoxAnimate.Invalidate();

        }

        private void pictureBoxCurve_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(0, pictureBoxCurve.Height);
            g.ScaleTransform(1, -1);

            g.DrawImage(plotClass.map, 0, 0);
            Rectangle rectPtHead = new Rectangle(plotClass.ptHead - new Size(3, 3), new Size(6, 6));
            g.DrawRectangle(new Pen(Color.Black), rectPtHead);
        }

        private void FormRollerAndSpring_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
            }
        }

        private void buttonTakeCurvePic_Click(object sender, EventArgs e)
        {
            string dir = this.Text + "(" + labelShape.Text + ")" + " Images";
            System.IO.Directory.CreateDirectory(dir);
            string st0 = dir + "\\" + labelY.Text + "(" + labelX.Text + ")";
            string file0 = st0 + ".jpg";
            string file = file0;
            if (System.IO.File.Exists(file))
            {
                int i = 1;
                do { file = st0 + "_" + i + ".jpg"; i++; }
                while (System.IO.File.Exists(file));
            }

            Bitmap curveBit = new Bitmap(pictureBoxCurve.Width, pictureBoxCurve.Height);
            pictureBoxCurve.DrawToBitmap(curveBit, new Rectangle(0, 0, pictureBoxCurve.Width, pictureBoxCurve.Height));
            curveBit.Save(file);
            curveBit.Dispose();
        }

        

        
    }
    //****************************************************
    //****************************************************
    public class RollerAndSpring
    {
        public double[] X, Y, S;
        public double g, b, m;
        public double x_s, y_s, k, R0; // spring parameters
        public bool curveisClosed;

        public RollerAndSpring()
        {
        }
        public RollerAndSpring(double m, double b, double g, bool curveisClosed,
            double x_s, double y_s, double k, double R0)
        {
            this.m = m;
            this.b = b;
            this.g = g;
            this.curveisClosed = curveisClosed;
            this.x_s = x_s;
            this.y_s = y_s;
            this.k = k;
            this.R0 = R0;
        }


        //public double Finds(double x, double y, int j, int num)
        //{
        //    double dmin;
        //    int index = 0;
        //    if (j == 0)
        //        index = 1;
        //    else if (j - num < 0)
        //        index = 0;


        //    dmin = (x - X[index]) * (x - X[index]) + (y - Y[index]) * (y - Y[index]);
        //    for (int i = index+1; (i <= j+ num) && (i < X.Length); i++)
        //    {
        //        if (i == j)
        //            continue;
        //        double d = (x - X[i]) * (x - X[i]) + (y - Y[i]) * (y - Y[i]);
        //        if (d < dmin)
        //        {
        //            dmin = d;
        //            index = i;
        //        }
        //    }
        //    return S[index];
        //}

        public  double Finds(double x, double y)
        {
            double dmin;
            int index = 0;
            dmin = (x - X[0]) * (x - X[0]) + (y - Y[0]) * (y - Y[0]);
            for (int i = 1; i < S.Length; i++)
            {
                double d = (x - X[i]) * (x - X[i]) + (y - Y[i]) * (y - Y[i]);
                if(d < dmin)
                {
                    dmin = d;
                    index = i;
                }     
            }
            return S[index];
            
        }// End Finds

        public void Findxyi(double s, out double x, out double y, out int currenti)
        {
            int index = 0;
            for (int i = 0; i < S.Length; i++)
            {
                if (s == S[i])
                {
                    index = i;
                    break;
                }
                else if (s > S[i] && s < S[i + 1])
                {
                    if (s - S[i] <= S[i + 1] - s)
                        index = i;
                    else
                        index = i + 1;

                    break;
                }
            }// end for

            x = X[index];
            y = Y[index];
            currenti = index;

        }
        private void Findxy2(double s, ref double x, ref double y, ref int currenti)
        {
            int index = 0;

            if (s >= S[currenti])
            {
                for (int i = currenti; i < S.Length; i++)
                {
                    if (s == S[i])
                    {
                        index = i;
                        break;
                    }
                    else if (s > S[i] && s < S[i + 1])
                    {
                        if (s - S[i] <= S[i + 1] - s)
                            index = i;
                        else
                            index = i + 1;
                        break;

                    }

                }// end for
            }// End if exist in up

            else
            {
                for (int i = currenti; i >= 0; i--)
                {
                    if (s == S[i])
                    {
                        index = i;
                        break;
                    }
                    else if (s < S[i] && s > S[i -1])
                    {
                        if (s - S[i - 1] <= S[i] - s)
                            index = i - 1;
                        else
                            index = i;

                        break;
                    }

                }// end for
            }//  End if exist in down

            x = X[index];
            y = Y[index];
            currenti = index;
        }
        public int GetIndex(double s)
        {
            if (s >= S[S.Length - 1])
                s -= S[S.Length - 1];  
            else if (s < 0)
                s += S[S.Length - 1];
          


            for (int i = 0; i < S.Length - 1; i++)
            {
                if (s == S[i])
                    return i;
                else if (s > S[i] && s < S[i + 1])
                    return i;
            }
            return S.Length - 2;
        }// End GetIndex
        public int GetIndexWithPereviousI(double s, int beforei)
        {
            if (s >= S[S.Length - 1])
            {
                s -= S[S.Length - 1];
                for (int i = 0; i < S.Length - 1; i++)
                {
                    if (s == S[i])
                        return i;
                    else if (s > S[i] && s < S[i + 1])
                        return i;
                }
            }
            else if (s < 0)
            {
                s += S[S.Length - 1];
                for (int i = S.Length - 1; i >= 0; i--)
                {
                    if (s == S[i])
                        return i;
                    else if (s > S[i - 1] && s < S[i])
                        return i - 1;
                }
            }
             

            if (s >= S[beforei])  // target is up
            {
                for (int i = beforei; i < S.Length - 1; i++)
                {
                    if (s == S[i])
                        return i;
                    else if (s > S[i] && s < S[i + 1])
                        return i;
                }
            } 
            else  // target is down
            {
                for (int i = beforei; i >= 0; i--)
                {
                    if (s == S[i])
                        return i;
                    else if (s > S[i - 1] && s < S[i])
                        return i - 1;
                }
            }

            return S.Length - 2;

        }// End GetIndexWithBeforI

        private double F(double s, double v, int beforei)
        {
            int i = GetIndexWithPereviousI(s, beforei);
            double dx = X[i + 1] - X[i];
            double dy = Y[i + 1] - Y[i];
            double dr = Math.Sqrt(dx * dx + dy * dy);

            double r = Math.Sqrt((X[i] - x_s) * (X[i] - x_s) + (Y[i] - y_s) * (Y[i] - y_s));
            double f_Sp;
            if (r == 0)
                f_Sp = 0;
            else  // k is constant of Spring
                f_Sp = -k * (r - R0) / r * ((X[i] - x_s) * dx + (Y[i] - y_s) * dy) / dr;

            return -g * dy / dr - b * v / m + f_Sp / m;


        }// End F

        public double F_By_Currenti(double s, double v, int currenti)
        {
            int i = currenti;
            int iup = i + 1;
            if (iup == X.Length )
                iup = 1;
            double dx = X[iup] - X[i];
            double dy = Y[iup] - Y[i];
            double dr = Math.Sqrt(dx * dx + dy * dy);

            double r = Math.Sqrt((X[i] - x_s) * (X[i] - x_s) + (Y[i] - y_s) * (Y[i] - y_s));
            double f_Sp;
            if (r == 0)
                f_Sp = 0;
            else  // k is constant of Spring
                f_Sp = -k * (r - R0) / r * ((X[i] - x_s) * dx + (Y[i] - y_s) * dy) / dr;

            return -g * dy / dr - b * v / m + f_Sp / m;

            
        }// End F_By_Curreni

        public double F_Spring(int currenti)
        {
            int i = currenti;
            int iup = i + 1;
            if (iup == X.Length)
                iup = 1;
            double dx = X[iup] - X[i];
            double dy = Y[iup] - Y[i];
            double dr = Math.Sqrt(dx * dx + dy * dy);

            double r = Math.Sqrt((X[i] - x_s) * (X[i] - x_s) + (Y[i] - y_s) * (Y[i] - y_s));
            double f_Sp;
            if (r == 0)
                f_Sp = 0;
            else  // k is constant of Spring
                f_Sp = -k * (r - R0) / r * ((X[i] - x_s) * dx + (Y[i] - y_s) * dy) / dr;

            return f_Sp;
        }


        public void UpDate(double dt, ref double s, ref double v, ref double x, ref double y, ref int currenti)
        {
            double
                As, Bs, Cs, Ds,
                Av, Bv, Cv, Dv;


            As = v * dt;
            Av = F(s, v, currenti) * dt;

            Bs = (v + Av / 2) * dt;
            Bv = F(s + As / 2, v + Av / 2, currenti) * dt;

            Cs = (v + Bv / 2) * dt;
            Cv = F(s + Bs / 2, v + Bv / 2, currenti) * dt;

            Ds = (v + Cv) * dt;
            Dv = F(s + Cs, v + Cv, currenti) * dt;


            s += (As + 2 * Bs + 2 * Cs + Ds) / 6;
            v += (Av + 2 * Bv + 2 * Cv + Dv) / 6;

            if (s >= S[S.Length - 1])
                s -= S[S.Length - 1];
            else if (s < 0)
                s += S[S.Length - 1];

            Findxy2(s, ref x, ref y, ref currenti );

        }// End UpDate

        public double MechanicalEnergy(double y, double v, double x)
        {
            double r = Math.Sqrt((x - x_s) * (x - x_s) + (y - y_s) * (y - y_s));
            return m / 2 * v * v + m * g * y + k / 2 * (r - R0) * (r - R0);
        }


       /* public void CreateHumpArrays(double y, double v, double dt)
        {
            curveisClosed = false;
            double t0 = -2.1;
            double te = 2.1;
            double hmax = (v * v / 2 + g * y) / g;
            while (true)
            {
                double ymax = 3 - 7 * te * te / 6 + Math.Pow(te, 4) / 6;
                if (ymax <= hmax)
                {
                    t0 -= 1;
                    te += 1;
                }
                else
                    break;
            }

            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = t;
                Y[i] = 3 - 7 * t * t / 6 + Math.Pow(t, 4) / 6;

                if (i == 0)
                    S[i] = 0;
                else
                    S[i] = S[i - 1] + Math.Sqrt(9 + 49 * t * t - 28 * Math.Pow(t, 4) + 4 * Math.Pow(t, 6)) * dt / 3; ;
            }

        } // CreateHumpArrays 
        */

        public void CreateHumpArrays(double dt)
        {
            curveisClosed = false;
            double t0 = -4.1;
            double te = 4.1;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = t;
                Y[i] = 3 - 7 * t * t / 6 + Math.Pow(t, 4) / 6;

                if (i == 0)
                    S[i] = 0;
                else
                    S[i] = S[i - 1] + Math.Sqrt(9 + 49 * t * t - 28 * Math.Pow(t, 4) + 4 * Math.Pow(t, 6)) * dt / 3; ;
            }
        } // end CreateHumpArrays


        public void CreateCircleArrays(double dt)
        {
            curveisClosed = true;

            double t0 = 0;
            double te = 2 * Math.PI;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = 1 * Math.Cos(t);
                Y[i] = 1 * Math.Sin(t);

                S[i] = 1 * t;
            }
        } // end CreateCircleArrays


        public void CreateInfinteArrays(double dt)
        {
            curveisClosed = true;

            double t0 = -3;
            double te = 3;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = 1 * Math.Sin(t * Math.PI / 3);
                Y[i] = 1 * Math.Sin(2 * t * Math.PI / 3);

                if (i == 0)
                    S[i] = 0;
                else
                {
                    double u1 = Math.PI / 3 * Math.Cos(t * Math.PI / 3);
                    double u2 = 2 * Math.PI / 3 * Math.Cos(2 * t * Math.PI / 3);
                    S[i] = S[i - 1] + Math.Sqrt(u1 * u1 + u2 * u2) * dt;
                }
            }
        }// End CreateInfiniteArrays

        public void CreateCardiodArrays(double dt)
        {
            curveisClosed = true;

            double t0 = 0;
            double te = 2;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = 1 * Math.Cos(t * Math.PI) * (1 + 2 * Math.Cos(t * Math.PI));
                Y[i] = 0.8 * Math.Sin(t * Math.PI) * (1 + 2 * Math.Cos(t * Math.PI));

                if (i == 0)
                    S[i] = 0;
                else
                {
                    double u1 = -2 * Math.PI * Math.Cos(Math.PI * t) * Math.Sin(Math.PI * t) -
                         1 * Math.PI * (1 + 2 * Math.Cos(Math.PI * t)) * Math.Sin(Math.PI * t);

                    double u2 = -1.6 * Math.PI * Math.Sin(Math.PI * t) * Math.Sin(Math.PI * t) +
                          0.8 * Math.PI * (1 + 2 * Math.Cos(Math.PI * t)) * Math.Cos(Math.PI * t);

                    S[i] = S[i - 1] + Math.Sqrt(u1 * u1 + u2 * u2) * dt;
                }
            }
        }// End CreateCardiod

        public void CreateButterflyArrays(double dt)
        {
            curveisClosed = true;
            double t0 = 0;
            double te = 2 * Math.PI;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = Math.Cos(t) *
                    (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 2), 5));

                Y[i] = Math.Sin(t) *
                    (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 2), 5));
                if (i == 0)
                    S[i] = 0;
                else
                {
                    double u1 =
                        -Math.Sin(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 2), 5)) +
                        Math.Cos(t) * (-Math.Sin(t) * Math.Exp(Math.Cos(t)) +
                        8 * Math.Sin(4 * t) - 5 * Math.Cos(t / 2) / 2 * Math.Pow(Math.Sin(t / 2), 4));


                    double u2 = Math.Cos(t) * (Math.Exp(Math.Cos(t)) - 2 * Math.Cos(4 * t) - Math.Pow(Math.Sin(t / 2), 5)) +
                        Math.Sin(t) * (-Math.Sin(t) * Math.Exp(Math.Cos(t)) +
                        8 * Math.Sin(4 * t) - 5 * Math.Cos(t / 2) / 2 * Math.Pow(Math.Sin(t / 2), 4));



                    S[i] = S[i - 1] + Math.Sqrt(u1 * u1 + u2 * u2) * dt;
                }
            }
        }// End ButterflyCraetion

        public void CreateOvalArrays(double dt)
        {
            curveisClosed = true;
            double t0 = 0;
            double te = OvalPath.te;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = OvalPath.FindX_Oval(t);
                Y[i] = OvalPath.FindY_Oval(t);
                S[i] = OvalPath.FindS_Oval(t);
            }
        }// End Creation Oval

        public void CreateSpiralArrays(double dt)
        {
            curveisClosed = true;
            double t0 = 0;
            double te = SpiralPath.te;
            int n = (int)((te - t0) / dt);
            X = new double[n];
            Y = new double[n];
            S = new double[n];

            for (int i = 0; i < n; i++)
            {
                double t = i * dt + t0;
                X[i] = SpiralPath.FindX_Spiral(t);
                Y[i] = SpiralPath.FindY_Spiral(t);
                if (i == 0)
                    S[i] = 0;
                else
                {
                    S[i] = SpiralPath.FindS_Spiral(t, S[i - 1], t - dt, dt);
                }
            }
        }// End Creation Spiral
    }//
    //**********************************************
    //********************************************
    

}