using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FigureCollections;

namespace Simple
{
    class SimpleStack<T> : SimpleList<T>
        where T : IComparable
    {
        public void Push(T element)
        {
            Add(element);
        }

        public T Pop()
        {
            T result = default(T);

            if (Count == 0)
                return result;
            if (Count == 1)
            {
                result = first.data;
                first.next = null;
                last.next = null;
            }
            else
            {
                SimpleListItem<T> newlast = GetItem(Count - 2);
                result = newlast.next.data;
                last = newlast;
                newlast.next = null;
            }
            Count--;
            return result;
        }
    }
}
