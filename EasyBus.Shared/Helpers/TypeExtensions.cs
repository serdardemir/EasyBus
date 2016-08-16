using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBus.Shared.Helpers
{
    public static class TypeExtensions
    {

        public static bool IsMessageHandler(this Type type, Type targetType)
        {
            if (type.BaseType != null && (type.BaseType).IsGenericType)
                return (type.BaseType.GetGenericTypeDefinition() == targetType);

            else return false;
        }
    }
}
