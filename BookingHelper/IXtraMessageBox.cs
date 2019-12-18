using static BookingHelper.HousekeeperService;

namespace BookingHelper
{
    public  interface IXtraMessageBox
    {
        void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }
}