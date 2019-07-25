namespace ShutdownApi.Data
{
    public class Actions
    {
        public int Lock { get; set; }
        public int Signout { get; set; }
        public int Restart { get; set; }
        public int Shutdown { get; set; }

        public Actions(int _lock, int _signout, int _restart, int _shutdown)
        {
            this.Lock = _lock;
            this.Signout = _signout;
            this.Restart = _restart;
            this.Shutdown = _shutdown;

        }
    }

}