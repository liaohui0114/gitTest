using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class FPSComponent : MonoBehaviour
{
    public float frequency = 0.5f;

    [DebuggerHidden]
    private IEnumerator FPS() => 
        new <FPS>c__Iterator3 { <>f__this = this };

    private void Start()
    {
        base.StartCoroutine(this.FPS());
    }

    public int FramesPerSec { get; protected set; }

    [CompilerGenerated]
    private sealed class <FPS>c__Iterator3 : IDisposable, IEnumerator, IEnumerator<object>
    {
        internal object $current;
        internal int $PC;
        internal FPSComponent <>f__this;
        internal int <frameCount>__3;
        internal int <lastFrameCount>__0;
        internal float <lastTime>__1;
        internal float <timeSpan>__2;

        [DebuggerHidden]
        public void Dispose()
        {
            this.$PC = -1;
        }

        public bool MoveNext()
        {
            uint num = (uint) this.$PC;
            this.$PC = -1;
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    this.<timeSpan>__2 = Time.realtimeSinceStartup - this.<lastTime>__1;
                    this.<frameCount>__3 = Time.frameCount - this.<lastFrameCount>__0;
                    this.<>f__this.FramesPerSec = Mathf.RoundToInt(((float) this.<frameCount>__3) / this.<timeSpan>__2);
                    this.<>f__this.gameObject.guiText.text = this.<>f__this.FramesPerSec.ToString() + " fps";
                    break;

                default:
                    goto Label_00DE;
            }
            this.<lastFrameCount>__0 = Time.frameCount;
            this.<lastTime>__1 = Time.realtimeSinceStartup;
            this.$current = new WaitForSeconds(this.<>f__this.frequency);
            this.$PC = 1;
            return true;
            this.$PC = -1;
        Label_00DE:
            return false;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current =>
            this.$current;

        object IEnumerator.Current =>
            this.$current;
    }
}

