using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video;
using AForge;
using AForge.Video.DirectShow;
using AForge.Video.VFW;


namespace Camera
{
    public partial class Form1 : Form
    {
        AForge.Video.DirectShow.VideoCaptureDevice cam;
        AForge.Controls.VideoSourcePlayer player;
        AForge.Video.VFW.AVIWriter writer;
        int width = 0;
        int height = 0;
        string path="";
        string pathvideo = "";
        string str="";
        bool vkl = false;
        bool stopzapis = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vkl = true;
            button5.Enabled = false;
            button4.Enabled = true;
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            height = Settings.height;
            width = Settings.width;
            player=new VideoSourcePlayer();
            player.Top=30;
            player.Left=30;
            player.Width=width;
            player.Height=height;
            player.Show();
            Controls.Add(player);
            button1.Top = height+30;
            button1.Left = width+50;
            button2.Top = height + 60;
            button2.Left = width + 50;
            button3.Top = height + 90;
            button3.Left = width + 50;
            button4.Top = height + 120;
            button4.Left = width + 50;
            button5.Top = height + 160;
            button5.Left = width + 50;
           cam = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString);
            cam.Start();
          player.VideoSource=cam;
            player.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vkl) {
                Bitmap b=new Bitmap(width,height);
                
            Rectangle rct=new Rectangle(0,0,width,height);
            if (cam.IsRunning)
            {
                player.DrawToBitmap(b, rct);
            }
            else {
                player.Stop();
            }
            if (str.Length > 0) { path = str + "\\" + System.DateTimeOffset.UtcNow.UtcDateTime.ToShortDateString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Hour.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Minute.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Second.ToString() + ".jpg"; }
            else { path =  System.DateTimeOffset.UtcNow.UtcDateTime.ToShortDateString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Hour.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Minute.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Second.ToString() + ".jpg"; }
                b.Save(path,System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (vkl) { 
            cam.Stop();
             player.Stop();
            
            }
             
           
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vkl) { 
            player.Stop();
            cam.Stop();
            player.Dispose();
            }
            
             Application.Exit();
        }

        private void размерыЭкранаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size frm = new Size();
            frm.ShowDialog();
        }

        private void папкаСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            fld.ShowDialog();
          str = fld.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stopzapis = false;
            button5.Enabled = true;
            button4.Enabled = false;
            if (vkl) {
              
                writer = new AForge.Video.VFW.AVIWriter("XviD");
                
            if (str.Length > 0) 
            { pathvideo = str + "\\" + System.DateTimeOffset.UtcNow.UtcDateTime.ToShortDateString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Hour.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Minute.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Second.ToString() + ".avi"; }
            else
            { pathvideo = System.DateTimeOffset.UtcNow.UtcDateTime.ToShortDateString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Hour.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Minute.ToString() + " " + System.DateTimeOffset.UtcNow.UtcDateTime.Second.ToString() + ".avi"; }
            writer.Open(pathvideo,640,480);
            cam.NewFrame += new NewFrameEventHandler(video_NewFrame); 
            }
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs) //обработчик события NewFrame 
        {
            if (!stopzapis) {
                Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            writer.AddFrame(img); 
            }
        }
    
        private void button5_Click(object sender, EventArgs e)
        {
            stopzapis = true;
            button5.Enabled = false;
            button4.Enabled = true;
            writer.Close();
        }    
    }
}
