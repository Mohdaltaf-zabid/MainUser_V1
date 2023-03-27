using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUser.Views.FlyoutCaretaker
{

    public class MasterDetailPageCaretakerFlyoutMenuItem
    {
        public MasterDetailPageCaretakerFlyoutMenuItem()
        {
            TargetType = typeof(MasterDetailPageCaretakerFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}