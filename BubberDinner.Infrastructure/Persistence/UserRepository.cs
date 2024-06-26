using BubberDinner.Application.Common.Interfaces.Persistence;
using BubberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberDinner.Infrastructure.Persistence
{
	public class UserRepository : IUserRepository
	{
		private static readonly List<User> users = new();
		void IUserRepository.Add(User user)
		{
			users.Add(user);
		}

		User? IUserRepository.GetUserByEmail(string email)
		{
			return users.SingleOrDefault(u => u.Email == email);
		}
	}
}
