using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public interface IDebuffEffect 
{
    void ApplyDebuffEffect(List<DebuffEntry> debuffs);
}
