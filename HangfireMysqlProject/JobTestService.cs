namespace HangfireMysqlProject
{
    public interface IJobTestService
    {
        void FireAndForgetJob();
        void ReccuringJob();
        void DelayedJob();
        void ContinuationJob();
    }

    public class JobTestService : IJobTestService
    {
        public void FireAndForgetJob()
        {
            Console.WriteLine($"{DateTime.Now} Hello from a Fire and Forget job!");
        }

        public void ReccuringJob()
        {
            Console.WriteLine($"{DateTime.Now} Hello from a Scheduled job!");
        }

        public void DelayedJob()
        {
            Console.WriteLine($"{DateTime.Now} Hello from a Delayed job!");
        }

        public void ContinuationJob()
        {
            Console.WriteLine($"{DateTime.Now} Hello from a Continuation job!");
        }

    }
}
