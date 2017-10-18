using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Common.Constants
{
    public class MessageResConst
    {       

        public const string Success = "Success";
        public const string ErrorCommonRequestParam = "The request was unacceptable, often due to missing a required parameter";
        public const string ErrorSystem = "Internal server error";
        public const string NotExistMsg = "This record is not exist in DB";
        public const int PageSize = 10;
        public const string Decrease = "decrease";
        public const string Increase = "increase";
    }
}
