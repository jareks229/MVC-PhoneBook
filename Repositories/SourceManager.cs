using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warsztaty_3.Models;
using Warsztaty_3.Repositories;

namespace Warsztaty_3
{
    public class SourceManager : ActiveRecord
    {
        public List<PersonModel> Get()
        {
            var result = new List<PersonModel>();
            var query = "SELECT Id, FirstName, LastName, Phone, Email, Created, Updated FROM People";
            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(ReaderToPeople(reader));
                }
            }

            return result;
        }

        public PersonModel GetById(int id)
        {
            var query = "SELECT * FROM People WHERE Id = @Id";

            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return ReaderToPeople(reader);
                }

                return null;
                
            }

        }

        public void Add(PersonModel model)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"INSERT INTO People (FirstName, LastName, Phone, Email, Created, Updated) ");
            sqlBuilder.Append($"VALUES ('{model.FirstName}', '{model.LastName}', '{model.Phone}', '{model.Email}', '{model.Created}', '{model.Updated}')");

            var sql = sqlBuilder.ToString();

            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Update(PersonModel model)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"UPDATE dbo.People ");
            sqlBuilder.Append($"SET FirstName = '{model.FirstName}', LastName = '{model.LastName}', Phone = '{model.Phone}', Email = '{model.Email}', Updated = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ");
            sqlBuilder.Append($"WHERE Id = '{model.ID}'");

            var sql = sqlBuilder.ToString();
            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            
        }

        public void Remove(int id)
        {
            var sql = $"DELETE FROM People WHERE Id = {id}";

            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            /*
            using (var connection = GetOpenConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex}");
                }
            }
            */

        }

        private static PersonModel ReaderToPeople(SqlDataReader reader)
        {
            return new PersonModel()
            {
                ID = Convert.ToInt32(reader["Id"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Email = Convert.ToString(reader["Email"]),
                Phone = Convert.ToString(reader["Phone"]),
                Created = Convert.ToDateTime(reader["Created"]),
                Updated = Convert.ToDateTime(reader["Updated"])
            };
        }




    }
}
