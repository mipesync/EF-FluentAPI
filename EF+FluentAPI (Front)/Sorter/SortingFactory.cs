namespace EF_FluentAPI__Front_.Sorter
{
    public class SortingFactory
    {
        public static IOrderSorter UseSorter()
        {
            return new OrderSorter();
        }
    }
}
