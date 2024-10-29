using BlazingBooks.Shared.Interfaces;

namespace BlazingBooks.Mobile.Services
{
    public class CommonService : ICommonService
    {
        public bool IsWeb => false;

        public bool IsMobile => true;
    }
}
