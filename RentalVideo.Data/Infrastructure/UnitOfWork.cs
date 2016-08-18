using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalVideo.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private RentalVideoContext context;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public RentalVideoContext DbContext
        {
            get
            {
                return this.context ?? (this.context = this.dbFactory.Init());
            }
        }

        public void Commit()
        {
            this.DbContext.Commit();
        }
    }
}
