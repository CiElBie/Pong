

namespace Pong
{
    internal class Settings
    {
        public static bool gamestart { get; set; } // has the game started? 
        public static bool gameover { get; set; } // has the game started? 
        public static int speed { get; set; } // speed of updates?
        public static int pdlspeed { get; set; } // set the paddle speed
        public static int ballspeedx { get; set; } // set the balls x speed
        public static int ballspeedy { get; set; } // set the balls y speed
        public static int ballx { get; set; } // balls x coordinate
        public static int bally { get; set; } // balls y coordinate


        public Settings()
        {
            gamestart = false;
            gameover = false;
            speed = 400;
            pdlspeed = 6;
            ballspeedx = -4; //default -4
            ballspeedy = -1;
            ballx = 329;
            bally = 190;
        }

        // this is the default settings function when game starts
        // set the paddle y coordinate to 200
    }
}


