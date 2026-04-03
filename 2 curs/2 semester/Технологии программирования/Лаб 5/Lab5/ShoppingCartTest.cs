using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    [TestFixture]
    internal class ShoppingCartTest
    {
        [Test]
        public void addItemSuccess()
        {
            var cart = new ShoppingCart();

            cart.addItem("Book", 10.50m, 1);

            Assert.True(cart.items.Count == 1);
        }

        [Test]
        public void addItemPriceValidating()
        {
            var cart = new ShoppingCart();

            Assert.Throws<ArgumentException>(() => cart.addItem("Pen", 0, 1));
            Assert.Throws<ArgumentException>(() => cart.addItem("Pen", -5, 1));
        }


        [Test]
        public void addItemQuantityValidating()
        {
            var cart = new ShoppingCart();

            Assert.Throws<ArgumentException>(() => cart.addItem("Pen", 1, 0));
            Assert.Throws<ArgumentException>(() => cart.addItem("Pen", 1, -5));
        }

        [Test]
        public void getTotalSum()
        {
            var cart = new ShoppingCart();
            cart.addItem("Apple", 5.50m, 3); //16.50
            cart.addItem("Banan", 6, 2); //12.00

            decimal total = cart.getTotal();

            Assert.True(total == 28.50m);
        }

        [Test]
        [Description("В корзину добавляется 4 уникальных позиции." +
            "Должна примениться скидка к итоговой сумме")]
        public void getDiscountSuccess()
        {
            var cart = new ShoppingCart();
            cart.addItem("A", 10, 1); // 10
            cart.addItem("B", 10, 1); // 10
            cart.addItem("C", 10, 1); // 10
            cart.addItem("D", 10, 1); // 10

            decimal total = cart.getTotal();

            // total = total-discount(10%)
            Assert.True(total == 40m - (40m * 0.1m));
        }

        [Test]
        [Description("В корзину добавляется 2 уникальных позиции." +
            "Скидка к итогой сумме не применяется")]
        public void getDiscountInsuccess()
        {
            var cart = new ShoppingCart();
            cart.addItem("A", 10, 2); // 20
            cart.addItem("B", 10, 1); // 10

            decimal total = cart.getTotal();

            Assert.True(total == 30m);
        }

        [Test]
        [Description("Объединение кол-ва при повторном добавлении позиции")]
        public void addDuplicateItemValidating()
        {
            var cart = new ShoppingCart();
            cart.addItem("Apple", 1.0m, 2);
            cart.addItem("Apple", 1.0m, 3);

            Assert.True(cart.items.Count == 1);
            Assert.True(cart.items[0].quantity == 5);
            Assert.True(cart.items[0].price == 1.0m);
        }

        [Test]
        [Description("Ошибка при повторном добавлении позиции с другой ценой")]
        public void addDuplicateItemWithDifferentPrice()
        {
            var cart = new ShoppingCart();
            cart.addItem("Apple", 1.0m, 1);

            Assert.Throws<InvalidOperationException>(() => cart.addItem("Apple", 1.5m, 1));
        }

        [Test]
        public void removeItem()
        {
            var cart = new ShoppingCart();
            cart.addItem("Apple", 1.0m, 2);
            cart.addItem("Banan", 1.5m, 1);

            cart.removeItem("Apple");

            Assert.True(cart.items.Count == 1);
            Assert.True(cart.items[0].name == "Banan");
        }

        [Test]
        public void getTotalAfterRemoveItem()
        {
            var cart = new ShoppingCart();
            cart.addItem("A", 10, 1);
            cart.addItem("B", 10, 1);
            cart.addItem("C", 10, 1);
            cart.addItem("D", 10, 1);

            Assert.True(cart.getTotal()==36m); // со скидкой

            cart.removeItem("D");

            Assert.True(cart.getTotal()==30m); // без скидки
        }
    }
}
