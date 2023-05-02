using System;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public enum UserRole { owner, guide, guest1, guest2 }
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsSuper { get; set; }
        public User() { }
        public User(int id, string username, string password, UserRole role, string fullName, DateTime birthDate, bool isSuper)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            FullName = fullName;
            BirthDate = birthDate;
            IsSuper = isSuper;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), FullName, BirthDate.ToString(), IsSuper.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = Enum.Parse<UserRole>(values[3]);
            FullName = values[4];
            BirthDate = DateTime.Parse(values[5]);
            IsSuper = bool.Parse(values[6]);
        }
    }
}