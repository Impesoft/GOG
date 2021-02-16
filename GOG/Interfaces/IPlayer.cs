namespace GameOfGoose
{
    public interface IPlayer
    {
        public PlayerPawn PlayerPawn { get; set; }
        string Name { get; set; }
        int Position { get; set; }
        int ToSkipTurns { get; set; }

        void Move(int direction);
    }
}