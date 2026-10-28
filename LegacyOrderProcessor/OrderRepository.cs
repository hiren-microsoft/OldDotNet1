using System;
using System.Data.SqlClient;

namespace LegacyOrderProcessor
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(int orderId, string customerName, decimal originalAmount, decimal finalAmount, DateTime processedUtc)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
INSERT INTO ProcessedOrders (OrderId, CustomerName, OriginalAmount, FinalAmount, ProcessedUtc)
VALUES (@OrderId, @CustomerName, @OriginalAmount, @FinalAmount, @ProcessedUtc);";

                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@CustomerName", customerName);
                command.Parameters.AddWithValue("@OriginalAmount", originalAmount);
                command.Parameters.AddWithValue("@FinalAmount", finalAmount);
                command.Parameters.AddWithValue("@ProcessedUtc", processedUtc);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
