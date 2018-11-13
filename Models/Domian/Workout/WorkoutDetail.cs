namespace Engineering_Project.Models.Domian.Workout
{
    public class WorkoutDetail
    {
        public WorkoutDetail(double distance, double duration)
        {
            Distance = distance;
            Duration = duration;
        }

        public double Distance { get; private set; }
        public double Duration { get; private set; }
    }
}