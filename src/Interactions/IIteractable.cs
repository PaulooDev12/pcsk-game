using Microsoft.Xna.Framework;

namespace interactions
{
    public interface IInteractable
    {
        Rectangle Bounds { get; }
        void Interact();
    }
}