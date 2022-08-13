// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 0.1.0
//  
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Kae.StateMachine;
using Kae.DomainModel.Csharp.Framework;

namespace ProcessManagement
{
    partial class DomainClassPSStateMachine
    {
        protected void ActionReady()
        {
            // Action Description on Model as a reference.



        }

        protected void ActionWorking()
        {
            // Action Description on Model as a reference.

            //  1 : SELECT ONE orderSpec RELATED BY SELF->OS[R4];
            //  2 : SELF.ExecuteCommand( command:orderSpec.Command );

            // Line : 1
            var orderSpec = target.LinkedR4();

            // Line : 2
            target.ExecuteCommand(command:orderSpec.Attr_Command);

        }

        protected void ActionWaitForNextPreparetion()
        {
            // Action Description on Model as a reference.

            //  1 : SELF.Finished = true;
            //  2 : SELECT ONE process RELATED BY SELF->P[R2];
            //  3 : SELECT ONE iWork RELATED BY SELF->IW[R5.'successor'];
            //  4 : IF NOT_EMPTY iWork
            //  5 : 	GENERATE IW1:Start TO iWork;
            //  6 : END IF;

            // Line : 1
            target.Attr_Finished = true;
            // Line : 2
            var process = target.LinkedR2();

            // Line : 3
            var iWork = target.LinkedR5OtherPredecessor();

            // Line : 4
            if (iWork != null)
            {
                // Line : 5
                DomainClassIWStateMachine.IW1_Start.Create(receiver:iWork, sendNow:true);

            }


        }

        protected void ActionDone()
        {
            // Action Description on Model as a reference.

            //  1 : SELECT ONE process RELATED BY SELF->P[R2];
            //  2 : UNRELATE SELF FROM process ACROSS R7;
            //  3 : SELECT ONE nextStep RELATED BY SELF->PS[R5.'successor'];
            //  4 : IF NOT_EMPTY nextStep
            //  5 : 	RELATE nextStep TO process ACROSS R7;
            //  6 : ELSE
            //  7 : 	GENERATE P3:'Done All Steps' TO process;
            //  8 : END IF;

            // Line : 1
            var process = target.LinkedR2();

            // Line : 2
            // Unrelate SELF From process Across R7
            process.UnlinkR7CurrentStep(target, changedStates);;

            // Line : 3
            DomainClassPS nextStep = null;
            var targetIn0RL1 = target.LinkedR5OtherPredecessor();
            if (targetIn0RL1 != null)
            {
                nextStep = targetIn0RL1.LinkedR5OtherPredecessor();
            }

            // Line : 4
            if (nextStep != null)
            {
                // Line : 5
                // nextStep - R7 -> process;
                process.LinkR7CurrentStep(nextStep, changedStates);;

            }
            else
            {
                // Line : 7
                DomainClassPStateMachine.P3_DoneAllSteps.Create(receiver:process, sendNow:true);

            }


        }

    }
}
