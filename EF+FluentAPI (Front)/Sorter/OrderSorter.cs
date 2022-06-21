using EF_FluentAPI__Front_.Models;

namespace EF_FluentAPI__Front_.Sorter
{
    public class OrderSorter : IOrderSorter
    {
        public List<Order> Sort(List<Order> orders, int id)
        {
            switch (id)
            {
                case 1:
                    orders.Sort((a, b) => a.OrderDate.CompareTo(b.OrderDate));
                    break;
                case 2:
                    orders.Sort((a, b) => b.OrderDate.CompareTo(a.OrderDate));
                    break;
                case 3:
                    orders.Sort((a, b) => a.TotalPrice.CompareTo(b.TotalPrice));
                    break;
                case 4:
                    orders.Sort((a, b) => b.TotalPrice.CompareTo(a.TotalPrice));
                    break;
            }
            return orders;
        }
    }
}
