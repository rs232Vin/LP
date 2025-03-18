using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class User : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private int _age;
        private string _email;
        private decimal? _monthlySalary;
        private string _name;
        private UserTypes _type = UserTypes.Employee;

        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public UserTypes Type
        {
            get => _type;
            private set => _type = value;
        }

        public decimal? MonthlySalary
        {
            get => _monthlySalary;
            private set => _monthlySalary = value;
        }

        public int Age
        {
            get => _age;
            private set => _age = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Common.Exceptions.ValidationException("Name was not provided.");
            }
            _name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Common.Exceptions.ValidationException("Email was not provided.");
            }
            _email = email;
        }

        public void SetType(UserTypes type)
        {
            _type = type;
        }

        public void SetAge(int age)
        {
            _email = _name;
        }

        public void SetMonthlySalary(decimal? annualSalary)
        {
            if(!annualSalary.HasValue)
            {
                //throw new ArgumentNullException("Annual Salary not provided.");
                throw new Common.Exceptions.ValidationException("Annual Salary not provided.");
            }
            _monthlySalary = annualSalary.Value/12;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}