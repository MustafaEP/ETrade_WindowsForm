using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;




namespace ado.net
{
    public class ProductDal
    {

        
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=ETrade;integrated security=true");
        //sqle bağlantı nesnesi oluştuk

        private void ConnectionControl()
        {
            try
            {            
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sql' e bağlanılamadı! Hata:" + ex);
            }
        }


        public List<Product> GetAll()
        {
            ConnectionControl();


            SqlCommand command = new SqlCommand("select * from Products", _connection);
            //SqlCommand türünde sql fonksiyonumuzu içern nesneyi oluşturduk

            SqlDataReader reader = command.ExecuteReader();
            //komutumuzu reader nesnesine atadık

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };
                products.Add(product);
            }

            reader.Close();
            //reader' ı kapattık
            _connection.Close();
            //mssql ile bağlantımızı bitirdik

            return products;
        }

        public DataTable GetAll2()
        {

            ConnectionControl();
            //Bağlandık
            SqlCommand command = new SqlCommand("select * from Products", _connection);
            //SqlCommand türünde sql fonksiyonumuzu içern nesneyi oluşturduk

            SqlDataReader reader = command.ExecuteReader();
            //komutumuzu reader nesnesine atadık

            DataTable dataTable = new DataTable();
            //DataTable oluşturduk
            dataTable.Load(reader);
            //DataTable'yi doldurduk
            reader.Close();
            //reader' ı kapattık
            _connection.Close();
            //mssql ile bağlantımızı bitirdik

            return dataTable;
        }

        public List<Product> GetAllFiltreExpensive()
        {
            ConnectionControl();


            SqlCommand command = new SqlCommand("select * from Products order by UnitPrice desc", _connection);
            //SqlCommand türünde sql fonksiyonumuzu içern nesneyi oluşturduk
            
            

            SqlDataReader reader = command.ExecuteReader();
            //komutumuzu reader nesnesine atadık

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };
                products.Add(product);
            }

            reader.Close();
            //reader' ı kapattık
            _connection.Close();
            //mssql ile bağlantımızı bitirdik

            return products;
        }
        public List<Product> GetAllFiltreCheap()
        {
            ConnectionControl();


            SqlCommand command = new SqlCommand("select * from Products order by UnitPrice asc", _connection);
            //SqlCommand türünde sql fonksiyonumuzu içern nesneyi oluşturduk



            SqlDataReader reader = command.ExecuteReader();
            //komutumuzu reader nesnesine atadık

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };
                products.Add(product);
            }

            reader.Close();
            //reader' ı kapattık
            _connection.Close();
            //mssql ile bağlantımızı bitirdik

            return products;
        }
        public List<Product> GetAllFiltreSearch(String search)
        {
            ConnectionControl();


            SqlCommand command = new SqlCommand("select * from Products where Name like '" + search + "%'", _connection);
            
            //SqlCommand türünde sql fonksiyonumuzu içern nesneyi oluşturduk



            SqlDataReader reader = command.ExecuteReader();
            //komutumuzu reader nesnesine atadık

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                };
                products.Add(product);
            }

            reader.Close();
            //reader' ı kapattık
            _connection.Close();
            //mssql ile bağlantımızı bitirdik

            return products;
        }

        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Insert into Products values(@name, @unitPrice, @stockAmount)", _connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        
 

        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where Id=@id", _connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Delete from Products where Id=@id", _connection);

            
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        


        
    }
}
