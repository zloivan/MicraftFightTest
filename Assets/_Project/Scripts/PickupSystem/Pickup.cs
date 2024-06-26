using _Project.Scripts.Characters;
using UnityEngine;

namespace _Project.Scripts.PickupSystem
{
    public abstract class Pickup : MonoBehaviour, IVisitor
    {
        protected abstract void ApplyPickupEffect(Entity entity);

        public void Visit<T>(T visitable) where T : Component, IVisitable
        {
            if (visitable is Entity entity)
            {
                ApplyPickupEffect(entity);
            }
        }

        public void OnTriggerEnter(Collider collier)
        {
            collier.GetComponent<IVisitable>()?.Accept(this);

            Destroy(gameObject);
        }
    }
}