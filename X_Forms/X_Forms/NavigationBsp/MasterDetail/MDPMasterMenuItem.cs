using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Forms.NavigationBsp.MasterDetail
{
    public class MDPMasterMenuItem
    {
        //Diese Model-Klasse wird vom MasterDetailPage-Template automatisch generiert und beinhaltet die Definition eines
        //in der Master-Page angezeigten MenüItems
        public MDPMasterMenuItem()
        {
            TargetType = typeof(MDPMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}