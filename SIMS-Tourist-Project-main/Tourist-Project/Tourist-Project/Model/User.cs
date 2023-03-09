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
        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), password, role.ToString()};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            password = values[1];
            role = Convert.ToInt32(values[2]);
        }
    }
}