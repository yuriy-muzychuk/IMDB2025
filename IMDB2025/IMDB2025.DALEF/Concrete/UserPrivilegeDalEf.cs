using AutoMapper;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DALEF.Models;
using IMDB2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB2025.DALEF.Concrete
{
    public class UserPrivilegeDalEf : IUserPrivilegeDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public UserPrivilegeDalEf(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }

        public void AddPrivilegeToUser(int userId, PrivilegeType privilegeType)
        {
            using (var context = new ImdbContext(_connStr))
            {
                var privilege = context.Privileges
                    .SingleOrDefault(p => p.Name == privilegeType.ToString());

                if (privilege == null)
                {
                    throw new Exception($"Privilege '{privilegeType}' not found.");
                }

                var userPrivilege = new UserPrivilege
                {
                    UserId = userId,
                    PrivilegeId = privilege.PrivilegeId
                };

                context.UserPrivileges.Add(userPrivilege);
                context.SaveChanges();

                if (userPrivilege.RowInsertTime == default)
                {
                    throw new Exception("Failed to add privilege to user.");
                }
            }
        }
    }
}
