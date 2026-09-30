using Microsoft.Xna.Framework;

namespace pcsk.src.Interactions
{
    public interface IInteractable
    {
        Rectangle Bounds { get; }
        void Interact();
    }
}