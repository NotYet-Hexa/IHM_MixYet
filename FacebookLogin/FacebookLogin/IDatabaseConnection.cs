using System;
using System.Collections.Generic;
using System.Text;

namespace FacebookLogin
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
