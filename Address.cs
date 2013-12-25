using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointerFactory
{
    public class Address : IDisposable
    {
        public int RefCount;
        public Object item;
        public Guid ID;

        public Address(Object O, Guid id_) { item = O; ID = id_; RefCount = 1; }

        public void Increment(bool dec) {
            if (dec) System.Threading.Interlocked.Decrement(ref RefCount);
            else System.Threading.Interlocked.Increment(ref RefCount);
            if (RefCount == 0) Dispose();
        }
       
        public dynamic ReturnCopy() { return item; }
       
        public void Update(dynamic i) { item = i; }

        public void Dispose()
        {
            PointerFactory.RemoveAddress(this);
            if (!IsDisposable(item, true)) item = null;
            
        }

        public Boolean IsDisposable(Object Item, Boolean DeleteIfDisposable)
        {
            if (Item is IDisposable)
            {
                if (DeleteIfDisposable)
                {
                    IDisposable DisposableItem;
                    DisposableItem = (IDisposable)Item;
                    DisposableItem.Dispose();
                }
                return true;
            }
            else
                return false;
        }
    }
}
