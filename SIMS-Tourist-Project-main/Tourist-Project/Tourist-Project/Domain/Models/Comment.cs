using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Comment : INotifyPropertyChanged, ISerializable
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                if(value == id) return;
                id = value;
                OnPropertyChanged();
            }
        }

        private string commentText;

        private string CommentText
        {
            get => commentText;
            set
            {
                if(value == commentText) return;
                commentText = value;
                OnPropertyChanged();
            }
        }

        private int reportNo;
        public int ReportNo
        {
            get => reportNo;
            set
            {
                if(reportNo == value) return;
                reportNo = value;
                OnPropertyChanged();
            }
        }
        public Comment() { }

        public Comment(string commentText, int reportNo)
        {
            CommentText = commentText;
            ReportNo = reportNo;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                CommentText,
                ReportNo.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            CommentText = values[1];
            ReportNo = Convert.ToInt32(values[2]);
        }
    }

}