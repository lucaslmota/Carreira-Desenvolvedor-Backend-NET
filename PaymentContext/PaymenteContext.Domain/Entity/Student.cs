using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymenteContext.Domain.Entity
{
    public class Student
    {
        private IList<Subscription> _subscriptions;
        public Student(string firstName, string lastName, string document, string email, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            Address = address;
            _subscriptions = new List<Subscription>();

            if (firstName.Length == 0)
                throw new Exception("Nome inválido");
        }

        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Document { get; private set; }
        public string? Email { get; private set; }
        public string? Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            foreach (var item in Subscriptions)
            {
                item.Inactivate();
            }

            _subscriptions.Add(subscription);
        }
    }
}