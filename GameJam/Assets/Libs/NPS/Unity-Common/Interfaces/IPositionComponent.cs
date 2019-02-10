using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Interface for objects that expose a Position.
    /// </summary>
    public interface IPositionComponent
    {
        /// <summary>
        /// Gets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        Vector3 Position { get; }
    }
}