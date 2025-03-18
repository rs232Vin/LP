using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _category;
        private decimal? _price;
        public string Name
        {
            get => _name;
            private set => _name = value;
        }
        public string Category
        {
            get => _category;
            private set => _category = value;
        }
        public decimal? Price
        {
            get => _price;
            private set => _price = value;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new Common.Exceptions.ValidationException("Name was not provided.");
            }
            _name = name;
        }
        public void SetCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new Common.Exceptions.ValidationException("Category was not provided.");
            }
            _category = category;
        }

        public void SetPrice(decimal? price)
        {
            _price = price;
        }
    }
}