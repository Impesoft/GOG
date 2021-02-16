namespace GameOfGoose
{
    public interface IPlayer
    {
        string Name { get; set; }
        int Position { get; set; }
        int ToSkipTurns { get; set; }

        void Move(int direction);
    }
}