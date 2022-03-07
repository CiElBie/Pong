using System;
using System.Drawing;
using System.Windows.Forms;

/* Things to add
 * add gameover screen with winner announcement
 * Maybe fix paddle movement
 * maybe fix the ball vibrating when behind paddle bug
 * */
namespace Pong
{
    public partial class Form1 : Form
    {
        public Keys keycode; //stores the key that was pressed
        public static bool keymove;  //determines if a key is currently being pressed
        int player1score = 0; //player 1 score
        int player2score = 0; //player 2 score
        Random rnd = new Random(); //random seed
        bool plyr1 = true; // if 1 player was selected                                    
        bool plyr2 = false; // if 2 players were selected
        bool rules = false; // if settings were highlighted on the main menu
        bool bo5 = true; // if best of 7 is chosen                                   
        bool bo11 = false; // if best of 11 is chosen
        bool ft10 = false; // if first to 10 is chosen
        int menucount = 1;  //switches which item on the menu is selected 
        bool rulemenu = false; //if we are on the rule menu
        public Form1()
        {
            InitializeComponent();
            new Settings();  //new instance of settings, so puts all original values

            gameTimer.Interval = 1000 / Settings.speed; // Changing the game time to settings speed
            gameTimer.Tick += updateSreen; // linking a updateScreen function to the timer
            gameTimer.Start(); // starting the timer
        }


