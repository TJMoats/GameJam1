using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Interface for objects that expose a Transform.
    /// </summary>
    public interface ITransformComponent
    {
        /// <summary>
        /// Gets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        Transform transform { get; }
    }
}
