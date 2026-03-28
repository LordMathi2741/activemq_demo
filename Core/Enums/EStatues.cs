using System.ComponentModel;

namespace Core.Enums;

public enum EStatues
{
    [Description("Deliver success!")]
    ONSUCCESS = 1,
    [Description("Deliver failed!")]
    ONFAILURE = 2,
    [Description("Connection failed!")]
    CONNECTIONFAILED = 3,
}