﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PassOne.Presentation
{
    public class Details : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        private string _title;
        private string _url;
        private string _username;
        private string _password;
        private string _email;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged("URL");
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Method to clear all details properties
        /// </summary>
        public void Clear()
        {
            Id = 0;
            UserId = 0;
            Title = string.Empty;
            Url = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Details(PropertyChangedEventHandler handler)
        {
            PropertyChanged = handler;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
