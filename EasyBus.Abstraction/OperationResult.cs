using System;
using System.Linq;

namespace EasyBus.Abstraction
{
    public class OperationResult
    {
        #region Properties

        public object Request { get; set; }

        public object Response { get; set; }

        public int State { get; set; }

        public bool HasError { get; set; }

        public Exception Exception { get; set; }

        #endregion

        public OperationResult()
        {
            //Set default value 
            this.State = 0;
        }
    }
}