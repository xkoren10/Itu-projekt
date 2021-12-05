using System;

/*
Model položky. Autor: Marek Križan
*/

namespace Itu.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Price { get; set; }

        public double Ammount { get; set; }
    }
}