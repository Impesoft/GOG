namespace GameOfGoose.Interface
{
    public interface ISquare
    {
        int Id { get; set; }
        string Name { get; set; }

        void Move(Player player);

        string ToString();
    }
}