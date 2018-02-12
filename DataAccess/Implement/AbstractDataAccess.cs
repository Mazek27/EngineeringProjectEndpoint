using Engineering_Project.Service.Context;

namespace Engineering_Project.DataAccess
{
    public abstract class AbstractDataAccess
    {
        protected Context _Context { get; set; }

        public AbstractDataAccess(Context context)
        {
            _Context = context;
        }
    }
}