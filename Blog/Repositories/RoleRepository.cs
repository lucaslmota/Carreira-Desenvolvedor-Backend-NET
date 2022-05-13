using System.Collections.Generic;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class RoleRepository
    {
        private readonly SqlConnection _connetion;

        public RoleRepository(SqlConnection connection)
        {
            _connetion = connection;
        }

        public IEnumerable<Role> GetAll()
        {
            return _connetion.GetAll<Role>();
        }

        public Role Get(int id)
        {
            return _connetion.Get<Role>(id);
        }

        public void Create(Role role)
        {
            role.Id = 0;
            _connetion.Insert<Role>(role);
        }

        public void Update(Role role)
        {
            if (role.Id != 0)
            {
                _connetion.Update<Role>(role);
            }
        }

        public void Delete(Role role)
        {
            if (role.Id != 0)
            {
                _connetion.Delete<Role>(role);
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
                return;

            var role = _connetion.Get<Role>(id);
            _connetion.Delete<Role>(role);

        }
    }
}