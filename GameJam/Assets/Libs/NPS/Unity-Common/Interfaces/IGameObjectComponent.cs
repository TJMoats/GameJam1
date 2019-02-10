using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Interface for objects that expose a GameObject.
    /// </summary>
    public interface IGameObjectComponent
    {
        /// <summary>
        /// Gets the game object.
        /// </summary>
        /// <value>
        /// The game object.
        /// </value>
        GameObject gameObject { get; }
    }
}
