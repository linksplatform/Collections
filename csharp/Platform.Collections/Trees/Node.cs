using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable ForCanBeConvertedToForeach
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Trees
{
    /// <summary>
    /// <para>
    /// Represents the node.
    /// </para>
    /// <para></para>
    /// </summary>
    public class Node
    {
        /// <summary>
        /// <para>
        /// The child nodes.
        /// </para>
        /// <para></para>
        /// </summary>
        private Dictionary<object, Node> _childNodes;

        /// <summary>
        /// <para>
        /// Gets or sets the value value.
        /// </para>
        /// <para></para>
        /// </summary>
        public object Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set;
        }

        /// <summary>
        /// <para>
        /// Gets the child nodes value.
        /// </para>
        /// <para></para>
        /// </summary>
        public Dictionary<object, Node> ChildNodes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _childNodes ?? (_childNodes = new Dictionary<object, Node>());
        }

        /// <summary>
        /// <para>
        /// The key.
        /// </para>
        /// <para></para>
        /// </summary>
        public Node this[object key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetChild(key) ?? AddChild(key);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => SetChildValue(value, key);
        }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="Node"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="value">
        /// <para>A value.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node(object value) => Value = value;

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="Node"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node() : this(null) { }

        /// <summary>
        /// <para>
        /// Determines whether this instance contains child.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="keys">
        /// <para>The keys.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsChild(params object[] keys) => GetChild(keys) != null;

        /// <summary>
        /// <para>
        /// Gets the child using the specified keys.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="keys">
        /// <para>The keys.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node GetChild(params object[] keys)
        {
            var node = this;
            for (var i = 0; i < keys.Length; i++)
            {
                node.ChildNodes.TryGetValue(keys[i], out node);
                if (node == null)
                {
                    return null;
                }
            }
            return node;
        }

        /// <summary>
        /// <para>
        /// Gets the child value using the specified keys.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="keys">
        /// <para>The keys.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The object</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetChildValue(params object[] keys) => GetChild(keys)?.Value;

        /// <summary>
        /// <para>
        /// Adds the child using the specified key.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node AddChild(object key) => AddChild(key, new Node(null));

        /// <summary>
        /// <para>
        /// Adds the child using the specified key.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <param name="value">
        /// <para>The value.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node AddChild(object key, object value) => AddChild(key, new Node(value));

        /// <summary>
        /// <para>
        /// Adds the child using the specified key.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <param name="child">
        /// <para>The child.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The child.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node AddChild(object key, Node child)
        {
            ChildNodes.Add(key, child);
            return child;
        }

        /// <summary>
        /// <para>
        /// Sets the child using the specified keys.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="keys">
        /// <para>The keys.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node SetChild(params object[] keys) => SetChildValue(null, keys);

        /// <summary>
        /// <para>
        /// Sets the child using the specified key.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node SetChild(object key) => SetChildValue(null, key);

        /// <summary>
        /// <para>
        /// Sets the child value using the specified value.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="value">
        /// <para>The value.</para>
        /// <para></para>
        /// </param>
        /// <param name="keys">
        /// <para>The keys.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The node.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node SetChildValue(object value, params object[] keys)
        {
            var node = this;
            for (var i = 0; i < keys.Length; i++)
            {
                node = SetChildValue(value, keys[i]);
            }
            node.Value = value;
            return node;
        }

        /// <summary>
        /// <para>
        /// Sets the child value using the specified value.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="value">
        /// <para>The value.</para>
        /// <para></para>
        /// </param>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The child.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Node SetChildValue(object value, object key)
        {
            if (!ChildNodes.TryGetValue(key, out Node child))
            {
                child = AddChild(key, value);
            }
            child.Value = value;
            return child;
        }
    }
}