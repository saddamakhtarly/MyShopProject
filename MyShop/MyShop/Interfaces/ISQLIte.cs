using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Interfaces
{
    public interface ISQLIte
    {
        SQLiteConnection GetConnection();
    }
}

