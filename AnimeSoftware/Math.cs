using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeSoftware
{
    class VectorMath
    {
        public static float Distance(Vector3 src, Vector3 dst)
        {
            float result = (float)Math.Sqrt((dst.x-src.x) * (dst.x-src.x) + (dst.y-src.y) * (dst.y-src.y) + (dst.z-src.z) * (dst.z-src.z));
            return result;
        }
    }
}
