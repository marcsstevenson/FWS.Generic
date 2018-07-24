using System;
using System.Xml.Serialization;
using FWS.Generic.Framework.Helpers.ExceptionHanlding;

namespace FWS.Generic.Framework.AbstractClasses
{
    public abstract class GenericResult
    {
        protected GenericResult()
        {
        }

        protected GenericResult(Exception e)
            : this()
        {
            this.Error = e;
        }

        [XmlIgnore]
        public Exception Error { get; set; }
        public bool HasError => this.Error != null;
        public string ErrorMessage => Error?.GetInnerMostException().Message ?? string.Empty;
    }
}
