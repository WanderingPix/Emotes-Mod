using EmotesMod.Modules.Components;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Extensions;

namespace EmotesMod;

public static class Networking
{
    [MethodRpc((uint)RpcCalls.PlayEmote)]
    public static void RpcPlayEmote(this PlayerControl p, string emoteName)
    {
        var emote = Assets.Bundle.LoadAsset<Emote>(emoteName);
        var emoteBehaviour = p.GetComponent<EmoteBehaviour>();
        emoteBehaviour.StopEmote();
        emoteBehaviour.currentEmote = emote;
        emoteBehaviour.PlayEmote();
    }

    [MethodRpc((uint)RpcCalls.StopEmote)]
    public static void RpcStopEmote(this PlayerControl p)
    {
        var emoteBehaviour = p.GetComponent<EmoteBehaviour>();
        emoteBehaviour.StopEmote();
    }
}
public enum RpcCalls
{
    PlayEmote,
    StopEmote
}