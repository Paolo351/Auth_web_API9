using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Infrastructure
{
    public interface IMongoBDO
    {
        string CollectionName_depl { get; set; }
        string CollectionName_pro { get; set; }
        string CollectionName_user { get; set; }
        string CollectionName_db { get; set; }
        
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
