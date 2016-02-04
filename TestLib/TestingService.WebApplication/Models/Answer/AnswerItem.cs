using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingService.WebApplication.Models;

namespace TestingService.WebApplication.Models.Answer
{
    public class AnswerItem
    {
        public DisplayType ItemType { get; set; }
        public string Value {
            get
            {
                return _value;
            }
            set
            {
                try
                {
                    _boolValue = bool.Parse(value);
                }
                catch (FormatException)
                {

                }
                _value = value;
            }
        }
        public bool BoolValue {
            get { return _boolValue; }
            set
            {
                _boolValue = value;
                _value = value.ToString();
            }
        }

        private string _value;
        private bool _boolValue;
    }
}