using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUser.Views.Flyout
{

    public class MasterDetailPageUserFlyoutMenuItem
    {
        public MasterDetailPageUserFlyoutMenuItem()
        {
            TargetType = typeof(MasterDetailPageUserFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}