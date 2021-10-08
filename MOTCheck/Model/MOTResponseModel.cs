using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTCheck.Model
{
    public class MOTResponseModel
    {
        public string registration { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string primaryColour { get; set; }
        public List<MotTest> motTests { get; set; }
    }

    public class MotTest
    {
        public string completedDate { get; set; }
        public string testResult { get; set; }
        public string expiryDate { get; set; }
        public string odometerValue { get; set; }
        public string odometerUnit { get; set; }
        public string motTestNumber { get; set; }
    }    
}
