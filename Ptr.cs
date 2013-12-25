using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PointerFactory
{

    public class Ptr<T> : IDisposable
    {

        public T I
        {
            get { return (T)FetchItem(); }
            set { Update(value); }
        }

        public Guid ID;
        public Type type;

        public Ptr(Address a) { PointerFactory.AllocateType(a.item.GetType()); ID = a.ID; type = a.item.GetType(); Addreference(); }

        public Ptr(Object O) { PointerFactory.AllocateType(O.GetType()); ID = Guid.NewGuid(); type = O.GetType(); PointerFactory.RegisterPointer(this, O); }

        public Ptr(Type t) { PointerFactory.AllocateType(t); ID = Guid.NewGuid(); type = t; }

        public Ptr() { ID = Guid.NewGuid(); type = new Object().GetType(); }

        //public Ptr(Ptr<T> other) { ID = Guid.NewGuid(); type = other.type; PointerFactory.RegisterPointer(this, other.I); }

        public Ptr(Ptr<T> other) { ID = other.ID; type = other.type; PointerFactory.UpdateRefCount(this, false); }
       
        public void Update(Object O){
           PointerFactory.UpdateAddress(this, O);
       }

        public Ptr<T> Addreference() { PointerFactory.UpdateRefCount(this, false); return this; }

        static public void AllocateType(Type ty) { PointerFactory.AllocateType(ty); }

        public void Dispose()
       {
           PointerFactory.UpdateRefCount(this, true);            
       }

        public T FetchItem() { return (T)PointerFactory.ReturnAddress(this).item;}

        public Address FetchAddress() { return PointerFactory.ReturnAddress(this); }
    }
}
