using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class SignalProcessing : MonoBehaviour
{
    VisualEffect vfx;
    private const string signalChannelsBufferName = "SignalChannels";
    private static readonly int screenForwardDirId = Shader.PropertyToID("ScreenForwardDir");
    private GraphicsBuffer signalChannelsGBuffer;
    [VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    public struct SignalChannelVfx
    {
        public Vector3 position;
        public Vector3 endPosition;
    }
    public struct SignalChannel
    {
        public Transform start;
        public Transform end;
    }
    public List<SignalChannelVfx> signalChannelVfxBuffer = new List<SignalChannelVfx>();
    public List<SignalChannel> signalChannels = new List<SignalChannel>();
    List<SignalChannel> originalSignalChannels = new List<SignalChannel>();
    public Transform signalChannelsParent;
    int signalChannelCount;
    private void Start()
    {
        var vfxObject = GameObject.FindGameObjectWithTag("MinigameVfx");
        vfx = vfxObject.GetComponent<VisualEffect>();

        Release();

        for (int i = 0; i < signalChannelsParent.childCount; i++)
        {
            var channelTransform = signalChannelsParent.GetChild(i);
            var endTransform = channelTransform.GetChild(0);
            signalChannels.Add(new SignalChannel
            {
                start = channelTransform,
                end = endTransform,
            });
        }
        originalSignalChannels = signalChannels;
    }
    void Release()
    {
        if (signalChannelsGBuffer != null)
        {
            signalChannelsGBuffer.Release();
            signalChannelsGBuffer = null;
        }
    }
    void Update()
    {
        if (signalChannelCount != signalChannels.Count)
        {
            Release();

            signalChannelVfxBuffer.Clear();
            for (int i = 0; i < signalChannels.Count; i++)
            {
                signalChannelVfxBuffer.Add(new SignalChannelVfx
                {
                    position = signalChannels[i].start.position,
                    endPosition = signalChannels[i].end.position,
                });
            }
            signalChannelsGBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, signalChannelVfxBuffer.Count, Marshal.SizeOf(typeof(SignalChannelVfx)));
            signalChannelCount = signalChannelVfxBuffer.Count;
        }

        for (int i = 0; i < signalChannelVfxBuffer.Count; i++)
        {
            var signalChannelVfxBufferElement = signalChannelVfxBuffer[i];
            signalChannelVfxBufferElement.position = signalChannels[i].start.position;
            signalChannelVfxBufferElement.endPosition = signalChannels[i].end.position;
            signalChannelVfxBuffer[i] = signalChannelVfxBufferElement;
        }
        signalChannelsGBuffer.SetData(signalChannelVfxBuffer);
        vfx.SetGraphicsBuffer(signalChannelsBufferName, signalChannelsGBuffer);

        vfx.SetVector3(screenForwardDirId, -transform.forward);
    }

}
