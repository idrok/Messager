namespace Bnyx.AI
{
    public class AutoID
    {
        private static bool mInit;

        private static AutoID mAutoID;

        private static int mIds;

        private AutoID () { }

        public static AutoID Instance() {
            if (mAutoID == null) {
                mAutoID = new AutoID();
                mInit = true;
            }
            return mAutoID;
        }

        public static int BuildID () 
        {
            return (mIds ++) * 10000;
        }

        public static void Reset() 
        {
            mIds = 0;
        }

    }
}