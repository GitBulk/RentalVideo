using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalVideo.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        RentalVideoContext context;
        public RentalVideoContext Init()
        {
            return context ?? (context = new RentalVideoContext());
        }

        protected override void DisposeCore()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
