namespace FDK.Core.Assignable
{
    public abstract class Assignable 
    {
        public string Id { get; set; }

        public void Assign(string id)
        {
            Id = id;
        }

        public void Unassign()
        {
            Id = null;
        }
    }
}
