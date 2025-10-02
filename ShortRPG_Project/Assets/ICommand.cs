using UnityEngine;
using System.Collections.Generic;
public interface ICommand 
{
    
    public void Execute( CharacterStatus commander, CharacterStatus targets );
}
