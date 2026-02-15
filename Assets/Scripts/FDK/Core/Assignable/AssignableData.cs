namespace FDK.Core.Assignable
{
    public interface IAssignableData : ISelectableUIData
    {
        bool IsEmpty { get; }
    }

    public interface IAssignableSlotData<T> where T : IAssignableData
    {
        T Item { get; }
        bool IsEmpty { get; }

        void Assign(T item);
        void Unassign();
    }

    public abstract class AssignableData<T> : IAssignableSlotData<T> where T : IAssignableData
    {
        public T Item { get; private set; }
        public bool IsEmpty => Item == null;


        public AssignableData(T item)
        {
            Item = item;
        }

        public void Assign(T item)
        {
            Item = item;
        }

        public void Unassign()
        {
            Item = default(T);
        }
    }
}
