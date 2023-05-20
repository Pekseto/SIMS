using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Guest : INotifyPropertyChanged, ISerializable
    {
        #region Guest properties

        private int _guestId;
        public int GuestId
        {
            get { return _guestId; }
            set
            {
                if (value == _guestId) return;
                _guestId = value;
                OnPropertyChanged("GuestId");
            }
        }
            
        private String _guestName;
        public String GuestName
        {
            get { return _guestName; }
            set
            {
                if(value == _guestName) return;
                _guestName = value;
                OnPropertyChanged("GuestName");
            }
        }

        private bool _isSuper;
        public bool IsSuper
        {
            get { return _isSuper; }
            set
            {
                if(value == _isSuper) return;
                _isSuper = value;
                OnPropertyChanged("IsSuper");
            }
        }
       
        private DateTime _superGuestBeginnig;
        public DateTime SuperGuestBeginnig
        {
            get { return _superGuestBeginnig; }
            set
            {
                if(value == _superGuestBeginnig) return;
                _superGuestBeginnig = value;
                OnPropertyChanged("SuperGuestBeginnig");
            }
        }

        private DateTime _superGuestEnding;
        public DateTime SuperGuestEnding
        {
            get { return _superGuestEnding; }
            set
            {
                if(value ==_superGuestEnding) return;
                _superGuestEnding = value;
                OnPropertyChanged("SuperGuestEnding");
            }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set
            {
                if(value == _points) return;
                _points = value;
                OnPropertyChanged("Points");
            }
        }

        #endregion

        public Guest()
        {

        }
        public Guest(User user)
        {
            GuestId = user.Id;
            GuestName = user.Username;
            
        }

        #region Serialization
        public string[] ToCSV()
        {
            
            string[] csvValues = {
                GuestId.ToString(),
                GuestName,
                IsSuper.ToString(),
                SuperGuestBeginnig.ToString(), 
                SuperGuestEnding.ToString(),
                Points.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            GuestName = values[1];
            IsSuper = Convert.ToBoolean(values[2]);
            SuperGuestBeginnig = Convert.ToDateTime(values[3]);
            SuperGuestEnding = Convert.ToDateTime(values[4]);
            Points = Convert.ToInt32(values[5]);
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
