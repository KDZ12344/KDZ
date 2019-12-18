namespace Study.Core
{
    public class Factory
    {
        // Singleton
        private static Factory instance;

        private Factory() { }
        public static Factory Instance
        {
            get
            {
                // Lazy initialization; initialization on demand
                if (instance == null)
                    instance = new Factory();
                return instance;
            }
        }

        private Repository repository;
        public Repository GetRepository()
        {
            return repository ?? (repository = new Repository());
        }
    }
}

