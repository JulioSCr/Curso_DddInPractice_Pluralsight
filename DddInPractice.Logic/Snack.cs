namespace DddInPractice.Logic
{
    public class Snack : AggregateRoot
    {
        public string Name { get; protected set; }

        protected Snack() { }

        public Snack(string name)
            : this()
        {
            Name = name;
        }
    }
}