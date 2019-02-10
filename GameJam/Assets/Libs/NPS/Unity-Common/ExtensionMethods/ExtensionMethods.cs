using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Various extension to Unity types.
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Checks if one vector is approximately equal to another
        /// </summary>
        /// <param name="_me">Me.</param>
        /// <param name="_other">The other.</param>
        /// <param name="_allowedDifference">The allowed difference.</param>
        /// <returns><c>true</c> if the are approximately equal, otherwise <c>false</c></returns>
        public static bool Approximately(this Vector3 _me, Vector3 _other, float _allowedDifference)
        {
            float dx = _me.x - _other.x;
            if (dx < -_allowedDifference || dx > _allowedDifference)
            {
                return false;
            }

            float dy = _me.y - _other.y;
            if (dy < -_allowedDifference || dy > _allowedDifference)
            {
                return false;
            }

            float dz = _me.z - _other.z;

            return (dz >= -_allowedDifference) && (dz <= _allowedDifference);
        }

        /// <summary>
        /// Get the direction between two point in the xz plane only
        /// </summary>
        /// <param name="_from">The from position.</param>
        /// <param name="_to">The to position.</param>
        /// <returns>The direction vector between the two points.</returns>
        public static Vector3 DirToXZ(this Vector3 _from, Vector3 _to)
        {
            return new Vector3(_to.x - _from.x, 0f, _to.z - _from.z);
        }

        /// <summary>
        /// Discards the y-component of the vector
        /// </summary>
        /// <param name="_v">The vector.</param>
        /// <returns>The vector with y set to 0</returns>
        public static Vector3 OnlyXZ(this Vector3 _v)
        {
            _v.y = 0f;
            return _v;
        }

        /// <summary>
        /// Gets the first MonoBehavior on the component's game object that is of type T. This is different from GetComponent in that the type can be an interface or class that is not itself a component.
        /// It is however a relatively slow operation, and should not be used in actions that happen frequently, e.g. Update.
        /// </summary>
        /// <typeparam name="T">The type of behavior to look for</typeparam>
        /// <param name="_c">The component whose siblings will be inspected if they are of type T</param>
        /// <param name="_searchParent">if set to <c>true</c> the parent transform will also be inspected if no match is found on the current component's transform.</param>
        /// <param name="_required">if set to <c>true</c> and the requested component is not found, an exception will be thrown.</param>
        /// <returns>
        /// The T behavior sibling of the component or null if not found.
        /// </returns>
        public static T As<T>(this Component _c, bool _searchParent = false, bool _required = false) where T : class
        {
            if (_c.Equals(null))
            {
                return null;
            }

            return As<T>(_c.gameObject, _searchParent, _required);
        }

        /// <summary>
        /// Gets the first MonoBehavior on the component's game object that is of type T. This is different from GetComponent in that the type can be an interface or class that is not itself a component.
        /// It is however a relatively slow operation, and should not be used in actions that happen frequently, e.g. Update.
        /// </summary>
        /// <typeparam name="T">The type of behavior to look for</typeparam>
        /// <param name="_c">The component whose siblings will be inspected if they are of type T</param>
        /// <param name="_searchParent">if set to <c>true</c> the parent transform will also be inspected if no match is found on the current component's transform.</param>
        /// <param name="_required">if set to <c>true</c> and the requested component is not found, an exception will be thrown.</param>
        /// <returns>
        /// The T behavior sibling of the component or null if not found.
        /// </returns>
        public static T As<T>(this IGameObjectComponent _c, bool _searchParent = false, bool _required = false) where T : class
        {
            if (_c.Equals(null))
            {
                return null;
            }

            return As<T>(_c.gameObject, _searchParent, _required);
        }

        /// <summary>
        /// Gets the first MonoBehavior on the game object that is of type T. This is different from GetComponent in that the type can be an interface or class that is not itself a component.
        /// It is however a relatively slow operation, and should not be used in actions that happen frequently, e.g. Update.
        /// </summary>
        /// <typeparam name="T">The type of behavior to look for</typeparam>
        /// <param name="_go">The game object whose components will be inspected if they are of type T</param>
        /// <param name="_searchParent">if set to <c>true</c> the parent transform will also be inspected if no match is found on the current game object.</param>
        /// <param name="_required">if set to <c>true</c> and the requested component is not found, an exception will be thrown.</param>
        /// <returns>
        /// The T behavior or null if not found.
        /// </returns>
        public static T As<T>(this GameObject _go, bool _searchParent = false, bool _required = false) where T : class
        {
            if (_go.Equals(null))
            {
                return null;
            }

            T c = _go.GetComponent(typeof(T)) as T;

            if (c == null && _searchParent && _go.transform.parent != null)
            {
                return As<T>(_go.transform.parent.gameObject, false, _required);
            }

            if (c == null && _required)
            {
                throw new MissingComponentException(string.Format("Game object {0} does not have a component of type {1}.", _go.name, typeof(T).Name));
            }

            return c;
        }

        /// <summary>
        /// Warns if multiple instances of the component exists on its game object.
        /// </summary>
        /// <param name="_component">The component.</param>
        public static void WarnIfMultipleInstances(this MonoBehaviour _component)
        {
            Type t = _component.GetType();

            if (_component.GetComponents(t).Length > 1)
            {
                Debug.LogWarning(string.Format("GameObject '{0}' defines multiple instances of '{1}' which is not recommended.", _component.gameObject.name, t.Name));
            }
        }

        /// <summary>
        /// Warns if multiple instances of the component exists on its game object.
        /// </summary>
        /// <param name="_component">The component.</param>
        public static void WarnIfMultipleInstances<TInterface>(this MonoBehaviour _component) where TInterface : class
        {
            int counter = 0;
            MonoBehaviour[] components = _component.GetComponents<MonoBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] is TInterface v)
                {
                    counter++;
                }
            }

            if (counter > 1)
            {
                Debug.LogWarning(string.Format("GameObject '{0}' defines multiple component implementing '{1}' which is not recommended.", _component.gameObject.name, typeof(TInterface).Name));
            }
        }

        /// <summary>
        /// Determines whether another bounds overlaps this one (and vice versa).
        /// </summary>
        /// <param name="_a">This bounds.</param>
        /// <param name="_b">The other bounds.</param>
        /// <returns><c>true</c> if they overlap, otherwise false.</returns>
        public static bool Overlaps(this Bounds _a, Bounds _b)
        {
            if ((_b.max.x <= _a.min.x) || (_b.min.x >= _a.max.x))
            {
                return false;
            }

            return ((_b.max.z > _a.min.z) && (_b.min.z < _a.max.z));
        }

        /// <summary>
        /// Translates the specified bounds a certain amount.
        /// </summary>
        /// <param name="_b">The bounds</param>
        /// <param name="_translation">The translation vector.</param>
        /// <returns>The bounds after the translation.</returns>
        public static Bounds Translate(this Bounds _b, Vector3 _translation)
        {
            _b.center = _b.center + _translation;
            return _b;
        }

        /// <summary>
        /// Translates the specified bounds a certain amount.
        /// </summary>
        /// <param name="_b">The bounds</param>
        /// <param name="_x">The x component of the translation.</param>
        /// <param name="_y">The y component of the translation.</param>
        /// <param name="_z">The z component of the translation.</param>
        /// <returns>The bounds after the translation.</returns>
        public static Bounds Translate(this Bounds _b, float _x, float _y, float _z)
        {
            Vector3 center = _b.center;
            center.x += _x;
            center.y += _y;
            center.z += _z;
            _b.center = center;
            return _b;
        }

        /// <summary>
        /// Resizes a bounds by a certain amount.
        /// </summary>
        /// <param name="_b">The bounds.</param>
        /// <param name="_delta">The delta vector.</param>
        /// <returns>The resized bounds.</returns>
        public static Bounds DeltaSize(this Bounds _b, Vector3 _delta)
        {
            _b.size = _b.size + _delta;
            return _b;
        }

        /// <summary>
        /// Resizes a bounds by a certain amount.
        /// </summary>
        /// <param name="_b">The bounds</param>
        /// <param name="_dx">The x component of the delta.</param>
        /// <param name="_dy">The y component of the delta.</param>
        /// <param name="_dz">The z component of the delta.</param>
        /// <returns>The resized bounds.</returns>
        public static Bounds DeltaSize(this Bounds _b, float _dx, float _dy, float _dz)
        {
            Vector3 size = _b.size;
            size.x += _dx;
            size.y += _dy;
            size.z += _dz;
            _b.size = size;
            return _b;
        }

        /// <summary>
        /// Merges the two bounds
        /// </summary>
        /// <param name="_b">The first bounds.</param>
        /// <param name="_other">The second bounds.</param>
        /// <returns>A bounds representing the union of the two bounds.</returns>
        public static Bounds Merge(this Bounds _b, Bounds _other)
        {
            return new Bounds
            {
                min = new Vector3(Mathf.Min(_b.min.x, _other.min.x), Mathf.Min(_b.min.y, _other.min.y), Mathf.Min(_b.min.z, _other.min.z)),
                max = new Vector3(Mathf.Max(_b.max.x, _other.max.x), Mathf.Max(_b.max.y, _other.max.y), Mathf.Max(_b.max.z, _other.max.z)),
            };
        }

        /// <summary>
        /// Get the bounds that represents the intersection of two bounds.
        /// </summary>
        /// <param name="_a">The first bounds.</param>
        /// <param name="_b">The second bounds.</param>
        /// <returns>The intersection bounds.</returns>
        public static Bounds Intersection(this Bounds _a, Bounds _b)
        {
            Vector3 min = new Vector3(Mathf.Max(_a.min.x, _b.min.x), Mathf.Max(_a.min.y, _b.min.y), Mathf.Max(_a.min.z, _b.min.z));
            Vector3 max = new Vector3(Mathf.Min(_a.max.x, _b.max.x), Mathf.Min(_a.max.y, _b.max.y), Mathf.Min(_a.max.z, _b.max.z));

            Bounds res = new Bounds();
            res.SetMinMax(min, max);

            return res;
        }

        /// <summary>
        /// Determines whether one <see cref="Rect"/>. contains another.
        /// </summary>
        /// <param name="_rect">The rect to check.</param>
        /// <param name="_other">The other rect.</param>
        /// <returns><c>true</c> if <paramref name="_other"/> is contained in <paramref name="_rect"/>; otherwise <c>false</c></returns>
        public static bool Contains(this Rect _rect, Rect _other)
        {
            return _rect.Contains(_other.min) && _rect.Contains(_other.max);
        }

        /// <summary>
        /// Rounds all members of the specified Rect, producing a Rect with whole number members.
        /// </summary>
        /// <param name="_rect">The rect to round.</param>
        /// <returns>The Rect with all members rounded to the nearest whole number.</returns>
        public static Rect Round(this Rect _rect)
        {
            _rect.xMin = Mathf.Round(_rect.xMin);
            _rect.xMax = Mathf.Round(_rect.xMax);
            _rect.yMin = Mathf.Round(_rect.yMin);
            _rect.yMax = Mathf.Round(_rect.yMax);
            return _rect;
        }

        /// <summary>
        /// Rounds x and y of the specified Vector2 to the nearest whole number.
        /// </summary>
        /// <param name="_v">The vector to round.</param>
        /// <returns>The Vector2 with x and y rounded to the nearest whole number.</returns>
        public static Vector2 Round(this Vector2 _v)
        {
            _v.x = Mathf.Round(_v.x);
            _v.y = Mathf.Round(_v.y);
            return _v;
        }

        /// <summary>
        /// Rounds x, y and z of the specified Vector3 to the nearest whole number.
        /// </summary>
        /// <param name="_v">The vector to round.</param>
        /// <returns>The Vector3 with x, y and z rounded to the nearest whole number.</returns>
        public static Vector3 Round(this Vector3 _v)
        {
            _v.x = Mathf.Round(_v.x);
            _v.y = Mathf.Round(_v.y);
            _v.z = Mathf.Round(_v.z);
            return _v;
        }

        /// <summary>
        /// Adds a component of the specified type if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The type of component to add</typeparam>
        /// <param name="_target">The target to which the component will be added.</param>
        /// <param name="_entireScene">if set to <c>true</c> the check to see if the component already exists will be done in the entire scene, otherwise it will check the <paramref name="_target"/>.</param>
        /// <param name="_component">The component regardless of whether it was just added or already existed.</param>
        /// <returns><c>true</c> if the component was added; or <c>false</c> if the component already exists on the game object.</returns>
        public static bool AddIfMissing<T>(this GameObject _target, bool _entireScene, out T _component) where T : Component
        {
            if (_entireScene)
            {
                _component = ComponentHelper.FindFirstComponentInScene<T>();
            }
            else
            {
                _component = _target.GetComponent<T>();
            }

            if (_component == null)
            {
                _component = _target.AddComponent<T>();
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Adds a component of the specified type if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The type of component to add</typeparam>
        /// <param name="_target">The target to which the component will be added.</param>
        /// <param name="_entireScene">if set to <c>true</c> the check to see if the component already exists will be done in the entire scene, otherwise it will check the <paramref name="_target"/>.</param>
        /// <returns><c>true</c> if the component was added; or <c>false</c> if the component already exists on the game object.</returns>
        public static bool AddIfMissing<T>(this GameObject _target, bool _entireScene) where T : Component
        {
            return AddIfMissing(_target, _entireScene, out T component);
        }

        public static T GetOrAddComponent<T>(this GameObject _target, out T _component) where T : Component
        {
            _component = _target.GetComponent<T>();
            if (_component == null)
            {
                _component = _target.AddComponent<T>();
            }
            return _component;
        }

        public static T GetOrAddComponent<T>(this GameObject _target) where T : Component
        {
            return GetOrAddComponent(_target, out T component);
        }

        /// <summary>
        /// Adds a component of the specified type if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The type of component to add</typeparam>
        /// <param name="_target">The target to which the component will be added.</param>
        /// <param name="_component">The component regardless of whether it was just added or already existed.</param>
        /// <returns><c>true</c> if the component was added; or <c>false</c> if the component already exists on the game object.</returns>
        public static bool AddIfMissing<T>(this GameObject _target, out T _component) where T : Component
        {
            return AddIfMissing(_target, false, out _component);
        }

        /// <summary>
        /// Adds a component of the specified type if it does not already exist.
        /// </summary>
        /// <typeparam name="T">The type of component to add</typeparam>
        /// <param name="_target">The target to which the component will be added.</param>
        /// <returns><c>true</c> if the component was added; or <c>false</c> if the component already exists on the game object.</returns>
        public static bool AddIfMissing<T>(this GameObject _target) where T : Component
        {
            T component;
            return AddIfMissing(_target, false, out component);
        }

        /// <summary>
        /// Adds a component but delays its Awake call until it has been configured.
        /// </summary>
        /// <typeparam name="T">The type of component to add</typeparam>
        /// <param name="_target">The target to which the component will be added.</param>
        /// <param name="_configurator">The action to execute in order to configure the newly added item.</param>
        /// <returns>The component that was added</returns>
        public static T AddComponentSafe<T>(this GameObject _target, Action<T> _configurator) where T : Component
        {
            _target.SetActive(false);
            T c = _target.AddComponent<T>();
            _configurator(c);
            _target.SetActive(true);
            return c;
        }

        /// <summary>
        /// Removes a single component of type T on the specified game object.
        /// </summary>
        /// <typeparam name="T">The type parameter, i.e. what type of component should be removed.</typeparam>
        /// <param name="_go">The game object on which the expected component is.</param>
        /// <returns><c>true</c> if it finds and destroys the desired component, <c>false</c> otherwise.</returns>
        public static bool NukeSingle<T>(this GameObject _go) where T : Component
        {
            T res = _go.GetComponent<T>();
            if (res != null)
            {
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(res);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(res, true);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Collects a game object and all its descendant game objects in a list.
        /// </summary>
        /// <param name="_root">The root game object.</param>
        /// <param name="_collector">The list populated by <paramref name="_root"/> and all its descendants.</param>
        public static void SelfAndDescendants(this GameObject _root, List<GameObject> _collector)
        {
            _collector.Add(_root);

            Transform t = _root.transform;
            int childCount = t.childCount;
            for (int i = 0; i < childCount; i++)
            {
                SelfAndDescendants(t.GetChild(i).gameObject, _collector);
            }
        }
    }
}
