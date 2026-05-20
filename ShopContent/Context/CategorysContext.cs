using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopContent.Classes;
using ShopContent.Modell;

namespace ShopContent.Context
{
    public class CategorysContext : Categorys
    {
        public static ObservableCollection<Categorys> AllCategorys()
        {
            ObservableCollection<Categorys> allCategorys = new ObservableCollection<Categorys>();

            SqlConnection connection = Connection.OpenConnection();
            SqlDataReader dataCategorys = Connection.Query("SELECT * FROM [dbo].[Categorys]", connection);
            while (dataCategorys.Read())
            {
                allCategorys.Add(new CategorysContext()
                {
                    Id = dataCategorys.GetInt32(0),
                    Name = dataCategorys.GetString(1)
                });
            }
            Connection.CloseConnection(connection);

            return allCategorys;
        }
    }
}
