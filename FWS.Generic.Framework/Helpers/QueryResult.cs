using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWS.Generic.Framework.AbstractClasses;
using FWS.Generic.Framework.Enumerations;
using FWS.Generic.Framework.Helpers.ExceptionHanlding;
using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.Helpers
{
    public class QueryResult<T> : GenericResult
    {
        public int TotalCount { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public QueryResult(Query query)
            : base()
        {
            this.Skip = query.Skip;
            this.Take = query.Take;
        }

        public QueryResult(List<T> results)
            : base()
        {
            this.Results = results;
        }
        
        public QueryResult(Exception e)
        {
            this.Error = e;
        }

        public List<T> Results { get; set; }
    }
}
