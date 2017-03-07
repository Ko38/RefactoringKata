using System.Text;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private readonly Orders _orders;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            var sb = new StringBuilder("{\"orders\": [");
            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                GetOrder(i, sb);
            }
            if (_orders.GetOrdersCount() > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.Append("]}").ToString();
        }

        private void GetOrder(int i, StringBuilder sb)
        {
            var order = _orders.GetOrder(i);
            sb.AppendFormat("{0}\"id\": {1}, \"products\": [", "{", order.GetOrderId());
            for (var j = 0; j < order.GetProductsCount(); j++)
            {
                GetProduct(sb, order, j);
            }
            if (order.GetProductsCount() > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            sb.AppendFormat("]{0}, ", "}");
        }

        private static void GetProduct(StringBuilder sb, Order order, int j)
        {
            var product = order.GetProduct(j);
            sb.AppendFormat("{0}\"code\": \"{1}\", \"color\": \"{2}\", ", "{", product.Code, GetColorFor(product));
            if (product.Size != Product.SIZE_NOT_APPLICABLE)
            {
                sb.AppendFormat("\"size\": \"{0}\", ", GetSizeFor(product));
            }
            sb.AppendFormat("\"price\": {0}, \"currency\": \"{1}\"{2}, ", product.Price, product.Currency, "}");
        }


        private static string GetSizeFor(Product product)
        {
            var sizes = new[] {"Invalid Size", "XS", "S", "M", "L", "XL", "XXL"};
            try
            {
                return sizes[product.Size];
            }
            catch
            {
                return sizes[0];
            }
        }

        private static string GetColorFor(Product product)
        {
            var colors = new[] {"no color", "blue", "red", "yellow"};
            try
            {
                return colors[product.Color];
            }
            catch
            {
                return colors[0];
            }
        }
    }
}