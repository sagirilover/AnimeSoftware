using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Objects
{
    public class Entity
    {
        protected int _index = -1;
        protected uint _ptr;

        public IntPtr Ptr => (IntPtr) _ptr;
        public virtual int Index => _index;

        public Entity(int index)
        {
            this._index = index;
            _ptr = Memory.Read<uint>(Memory.Client + Signatures.dwEntityList + (index - 1) * 0x10);
        }

        public Entity(uint ptr)
        {
            this._ptr = ptr;
        }
    }
}