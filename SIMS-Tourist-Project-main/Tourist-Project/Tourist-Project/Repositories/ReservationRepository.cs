using System;
using System.Collections.Generic;
using System.Linq;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private const string FilePath = "../../../Data/reservations.csv";
        private readonly Serializer<Reservation> serializer;
        private List<Reservation> reservations;
        public ReservationRepository()
        {
            serializer = new Serializer<Reservation>();
            reservations = serializer.FromCSV(FilePath);
        }
        public List<Reservation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.Id = NextId();
            reservations = serializer.FromCSV(FilePath);
            reservations.Add(reservation);
            serializer.ToCSV(FilePath, reservations);
            return reservation;
        }

        public int NextId()
        {
            reservations = serializer.FromCSV(FilePath);
            if (reservations.Count < 1)
            {
                return 1;
            }
            return reservations.Max(c => c.Id) + 1;
        }

        public void Delete(int id)
        {
            reservations = serializer.FromCSV(FilePath);
            var founded = reservations.Find(c => c.Id == id);
            reservations.Remove(founded);
            serializer.ToCSV(FilePath, reservations);
        }

        public Reservation Update(Reservation reservation)
        {
            reservations = serializer.FromCSV(FilePath);
            var current = reservations.Find(c => c.Id == reservation.Id);
            var index = reservations.IndexOf(current);
            reservations.Remove(current);
            reservations.Insert(index, reservation);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, reservations);
            return reservation;
        }

        public Reservation GetById(int id)
        {
            return reservations.Find(c => c.Id == id);
        }

        public Reservation GenerateAvailableDates(Reservation reservation)
        {
            Date date = new Date(DateTime.Now, true);
            reservation.AvailableDates.Add(date);
           for(int i = 1; i < 10; i++)
            {
                Date addedDate = new Date(DateTime.FromOADate(i), true);
                reservation.AvailableDates.Add(addedDate); 
            }

            return reservation;
        }
    }
}