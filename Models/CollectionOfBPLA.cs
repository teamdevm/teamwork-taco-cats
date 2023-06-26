﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class CollectionOfBPLA
    {
        ObservableCollection<BPLA> CollectionOfBPLA { get; set; } = ObservableCollection<BPLA>();
        public void create()
        {

            CollectionOfBPLA.Add(new BPLA
            {
                ID = "1",
                Name = "Test object №1",
                Description = "Test object №1",
                CollectionOfMove.Add(new Move { "1", new double[] { 58.00711, 56.18835 }, (2023, 6, 20, 18, 30, 25) },
                                     new Move { "1", new double[] { 58.01587, 56.24571 }, (2023, 6, 20, 18, 35, 25) })
            });
            CollectionOfBPLA.Add(new BPLA
            {
                ID = "2",
                Name = "Test object №2",
                Description = "Test object №2",
                CollectionOfMove.Add(new Move { "2", new double[] { 58.05427, 56.41754 }, (2023, 6, 20, 18, 30, 25) },
                                     new Move { "2", new double[] { 58.06807, 56.55899 }, (2023, 6, 20, 18, 35, 46) })
            });
            return CollectionOfBPLA;
        }
    }
}