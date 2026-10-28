using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace LegacyOrderProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args.Length > 0 ? args[0] : "orders.csv";
            var repository = new OrderRepository(ConfigurationManager.AppSettings["DbConnection"]);
            var processor = new DiscountEngine(
                decimal.Parse(ConfigurationManager.AppSettings["HighValueThreshold"], CultureInfo.InvariantCulture),
                decimal.Parse(ConfigurationManager.AppSettings["HighValueDiscountPercent"], CultureInfo.InvariantCulture));

            var orders = CsvOrderLoader.Load(input);
            foreach (var order in orders)
            {
                var finalAmount = processor.Apply(order.TotalAmount);
                repository.Save(order.Id, order.CustomerName, order.TotalAmount, finalAmount, DateTime.UtcNow);
                Console.WriteLine($"Processed order {order.Id}: {order.TotalAmount:C} -> {finalAmount:C}");
            }
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public static class CsvOrderLoader
    {
        public static IEnumerable<Order> Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Order input file not found", path);
            }

            var lines = File.ReadAllLines(path);
            var result = new List<Order>();
            for (var i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                result.Add(new Order
                {
                    Id = int.Parse(parts[0], CultureInfo.InvariantCulture),
                    CustomerName = parts[1],
                    TotalAmount = decimal.Parse(parts[2], CultureInfo.InvariantCulture)
                });
            }

            return result;
        }
    }
}
