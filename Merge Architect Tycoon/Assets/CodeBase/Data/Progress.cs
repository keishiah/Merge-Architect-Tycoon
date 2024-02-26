namespace CodeBase.Data
{
    public class Progress
    {
        public Coins Coins;
        public Buldings Buldings;

        public Progress()
        {
            Coins = new Coins();
            Buldings = new Buldings();
        }
    }
}