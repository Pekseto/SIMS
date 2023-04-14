using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum UserRole
    {
        owner, guide, guest
    }
    public class User : ISerializable
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private UserRole role;
        public UserRole Role
        {
            get { return role; }
            set { role = value; }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private DateTime birthDate;
        public DateTime BirtDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        private bool isSuper;

        public bool IsSuper
        {
            get { return isSuper; }
            set { isSuper = value; }
        }

        public User() { }
        public User(int id, string username, string password, UserRole role, string fullName, DateTime birthDate, bool isSuper)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.role = role;
            this.fullName = fullName;
            this.birthDate = birthDate;
            this.isSuper = isSuper;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { id.ToString(), username, password, role.ToString(), fullName, birthDate.ToString(), isSuper.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            username = values[1];
            password = values[2];
            role = Enum.Parse<UserRole>(values[3]);
            fullName = values[4];
            birthDate = DateTime.Parse(values[5]);
            isSuper = bool.Parse(values[6]);
        }
    }
}