using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.InformationHolders
{
    /// <summary>
    /// Holds two objects of type <typeparamref name="T"/> and type <typeparamref name="Y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Y"></typeparam>
    class ObjectHolder<T, Y>
    {
        private T _obj1;
        private Y _obj2;

        /// <summary>
        /// Inititializes a new instance of ObjectHolder.
        /// </summary>
        /// <param name="obj1">The first object.</param>
        /// <param name="obj2">The second object.</param>
        public ObjectHolder(T obj1, Y obj2)
        {
            _obj1 = obj1;
            _obj2 = obj2;
        }

        /// <summary>
        /// Gets the first object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        public T FirstObject { get { return _obj1; } }
        /// <summary>
        /// Gets the second object of type <typeparamref name="Y"/>
        /// </summary>
        /// <typeparam name="Y">The type of the object</typeparam>
        public Y SecondObject { get { return _obj2; } }
    }
}
