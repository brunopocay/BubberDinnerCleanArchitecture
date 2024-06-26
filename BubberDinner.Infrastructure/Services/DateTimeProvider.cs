using BubberDinner.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberDinner.Infrastructure.Services
{
	public class DateTimeProvider : IDateTimeProvider
	{
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
