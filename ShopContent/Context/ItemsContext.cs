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
    public class ItemsContext : Items
    {
        public ItemsContext(bool save = false)
        {
            if (save) Save(true);
            Category = new Categorys();
        }
        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<Categorys> allCategorys = CategorysContext.AllCategorys();

            SqlConnection connection = Connection.OpenConnection();
            SqlDataReader dataItem = Connection.Query("SELECT * FROM [dbo].[Items]", connection);
            while (dataItem.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = dataItem.GetInt32(0),
                    Name = dataItem.GetString(1),
                    Price = dataItem.GetDouble(2),
                    Description = dataItem.GetString(3),
                    Category = dataItem.IsDBNull(4) ? null : allCategorys.Where(x => x.Id == dataItem.GetInt32(4)).First()
                });
            }
            Connection.CloseConnection(connection);
            return allItems;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection = Connection.OpenConnection();
            if (New)
            {
                SqlDataReader dataItem = Connection.Query(
                    $"INSERT INTO " +
                        $"[dbo].[Items](" +
                            $"Name, " +
                            $"Price, " +
                            $"Description) " +
                    $"OUTPUT " +
                        $"Insertes.Id " +
                    $"VALUES(" +
                        $"N'{this.Name}, " +
                        $"'{this.Price}, " +
                        $"N'{this.Description}')", connection);
                dataItem.Read();
                this.Id = dataItem.GetInt32(0);
            }
            else
            {
                Connection.Query(
                    $"UPDATE " +
                        $"[dbo].[Items](" +
                    $"SET " +
                        $"Name=N'{this.Name}, " +
                        $"Price='{this.Price}, " +
                        $"Description=N'{this.Description}', " +
                        $"IdCategory={this.Category.Id}" +
                    $"WHERE " +
                        $"Id={this.Id}", connection);
            }
            Connection.CloseConnection(connection);
        }
        public void Delete()
        {
            SqlConnection connection = Connection.OpenConnection();
            Connection.Query($"DELETE FROM [dbo].[Items] WHERE Id={this.Id}", connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Add());
                });
            }
        }
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Category = CategorysContext.AllCategorys().Where(x => x.Id == this.Category.Id).First();
                    Save();
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
