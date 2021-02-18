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
using System.Media;
using System.Windows.Input;
using Microsoft.VisualBasic.Devices;

namespace FlappyBirdReplica
{
    public partial class Form1 : Form
    {

        int pipeSpeed = 8;
        int gravity = 15;
        int score = 0;

        class OvalPictureBox : PictureBox
        {
            public OvalPictureBox()
            {
                this.BackColor = Color.DarkGray;
            }
            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                using (var gp = new GraphicsPath())
                {
                    gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                    this.Region = new Region(gp);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            gameTimer.Stop();

            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Undertale-Another Medium.wav ";
            player.Play();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            //int highscore = 0;
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score; 

            if(pipeBottom.Left<-150)
            {
                pipeBottom.Left = 800;
                score++;
            }

            if(pipeTop.Left<-180)
            {
                pipeTop.Left = 950;
                score++;
            }

            //if(score>=highscore)
            //{  
              //  highscore = score;
            //}

            if(flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds)||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds)||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top < -25
                )
            {
                endGame();
                if (MessageBox.Show("Restart ?\n Score: "+ score, "You died  :(", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }

           if(score >= 5 && score%5==0)
            {
                pipeSpeed++;
            }

        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Space)
            {
                gravity = -13;
                gameTimer.Start();
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 13;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game over!";
        }

        private void flappyBird_Click(object sender, EventArgs e)
        {
            Rectangle r = new Rectangle(0, 0, flappyBird.Width, flappyBird.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            flappyBird.Region = new Region(gp);
        }

        private void scoreText_Click(object sender, EventArgs e)
        {

        }
    }
}
