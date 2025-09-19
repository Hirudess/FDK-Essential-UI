namespace FDK.Core.Assignable
{
    public abstract class AssignableSlotUIItem : BaseUiItem
    {
        public Assignable Item { get; set; }

        protected void Assign(Assignable item)
        {
            Item = item;
            Item.Assign(Item.Id);
        }

        protected void Unassign()
        {
            if (Item == null) return;
            Item.Unassign();
        }
    }
}
