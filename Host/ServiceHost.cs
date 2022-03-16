using System;

namespace Host
{
    internal class ServiceHost : IDisposable
    {
        private Type type;

        public ServiceHost(Type type)
        {
            this.type = type;
        }
    }
}