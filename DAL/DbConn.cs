using System.Data.SQLite;

namespace EmployeeCRUD.DAL
{
    class DbConn
    {
        public SQLiteConnection Con = new SQLiteConnection(@"Data Source= EmployeeDB.db; Version=3; FailIfMissing=True; Foreign Keys=True;");
    }
}
