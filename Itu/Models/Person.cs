using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Itu.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public List<Item> Items { get; set; }
    }
}