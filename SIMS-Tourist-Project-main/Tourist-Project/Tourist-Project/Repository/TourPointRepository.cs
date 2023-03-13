﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tourist_Project.Model;
using Tourist_Project.Serializer;

namespace Tourist_Project.Repository
{
    public class TourPointRepository
    {
        private const string filePath = "../../.../Data/tourPoints.csv";
        private readonly Serializer<TourPoint> serializer;
        private List<TourPoint> tourPoints;

        public TourPointRepository()
        {
            serializer = new Serializer<TourPoint>();
            tourPoints = serializer.FromCSV(filePath);
        }

        public List<TourPoint> GetAll()
        {
            return serializer.FromCSV(filePath);
        }

        public void Save(TourPoint tourPoint)
        {
            tourPoint.Id = NextId();
            tourPoints = serializer.FromCSV(filePath);
            tourPoints.Add(tourPoint);
            serializer.ToCSV(filePath, tourPoints);
        }

        public int NextId()
        {
            tourPoints = serializer.FromCSV(filePath);
            if (tourPoints.Count < 1)
            {
                return 1;
            }
            return tourPoints.Max(c => c.Id) + 1;
        }
    }
}
