using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
           double[,] array = Program.AvarageArray(Program.CreateArray3());
          void fillData()
        {
            // Взял готовую часть кода с созданием тепловой карты из интернета, изменив частично под свою задачу
            int maxRow = array.GetLength(0);
            int maxCol = array.GetLength(1);
            double k = 200; // Значение коэффициента подбирается для лучшей картины эквипотенциальных поверхностей, тепловой карты. Менял в диапозоне 100<k<2000
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            //..

            int rowHeight = dataGridView1.ClientSize.Height / maxRow - 1;
            int colWidth = dataGridView1.ClientSize.Width / maxCol - 1;

            for (int i = 0; i < maxRow; i++) dataGridView1.Columns.Add(i.ToString(), "");
            for (int i = 0; i < maxRow; i++) dataGridView1.Columns[i].Width = colWidth;
            dataGridView1.Rows.Add(maxRow);
            for (int j = 0; j < maxRow; j++) dataGridView1.Rows[j].Height = rowHeight;

            List<Color> baseColors = new List<Color>();  // create a color list
            baseColors.Add(Color.RoyalBlue);
            baseColors.Add(Color.LightSkyBlue);
            baseColors.Add(Color.LightGreen);
            baseColors.Add(Color.Yellow);
            baseColors.Add(Color.Orange);
            baseColors.Add(Color.Red);
            List<Color> colors = interpolateColors(baseColors, 5000);

            for (int j = 0; j < maxRow; j++)
            {
                for (int i = 0; i < maxRow; i++)
                {
                    dataGridView1[j, i].Value = Math.Round(array[j, i], 3).ToString();
                    dataGridView1[j, i].Style.BackColor =
                                   colors[Convert.ToInt16(Math.Abs( 10 - array[j, i]) * k)];

                }
            }

        }
        List<Color> interpolateColors(List<Color> stopColors, int count)
        {
            SortedDictionary<float, Color> gradient = new SortedDictionary<float, Color>();
            for (int i = 0; i < stopColors.Count; i++)
                gradient.Add(1f * i / (stopColors.Count - 1), stopColors[i]);
            List<Color> ColorList = new List<Color>();

            using (Bitmap bmp = new Bitmap(count, 1))
            using (Graphics G = Graphics.FromImage(bmp))
            {
                Rectangle bmpCRect = new Rectangle(Point.Empty, bmp.Size);
                LinearGradientBrush br = new LinearGradientBrush
                                        (bmpCRect, Color.Empty, Color.Empty, 0, false);
                ColorBlend cb = new ColorBlend();
                cb.Positions = new float[gradient.Count];
                for (int i = 0; i < gradient.Count; i++)
                    cb.Positions[i] = gradient.ElementAt(i).Key;
                cb.Colors = gradient.Values.ToArray();
                br.InterpolationColors = cb;
                G.FillRectangle(br, bmpCRect);
                for (int i = 0; i < count; i++) ColorList.Add(bmp.GetPixel(i, 0));
                br.Dispose();
            }
            return ColorList;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            fillData();
            textBox1.Text = "Минимальное число итераций для усреднения в 1 задании = " + Program.FindCount(Program.CreateArray1()).ToString();
        }
    }
}
