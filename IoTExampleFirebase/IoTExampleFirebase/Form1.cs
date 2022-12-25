using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using IoTExampleFirebase.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoTExampleFirebase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "your token",
            BasePath = "your address"
        };

        IFirebaseClient istemci_firebase;


        private async void timer1_TickAsync(object sender, EventArgs e)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            FirebaseResponse response = await istemci_firebase.GetAsync("deger");
            string values1 = response.ResultAs<String>();

            FirebaseResponse response2 = await istemci_firebase.GetAsync("deger2");
            string values2 = response2.ResultAs<String>();


            response = await istemci_firebase.GetAsync("deger");
            textBox1.Text = response.ResultAs<String>();

            response2 = await istemci_firebase.GetAsync("deger2");
            textBox2.Text = response2.ResultAs<String>();

            textBox3.Text = (double.Parse(values1) - double.Parse(values2)).ToString();
            double diff = (double.Parse(values1) - double.Parse(values2));

            double oran = (100 * (diff)) / double.Parse(values1);
            textBox4.Text = oran.ToString();


            if (diff<0)
            {
                pictureBox1.Image = Resources.notstonk;             
            }
            else if (diff>0)
            {
                pictureBox1.Image = Resources.stonk;
            }
            else
            {
                pictureBox1.Image = Resources.confusedstonk;
            }
            label4.Text = DateTime.Now.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            istemci_firebase = new FirebaseClient(config);
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            var pane = new ZedGraph.GraphPane();
            var curve1 = pane.AddCurve(
                label: "demo",
                x: new double[] { 1, 2, 3, 4, 5 },
                y: new double[] { 1, 4, 9, 16, 25 },
                color: Color.Blue);
            curve1.Line.IsAntiAlias = true;
            pane.AxisChange();
            Bitmap bmp = pane.GetImage(400, 300, dpi: 100, isAntiAlias: true);
            bmp.Save("zedgraph-console-quickstart.png", ImageFormat.Png);
        }
    }
}
