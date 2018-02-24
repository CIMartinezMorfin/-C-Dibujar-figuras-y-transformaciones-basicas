using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DibujarFiguras
{
    public partial class Form1 : Form
    {
        Graphics g;List<Point> listaPuntos = new List<Point>();Pen lapiz = new Pen(Color.Black, 1);
        bool primer_punto;
        int x_origen, y_origen;
        public Form1()
        {
            this.KeyPreview = true;
            InitializeComponent();
            g = this.CreateGraphics();
            this.KeyPreview = true;
            label5.Text =""+ this.Width / 2;
            label6.Text = "" + this.Height / 2;
            primer_punto = false;
            x_origen = this.Width / 2;
            y_origen = this.Height / 2;
        }

        private void CrearPoligono(int numLados, float radio)
        {
            listaPuntos.Clear();
            double a = (Math.PI*2) / numLados;
            
            for (int i = 0; i < numLados; i++) {
                float x, y;
                x =(float)(radio * Math.Cos(a*i));
                y = (float)(radio * Math.Sin(a*i));
                x += x_origen ;
                y += y_origen;
                listaPuntos.Add(new Point((int)x,(int) y));
            }
            
        }   
        private void button1_Click(object sender, EventArgs e)
        {

            int numLados;
            float radio;
            try
            {
                numLados = int.Parse(textBox1.Text);
                radio = float.Parse(textBox2.Text);
                if (numLados >= 3)
                {
                    CrearPoligono(numLados, radio);
                    DibujarPoligono();
                }
                else MessageBox.Show("muy pocos lados... minimo 3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dato invalido u_u");
            }
        }

        private void DibujarPoligono()
        {
 g.Clear(Color.White);
            if (checkBox1.Checked == false)
            {
               
                for (int i = 1; i < listaPuntos.Count; i++)
                {
                    g.DrawLine(lapiz, listaPuntos[i - 1], listaPuntos[i]);
                }
                g.DrawLine(lapiz, listaPuntos[listaPuntos.Count - 1], listaPuntos[0]);
               

            }
            else {

                for (int i = 2; i < listaPuntos.Count; i++)
                {
                    g.DrawLine(lapiz, listaPuntos[i - 2], listaPuntos[i]);
                }
                g.DrawLine(lapiz, listaPuntos[listaPuntos.Count - 2], listaPuntos[0]);
                g.DrawLine(lapiz, listaPuntos[listaPuntos.Count - 1], listaPuntos[1]);
            }

            g.DrawRectangle(new Pen(Color.FromArgb(0, 255, 0), 3), x_origen, y_origen, 3, 3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double angulo = double.Parse(textBox3.Text) * (Math.PI/180)  ;
                double xr = double.Parse(textBox4.Text);
                double yr = double.Parse(textBox5.Text);
                for (int i = 0; i < listaPuntos.Count; i++) {
                    var p = listaPuntos[i];
                    double x_p = 0;
                    double y_p = 0;
                    double sen = Math.Sin(angulo);
                    double cos = Math.Cos(angulo);
                    x_p = xr + ((p.X - xr) * cos) - ((p.Y - yr) * sen);
                    y_p = yr + ((p.Y - yr) * cos) + ((p.X - xr) * sen);

                   x_p = Math.Ceiling(x_p) - x_p <= x_p - Math.Floor(x_p) ? Math.Ceiling(x_p) : Math.Floor(x_p);
                   y_p = Math.Ceiling(y_p) - y_p <= y_p - Math.Floor(y_p) ? Math.Ceiling(y_p) : Math.Floor(y_p);

                    p.X =(int) (x_p);
                    p.Y = (int) (y_p);
                    listaPuntos[i] = p;
                }
                DibujarPoligono();

            } catch (Exception IO){ }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int tx = 0;
            int ty = 0;

            if (e.KeyCode == Keys.Down)
                ty += 10;
            if (e.KeyCode == Keys.Up)
                ty -= 10;
            if (e.KeyCode == Keys.Left)
                tx -= 10;
            if (e.KeyCode == Keys.Right)
                tx += 10;

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < listaPuntos.Count; i++)
                {
                    var p = listaPuntos[i];
                    p.X += tx;
                    p.Y += ty;
                    listaPuntos[i] = p;
                }

                DibujarPoligono();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            primer_punto = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (primer_punto == false) {
                primer_punto = true;
                x_origen = e.X;
                y_origen = e.Y;
                g.DrawRectangle(new Pen(Color.FromArgb(0, 255, 0), 3), e.X, e.Y, 3, 3);
                label5.Text = x_origen+"";
                label6.Text = y_origen + "";
            }
        }
    }
}
