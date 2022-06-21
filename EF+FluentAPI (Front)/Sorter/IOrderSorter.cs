using EF_FluentAPI__Front_.Models;

namespace EF_FluentAPI__Front_.Sorter
{
    public interface IOrderSorter
    {
        List<Order> Sort(List<Order> orders, int id);
    }
}
