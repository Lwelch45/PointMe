using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointerFactory
{
    public class PointerFactory
    {

        static public Dictionary<Type, Dictionary<Guid, Address>> PointerMap = new Dictionary<Type, Dictionary<Guid, Address>>() ;

        static public void AllocateType(Type ty) {
            if (!PointerMap.ContainsKey(ty)){
                PointerMap.Add(ty,new Dictionary<Guid,Address>());
            }
        }

        static public object ItemExists(Object t) 
        {
            if (!(PointerMap.ContainsKey(t.GetType())))
            {
                return null;
            }
            else
            {
                var map = PointerMap[t.GetType()];
                foreach (KeyValuePair<Guid,Address> m in map)
                {
                    if (m.Value.item == t) return m;
                }
                return null;
            }
        }

        static public Ptr<T> CreatePointer<T>(Object t)
        {
            //var res = ItemExists(t);
            //if (!(res == null))
            //{
            //    Ptr<T> ptr = new Ptr<T>(((KeyValuePair<Guid, Address>)res).Value);
            //    return ptr;
            //}
            
            
            if (PointerMap.ContainsKey(t.GetType()))
            {
                Ptr<T> ptr = new Ptr<T>();
                
                PointerMap[t.GetType()].Add(ptr.ID, new Address(t,ptr.ID));
                return ptr;
            }
            else
            {
                AllocateType(t.GetType());
                Ptr<T> ptr = new Ptr<T>();
                PointerMap[t.GetType()].Add(ptr.ID, new Address(t, ptr.ID));
                return ptr;
            }
        }

        static public void RegisterPointer<T>(Ptr<T> p, Object t)
        {
            PointerMap[t.GetType()].Add(p.ID, new Address(t,p.ID));
        }

        static public void UpdateAddress<T>(Ptr<T> p, Object a)
        {
            PointerMap[p.type][p.ID].Update(a);
        }

        static public void UpdateRefCount<T>(Ptr<T> p, bool dec) { PointerMap[p.type][p.ID].Increment(dec); }

        static public Address ReturnAddress<T>(Ptr<T> p)
        {
           return PointerMap[p.type][p.ID];
        }

        static public void RemoveAddress(Address a)
        {
            try
            { PointerMap[a.item.GetType()].Remove(a.ID);}
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
           
        }
    }
}
