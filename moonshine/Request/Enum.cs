using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moonshine.EnumAll
{
    public enum FlowStatus
    {
        // below are build-in / predefined status for all Jabil WUX. 
        // Do NOT change it.
        Created = 0,
        Returned = 555,
        Closed = 999,
        Rejected = 444,
        Canceled = 777,
        PendingForMs = 900,//moonshine supervistor
        PendingForCost = 901,//costCenter W/C Manager
        PendingForIE = 902,//IE Function Manager

        // toDo:  add the status here base on the state diagram, 10~49

    }

    public enum ActionType
    {
        // Below are pre-defined action. Not Allow to change.
        Create = 0,
        Approve = 1,
        Reject = 2,
        Submit = 3,
        Return = 4,
        NoResponse = 5,
        Cancel = 6,

        // toDo: add actions here, 10~49
        Commit = 11,
        Forward = 20


    }

    public enum RoleType
    {

        Requestor = 10,
        // toDo: add role here, 11~49

        MS = 12,
        Cost = 13,
        IE = 14,
        Admin = 99
    }

}//namespace