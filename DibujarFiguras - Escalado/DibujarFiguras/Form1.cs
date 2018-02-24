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
                int x, y;
                x =(int)(radio * Math.Cos(a*i));
                y = (int)(radio * Math.Sin(a*i));
                x += this.Width / 2;
                y += this.Height / 2;
                listaPuntos.Add(new Point(x, y));
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
                double magnitud = (double.Parse(textBox3.Text));
                double xr = double.Parse(textBox4.Text);
                double yr = double.Parse(textBox5.Text);
                for (int i = 0; i < listaPuntos.Count; i++) {
                    var p = listaPuntos[i];
                    p.X =(int)(p.X * magnitud+(1-magnitud)*xr);
                    p.Y = (int)(p.Y * magnitud + (1 - magnitud) * yr);
                    listaPuntos[i] = p;
                }
                DibujarPoligono();

            } catch (Exception IO){ }
        }
    }
}
