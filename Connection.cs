using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem
{
    class Connection
    {
        public SqlConnection conn;
        public Connection()
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E17E3B9;Initial Catalog=ContactSystem;Integrated Security=True");
        }

    }
}
