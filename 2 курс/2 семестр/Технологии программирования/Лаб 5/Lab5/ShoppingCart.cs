using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class ShoppingCart
    {
        public List<CartItem> items { get; } = new List<CartItem>();

        public void addItem(string name, decimal price, int quantity)
        {
            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше нуля",
                    nameof(price));

            if (quantity <= 0)
                throw new ArgumentException("Кол-во должно быть больше нуля",
                    nameof(quantity));

            CartItem existingItem = items.FirstOrDefault(item => item.name == name);

            if (existingItem != null)
            {
                if(existingItem.price != price)
                    throw new InvalidOperationException($"Позиция '{name}' уже есть в корзине с другой ценой.");

                int index=items.IndexOf(existingItem);

                items[index] = existingItem with { quantity = existingItem.quantity + quantity };
            }
            else
            {
                items.Add(new CartItem(name, price, quantity));
            }
        }

        public decimal getTotal()
        {
            decimal total = items.Sum(item => item.price * item.quantity);

            int uniqueItemCount = items
                .Select(item => item.name)
                .Distinct()
                .Count();

            if (uniqueItemCount > 3) total *= 0.9m;

            return total;
        }

        public void removeItem(string name)
        {
            CartItem item = items.FirstOrDefault(item => item.name == name);

            if(item==null)
                throw new InvalidOperationException($"Позиция '{name}' не найдена в корзине.");

            items.Remove(item);
        }
    }

    public record CartItem(string name, decimal price, int quantity);
}
