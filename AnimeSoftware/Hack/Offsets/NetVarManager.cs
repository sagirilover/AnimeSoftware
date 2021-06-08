using AnimeSoftware.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware.Offsets
{
    internal class NetVarManager
    {
        public static Hashtable NetVars;

        public static void Init()
        {
            NetVars = FullDump();
        }

        public static unsafe Hashtable FullDump()
        {
            var hashtable = new Hashtable();
            for (var i = (ClientClass*) (Memory.Client + Signatures.dwGetAllClasses); i != null; i = i->Next())
            {
                Log.Debug("Dumping:", i->GetRecvTable()->GetName());
                if (!hashtable.ContainsKey(i->GetRecvTable()->GetName()))
                    hashtable.Add(i->GetRecvTable()->GetName(), DumpTable(i->GetRecvTable()));
            }

            return hashtable;
        }

        public static unsafe Hashtable DumpTable(RecvTable* table, int offset = 0)
        {
            var hashtable = new Hashtable();
            for (var i = 0; i < table->GetPropsCount(); i++)
            {
                var prop = (RecvProp*) ((IntPtr) table->GetRecvProps() + i * sizeof(RecvProp));

                if (prop == null)
                    continue;
                if (prop->GetName().Contains("baseclass") || prop->GetName().StartsWith("0") ||
                    prop->GetName().StartsWith("1") || prop->GetName().StartsWith("2"))
                    continue;

                if (!hashtable.ContainsKey(prop->GetName()))
                    hashtable.Add(prop->GetName(), prop->GetOffset() + offset);

                if (prop->GetDataTable() != null)
                    foreach (DictionaryEntry entry in DumpTable(prop->GetDataTable(), prop->GetOffset() + offset))
                        if (!hashtable.ContainsKey(entry.Key))
                            hashtable.Add(entry.Key, entry.Value);
            }

            return hashtable;
        }

        #region Debug

        //public unsafe static void DumpTable(RecvTable* table, int depth)
        //{
        //    for (int i = 0; i < table->GetPropsCount(); i++)
        //    {
        //        RecvProp* prop = (RecvProp*)((IntPtr)table->GetRecvProps() + i * sizeof(RecvProp));
        //
        //        try
        //        {
        //            if (prop == null)
        //                continue;
        //            if (prop->GetName().Contains("baseclass") || prop->GetName().StartsWith("0") || prop->GetName().StartsWith("1") || prop->GetName().StartsWith("2"))
        //                continue;
        //
        //            Console.WriteLine(new string(' ', depth * 2) + prop->GetName() + "  0x" + prop->GetOffset().ToString("X"));
        //
        //            if (prop->GetDataTable() != null)
        //            {
        //                DumpTable(prop->GetDataTable(), depth + 1);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //
        //            Console.WriteLine(((IntPtr)prop).ToString("X"));
        //            Console.WriteLine(ex.ToString());
        //        }
        //
        //    }
        //}
        //public unsafe static void DebugFullDump()
        //{
        //    for (ClientClass* i = (ClientClass*)(Memory.Client + signatures.dwGetAllClasses); i != null; i = i->Next())
        //    {
        //        Console.WriteLine(i->GetName());
        //        Console.WriteLine("__" + i->GetRecvTable()->GetName());
        //        DumpTable(i->GetRecvTable(), 2);
        //
        //    }
        //}

        #endregion
    }
}