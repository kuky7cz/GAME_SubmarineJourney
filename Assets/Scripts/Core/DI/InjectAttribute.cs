using System;

namespace SubmarineJourney.Core.DI {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute { }
}
