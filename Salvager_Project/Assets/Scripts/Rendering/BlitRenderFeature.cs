using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitRenderFeature : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier source;
        public RenderTargetHandle tempHandle;

        private Material material;

        public CustomRenderPass(Material material)
        {
            this.material = material;
            tempHandle.Init("_TemporaryColorTexture");
        }


        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            //get command buffer required for blit
            CommandBuffer cmd = CommandBufferPool.Get();


            //set up temporary render texture
            cmd.GetTemporaryRT(tempHandle.id, renderingData.cameraData.cameraTargetDescriptor);

            //Blit
            Blit(cmd, source, tempHandle.Identifier(), material);
            Blit(cmd, tempHandle.Identifier(), source);

            //execute command buffer
            context.ExecuteCommandBuffer(cmd);

            //release the command buffer
            CommandBufferPool.Release(cmd);
        }


        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Material material = null;
        public RenderPassEvent renderPassEvent;
    }
    //create settings that are visible in editor
    public Settings settings = new Settings();
    
    CustomRenderPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(settings.material);

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = settings.renderPassEvent;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


