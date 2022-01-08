using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Mobil.Models
{
    public class ListMovie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(RentalList))]
        public int RentalListID { get; set; }
        public int MovieID { get; set; }
    }
}
