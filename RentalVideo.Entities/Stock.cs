﻿using System;
using System.Collections.Generic;

namespace RentalVideo.Entities
{
    public class Stock : IEntityBase
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid UniqueKey { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}