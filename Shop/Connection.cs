using System.Data.SqlClient;

namespace Shop
{
    public class Connection
    {
        static SqlConnection sqlConnection;
        public static SqlConnection GetConnection()
        {
            if(sqlConnection == null)
            {
                sqlConnection = new SqlConnection("Data Source=SQL8002.site4now.net;Initial Catalog=db_a88c09_shop;User Id=db_a88c09_shop_admin;Password=qwerty123");
                sqlConnection.Open();
            }
            return sqlConnection;   
        }
    }
}
