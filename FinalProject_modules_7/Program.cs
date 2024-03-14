namespace FinalProject_modules_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var courier1 = new Courier("Вася");
            var deliveryHome = new HomeDelivery("калатушкина", courier1, 100);
            var order1 = new Order<HomeDelivery>(1,deliveryHome);
            var product1 = new Product(1, "Клубника", "Фрукт");
            var product2 = new Product(2, "яблоки", "Фрукт");
            order1.AddProductInOrder(product1);
            var client = new Client<HomeDelivery>(1, "Игорь клиент",order1);
            
            var deliveryPoint = new PickPointDelivery("cjsajd", courier1, 500);
            var order2 = new Order<PickPointDelivery>(2, deliveryPoint);
            order2.AddProductInOrder(product2 );
            var client2 = new Client<PickPointDelivery>(8, "ПУНКТ ВЫДАЧИ КАСТРЮЛЯ", order2);

            client2.PrintAllInfo();
            Console.WriteLine();
            Console.WriteLine();
            client.PrintAllInfo();

        }
        class Courier
        {
           
            public string Name { get; private set; }
            public Courier(string name)
            {
                Name = name;
            }
        }
        abstract class Delivery
        {
            public int BasePrice { get; protected set; }
            public Courier Courier { get; protected set; }
            public string Address {  get; protected set; }
            public int DistanceInKilometers {  get; protected set; }
            public Delivery(string adress, Courier courier, int DistanceInKilometers)
            {
                this.DistanceInKilometers = DistanceInKilometers;
                Address = adress;
                Courier = courier;
            }
           
           
           
        }
        class HomeDelivery : Delivery
        {
            public HomeDelivery(string adress, Courier courier, int distanceInKilometers) : base(adress, courier, distanceInKilometers)
            {
                BasePrice = 5;
               
            }
  
        }
        class PickPointDelivery : Delivery
        {

            public string AdressDeliveryPoint { get; private set; }
            public PickPointDelivery(string adress, Courier courier, int distanceInKilometers) : base(adress,courier, distanceInKilometers)
            {
                AdressDeliveryPoint = adress;
                BasePrice = 10;
            }

            
        }
        class ShopDelivery : Delivery
        {
            public string ShopName { get; private set; }

            public ShopDelivery(string shopName, string address, Courier courier, int distanceInKilometers) : base(address, courier, distanceInKilometers)
            {
                ShopName = shopName;
                BasePrice = 0; 
            }
        }

        class Order<TDelivery> where TDelivery : Delivery
        {
            public TDelivery delivery;

            public int Number;

            public List<Product> Products {  get; private set; }


            public void DisplayAddress()
            {
                Console.WriteLine(delivery.Address);
            }
            public Order(int number,TDelivery delivery)
            {
                this.delivery = delivery;
                Products = new List<Product>();
                Number = number;
                
            }
            public Order<TDelivery> AddProductInOrder(Product product)
            {
                Products.Add(product);
                return this;

            }
               
            public  int PriceDelivery()
            {
                return delivery.BasePrice * delivery.DistanceInKilometers + Products.Count;
            }
           
           
            

        }
        class Product
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public Product(int id, string name, string description)
            {
                Id = id;
                Name = name;
                Description = description;
            }
        }
        class Client<TDelivery> where TDelivery : Delivery 
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public Order<TDelivery> Order { get; private set; }
            public Client(int id, string name, Order<TDelivery> order)
            {

                Order = order; 
                Id = id;
                Name = name;
            }
            public void PrintAllInfo()
            {
                Console.Write($"Клиент: {Name}\nНомер заказа: {Order.Number}\nДоставка по адресу: {Order.delivery.Address}\nСтоимость доставки:" +
                    $"{Order.PriceDelivery()} рублей\nКурьер: {Order.delivery.Courier.Name}\nСоставляющее заказа:" );
                foreach (Product product in Order.Products)
                {
                    Console.Write(product.Name + " ");
                }
            }

        }
    }
}
