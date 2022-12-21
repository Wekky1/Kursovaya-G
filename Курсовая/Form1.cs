using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class Form1 : Form
    {
        //  List<Emitter> emitters = new List<Emitter>();
        Emitter snow, emitter1;
        int task;
        public Form1()
        {
            
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            task = 2;
            snow = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };

            emitter1 = new Emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 100,
                LifeMax = 100,
                LifeMin = 20,
                ColorFrom = Color.Red,
                ColorTo = Color.FromArgb(0, Color.Purple),
                ParticlesPerTick = 10,
                GravitationY = 1,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            tbLife.Visible = false; lifeInfo.Visible = false; lbLife.Visible = false;
            tbSpreading.Visible = false; spreadInfo.Visible = false; lbSpred.Visible = false;
            tbDirection.Visible = false; dirInfo.Visible = false; lbDir.Visible = false;
            tbTick.Visible = false; tickInfo.Visible = false; lbParticles.Visible = false;
            tbSpeed.Visible = false; speedInfo.Visible = false; lbSpeed.Visible = false;
            tbCntrl.Visible = false;


        }

        //  ---------------------- Переход к заданиям  -----------------------------

        private void button2_Click(object sender, EventArgs e) // 2   Точка в центре
        {
            task = 2;
            tbDirection.Visible = true; dirInfo.Visible = true; lbDir.Visible = true;
            tbSpreading.Visible = true; spreadInfo.Visible = true; lbSpred.Visible = true;
            tbTick.Visible = true; tickInfo.Visible = true; lbParticles.Visible = true;
            tbLife.Visible = true; lifeInfo.Visible = true; lbLife.Visible = true;
            tbSpeed.Visible = true; speedInfo.Visible = true; lbSpeed.Visible = true;

            tbCntrl.Visible = false;
            
            

        }

        private void button3_Click(object sender, EventArgs e) // 5 - 6
        {
            task = 56;
            tbCntrl.Visible = true;
            tbTick.Visible = true; tickInfo.Visible = true; lbParticles.Visible = true;
            
            tbLife.Visible = false; lifeInfo.Visible = false; lbLife.Visible = false;
            tbSpreading.Visible = false; spreadInfo.Visible = false; lbSpred.Visible = false;
            tbDirection.Visible = false; dirInfo.Visible = false; lbDir.Visible = false;
            tbSpeed.Visible = false; speedInfo.Visible = false; lbSpeed.Visible = false;

        }

        //  ------------------------------------------------------------------------


        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            // Цвет 
            if (tbCntrl.SelectedIndex == 0)
            {
                foreach (var p in snow.impactPoints)
                {
                    if (p is ColorPoint)
                    {
                        var x = (p as ColorPoint).X - e.X;
                        var y = (p as ColorPoint).Y - e.Y;
                        double r = Math.Sqrt(x * x + y * y);
                        if (r <= (p as ColorPoint).Power / 2)
                        {
                            if (e.Delta < 0 && (p as ColorPoint).Power > 30)
                            {
                                (p as ColorPoint).Power -= 10;
                            }
                            if (e.Delta > 0 && (p as ColorPoint).Power < 300)
                            {
                                (p as ColorPoint).Power += 10;
                            }
                        }
                    }
                }
            }
            // Счетчик 
            if (tbCntrl.SelectedIndex == 1)
            {
                foreach (var p in snow.impactPoints)
                {
                    if (p is CountPoint)
                    {
                        var x = (p as CountPoint).X - e.X;
                        var y = (p as CountPoint).Y - e.Y;
                        double r = Math.Sqrt(x * x + y * y);
                        if (r <= (p as CountPoint).Power / 2)
                        {
                            if (e.Delta < 0 && (p as CountPoint).Power > 30)
                            {
                                (p as CountPoint).Power -= 10;
                            }
                            if (e.Delta > 0 && (p as CountPoint).Power < 300)
                            {
                                (p as CountPoint).Power += 10;
                            }
                        }
                    }
                }
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            switch (task)
            {
                case 56:
                    {
                        snow.UpdateState();
                        using (var g = Graphics.FromImage(picDisplay.Image))
                        {

                            g.Clear(Color.Black);
                            snow.Render(g);

                        }

                        picDisplay.Invalidate();
                    } break;

                case 2:
                    {
                        emitter1.UpdateState();
                        using (var g = Graphics.FromImage(picDisplay.Image))
                        {

                            g.Clear(Color.Black);
                            emitter1.Render(g);

                        }

                        picDisplay.Invalidate();
                    } break;

            }

        }


        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {

            if(e.Button == MouseButtons.Left)
            {
                if(tbCntrl.SelectedIndex == 0)
                {
                    int x = e.X;
                    int y = e.Y;
                    Color color = Color.Red;
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            color = Color.Red;
                            break;
                        case 1:
                            color = Color.Navy;
                            break;
                        case 2:
                            color = Color.Yellow;
                            break;
                        case 3:
                            color = Color.Purple;
                            break;
                        case 4:
                            color = Color.Green;
                            break;
                    }
                    snow.impactPoints.Add(new ColorPoint
                    {
                        X = x,
                        Y = y,
                        color = color
                    }) ;
                }
                if (tbCntrl.SelectedIndex == 1)
                {
                    int x = e.X;
                    int y = e.Y;
                    snow.impactPoints.Add(new CountPoint
                    {
                        X = x,
                        Y = y
                    });
                }

                if (tbCntrl.SelectedIndex == 2)
                {

                }

            }



            if(e.Button == MouseButtons.Right)
            
            {
                if(tbCntrl.SelectedIndex == 0)
                {
                    foreach (var p in snow.impactPoints)
                    {
                        if (p is ColorPoint)
                        {
                            var x = (p as ColorPoint).X - e.X;
                            var y = (p as ColorPoint).Y - e.Y;
                            double r = Math.Sqrt(x * x + y * y);
                            if (r <= (p as ColorPoint).Power / 2)
                            {
                                snow.impactPoints.Remove((p as ColorPoint));
                                break;
                            }
                        }
                    }
                }
                if (tbCntrl.SelectedIndex == 1)
                {
                    foreach (var p in snow.impactPoints)
                    {
                        if (p is CountPoint)
                        {
                            var x = (p as CountPoint).X - e.X;
                            var y = (p as CountPoint).Y - e.Y;
                            double r = Math.Sqrt(x * x + y * y);
                            if (r <= (p as CountPoint).Power / 2)
                            {
                                snow.impactPoints.Remove((p as CountPoint));
                                break;
                            }
                        }
                    }
                }
            }
        }


        // -----------------------------------------------------
        // -----------------Направление  -----------------------
        private void tbSpeed1_Scroll(object sender, EventArgs e)
        {
            emitter1.Direction = tbDirection.Value;
            dirInfo.Text = emitter1.Direction.ToString();
        }
        
        // ----------------- Разброс  -------------------------
        private void tbSpreading_Scroll(object sender, EventArgs e)
        {
            emitter1.Spreading = tbSpreading.Value;
            spreadInfo.Text = emitter1.Spreading.ToString();
        }
        // ------------------ Жизька --------------------------
        private void tbLife_Scroll(object sender, EventArgs e)
        {
            emitter1.LifeMax = tbLife.Value;
            lifeInfo.Text = emitter1.LifeMax.ToString();
        }
        // ------------------ Скорость --------------------------
        private void tbSpeed_Scroll(object sender, EventArgs e)
        {
            emitter1.SpeedMax = tbSpeed.Value;
            speedInfo.Text = emitter1.SpeedMax.ToString();
        }
         // --------------- Кол-во чатсиц -----------------------
        private void tbTick_Scroll(object sender, EventArgs e)
        {
            snow.ParticlesPerTick = tbTick.Value;
            tickInfo.Text = snow.ParticlesPerTick.ToString();

            emitter1.ParticlesPerTick = tbTick.Value;
            tickInfo.Text = emitter1.ParticlesPerTick.ToString();
        }
        // ------------------------------------------------------
    }
}
