using MongoDB.Driver;
using Siigo.ClientTeams.Domain.Models;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Data.SqlClient;
using System.Data;

namespace Siigo.ClientTeams.Domain.Strategy
{
    public class CustomSqlConnection : ConnectionBase, IDatabaseStrategy
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        public CustomSqlConnection(MessageBody messageBody) : base(messageBody)
        {
        }

        public IEnumerable<Fact> BuildFacts()
        {

            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(messageBody.Collection, connection);
            connection.Open();
            var jsonObjects = ExecuteToJson(command);
            var jsonObject = JsonConvert.DeserializeObject<List<object>>(jsonObjects);
            connection.Close();
            return GetFacts(jsonObject.FirstOrDefault());
        }

        private static string ExecuteToJson(SqlCommand cmd)
        {
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            using DataTable dt = new();
            using SqlDataAdapter da = new(cmd);
            da.Fill(dt);

            List<Dictionary<string, object>> rows = new();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return JsonConvert.SerializeObject(rows);
        }
    }

}
