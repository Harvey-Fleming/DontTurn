namespace Beginning.CSharp
{
    public struct Player
    {
        private int lives;

        public Player(int lives, string name, int score) : this()
        {
            this.lives = lives;
            this.Name = name;
            this.Score = score;
        }

        public Player(int score) : this(3, "Unknown", 0)
        {
            this.Score = score;
        }
        public int Lives
        {
            get { return lives; }
            set { lives = value + 1; }
        }
        public string Name
        {
            get; set;
        }
        public int Score
        {
            get; set;
        }
    }
}


