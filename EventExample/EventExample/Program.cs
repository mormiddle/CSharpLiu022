using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EventExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;
            customer.Action();
            customer.PlayTheBill();
        }
    }
    //事件参数
    public class OrderEventArgs :EventArgs
    { 
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    //定义委托：委托是类
    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);
        

    public class Customer
    {
        //定义一个委托类型字段，用来引用（存储）事件处理器
        //也就是委托的实例
        private OrderEventHandler OrderEventHandler;

        //创建事件
        public event OrderEventHandler Order {
            add
            {
                this.OrderEventHandler += value;
            }
            remove
            {
                this.OrderEventHandler -= value;
            }
        }

        public double Bill { get; set; }

        public void PlayTheBill()
        {
            Console.WriteLine("I will play ${0}",this.Bill);
        }

        //事件拥有者的内部逻辑来触发事件
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDownd()
        {
            Console.WriteLine("Sit Down.");
        }
        public void Think()
        {
            //客户思考一会
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine("Let me think..");
                Thread.Sleep(1000);
            }

            //客户点菜，设置事件参数
            if (this.OrderEventHandler != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.OrderEventHandler.Invoke(this, e);
            }
        }

        //设置action方法，用来调用上述一连串的方法
        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDownd();
            this.Think();
        }
    }

    public class Waiter
    {
        internal void Action(Customer customer, OrderEventArgs e)
        {
            Console.WriteLine("I will serve you the dish - {0}", e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }

            customer.Bill += price;
        }
    }
}
