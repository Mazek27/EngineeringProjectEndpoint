using Engineering_Project.Service.Context;

namespace Engineering_Project.DataAccess
{
    public abstract class AbstractDataAccess
    {
        protected ApplicationContext ApplicationContext { get; set; }

        public AbstractDataAccess(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }
    }
}