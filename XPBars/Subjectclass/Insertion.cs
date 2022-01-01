﻿using System;

namespace XPBars
{
    public class Insertion : BaseViewModel
    {
        #region Members

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private int value;

        public int Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged(); }
        }

        private XPWeight weight;

        public XPWeight Weight
        {
            get { return weight; }
            set { weight = value; OnPropertyChanged(); }
        }

        private DateTime date = DateTime.UtcNow;

        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }

        public bool IgnoreWeight { get; set; }

        #endregion

        public Insertion(string description, int value, XPWeight weight)
        {
            Description = description;
            Value = value;
            Weight = weight;
        }
        public Insertion(string description, int value, XPWeight weight, DateTime date) : this(description, value, weight)
        {
            Date = date;
        }
        public Insertion(string description, int value, bool ignoreWeight)
        {
            Description = description;
            Value = value;
            IgnoreWeight = ignoreWeight;
        }
        public Insertion(string description, int value, XPWeight weight, bool ignoreWeight, DateTime date) : this(description, value, weight, date)
        {
            IgnoreWeight = ignoreWeight;
        }

        public override string ToString()
        {
            return $"{Description} | {Value} {Weight}";
        }
    }
}
