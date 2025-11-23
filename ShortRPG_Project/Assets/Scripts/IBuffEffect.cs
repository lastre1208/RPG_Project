using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public interface IBuffEffect 
{

    void ApplyBuffEffect(List<BuffEntry> buffs);
}
