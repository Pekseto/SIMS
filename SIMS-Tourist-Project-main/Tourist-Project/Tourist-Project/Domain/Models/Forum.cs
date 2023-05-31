using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Forum : INotifyPropertyChanged, ISerializable
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                if (value == id) return;
                id = value;
                OnPropertyChanged();
            }
        }

        private int locationId;
        public int LocationId
        {
            get => locationId;
            set
            {
                if (value == locationId) return;
                locationId = value;
                OnPropertyChanged();
            }
        }

        private string commentsIdsCsv;

        public string CommentsIdsCsv
        {
            get => commentsIdsCsv;
            set
            {
                if(value == commentsIdsCsv) return;
                commentsIdsCsv = value;
                OnPropertyChanged();
            }
        }

        public List<int> CommentsIds { get; set; } = new();

        public Forum()
        {
        }

        public Forum(int locationId, string commentsIdsCsv)
        {
            LocationId = locationId;
            CommentsIdsCsv = commentsIdsCsv;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            CommentIdesToCsv();
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString(),
                CommentsIdsCsv
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            CommentsIdsCsv = values[2];
            CommentsIdsFromCsv(CommentsIdsCsv);
        }

        public void CommentIdesToCsv()
        {
            if (CommentsIds.Count <= 0) return;
            CommentsIdsCsv = string.Empty;
            foreach (var imageIde in CommentsIds)
            {
                CommentsIdsCsv += imageIde + ",";
            }
            CommentsIdsCsv = CommentsIdsCsv.Remove(CommentsIdsCsv.Length - 1);
        }

        public void CommentsIdsFromCsv(string value)
        {
            var commentIdesCsv = value.Split(",");
            foreach (var commentIde in commentIdesCsv)
            {
                if (commentIde != string.Empty)
                    CommentsIds.Add(int.Parse(commentIde));
            }
        }
    }

}