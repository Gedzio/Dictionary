namespace Dictionary.Domain
{
    public class EntityBase
    {
        public int Id { get; private set; }

        protected EntityBase()
        {
        }

        protected EntityBase(int id)
        {
            Id = id;
        }
    }
}