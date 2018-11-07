using System;

namespace Discord.CoD.Blops.Models.Database
{
    public abstract class DbConnection : IDisposable
    {
        public abstract void Dispose();
    }
}
