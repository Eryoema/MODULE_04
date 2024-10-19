using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAC
{
    public class Order
    {
        public List<OrderItem> Items { get; private set; }
        public double TotalAmount { get; private set; }
        public IPayment PaymentMethod { get; set; }
        public IDelivery DeliveryMethod { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

        public void AddItem(string productName, int quantity, double price)
        {
            Items.Add(new OrderItem(productName, quantity, price));
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.Price * item.Quantity;
            }
        }

        public void ProcessPayment()
        {
            PaymentMethod.ProcessPayment(TotalAmount);
        }

        public void DeliverOrder()
        {
            DeliveryMethod.DeliverOrder(this);
        }
    }

    public class OrderItem
    {
        public string ProductName { get; }
        public int Quantity { get; }
        public double Price { get; }

        public OrderItem(string productName, int quantity, double price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }

    public interface IPayment
    {
        void ProcessPayment(double amount);
    }

    public class CreditCardPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Обработка платежа по кредитной карте на сумму {amount:C}");
        }
    }

    public class PayPalPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Обработка платежа через PayPal на сумму {amount:C}");
        }
    }

    public class BankTransferPayment : IPayment
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Обработка банковского перевода на сумму {amount:C}");
        }
    }

    public interface IDelivery
    {
        void DeliverOrder(Order order);
    }

    public class CourierDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine($"Доставка заказа курьером. Общая сумма: {order.TotalAmount:C}");
        }
    }

    public class PostDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine($"Доставка заказа почтой. Общая сумма: {order.TotalAmount:C}");
        }
    }

    public class PickUpPointDelivery : IDelivery
    {
        public void DeliverOrder(Order order)
        {
            Console.WriteLine($"Заказ готов к самовывозу. Общая сумма: {order.TotalAmount:C}");
        }
    }

    public interface INotification
    {
        void SendNotification(string message);
    }

    public class EmailNotification : INotification
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Отправка email-уведомления: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Отправка SMS-уведомления: {message}");
        }
    }

    public class DiscountCalculator
    {
        private List<IDiscountRule> discountRules;

        public DiscountCalculator()
        {
            discountRules = new List<IDiscountRule>();
        }

        public void AddDiscountRule(IDiscountRule rule)
        {
            discountRules.Add(rule);
        }

        public double CalculateDiscount(Order order)
        {
            double discount = 0;
            foreach (var rule in discountRules)
            {
                discount += rule.ApplyDiscount(order);
            }
            return discount;
        }
    }

    public interface IDiscountRule
    {
        double ApplyDiscount(Order order);
    }

    public class PercentageDiscount : IDiscountRule
    {
        private readonly double percentage;

        public PercentageDiscount(double percentage)
        {
            this.percentage = percentage;
        }

        public double ApplyDiscount(Order order)
        {
            return order.TotalAmount * (percentage / 100);
        }
    }

    public class Program
    {
        public static void Main()
        {
            var order = new Order();
            order.AddItem("Товар A", 2, 100);
            order.AddItem("Товар B", 1, 200);

            order.PaymentMethod = new CreditCardPayment();
            order.DeliveryMethod = new CourierDelivery();

            order.ProcessPayment();

            order.DeliverOrder();

            var notification = new EmailNotification();
            notification.SendNotification("Ваш заказ успешно оформлен!");

            var discountCalculator = new DiscountCalculator();
            discountCalculator.AddDiscountRule(new PercentageDiscount(10));

            double discount = discountCalculator.CalculateDiscount(order);
            Console.WriteLine($"Примененная скидка: {discount:C}");
        }
    }
}
