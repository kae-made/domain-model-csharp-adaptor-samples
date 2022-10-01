// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 1.0.0
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
    partial class DomainClassRAStateMachine
    {
        protected void ActionWaitingForRequest()
        {
            // Action Description on Model as a reference.

            //  1 : SELECT ANY requester RELATED BY SELF->RES[R6]->REQ[R8];
            //  2 : IF NOT_EMPTY requester
            //  3 : 	GENERATE RA1:'Request Resource' TO SELF;
            //  4 : END IF;

            // Line : 1
            DomainClassREQ requester = null;
            var targetIn0RL0Set = target.LinkedR6();
            foreach (var targetIn0RL0 in targetIn0RL0Set)
            {
                requester = targetIn0RL0.LinkedR8().FirstOrDefault();
                break;
            }

            // Line : 2
            if (requester != null)
            {
                // Line : 3
                DomainClassRAStateMachine.RA1_RequestResource.Create(receiver:target, sendNow:true);

            }


        }

        protected void ActionWaitingForResource()
        {
            // Action Description on Model as a reference.

            //   1 : SELECT MANY resourceSet RELATED BY SELF->RES[R6];
            //   2 : FOR EACH resource IN resourceSet
            //   3 : 	SELECT ONE usingRequester RELATED BY resource->REQ[R1.'is used by'];
            //   4 : 	IF EMPTY usingRequester
            //   5 : 		SELECT ANY requester RELATED BY resource->REQ[R8];
            //   6 : 		IF NOT_EMPTY requester
            //   7 : 			GENERATE RA2:'Resource Freed' TO SELF;
            //   8 : 			BREAK;
            //   9 : 		END IF;
            //  10 : 	END IF;
            //  11 : END FOR;

            // Line : 1
            var resourceSet = target.LinkedR6() as List<DomainClassRES>;

            // Line : 2
            foreach (var resource in resourceSet)
            {
                // Line : 3
                DomainClassREQ usingRequester = null;
                var resourceIn0RL1 = resource.LinkedR1OneIsUsedBy();
                if (resourceIn0RL1 != null)
                {
                    usingRequester = resourceIn0RL1.LinkedR1OneIsUsedBy();
                }

                // Line : 4
                if (usingRequester == null)
                {
                    // Line : 5
                    var requester = resource.LinkedR8().FirstOrDefault();

                    // Line : 6
                    if (requester != null)
                    {
                        // Line : 7
                        DomainClassRAStateMachine.RA2_ResourceFreed.Create(receiver:target, sendNow:true);

                        // Line : 8
                        break;
                    }

                }

            }


        }

        protected void ActionResourcAssigned()
        {
            // Action Description on Model as a reference.

            //   1 : SELECT MANY resourceSet RELATED BY SELF->RES[R6];
            //   2 : FOR EACH resource IN resourceSet
            //   3 : 	SELECT ONE usingRequester RELATED BY resource->REQ[R1.'is used by'];
            //   4 : 	IF EMPTY usingRequester
            //   5 : 		SELECT ANY requester RELATED BY resource->REQ[R8];
            //   6 : 		IF NOT_EMPTY requester
            //   7 : 			UNRELATE resource FROM requester ACROSS R8;
            //   8 : 			GENERATE P1:'Start Process'( Requester_ID:requester.Requester_ID, Resource_ID:resource.Resource_ID) TO P CREATOR;
            //   9 : 		END IF;
            //  10 : 	END IF;
            //  11 : END FOR;
            //  12 : GENERATE RA3:Assigned TO SELF;

            // Line : 1
            var resourceSet = target.LinkedR6() as List<DomainClassRES>;

            // Line : 2
            foreach (var resource in resourceSet)
            {
                // Line : 3
                DomainClassREQ usingRequester = null;
                var resourceIn0RL1 = resource.LinkedR1OneIsUsedBy();
                if (resourceIn0RL1 != null)
                {
                    usingRequester = resourceIn0RL1.LinkedR1OneIsUsedBy();
                }

                // Line : 4
                if (usingRequester == null)
                {
                    // Line : 5
                    var requester = resource.LinkedR8().FirstOrDefault();

                    // Line : 6
                    if (requester != null)
                    {
                        // Line : 7
                        // Unrelate resource From requester Across R8
                        requester.UnlinkR8IsRequesting(resource, changedStates);

                        // Line : 8
                        DomainClassPStateMachine.P1_StartProcess.Create(receiver:null, Requester_ID:requester.Attr_Requester_ID, Resource_ID:resource.Attr_Resource_ID, sendNow:true, instanceRepository:instanceRepository, logger:logger);

                    }

                }

            }

            // Line : 12
            DomainClassRAStateMachine.RA3_Assigned.Create(receiver:target, sendNow:true);


        }

    }
}
