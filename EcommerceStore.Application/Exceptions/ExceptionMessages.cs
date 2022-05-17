using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceStore.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public static string OrderUpdateFailed { get; private set; } = "An exception occurred during order update with id={0}";
        public static string OrderCreateFailed { get; private set; } = "An exception occurred during order create with id={0}";
        public static string LinkCategoryToSectionFailed { get; private set; } = "An exception occurred during linking subproductcategory with id={0} to section since parent product category linked to another section";
    }
}
