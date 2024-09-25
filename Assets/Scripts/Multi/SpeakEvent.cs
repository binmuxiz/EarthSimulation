using Fusion;
using Photon.Voice.Fusion;
using UnityEngine;

public class SpeakEvent : NetworkBehaviour
{
    private VoiceNetworkObject voiceObj;
    
   [Networked,OnChangedRender(nameof(SpeakingEvent))] public bool IsSpeaking { get; set; } = false;
   

   public void SpeakingEvent()
   {
      
   }

   public override void Spawned()
   {
       
           
   }

   public override void FixedUpdateNetwork()
   {
       IsSpeaking = voiceObj.IsSpeaking;
   }
}
