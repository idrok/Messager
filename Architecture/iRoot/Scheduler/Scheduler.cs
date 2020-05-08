namespace AI.Architecture.iRoot
{
    public static class Scheduler
    {
        public static readonly GameLoopScheduler GameLoop = new GameLoopScheduler();
        public static readonly ThreadPoolScheduler ThreadPool = new ThreadPoolScheduler();
        public static readonly ImmediateScheduler Immediate = new ImmediateScheduler();
        
    }
}