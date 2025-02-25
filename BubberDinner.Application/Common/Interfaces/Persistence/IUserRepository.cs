﻿using BubberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberDinner.Application.Common.Interfaces.Persistence
{
	public interface IUserRepository
	{
		User? GetUserByEmail(string email);
		void Add(User user);
	}
}
