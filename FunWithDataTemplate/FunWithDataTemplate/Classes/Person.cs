using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithDataTemplate
{
	class Person
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public TemplateType TemplateType { get; set; }
	}

	enum TemplateType
	{
		Strict, Bright
	}
}
