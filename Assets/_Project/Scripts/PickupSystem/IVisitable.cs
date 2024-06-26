namespace _Project.Scripts.PickupSystem
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}