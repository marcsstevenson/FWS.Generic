using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using FWS.Generic.Framework.Helpers;

namespace FWS.Generic.Framework.Models
{
    [DataContract]
    public class CacheResponse<T> where T : class 
    {
        public CacheResponse()
            : this(true)
        {

        }

        public CacheResponse(bool start)
        {
            if (start) this.Start();
        }

        private DateTime? StartDateTime { get; set; }

        private T _returnValue = null;

        [DataMember]
        public T ReturnValue
        {
            get { return this._returnValue; }
            set { this._returnValue = value; this.Stop(); }
        }

        [DataMember]
        public double? AccessMs { get; set; }

        [XmlIgnore]
        public Exception Exception { get; set; }

        [DataMember]
        public string ExceptionMessage
        {
            get { return this.Exception == null ? string.Empty : this.Exception.ToString(); }
            set { /*Just to make serialisation happy*/ }
        }

        public void Start()
        {
            StartDateTime = Clock.Now();
        }
        public void Stop()
        {
            if (StartDateTime.HasValue)
                this.AccessMs = Clock.Now().Subtract(StartDateTime.Value).TotalMilliseconds;
        }
    }
}