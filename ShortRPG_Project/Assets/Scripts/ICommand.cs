using UnityEngine;
using System.Collections.Generic;

 public class ActionContext
    {
       public CommonStatus user;
       public CommonStatus target;
       public SkillData skill;
    }
public interface ICommand 
{
  
    public void Execute(ActionContext action)
    {
       
    }
}
