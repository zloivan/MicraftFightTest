using UnityEngine;

namespace _Project.Scripts.PickupSystem
{
    public interface IVisitor
    {
        void Visit<T>(T visitable) where T : Component, IVisitable;
    }
}