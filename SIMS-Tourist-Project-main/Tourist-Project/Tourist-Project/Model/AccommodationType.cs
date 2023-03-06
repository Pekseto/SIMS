using System;

namespace Tourist_Project.Model
{
	public class AccommodationType
	{
		int Id { get; set; }
		string Name { get; set; }

        public AccommodationType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}