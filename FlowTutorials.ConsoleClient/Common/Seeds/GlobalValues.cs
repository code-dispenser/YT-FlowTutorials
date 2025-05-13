using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowTutorials.ConsoleClient.Common.Seeds;

internal class GlobalValues
{
    public const string Console_Full_Screen_Text           = "Open full screen to see all the code and print outs, for some examples and/or set breakpoints in the example files for others . . .";
    public const string Console_Start_Instruction_Text     = "Enter the number of the example you would like to run or X to exit and press enter . . .";
    public const string console_Next_Instruction_Text      = "Press X to quit, R to repeat or any other key for the example menu and press enter . . .";

    public const string Console_Number_Rule_Text           = "Please enter a valid number between 1 and ";
    public const string Unable_To_Locate_Or_Read_File_Text = "Could not find or read the file:";


    public const string Json_Registration_File_SubPath     = @"Common\Data\RegistrationData.json";
    public const string Json_Registration_Bad_File_SubPath = @"Common\Data\RegistrationDataBad.json";

}
