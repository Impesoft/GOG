namespace GameOfGoose
{
    public interface IPlayer
    {
        string Name { get; set; }
        int Position { get; set; }
        int ToSkipTurns { get; set; }
        public Location PlayerLocation { get; set; }

        void Move(int[] dice, int direction);
    }
}