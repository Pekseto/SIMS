using System;

namespace Tourist_Project.Model
{
	public class User
	{
		int Id { get; set; }
		string password { get; set; }
		int role { get; set; }

        public User(int id, string password, int role)
        {
            Id = id;
            this.password = password;
            this.role = role;
        }
    }
}