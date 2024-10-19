using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    using System;
    using System.Collections.Generic;

    // Класс Order, который отвечает только за хранение данных о заказе
    public class Order
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    // Класс для расчета цены
    public class PriceCalculator
    {
        public double CalculateTotalPrice(Order order)
        {
            // Рассчет стоимости с учетом скидок
            return order.Quantity * order.Price * 0.9;
        }
    }

    // Классы для обработки разных типов сотрудников
    public abstract class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public abstract double CalculateSalary();
    }

    public class PermanentEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 1.2; // Permanent employee gets 20% bonus
        }
    }

    public class ContractEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 1.1; // Contract employee gets 10% bonus
        }
    }

    public class Intern : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 0.8; // Intern gets 80% of the base salary
        }
    }

    // Класс для расчета зарплаты
    public class EmployeeSalaryCalculator
    {
        public double CalculateSalary(Employee employee)
        {
            return employee.CalculateSalary();
        }
    }

    // Интерфейсы для принтеров
    public interface IPrintable
    {
        void Print(string content);
    }

    public interface IScannable
    {
        void Scan(string content);
    }

    public interface IFaxable
    {
        void Fax(string content);
    }

    // Реализация универсального принтера
    public class AllInOnePrinter : IPrintable, IScannable, IFaxable
    {
        public void Print(string content)
        {
            Console.WriteLine("Printing: " + content);
        }

        public void Scan(string content)
        {
            Console.WriteLine("Scanning: " + content);
        }

        public void Fax(string content)
        {
            Console.WriteLine("Faxing: " + content);
        }
    }

    // Реализация базового принтера
    public class BasicPrinter : IPrintable
    {
        public void Print(string content)
        {
            Console.WriteLine("Printing: " + content);
        }
    }

    // Интерфейсы для отправки уведомлений
    public interface INotificationSender
    {
        void SendNotification(string message);
    }

    // Реализация отправки уведомлений по электронной почте
    public class EmailSender : INotificationSender
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("Email sent: " + message);
        }
    }

    // Реализация отправки SMS-уведомлений
    public class SmsSender : INotificationSender
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("SMS sent: " + message);
        }
    }

    // Класс для управления уведомлениями
    public class NotificationService
    {
        private readonly List<INotificationSender> _notificationSenders;

        public NotificationService(List<INotificationSender> notificationSenders)
        {
            _notificationSenders = notificationSenders;
        }

        public void SendNotification(string message)
        {
            foreach (var sender in _notificationSenders)
            {
                sender.SendNotification(message);
            }
        }
    }

    // Пример использования
    class Program
    {
        static void Main(string[] args)
        {
            // Пример работы с заказом
            Order order = new Order { ProductName = "Laptop", Quantity = 2, Price = 1500.00 };
            PriceCalculator priceCalculator = new PriceCalculator();
            double totalPrice = priceCalculator.CalculateTotalPrice(order);
            Console.WriteLine($"Total price for the order: {totalPrice}");

            // Пример работы с зарплатами сотрудников
            EmployeeSalaryCalculator salaryCalculator = new EmployeeSalaryCalculator();
            Employee employee = new PermanentEmployee { Name = "Alice", BaseSalary = 5000 };
            Console.WriteLine($"{employee.Name}'s salary: {salaryCalculator.CalculateSalary(employee)}");

            // Пример работы с принтерами
            IPrintable basicPrinter = new BasicPrinter();
            basicPrinter.Print("Basic document");

            AllInOnePrinter allInOnePrinter = new AllInOnePrinter();
            allInOnePrinter.Print("All-in-one document");
            allInOnePrinter.Scan("Scanning document");
            allInOnePrinter.Fax("Faxing document");

            // Пример работы с уведомлениями
            var notificationSenders = new List<INotificationSender> { new EmailSender(), new SmsSender() };
            NotificationService notificationService = new NotificationService(notificationSenders);
            notificationService.SendNotification("Your order has been processed.");
        }
    }
}
