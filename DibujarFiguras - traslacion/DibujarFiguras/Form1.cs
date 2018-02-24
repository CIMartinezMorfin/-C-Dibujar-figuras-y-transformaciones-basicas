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
            g.Clear(Color.White);
            for (int i = 1; i < listaPuntos.Count; i++)
            {
                g.DrawLine(lapiz, listaPuntos[i - 1], listaPuntos[i]);
            }
            g.DrawLine(lapiz, listaPuntos[listaPuntos.Count - 1], listaPuntos[0]);
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

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) {
for (int i = 0; i < listaPuntos.Count; i++) {
                var p = listaPuntos[i];
                p.X += tx;
                p.Y += ty;
                listaPuntos[i] = p;
            }

            DibujarPoligono();
            }
            

        }
    }
}
