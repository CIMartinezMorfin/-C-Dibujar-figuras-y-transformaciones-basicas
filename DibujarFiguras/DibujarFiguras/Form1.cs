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
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }

        private void DibujarPoligono(int numLados, float radio)
        {
            g.Clear(Color.White);
            double a = (Math.PI*2) / numLados;
            List<Point> listaPuntos = new List<Point>();
            for (int i = 0; i < numLados; i++) {
                int x, y;
                x =(int)(radio * Math.Cos(a*i));
                y = (int)(radio * Math.Sin(a*i));
                x += this.Width / 2;
                y += this.Height / 2;
                listaPuntos.Add(new Point(x, y));
            }
            Pen lapiz = new Pen(Color.Black, 1);
           
            for (int i = 1; i < numLados; i++){
                g.DrawLine(lapiz, listaPuntos[i - 1], listaPuntos[i]);    
            }
           g.DrawLine(lapiz, listaPuntos[listaPuntos.Count-1], listaPuntos[0]);

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
                    DibujarPoligono(numLados, radio);
                }
                else MessageBox.Show("muy pocos lados... minimo 3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dato invalido u_u");
            }
        }
    }
}
