using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Common.Enums
{
    public enum StatusResponses
    {
        /// <summary>
        /// Success
        /// </summary>
        Success = 200,

        /// <summary>
        /// ErrorRequestParam
        /// </summary>
        ErrorRequestParam = 400,

        /// <summary>
        /// ErrorSystem
        /// </summary>
        ErrorSystem = 500,
    }
}
