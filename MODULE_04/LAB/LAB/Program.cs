using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB
{
    using System;
    using System.Collections.Generic;

    // SRP: Класс Invoice для представления данных счета-фактуры
    public class Invoice
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public double TaxRate { get; set; }
    }

    // SRP: Класс Item для представления элемента счета-фактуры
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    // SRP: Класс для расчета общей суммы счета-фактуры
    public class InvoiceCalculator
    {
        public double CalculateTotal(Invoice invoice)
        {
            double subTotal = 0;
            foreach (var item in invoice.Items)
            {
                subTotal += item.Price;
            }
            return subTotal + (subTotal * invoice.TaxRate);
        }
    }

    // SRP: Класс для сохранения счета-фактуры в базу данных
    public class InvoiceRepository
    {
        public void SaveToDatabase(Invoice invoice)
        {
            // Логика для сохранения счета-фактуры в базу данных
            Console.WriteLine($"Счет-фактура с ID {invoice.Id} сохранен в базу данных.");
        }
    }

    // OCP: Интерфейсы для стратегий расчета скидок
    public interface IDiscountStrategy
    {
        double CalculateDiscount(double amount);
    }

    public class RegularDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount) => amount;
    }

    public class SilverDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount) => amount * 0.9; // 10% скидка
    }

    public class GoldDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount) => amount * 0.8; // 20% скидка
    }

    // OCP: Класс для расчета скидок
    public class DiscountCalculator
    {
        private readonly IDiscountStrategy _discountStrategy;

        public DiscountCalculator(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public double CalculateDiscount(double amount)
        {
            return _discountStrategy.CalculateDiscount(amount);
        }
    }

    // ISP: Интерфейсы для работников
    public interface IWorker
    {
        void Work();
    }

    public interface IEater
    {
        void Eat();
    }

    public interface ISleeper
    {
        void Sleep();
    }

    // ISP: Класс для человека
    public class HumanWorker : IWorker, IEater, ISleeper
    {
        public void Work()
        {
            Console.WriteLine("Человек работает.");
        }

        public void Eat()
        {
            Console.WriteLine("Человек ест.");
        }

        public void Sleep()
        {
            Console.WriteLine("Человек спит.");
        }
    }

    // ISP: Класс для робота
    public class RobotWorker : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Робот работает.");
        }
    }

    // DIP: Интерфейс для уведомлений
    public interface INotificationService
    {
        void Send(string message);
    }

    // DIP: Класс для отправки Email
    public class EmailService : INotificationService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Отправка Email: {message}");
        }
    }

    // DIP: Класс для отправки уведомлений
    public class Notification
    {
        private readonly INotificationService _notificationService;

        public Notification(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Send(string message)
        {
            _notificationService.Send(message);
        }
    }

    // Пример использования всех классов
    public class Program
    {
        public static void Main(string[] args)
        {
            // Пример использования Invoice и связанных классов
            var invoice = new Invoice
            {
                Id = 1,
                TaxRate = 0.2, // 20% налог
            };
            invoice.Items.Add(new Item { Name = "Товар 1", Price = 100 });
            invoice.Items.Add(new Item { Name = "Товар 2", Price = 200 });

            var calculator = new InvoiceCalculator();
            double total = calculator.CalculateTotal(invoice);
            Console.WriteLine($"Общая сумма счета: {total}");

            var repository = new InvoiceRepository();
            repository.SaveToDatabase(invoice);

            // Пример использования DiscountCalculator
            var discountCalculator = new DiscountCalculator(new GoldDiscount());
            double discountedAmount = discountCalculator.CalculateDiscount(total);
            Console.WriteLine($"Сумма со скидкой: {discountedAmount}");

            // Пример использования работников
            IWorker humanWorker = new HumanWorker();
            humanWorker.Work();
            ((IEater)humanWorker).Eat();
            ((ISleeper)humanWorker).Sleep();

            IWorker robotWorker = new RobotWorker();
            robotWorker.Work();

            // Пример использования уведомлений
            var emailService = new EmailService();
            var notification = new Notification(emailService);
            notification.Send("Привет!");
        }
    }
}