        private void updateSreen(object sender, EventArgs e)
        {
            // this is the Timers update screen function. 
            // each tick will run this function
            if (Settings.gameover == false)
            {
                if (Settings.gamestart == true)
                {

                    pictureBox2.Invalidate(); //updates graphics on picturebox2
                    moveBall();  //runs command to change ball coordinates
                    if (keymove == true)
                    {
                        Player1move();
                        if (plyr2 == true)
                        {
                            Player2move();
                        }
                    }
                    if (plyr1 == true)
                    {
                        AI();
                    }
                }

            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            keycode = e.KeyCode;
            keymove = true;

            if (keycode == Keys.Escape)
            {
                Application.Exit();
            }
            {
                if (Settings.gamestart == false)
                {  //if game hasn't started
                    if (Settings.gameover == false){ 
                    if (keycode == Keys.Tab) //if tab key is pressed
                        {
                            if (rulemenu == false)  //if we are not on the rule menu
                            {
                                menuswitch();  //then we navigate the menu
                            }
                            if (rulemenu == true)  //if we are on the rule menu 
                            {
                                rulemenuswitch();  //then we navigate the rules menu
                            }
                        }
                    }


                    if (keycode == Keys.Enter) //if the enter key is pressed
                    {
                        if (Settings.gameover == false)
                        {
                            if (rulemenu == false) //if we are not on the rule menu
                            {
                                if (rules == false) //if rules isn't selected
                                {
                                    startgame();  //start the game
                                }
                                if (rules == true) //if rules are selected
                                {
                                    rule(); //go to the rule menu and change the text 
                                }
                            }
                            else  //if we are on the rule menu
                            {
                                menu(); //change back to menu text  
                            }
                        }
                        else
                        {
                            RESET();
                        }
                    }

                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keymove = false; //if the key is no longer pressed, the paddle stops moving
        }



        private void updateGraphics(object sender, PaintEventArgs e)
        {
            // this is where we will see the snake and its parts moving
            if (Settings.gameover == false)
            {
                if (Settings.gamestart == true)
                {
                    Graphics canvas = e.Graphics; // create a new graphics class called canvas

                    //draw ball
                    canvas.FillEllipse(Brushes.White,
                                    new Rectangle(
                                        Settings.ballx,
                                        Settings.bally,
                                        15, 15)
                                    );
                }
            }
        }
        private void startgame()
        {
            pictureBox1.Visible = false;
            label6.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            EnterLabel.Visible = false;
            Settings.gamestart = true;
            player1score = 0;
            player2score = 0;


            label1.Parent = pictureBox2;
            label2.Parent = pictureBox2;
        }
        private void rule()
        {
            label4.Text = "Best of 5";
            label5.Text = "Best of 11";
            label3.Text = "First to 10";
            EnterLabel.Text = "*Press enter to go back*";
            rulemenu = true;
            label7.Top = label4.Top;

            bo5 = true;
            bo11 = false; // if 2 players were selected
            ft10 = false;
            menucount = 1;



        }
        private void menu()
        {
            label4.Text = "1 Player";
            label5.Text = "2 Players";
            label3.Text = "Gamerules";
            EnterLabel.Text = "*Press enter to start*";
            rulemenu = false;
            label7.Top = label4.Top;

            plyr1 = true;
            plyr2 = false; // if 2 players were selected
            rules = false;
            menucount = 1;
        }
        private void menuswitch() //when on the main menu, this happpens
            {
            if (menucount < 3)
            {
                menucount += 1;
            }
            else
            {
                menucount = 1;
            }

            switch (menucount)
            {
                case 1:
                    plyr1 = true;
                    plyr2 = false; // if 2 players were selected
                    rules = false;
                    label7.Top = label4.Top;
                    break;
                case 2:
                    plyr1 = false;
                    plyr2 = true; // if 2 players were selected
                    rules = false;
                    label7.Top = label5.Top;
                    break;
                case 3:
                    plyr1 = false;
                    plyr2 = false; // if 2 players were selected
                    rules = true;
                    label7.Top = label3.Top;
                    break;
            }
        }
        private void rulemenuswitch()
        {
            if (menucount < 3)
            {
                menucount += 1;
            }
            else
            {
                menucount = 1;
            }
            switch (menucount)
            {
                case 1:
                    bo5 = true;
                    bo11 = false; // if 2 players were selected
                    ft10 = false;
                    label7.Top = label4.Top;
                    break;
                case 2:
                    bo5 = false;
                    bo11 = true; // if 2 players were selected
                    ft10 = false;
                    label7.Top = label5.Top;
                    break;
                case 3:
                    bo5 = false;
                    bo11 = false; // if 2 players were selected
                    ft10 = true;
                    label7.Top = label3.Top;
                    break;
            }

        }
        private void moveBall()
        {
            paddle1hit();
            paddle2hit();
            if ((Settings.bally < 1)||(Settings.bally > 391))
            {
                Settings.ballspeedy -= Settings.ballspeedy * 2;
            }
            if (Settings.ballx < 1)
            {
                Player2score();
            }
            if (Settings.ballx > 660)
            {
                Player1score();
                
            }

            Settings.ballx += Settings.ballspeedx;
            Settings.bally += Settings.ballspeedy;
        }

        private void paddle1hit()
        {
            if ((Settings.ballx < 41) && 
                (Settings.bally + 15 > paddle1.Top) && 
                (Settings.bally < paddle1.Top + 80))
            {
                Settings.ballspeedx -= Settings.ballspeedx * 2;
                Settings.ballspeedx += rnd.Next(2);
                if (Settings.ballspeedy > 0){
                    Settings.ballspeedy += (rnd.Next(4)/3);
                }
                else
                {
                    Settings.ballspeedy -= (rnd.Next(4) / 3);
                }

            }
        }
        private void paddle2hit()
        {
            if ((Settings.ballx > 620) && 
                (Settings.bally + 15 > paddle2.Top) && 
                (Settings.bally < paddle2.Top + 80))
            {
                Settings.ballspeedx -= Settings.ballspeedx * 2;
                Settings.ballspeedx -= rnd.Next(2);
                if (Settings.ballspeedy > 0)
                {
                    Settings.ballspeedy += (rnd.Next(4) / 3); 
                }
                else
                {
                    Settings.ballspeedy -= (rnd.Next(4) / 3);
                }
            }
        }
        private void Player1move()
        {
            
            if (keycode == Keys.W)
            {
                if (paddle1.Top > 1)
                {
                    paddle1.Top -= Settings.pdlspeed; //moves paddle if up key is pressed
                }

            }
            if (keycode == Keys.S)
            {
                if (paddle1.Top < 321)
                {
                    paddle1.Top += Settings.pdlspeed; //moves paddle if down key is pressed
                }
            
        }
        }
        private void Player2move()
        {
            
                if (keycode == Keys.Up)
                {
                    if (paddle2.Top > 1)
                    {
                        paddle2.Top -= Settings.pdlspeed; //moves paddle if up key is pressed
                    }

                }
                if (keycode == Keys.Down)
                {
                    if (paddle2.Top < 321)
                    {
                        paddle2.Top += Settings.pdlspeed; //moves paddle if down key is pressed
                    }
                }
            
        }
        private void Player1score()
        {
            player1score += 1;
            label1.Text = player1score.ToString();
            nextround();
        }
        private void Player2score()
        {
            player2score += 1;
            label2.Text = player2score.ToString();
            nextround();
        }

        private void nextround()
        {
            if (bo5 == true) //if playing to best of 5
            {
                if ((player1score == 3) || (player2score == 3))
                {   //if either player has scored 3 points
                    GameOver();
                }
            }
            if(bo11 == true)
            {
                if ((player1score == 6) || (player2score == 6))
                {   //if either player has scored 6 points
                    GameOver();
                }
            }
            if (ft10 == true)
            {
                if ((player1score == 10) || (player2score == 10))
                {   //if either player has scored 10 points
                    GameOver();
                }
            }
            if (Settings.gameover == false)
            {
                new Settings();
                Settings.gamestart = true;
            }
        }
        private void GameOver()
        {

            if (player1score > player2score)
            {
                Winner.Text = "Player 1 wins!";
            }
            if (player2score > player1score)
            {
                Winner.Text = "Player 2 wins!";
            }
            pictureBox1.Visible = true;
            label8.Visible = true;
            Winner.Visible = true;
            label9.Visible = true;
            Settings.gamestart = false;
            Settings.gameover = true;
        }
        private void RESET()
        {
            Application.Restart();
            /*Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);

            Settings.gamestart = false;
            Settings.gameover = false;
            Settings.speed = 400;
            Settings.pdlspeed = 6;
            Settings.ballspeedx = -4; //default -4
            Settings.ballspeedy = -1;
            Settings.ballx = 329;
            Settings.bally = 190;

            pictureBox2.Invalidate(); //updates graphics on picturebox2
            */
        }

        private void AI()
        {  
            
            if (Settings.ballspeedx > 0)
            {
                if (Settings.bally > paddle2.Top+40)
                {
                    if (paddle2.Top < 321)
                    {
                        paddle2.Top += Settings.pdlspeed;
                    }
                }
                
                if (Settings.bally < paddle2.Top+40)
                {
                    if (paddle2.Top > 0)
                    {
                        paddle2.Top -= Settings.pdlspeed;
                    }
                }
            }
            /*if the ball is going towards the paddle
             * if the ball is above center of paddle, move up
             * */

        }
        //rest is useless
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

