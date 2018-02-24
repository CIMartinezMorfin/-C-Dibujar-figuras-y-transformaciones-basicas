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
        Graphics g;List<PointF> listaPuntos = new List<PointF>();Pen lapiz = new Pen(Color.Black, 1);
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            this.KeyPreview = true;
            label5.Text =""+ this.Width / 2;
            label6.Text = "" + this.Height / 2;
        }

        private void CrearPoligono(int numLados, float radio)
        {
            listaPuntos.Clear();
            double a = (Math.PI*2) / numLados;
            
            for (int i = 0; i < numLados; i++) {
                float x, y;
                x =(float)(radio * Math.Cos(a*i));
                y = (float)(radio * Math.Sin(a*i));
                x += this.Width / 2;
                y += this.Height / 2;
                listaPuntos.Add(new PointF(x, y));
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
            for (int i = 1; i < listaPuntos.Count; i++)
            {
                g.DrawLine(lapiz, listaPuntos[i - 1], listaPuntos[i]);
            }
            g.DrawLine(lapiz, listaPuntos[listaPuntos.Count - 1], listaPuntos[0]);
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

                    p.X =(float) (x_p);
                    p.Y = (float) (y_p);
                    listaPuntos[i] = p;
                }
                DibujarPoligono();

            } catch (Exception IO){ }
        }
    }
}
