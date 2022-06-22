using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Shop
{
    public class SqlOperations
    {
        static SqlConnection sqlConnection = Connection.GetConnection();
        public static string Title = string.Empty;

        //Выборка начальных продуктов
        public static List<Product> SelectStartProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Product] JOIN [Category] ON " +
                "[Product].[idCategory] = [Category].[idCategory] WHERE [Product].[idCategory] = 1", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                                int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                            Title = reader["CategoryName"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return products;
        }

        //Выборка продуктов по категории
        public static List<Product> SelectProducts(string category)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Product] JOIN [Category] ON " +
                $"[Product].[idCategory] = [Category].[idCategory] WHERE [Product].[idCategory] = {category}", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                                int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                            Title = reader["CategoryName"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return products;
        }

        //Логин
        public static bool UserLogin(string username, string password)
        {
            try
            {
                password = ComputeSha256Hash(password);
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] WHERE [Login] = '{username}' AND [HeshPassword] = '{password}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return false;
        }

        //Регистрация
        public static bool UserRegister(string username, string password, string email)
        {
            try
            {
                password = ComputeSha256Hash(password);
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Users] VALUES('{username}','{password}', '{email}', 1)", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }

        //Хэширование пароля
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //Добавление категории
        public static bool AddCategory(string categoryname)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Category] VALUES('{categoryname}')", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        //Получение id пользователя
        public static string GetUserId(string login)
        {
            string idUser = string.Empty;
            try
            {
                if (login != null)
                {
                    using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] WHERE [Users].[Login] = '{login}'", sqlConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idUser = reader["idUser"].ToString();
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return idUser;
        }

        //Получение товаров в корзине
        public static List<Product> GetCartProduct(string idUser)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Cart] " +
                       $"JOIN [Product] ON [Cart].[idProduct] = [Product].[idProduct] WHERE [Cart].[idUser] = {idUser} ", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                                int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                            products.Last().Count = int.Parse(reader["Count"].ToString());
                            products.Last().Price = (int.Parse(reader["Price"].ToString()) * int.Parse(reader["Count"].ToString()));

                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return products;
        }

        //Получение id продукта
        public static string GetProductId(string productname)
        {
            string idProduct = string.Empty;
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Product] " +
                        $"WHERE [Product].[ProductName] = '{productname}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idProduct = reader["idProduct"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception) { }
            return idProduct;
        }

        //Добавление продуктов в корзину
        public static bool AddCartProduct(int count, string idProduct, string idUser)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Cart] VALUES({count},{idProduct}, {idUser})", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        //Удаление продуктов из корзины
        public static bool DeleteCartProduct(string idProduct, string idUser)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE [Cart] FROM [Cart] JOIN [Users] ON [Cart].[idUser] = [Users].[idUser]" +
                    $"WHERE [Cart].[idProduct] = {idProduct} AND [Cart].[idUser] = {idUser}", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        //Получение категорий
        public static List<Category> GetCategory()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Category]", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category(reader["CategoryName"].ToString()));
                            categories.Last().idCategory = int.Parse(reader["idCategory"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception) { }
            return categories;
        }

        //Удаление категории
        public static bool DeleteCategory(string categoryname)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Category] WHERE [Category].[CategoryName] = '{categoryname}'", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Exception) { }
            return false;
        }

        //Получение id категории
        public static string GetCategoryId(string categoryname)
        {
            string idCategory = string.Empty;
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Category] " +
                        $"WHERE [Category].[CategoryName] = '{categoryname}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idCategory = reader["idCategory"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception) { }
            return idCategory;
        }

        //Добавление продукта
        public static bool AddProduct(string productname, int price, string imagename, string info, string idCategory)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [Product] VALUES('{productname}', {price}, '{imagename}'," +
                    $"'{info}', {idCategory})", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        //Удаление продукта
        public static bool DeleteProduct(string idProduct)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM [Product] WHERE [Product].[idProduct] = {idProduct}", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Exception) { }
            return false;
        }

        //Существует ли такой продукт в корзине
        public static bool isProductExist(string idProduct, string idUser)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Cart] " +
                          $"WHERE [Cart].[idProduct] = '{idProduct}' AND [Cart].[idUser] = {idUser}", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        reader.Close();
                    }
                }

            }
            catch (System.Exception) { }

            return false;
        }
        //Кол-во продукта в корзине
        public static int ProductCount(string idProduct, string idUser)
        {
            int count = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Cart] " +
                          $"WHERE [Cart].[idProduct] = '{idProduct}' AND [Cart].[idUser] = {idUser}", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return count = int.Parse(reader["Count"].ToString());
                        }
                        reader.Close();
                    }
                }

            }
            catch (System.Exception) { }

            return count;
        }

        //Обновление продукта в корзине
        public static bool UpdateProduct(string idProduct, string idUser, int count)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE [Cart] " +
                                      $"SET [Cart].[Count] = {count} WHERE [Cart].[idProduct] = {idProduct}" +
                                      $"AND [Cart].[idUser] = {idUser}", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception) { }

            return false;
        }

        //Существует ли пользователь
        public static bool isUserExist(string login)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] " +
                          $"WHERE [Users].[Login] = '{login}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        reader.Close();
                    }
                }

            }
            catch (System.Exception) { }

            return false;
        }
        //Существует ли почта
        public static bool isEmailExist(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Users] " +
                          $"WHERE [Users].[Email] = '{email}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        reader.Close();
                    }
                }

            }
            catch (System.Exception) { }

            return false;
        }

        //Существует ли категория
        public static bool isCategoryExist(string category)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Category] " +
                          $"WHERE [Category].[CategoryName] = '{category}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        reader.Close();
                    }
                }

            }
            catch (System.Exception) { }

            return false;
        }

        //Выборка продуктов по названию
        public static List<Product> SelectExistProducts(string productname)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [Product] JOIN [Category] ON " +
                $"[Product].[idCategory] = [Category].[idCategory] WHERE [Product].[ProductName] = '{productname}'", sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader["ImageName"].ToString(), reader["ProductName"].ToString(),
                                int.Parse(reader["Price"].ToString()), reader["Info"].ToString()));
                            Title = reader["CategoryName"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (System.Exception){}
            return products;
        }


        //Удаление всех продуктов в категории
        public static bool DeleteAllProductInCategory(string categoryname)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE [Product] FROM [Product] JOIN [Category] ON " +
                    $"[Product].[idCategory] = [Category].[idCategory] WHERE [Category].[CategoryName] = '{categoryname}'", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Exception) { }
            return false;
        }

        //Удаление всех продуктов из корзины
        public static bool DeleteAllCartProduct(string idUser)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand($"DELETE [Cart] FROM [Cart] JOIN [Users] ON [Cart].[idUser] = [Users].[idUser]" +
                    $"WHERE [Cart].[idUser] = {idUser}", sqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}
